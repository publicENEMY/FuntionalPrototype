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

		/*
		 * var db = new SQLiteConnection("sqllite.db")
		 * db.CreateTable<SyncRecord> ();
		 * db.Insert (new SyncRecord () { SyncDate = DateTime.UtcNow });
		 * var query = db.Table<SyncRecord> ().Where(your lambda to filter);
		 */
		private string dbPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "MyDatabase.sqlite");

		private void createDb_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (File.Exists(dbPath))
				Logger.Info("File " + dbPath + " already exists");
			try
			{
				using (var m_dbConnection = new SQLite.SQLiteConnection(dbPath))
				{
				}
				Logger.Info("Database created/connected");
			}
			catch (System.Exception ex)
			{
				Logger.Error(ex.Message);
			}
			if (File.Exists(dbPath))
				Logger.Info("Verified " + dbPath + " exists");
			else
				Logger.Error("Verified " + dbPath + " doesnt exists");
		}

		public class highscore
		{
			[MaxLength(20)]
			public string name { get; set; }
			public int score { get; set; }
		}

		private void createTb_Click(object sender, RoutedEventArgs e)
		{
			// list available table

			// create table
			using (var m_dbConnection = new SQLite.SQLiteConnection(dbPath))
			{
				try
				{
					m_dbConnection.CreateTable<highscore>();
					Logger.Info("Table created");
				}
				catch (System.Exception ex)
				{
					Logger.Error(ex.Message);
				}
			}

			// check table exist

			/*
			 * var db = new SQLiteConnection("sqllite.db")
			 * db.CreateTable<SyncRecord> ();
			 * db.Insert (new SyncRecord () { SyncDate = DateTime.UtcNow });
			 * var query = db.Table<SyncRecord> ().Where(your lambda to filter);
			 */
		}

		private void fillTb_Click(object sender, RoutedEventArgs e)
		{
			using (var m_dbConnection = new SQLite.SQLiteConnection(dbPath))
			{
				try
				{
					m_dbConnection.Insert(new highscore()
					{
						name = "Me",
						score = 9001
					});
					m_dbConnection.Insert(new highscore()
					{
						name = "Me",
						score = 3000
					});
					m_dbConnection.Insert(new highscore()
					{
						name = "Myself",
						score = 6000
					});
					m_dbConnection.Insert(new highscore()
					{
						name = "And I",
						score = 9001
					});

					Logger.Info("Filled table");
				}
				catch (System.Exception ex)
				{
					Logger.Error(ex.Message);
				}
			}
		}

		private void queryDb_Click(object sender, RoutedEventArgs e)
		{
			//string sql = "select * from highscores order by score desc";
			//SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			//SQLiteDataReader reader = command.ExecuteReader();
			//while (reader.Read())
			//	Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);

			using (var m_dbConnection = new SQLite.SQLiteConnection(dbPath))
			{
				//var query = m_dbConnection.Table<highscore> ().Where(your lambda to filter);

				//var query2 =m_dbConnection.Query<highscore> ("",);
			}
		}

	}
}
