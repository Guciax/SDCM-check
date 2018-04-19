﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

                    string cxString = row["x"].ToString().Replace(".", ",");
                    string cyString = row["y"].ToString().Replace(".", ",");
                    if (cxString == "" || cyString == "") continue;
                    if (result.ContainsKey(serial)) continue;
                    if (model == "") continue;
                    double cx = Convert.ToDouble(cxString, new CultureInfo("pl-PL"));
                    double cy = double.Parse(row["y"].ToString().Replace(".", ","));
                    double sdcm = double.Parse(row["sdcm"].ToString());
                    double cct = double.Parse(row["cct"].ToString());
                    DateTime inspTime = DateTime.Parse(row["inspection_time"].ToString());

                    PcbTesterMeasurements newPcb = new PcbTesterMeasurements(cx, cy, sdcm, cct, inspTime, model);
                    result.Add(serial, newPcb);
                }
                
            }
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            modelSPecification = excelOperations.loadExcel();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (threadDone)
            {
                dataGridView1.DataSource = sourceTable;
                string tag = textBoxPcb.Text + textBoxBox.Text + textBoxLot.Text;
                dataGridView1.Tag = tag;
                autoSizeColumns();
                textBoxPcb.Text = "";
                textBoxBox.Text = "";
                textBoxLot.Text = "";
                pictureBox1.Visible = false;
                timer1.Enabled = false;
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
                    Dictionary<string, PcbTesterMeasurements> testedPcbs = dataTableToDict(SqlOperations.GetMeasurementsForLot(textBoxLot.Text));
                    sourceTable = SdcmCalculation.makeSdcmTable(testedPcbs, modelSPecification);
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
                    Dictionary<string, PcbTesterMeasurements> testedPcbs = dataTableToDict(SqlOperations.GetMeasurementsForPcb(textBoxPcb.Text));
                    sourceTable = SdcmCalculation.makeSdcmTable(testedPcbs, modelSPecification);
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
                    Dictionary<string, PcbTesterMeasurements> testedPcbs = dataTableToDict(SqlOperations.GetMeasurementsForBox(textBoxBox.Text));
                    sourceTable = SdcmCalculation.makeSdcmTable(testedPcbs, modelSPecification);
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

                    double sdcm = Convert.ToDouble(row.Cells["Wynik_SDCM"].Value.ToString());
                    double sdcmMax = Convert.ToDouble(row.Cells["SDCM_Max"].Value.ToString());

                    if (sdcm <= sdcmMax) break;
                    if (sdcm > sdcmMax)
                    {
                        ngCount++;
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Style.BackColor = Color.Red;
                            cell.Style.ForeColor = Color.White;
                        }
                    }
                }
                labelNgCount.Text = dataGridView1.Tag + " " +dataGridView1.Rows.Count+" szt."+Environment.NewLine+ "NG: " + ngCount + " szt.";
            }
        }

        private void textBox_enter(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            txtBox.SelectAll();
        }
    }
}
