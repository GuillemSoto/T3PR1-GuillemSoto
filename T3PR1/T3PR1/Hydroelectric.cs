using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3PR1
{
    internal class Hydroelectric:SistemaEnergia
    {
        public int VolumeOfFlow { get; set; }
        public Hydroelectric(int volumeOfFlow)
        {
            VolumeOfFlow = volumeOfFlow;
        }
        public override double CalculateEnergy()
        {
            return VolumeOfFlow * 9.8 * 0.8;
        }
    }
}
