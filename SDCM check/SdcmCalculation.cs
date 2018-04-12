using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            result.Columns.Add("spec");
            result.Columns.Add("Cx");
            result.Columns.Add("Cy");

            result.Columns.Add("SDCM");


            foreach (var testedPcb in testResults)
            {
                string model = testedPcb.Value.Model;
                string sdcm = CalculateSDCM(testedPcb.Value, modelSPecification[model]).ToString();
                result.Rows.Add(testedPcb.Key,model+" "+modelSPecification[model].Cx+"x"+modelSPecification[model].Cy, testedPcb.Value.Cx,testedPcb.Value.Cy, sdcm);
            }
            
            result.DefaultView.Sort = "SDCM DESC";
            return result;
        }
    }
}
