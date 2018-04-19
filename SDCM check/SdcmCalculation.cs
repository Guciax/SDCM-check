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

            double partA = Math.Pow((modelSPecification.Cx - testResults.Cx) * modelSPecification.A - (modelSPecification.Cy - testResults.Cy) * modelSPecification.B, 2) / Math.Pow(modelSPecification.C, 2);
            double partB = Math.Pow((modelSPecification.Cx - testResults.Cx) * modelSPecification.B + (modelSPecification.Cy - testResults.Cy) * modelSPecification.A, 2) / Math.Pow(modelSPecification.D, 2);
            result = Math.Sqrt(partA + partB);
            return result;
        }

        public static DataTable makeSdcmTable(Dictionary<string,PcbTesterMeasurements> testResults, Dictionary<string,ModelSpecification> modelSPecification)
        {
            DataTable result = new DataTable();
            result.Columns.Add("serial_No");
            result.Columns.Add("Model");
            result.Columns.Add("spec");
            result.Columns.Add("Cx");
            result.Columns.Add("Cy");
            result.Columns.Add("SDCM_Max");

            result.Columns.Add("Wynik_SDCM");


            foreach (var testedPcb in testResults)
            {
                string model = testedPcb.Value.Model;
                string sdcm = "";
                ModelSpecification modelSpec = null;

                if (modelSPecification.TryGetValue(model, out modelSpec))
                {
                    sdcm = CalculateSDCM(testedPcb.Value, modelSpec).ToString();
                    result.Rows.Add(testedPcb.Key, model, modelSPecification[model].Cx + "x" + modelSPecification[model].Cy + "  CCT="+ modelSPecification[model].Cct+"K" , testedPcb.Value.Cx, testedPcb.Value.Cy, modelSPecification[model].MaxSdcm, sdcm);
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
            
            result.DefaultView.Sort = "Wynik_SDCM DESC";
            return result;
        }
    }
}
