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

		private void convertFromInt_Click(object sender, RoutedEventArgs e)
		{

		}

		private static string ConvertHexByteToHexString(string hexByte)
		{
			string s = null;
			return s;
		}

		private static string ConvertHexStringToHexByte(string hexString)
		{
			string s = null;
			return s;
		}

		private static int ConvertHexStringToInt(string hex)
		{
			return Convert.ToInt32(hex, 16);
		}
	
		private static string ConvertIntToHexString(int intValue)
		{
			string s = null;
			return s;
		}

		private static byte[] ConvertIntToByteArray(int intValue)
		{
			byte[] byteArray = new byte[3];
			return byteArray;
		}
	}
}
