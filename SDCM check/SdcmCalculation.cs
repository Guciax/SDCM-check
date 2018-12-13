using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDCM_check
{
    class SdcmCalculation
    {
        private static double CalculateSDCM(PcbTesterMeasurements testResults, ModelSpecification modelSPecification)
        {
            double result = 0;
            if (modelSPecification.A > 0)
            {
                double partA = Math.Pow((modelSPecification.Cx - testResults.Cx) * modelSPecification.A - (modelSPecification.Cy - testResults.Cy) * modelSPecification.B, 2) / Math.Pow(modelSPecification.C, 2);
                //double partA = Math.Pow((testResults.Cx-modelSPecification.Cx ) * modelSPecification.A - (testResults.Cy-modelSPecification.Cy) * modelSPecification.B, 2) / Math.Pow(modelSPecification.C, 2);
                double partB = Math.Pow((modelSPecification.Cx - testResults.Cx) * modelSPecification.B + (modelSPecification.Cy - testResults.Cy) * modelSPecification.A, 2) / Math.Pow(modelSPecification.D, 2);
                //double partB = Math.Pow((testResults.Cx-modelSPecification.Cx) * modelSPecification.B + (testResults.Cy-modelSPecification.Cy) * modelSPecification.A, 2) / Math.Pow(modelSPecification.D, 2);
                double AllensFactor = 0.04;
                result = Math.Sqrt(partA + partB);
                //if (result > 0.04)
                //{
                //    result = result - AllensFactor;
                //}
            }
            return result;
        }

        public static DataTable makeSdcmTable(Dictionary<string,PcbTesterMeasurements> testResults, Dictionary<string,ModelSpecification> modelSPecification, ref string currentModel)
        {
            DataTable result = new DataTable();
            HashSet<string> modelCheck = new HashSet<string>();
            result.Columns.Add("serial_No");
            Dictionary<string, int[]> indexOfNg = new Dictionary<string, int[]>();

            result.Columns.Add("SDCM_calc", typeof (double));
            result.Columns.Add("SDCM_tester", typeof (double));
            result.Columns.Add("Vf", typeof(double));
            result.Columns.Add("lm", typeof(double));
            result.Columns.Add("lm_w", typeof(double));
            result.Columns.Add("CRI", typeof(double));
            result.Columns.Add("CCT", typeof(double));
            result.Columns.Add("WYNIK");

            foreach (var testedPcb in testResults)
            {
                string model = testedPcb.Value.Model;
                modelCheck.Add(model);
                double sdcmCalculated = 0;
                double sdcmTester = 0;
                ModelSpecification modelSpec = null;
                int[] ngIndex = new int[] { 0, 0, 0, 0, 0, 0 };
                double maxSdcm = modelSPecification[model].MaxSdcm;
                if (maxSdcm == 0) maxSdcm = 999;

                if (modelSPecification.TryGetValue(model, out modelSpec))
                {
                    bool allOK = true;
                    sdcmCalculated = CalculateSDCM(testedPcb.Value, modelSpec);
                    sdcmTester = testedPcb.Value.Sdcm_Tester;

                    if (sdcmCalculated> maxSdcm || sdcmTester > maxSdcm)
                    {
                        allOK = false;
                    }
                    if (testedPcb.Value.Vf < modelSPecification[model].Vf_Min || testedPcb.Value.Vf > modelSPecification[model].Vf_Max)
                    {
                        allOK = false;
                        
                    }
                    if (testedPcb.Value.Lm < modelSPecification[model].Lm_Min || testedPcb.Value.Lm > modelSPecification[model].Lm_Max)
                    {
                        allOK = false;
                    }
                    if (testedPcb.Value.Cct < modelSPecification[model].CctMin || testedPcb.Value.Cct > modelSPecification[model].CctMax)
                    {
                        allOK = false;
                    }
                    if (testedPcb.Value.LmW < modelSPecification[model].LmW_Min)
                    {
                        allOK = false;
                    }
                    if (testedPcb.Value.Cri < modelSPecification[model].CRI_Min)
                    {
                        allOK = false;
                    }

                    string testResult = "OK";
                    if (!allOK)
                    {
                        testResult = "NG";
                    }

                    //result.Rows.Add(testedPcb.Key, model, modelSPecification[model].Cx + "x" + modelSPecification[model].Cy + "  CCT="+ modelSPecification[model].Cct+"K" , testedPcb.Value.Cx, testedPcb.Value.Cy, modelSPecification[model].MaxSdcm, sdcm);
                    result.Rows.Add(testedPcb.Key, sdcmCalculated, sdcmTester, testedPcb.Value.Vf, testedPcb.Value.Lm, testedPcb.Value.LmW, testedPcb.Value.Cri, testedPcb.Value.Cct, testResult);
                }
                else
                {
                    if (File.Exists(@"Y:\Manufacturing_Center\Integral Quality Management\Dane CofC.xlsx"))
                    {
                        MessageBox.Show("Brak modelu: " + model + @" w pliku Excel: Y:\Manufacturing_Center\Integral Quality Management\Dane CofC.xlsx");
                    }
                    else
                    {
                        MessageBox.Show(@"Brak dostępu do pliku Y:\Manufacturing_Center\Integral Quality Management\Dane CofC.xlsx" + Environment.NewLine + "Sprawdź czy podłączone są dyski sieciowe");
                    }
                    break;
                }
                
            }
            if(modelCheck.Count>1)
            {
                string msg = "Uwaga wykryto pomeszane modele!" + Environment.NewLine;
                foreach (var mdl in modelCheck)
                {
                    msg += mdl + Environment.NewLine;
                    currentModel = "Kilka modeli!!";
                }
                MessageBox.Show(msg);
            }
            else if(modelCheck.Count>0)
            {
                currentModel = modelCheck.ToList()[0];
            }
                        
            //result.DefaultView.Sort = "Wynik_SDCM DESC";
            return result;
        }
    }
}
