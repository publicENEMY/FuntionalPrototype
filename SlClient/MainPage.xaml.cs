using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SlClient
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private static void Logger(string p)
		{
		}

		private void send_Click(object sender, RoutedEventArgs e)
		{
			IPAddress ipAddress;
			int port;

			//TODO Check if ip address is valid
			ipAddress = IPAddress.Parse(serverIp.Text);
			//TODO port range is 0-65000
			port = int.Parse(serverPort.Text);
			//TODO get message to send

			StartClient(ipAddress, port, Encoding.UTF8.GetBytes(DateTime.Today.ToString()));
		}

		private static async Task StartClient(IPAddress serverIpAddress, int port, byte[] message)
		{
			using (var client = new TcpClient())
			{
				try
				{
					await client.ConnectAsync(serverIpAddress, port);
					Logger("Connected to server");
					using (var networkStream = client.GetStream())
					{
						if (networkStream.CanWrite)
						{
							//var writeBuffer = Encoding.ASCII.GetBytes(message);
							await networkStream.WriteAsync(message, 0, message.Length);
							//Logger.Info("Sent : " + Encoding.ASCII.GetString(message));
							Logger("Sent : " + BitConverter.ToString(message));
						}
						else
						{
							Logger("Cannot write from this NetworkStream.");
						}

						var accumBuffer = new byte[] { };
						// Check to see if this NetworkStream is readable. 
						if (networkStream.CanRead)
						{
							var readBuffer = new byte[1500];

							// Incoming message may be larger than the buffer size. 
							do
							{
								var numberOfBytesRead = await networkStream.ReadAsync(readBuffer, 0, readBuffer.Length);
								accumBuffer = Combine(accumBuffer, readBuffer, numberOfBytesRead);
							}
							while (networkStream.DataAvailable);

							//Logger.Info("Received : " + Encoding.ASCII.GetString(accumBuffer));
							Logger("Received : " + BitConverter.ToString(accumBuffer));
						}
						else
						{
							Logger("Cannot read from this NetworkStream.");
						}
					}
				}
				catch (Exception e)
				{
					Logger(e);
				}
			}
		}
	}
}
