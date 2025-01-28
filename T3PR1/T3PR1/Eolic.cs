using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3PR1
{
    internal class Eolic:SistemaEnergia
    {
        public int WindSpe { get; set; }
        public Eolic(int WindSpe)
        {
            this.WindSpe = WindSpe;
        }
        public override double CalculateEnergy()
        {
            return Math.Pow(WindSpe, 3) * 0.2f;
        }
    }
}
