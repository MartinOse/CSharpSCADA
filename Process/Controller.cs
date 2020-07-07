using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process
{
    class Controller
    {
        public double setpoint { get; set; }
        static private double Kp = 0.85;
        static private double Ti = 20;
        static private double t_step = 1;
        private double z;

        public double controlSignal(double Temperature)
        {
            if (setpoint == 0)
            {
                setpoint = 30;
            }

            double setpointError = setpoint - Temperature;
            // proportional component
            double u_p = Kp * setpointError;
            // Integral component
            double u_i = ((Kp / Ti) * t_step * setpointError + ((Kp / Ti) * z));

            z = z + (t_step * setpointError);

            double u_signal = u_p + u_i;

            if (u_signal > 5)
            {
                u_signal = 5;
            }

            if (u_signal < 0)
            {
                u_signal = 0;
            }


            return u_signal;
        }
    }
}
