using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDCM_check
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Dictionary<string, ModelSpecification> modelSPecification = new Dictionary<string, ModelSpecification>();
        bool useArvhivedData = false;

        public static bool IsInt(string s)
        {
            int x = 0;
            return int.TryParse(s, out x);
        }

        private Dictionary<string, PcbTesterMeasurements> dataTableToDict(DataTable sqlTable)
        {
            Dictionary<string, PcbTesterMeasurements> result = new Dictionary<string, PcbTesterMeasurements>();
            if (sqlTable.Rows.Count > 0)
            {
                Dictionary<string, string> lotToModel = new Dictionary<string, string>();

                foreach (DataRow row in sqlTable.Rows)
                {
                    string serial = row["serial_no"].ToString();
                    if (result.ContainsKey(serial)) continue;

                    string lot = row["wip_entity_name"].ToString();
                    string model = "";

                    if (!IsInt(lot)) continue;

                    if (lotToModel.ContainsKey(lot))
                    {
                        model = lotToModel[lot];
                    }
                    else
                    {
                        model = SqlOperations.GetModelIdFromLot(lot);
                        lotToModel.Add(lot, model);
                    }

                    if (model == null) continue;
                    string cxString = row["x"].ToString().Replace(".", ",");
                    string cyString = row["y"].ToString().Replace(".", ",");
                    if (cxString == "" || cyString == "") continue;
                    
                    if (model == "") continue;
                    double cx = Convert.ToDouble(cxString, new CultureInfo("pl-PL"));
                    double cy = double.Parse(row["y"].ToString().Replace(".", ","));
                    double sdcm = double.Parse(row["sdcm"].ToString());
                    double cct = double.Parse(row["cct"].ToString());
                    double vf = double.Parse(row["v"].ToString());
                    double lm = double.Parse(row["lm"].ToString());
                    double lmW = double.Parse(row["lm_w"].ToString());
                    double cri = double.Parse(row["cri"].ToString());

                    DateTime inspTime = DateTime.Parse(row["inspection_time"].ToString());

                    PcbTesterMeasurements newPcb = new PcbTesterMeasurements(cx, cy, sdcm, cct, inspTime, model, vf, lm, lmW, cri, cct);
                    result.Add(serial, newPcb);
                }
                
            }
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            modelSPecification = excelOperations.loadExcel();
            modelSPecification = SqlOperations.AddModelOpticalSpecFromDb(modelSPecification);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = "SDCM check ver." + version;
        }

        string currentModel = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (threadDone)
            {
                if (currentModel != "")
                {
                    labelSpec.Text = "";
                    if (modelSPecification[currentModel].Vf_Min==0)
                    {
                        labelSpec.Text = "BRAK DANYCH W BAZIE!!!"+Environment.NewLine+Environment.NewLine;
                        panel2.BackColor = Color.Red;
                        panel2.ForeColor = Color.White;
                    }
                    else
                    {
                        panel2.BackColor = Color.LightGray;
                        panel2.ForeColor = Color.Black;
                    }
                    labelSpec.Text += "Specyfikacja:" + Environment.NewLine
                    + currentModel + Environment.NewLine
                    + "SDCM max: " + modelSPecification[currentModel].MaxSdcm + Environment.NewLine
                    + "Vf: " + modelSPecification[currentModel].Vf_Min + " - " + modelSPecification[currentModel].Vf_Max + Environment.NewLine
                    + "Lm: " + modelSPecification[currentModel].Lm_Min + " - " + modelSPecification[currentModel].Lm_Max + Environment.NewLine
                    + "Lm/W min: " + modelSPecification[currentModel].LmW_Min + Environment.NewLine
                    + "CRI min: " + modelSPecification[currentModel].CRI_Min + Environment.NewLine
                    + "CCT: " + modelSPecification[currentModel].CctMin + " - " + modelSPecification[currentModel].CctMax;


                }
                dataGridView1.DataSource = sourceTable;


                
                string tag = textBoxPcb.Text + textBoxBox.Text + textBoxLot.Text;
                dataGridView1.Tag = tag;
                
                
                textBoxPcb.Text = "";
                textBoxBox.Text = "";
                textBoxLot.Text = "";
                pictureBox1.Visible = false;
                timer1.Enabled = false;
                autoSizeColumns();
                CheckTestedValuesVsSpec();
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Sort(this.dataGridView1.Columns["WYNIK"], ListSortDirection.Ascending);
                }
            }
        }

        bool threadDone = false;
        DataTable sourceTable = new DataTable();
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                pictureBox1.Visible = true;
                timer1.Enabled = true;
                threadDone = false;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    Dictionary<string, PcbTesterMeasurements> testedPcbs = new Dictionary<string, PcbTesterMeasurements>();
                    if (useArvhivedData)
                    {
                        testedPcbs = dataTableToDict(SqlOperations.GetArchivedMeasurementsForLot(textBoxLot.Text));
                    }
                    else
                    {
                        DataTable testTable = SqlOperations.GetMeasurementsForLot(textBoxLot.Text);
                        if (testTable.Rows.Count > 0)
                        {
                            testedPcbs = dataTableToDict(testTable);
                        }
                    }
                    
                    sourceTable = SdcmCalculation.makeSdcmTable(testedPcbs, modelSPecification, ref currentModel);
                    threadDone = true;
                }).Start();
                
            }
        }

        private void textBoxPcb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                pictureBox1.Visible = true;
                timer1.Enabled = true;
                threadDone = false;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Dictionary<string, PcbTesterMeasurements> testedPcbs = new Dictionary<string, PcbTesterMeasurements>();
                    if (useArvhivedData)
                    {
                        testedPcbs = dataTableToDict(SqlOperations.GetArchivedMeasurementsForPcb(textBoxPcb.Text));
                    }
                    else
                    {
                        DataTable testTable = SqlOperations.GetMeasurementsForPcb(textBoxPcb.Text);
                        if (testTable.Rows.Count > 0)
                        {
                            testedPcbs = dataTableToDict(testTable);
                        }
                    }
                    
                    sourceTable = SdcmCalculation.makeSdcmTable(testedPcbs, modelSPecification, ref currentModel);
                    threadDone = true;
                }).Start();
            }
        }

        private void textBoxBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                pictureBox1.Visible = true;
                timer1.Enabled = true;
                threadDone = false;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Dictionary<string, PcbTesterMeasurements> testedPcbs = new Dictionary<string, PcbTesterMeasurements>();
                    if (useArvhivedData)
                    {
                        testedPcbs = dataTableToDict(SqlOperations.GetArchivedMeasurementsForBox(textBoxBox.Text));
                    }
                    else
                    {
                        DataTable testTable = SqlOperations.GetMeasurementsForBox(textBoxBox.Text);
                        if (testTable.Rows.Count > 0)
                        {
                            testedPcbs = dataTableToDict(testTable);
                        }
                    }

                    if (testedPcbs.Count > 0)
                    {
                        sourceTable = SdcmCalculation.makeSdcmTable(testedPcbs, modelSPecification, ref currentModel);
                    }
                    threadDone = true;
                }).Start();
            }
        }

        private void autoSizeColumns()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    
                }
                int ngCount = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["WYNIK"].Value.ToString()!="OK")
                    {
                        ngCount++;
                    }
                }
                labelNgCount.Text = "Ilość wszystkich: "+dataGridView1.Rows.Count + " szt." + Environment.NewLine + "NG: " + ngCount + " szt.";
            }
        }

        private void textBox_enter(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            txtBox.SelectAll();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                useArvhivedData = true;
            }
            else
            {
                useArvhivedData = false;
            }
        }

        private void CheckTestedValuesVsSpec()
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (currentModel != "")
            {
                dataGridView1.SuspendLayout();
                double sdcmMax = modelSPecification[currentModel].MaxSdcm;
                double vfMax = modelSPecification[currentModel].Vf_Max;
                double vfMin = modelSPecification[currentModel].Vf_Min;
                double lmMin = modelSPecification[currentModel].Lm_Min;
                double lmMax = modelSPecification[currentModel].Lm_Max;
                double lmWMin = modelSPecification[currentModel].LmW_Min;
                double cctMin = modelSPecification[currentModel].CctMin;
                double cctMax = modelSPecification[currentModel].CctMax;
                double criMin = modelSPecification[currentModel].CRI_Min;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["WYNIK"].Value.ToString() == "NG")
                    {
                        row.Cells["WYNIK"].Style.BackColor = Color.Red;
                        row.Cells["WYNIK"].Style.ForeColor = Color.White;
                    }

                    double sdcm = (double)row.Cells["SDCM"].Value;
                    double vf = (double)row.Cells["Vf"].Value;
                    double lm = (double)row.Cells["lm"].Value;
                    double lmW = (double)row.Cells["lm_w"].Value;
                    double cri = (double)row.Cells["CRI"].Value;
                    double cct = (double)row.Cells["CCT"].Value;

                    if (sdcm > sdcmMax)
                    {
                        MarkNgCell(row.Cells["SDCM"]);
                    }
                    if (vf > vfMax || vf < vfMin)
                    {
                        MarkNgCell(row.Cells["Vf"]);
                    }
                    if (lm > lmMax || lm < lmMin)
                    {
                        MarkNgCell(row.Cells["lm"]);
                    }
                    if (lmW < lmWMin)
                    {
                        MarkNgCell(row.Cells["lm_w"]);
                    }
                    if (cri < criMin)
                    {
                        MarkNgCell(row.Cells["CRI"]);
                    }
                    if (cct < cctMin || cct > cctMax)
                    {
                        MarkNgCell(row.Cells["CCT"]);
                    }
                }
                dataGridView1.ResumeLayout();
            }
            
        }
        private void MarkNgCell(DataGridViewCell cell)
        {
            cell.Style.BackColor = Color.Red;
            cell.Style.ForeColor = Color.White;
        }
    }
}
