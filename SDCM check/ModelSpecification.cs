using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCM_check
{
    public class ModelSpecification
    {
        public ModelSpecification(double maxSdcm, double Cx, double Cy, double A, double B, double C, double D, double cctMin, double cctMax, double Vf_min, double Vf_max, double lm_min, double lm_max, double lmW_min, double CRI_min)
        {
            MaxSdcm = maxSdcm;
            this.Cx = Cx;
            this.Cy = Cy;
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
            CctMin = cctMin;
            CctMax = cctMax;
            Vf_Min = Vf_min;
            Vf_Max = Vf_max;
            Lm_Min = lm_min;
            Lm_Max = lm_max;
            LmW_Min = lmW_min;
            CRI_Min = CRI_min;
        }

        public double MaxSdcm { get; }
        public double Cx { get; }
        public double Cy { get; }
        public double A { get; }
        public double B { get; }
        public double C { get; }
        public double D { get; }
        public double CctMin { get; set; }
        public double CctMax { get; set; }
        public double Vf_Min { get; set; }
        public double Vf_Max { get; set; }
        public double Lm_Min { get; set; }
        public double Lm_Max { get; set; }
        public double LmW_Min { get; set; }
        public double CRI_Min { get; set; }
    }
}
