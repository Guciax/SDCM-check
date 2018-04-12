using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCM_check
{
    class PcbTesterMeasurements
    {
        public PcbTesterMeasurements(double Cx, double Cy, double Sdcm, double Cct, DateTime inspectionTime, string model)
        {
            this.Cx = Cx;
            this.Cy = Cy;
            this.Sdcm = Sdcm;
            this.Cct = Cct;
            InspectionTime = inspectionTime;
            Model = model;
        }

        public double Cx { get; }
        public double Cy { get; }
        public double Sdcm { get; }
        public double Cct { get; }
        public DateTime InspectionTime { get; }
        public string Model { get; }
    }
}
