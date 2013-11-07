using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HexConverter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void convertFromHex_Click(object sender, RoutedEventArgs e)
		{
			byte[] byteArray = StringToByteArray(hex1.Text + hex2.Text + hex3.Text);
			if (byteArray == null)
			{
				int1.Text = int2.Text = int3.Text = "null";
			}
			else
			{
				int1.Text = byteArray[0].ToString();
				int2.Text = byteArray[1].ToString();
				int3.Text = byteArray[2].ToString();
			}
		}

		public static byte[] StringToByteArray(String hex)
		{
			int NumberChars = hex.Length;
			byte[] bytes = new byte[NumberChars / 2];
			try
			{
				for (int i = 0; i < NumberChars; i += 2)
					bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			}
			catch (Exception e)
			{
				bytes = null;
			}
			return bytes;
		}
	}
}
