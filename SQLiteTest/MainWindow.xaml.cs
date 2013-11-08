using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using NLog;
using SQLite;

namespace SQLiteTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ObservableCollection<string> _messages = new ObservableCollection<string>();
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private const string TargetName = "memoryex";

		public MainWindow()
		{
			InitializeComponent();
			Loaded += MainWindowLoaded;

			var target =
				LogManager.Configuration.AllTargets
				.Where(x => x.Name == TargetName)
				.Single() as MemoryTargetEx;

			if (target != null)
				target.Messages.Subscribe(msg => _messages.Add(msg));
		}

		void MainWindowLoaded(object sender, RoutedEventArgs e)
		{
			IncomingMessages = _messages;
			Logger.Info("Window is loaded");
			Logger.Info("These messages are logged in ..\\..\\Logs\\NLogSamples.log as well.");
		}

		public ObservableCollection<string> IncomingMessages
		{
			get { return _messages; }
			private set { _messages = value; }
		}

		private SQLiteConnection _db;

		private void createDb_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Logger.Info("Creating database");
			var dbPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Stocks.db");
			if (File.Exists(dbPath))
				Logger.Info("File " + dbPath + " already exists");
			try
			{
				_db = new SQLiteConnection(dbPath);
			}
			catch (System.Exception ex)
			{
				Logger.Error(ex.Message);
			}
			if (File.Exists(dbPath))
				Logger.Info("File " + dbPath + " exists");
			else
				Logger.Info("File " + dbPath + " doesnt exists");
		}

		private void connectDb_Click(object sender, RoutedEventArgs e)
		{
		}

		private void createTb_Click(object sender, RoutedEventArgs e)
		{
			// list available table
			// create table
			// check table exist
			// 
		}

	}
}
