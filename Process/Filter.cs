using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process
{
    class Filter
    {
        // Time constant for filter Tf
        static private double Tf = 0.5;
        // Time-step
        static private double Ts = 0.1;

        private double prevOutput = 20;


        public double filteredSignal(double signal)
        {
            double a = Ts / (Tf + Ts);
            double Yk = (1 - a) * prevOutput + a * signal;

            prevOutput = Yk;

            return Yk;
        }
    }
}
