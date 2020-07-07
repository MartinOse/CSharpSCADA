using NationalInstruments;
using NationalInstruments.NetworkVariable;
using NationalInstruments.NetworkVariable.WindowsForms;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;

namespace Process
{
	public partial class Form1 : Form
	{
		AirHeater airHeater = new AirHeater();
		Filter filter = new Filter();
		Controller controller = new Controller();

		private NetworkVariableBufferedWriter<double> _tempwriter;
		private NetworkVariableBufferedWriter<double> _unfilteredtempwriter;
		private NetworkVariableBufferedWriter<double> _sigwriter;
		private NetworkVariableBufferedSubscriber<double> _setpointreader;

		private const string TemperatureVariableLocation = @"\\localhost\AirHeater\Temperature";
		private const string UnfilteredTempVariableLocation = @"\\localhost\AirHeater\UnfilteredTemp";
		private const string SignalVariableLocation = @"\\localhost\AirHeater\Signal";
		private const string SetpointVariableLocation = @"\\localhost\AirHeater\Setpoint";

		int seconds = 0;

		double rawtemp = 30;
		double f_y = 30;
		double u = 0;

		public Form1()
		{
			InitializeComponent();

			timer1.Tick += new EventHandler(timer1_Tick);
			//Form1.Close() += new EventHandler(Form1_Close);

			timer1.Interval = 1000;
			timer1.Start();

			ConnectOPCServer();
		}

		private void ConnectOPCServer()
		{
			_tempwriter = new NetworkVariableBufferedWriter<double>(TemperatureVariableLocation);
			_unfilteredtempwriter = new NetworkVariableBufferedWriter<double>(UnfilteredTempVariableLocation);
			_sigwriter = new NetworkVariableBufferedWriter<double>(SignalVariableLocation);
			_setpointreader = new NetworkVariableBufferedSubscriber<double>(SetpointVariableLocation);
			
			_tempwriter.Connect();
			_unfilteredtempwriter.Connect();
			_sigwriter.Connect();
			_setpointreader.Connect();

			string txtStatus1 = _tempwriter.ConnectionStatus.ToString();
		}

		public void Form1_Load(object sender, EventArgs e)
		{
			chart1.Series["Temperature"].ChartType = SeriesChartType.Line;
			chart2.Series["Control Signal"].ChartType = SeriesChartType.Line;
			FillChart();
		}

		public void FillChart()
		{
			seconds += 1;

			rawtemp = Math.Round(airHeater.Temperature(u), 2, MidpointRounding.AwayFromZero);
			f_y = Math.Round(filter.filteredSignal(rawtemp), 2, MidpointRounding.AwayFromZero);
			u = Math.Round(controller.controlSignal(f_y), 2, MidpointRounding.AwayFromZero);

			chart1.Series["Temperature"].Points.AddXY(seconds, f_y);
			chart2.Series["Control Signal"].Points.AddXY(seconds, u);

			textBox3.Text = f_y.ToString();
			textBox4.Text = u.ToString();

			WriteOpc(rawtemp, f_y, u);
		}

		public void WriteOpc(double rawtemp, double filteredtemp, double signal)
		{
			try 
			{
				_tempwriter.WriteValue(filteredtemp);
				Thread.Sleep(0);
				_unfilteredtempwriter.WriteValue(rawtemp);
				Thread.Sleep(0);
				_sigwriter.WriteValue(signal);
				Thread.Sleep(0);
			}
			catch(TimeoutException e)
			{
				return;
			}
			
		}

		public void timer1_Tick(object sender, EventArgs e)
		{
			try
			{
				controller.setpoint = _setpointreader.ReadData().GetValue();
			}
			catch(TimeoutException)
			{
				return;
			}
			FillChart();
		}

		private void Form1_Close(object sender, FormClosingEventArgs e)
		{
			_tempwriter.Disconnect();
			_setpointreader.Disconnect();
		}
	}
}
