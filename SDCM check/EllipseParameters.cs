using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCM_check
{
    class EllipseParameters
    {
        public EllipseParameters(double A, double B, double C, double D, double Cx, double Cy)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
            this.Cx = Cx;
            this.Cy = Cy;
        }

        public double A { get; }
        public double B { get; }
        public double C { get; }
        public double D { get; }
        public double Cx { get; }
        public double Cy { get; }
    }
}
