using NationalInstruments;
using NationalInstruments.NetworkVariable;
using NationalInstruments.NetworkVariable.WindowsForms;
using NationalInstruments.Restricted;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataEngine
{
    public partial class Form1 : Form
    {
        SQLhandler sqlHandler = new SQLhandler();

        private NetworkVariableBufferedSubscriber<double> _tempreader;
        private NetworkVariableBufferedSubscriber<double> _unfilteredtempreader;
        private NetworkVariableBufferedSubscriber<double> _sigreader;
        private NetworkVariableBufferedWriter<double> _setpointwriter;

        private const string TemperatureVariableLocation = @"\\localhost\AirHeater\Temperature";
        private const string UnfilteredTempVariableLocation = @"\\localhost\AirHeater\UnfilteredTemp";
        private const string SignalVariableLocation = @"\\localhost\AirHeater\Signal";
        private const string SetpointVariableLocation = @"\\localhost\AirHeater\Setpoint";

        //Console.WriteLine("Starting software");

        public Form1()
        {
            InitializeComponent();

            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000;
            timer1.Start();

            ConnectOPCServer();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            Datalogging();
            Console.WriteLine("Loaded");
        }

        private void ConnectOPCServer()
        {
            _tempreader = new NetworkVariableBufferedSubscriber<double>(TemperatureVariableLocation);
            _unfilteredtempreader = new NetworkVariableBufferedSubscriber<double>(UnfilteredTempVariableLocation);
            _sigreader = new NetworkVariableBufferedSubscriber<double>(SignalVariableLocation);
            _setpointwriter = new NetworkVariableBufferedWriter<double>(SetpointVariableLocation);

            _tempreader.Connect();
            _unfilteredtempreader.Connect();
            _sigreader.Connect();
            _setpointwriter.Connect();

        }

        public void Datalogging()
        {
            try
            {
                Console.WriteLine(_tempreader.ReadData().GetValue());
                double temp = Math.Round(_tempreader.ReadData().GetValue(), 2, MidpointRounding.AwayFromZero);
                double unfilteredtemp = Math.Round(_unfilteredtempreader.ReadData().GetValue(), 2, MidpointRounding.AwayFromZero);
                double signal = Math.Round(_sigreader.ReadData().GetValue(), 2, MidpointRounding.AwayFromZero);
                double setpoint = Math.Round(sqlHandler.GetSetpoint(), 2, MidpointRounding.AwayFromZero);                

                // Write process data to DB
                sqlHandler.LogData(temp, unfilteredtemp, signal, _tempreader.ConnectionStatus.ToString());

                // Read last setpoint entry from DB, write to OPC server
                _setpointwriter.WriteValue(setpoint);

                textBox1.Text = "Temp: " + temp.ToString();
                textBox2.Text = "Signal: " + signal.ToString();
                textBox3.Text = "Status: " + _tempreader.ConnectionStatus.ToString();
                textBox4.Text = "Setpoint: " + setpoint.ToString();

            }
            catch (TimeoutException)
            {
                return;
            }
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            Datalogging();
        }

        public void Form1_Close(object sender, FormClosedEventArgs e)
        {
            _tempreader.Disconnect();
            _setpointwriter.Disconnect();
        }
    }
}
