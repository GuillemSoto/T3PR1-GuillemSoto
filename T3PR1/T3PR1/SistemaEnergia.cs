using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3PR1
{
    abstract class SistemaEnergia
    {
        public SistemaEnergia() { }
        public virtual double CalculateEnergy()
        {
            return 0f;
        }
    }
}
