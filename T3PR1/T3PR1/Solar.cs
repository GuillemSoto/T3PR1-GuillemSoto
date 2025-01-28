using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3PR1
{
    internal class Solar : SistemaEnergia
    {
        public int SunHours { get; set; }
        public Solar(int sunHours)
        {
            this.SunHours = sunHours;
        }
        public override double CalculateEnergy()
        {
            return SunHours * 1.5f;
        }
    }
}
