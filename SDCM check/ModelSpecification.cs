using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCM_check
{
    class ModelSpecification
    {
        public ModelSpecification(double maxSdcm, double Cx, double Cy, double A, double B, double C, double D, string cct)
        {
            MaxSdcm = maxSdcm;
            this.Cx = Cx;
            this.Cy = Cy;
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
            Cct = cct;
        }

        public double MaxSdcm { get; }
        public double Cx { get; }
        public double Cy { get; }
        public double A { get; }
        public double B { get; }
        public double C { get; }
        public double D { get; }
        public string Cct { get; }
    }
}
