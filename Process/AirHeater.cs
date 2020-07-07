using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process
{
    class AirHeater
    {
        private double Kh = 3.5;
        private double theta_constant = 23;
        private double temp_env = 20;
        private double T_k = 30;
        Random r = new Random();

        public double Temperature(double uSignal)
        {
            // Euler forward integration (Euler step):
            double calc = ((temp_env - T_k) + Kh * uSignal) / (theta_constant);
            // Calculate next point + random minor disturbance
            double T_kp1 = T_k + calc + r.NextDouble() / 10;

            T_k = T_kp1;

            return T_kp1;
        }
    }
}
