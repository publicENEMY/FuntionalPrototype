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

namespace ProgressBarAsync
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

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			var newWindow = new Progress();
			newWindow.Show();

			// The Progress<T> constructor captures our UI context,
			//  so the lambda will be run on the UI thread.
			var progress = new Progress<int>(percent =>
			{
				textBox1.Text = percent + "%";
				newWindow.connectionProgress.Value = percent;
			});

			// DoProcessing does most of its work on the thread pool.
			await DoProcessingOnThreadPoolAsync(progress);
		}

		private async Task DoProcessingOnThreadPoolAsync(IProgress<int> progress)
		{
			int timer = 0;

			do
			{
				await Task.Delay(10);
				if (progress != null)
					progress.Report(timer);
				timer += 1;
			} while (timer<=100);
		}

		/*
		public async Task DownloadFileAsync(string fileName, IProgress<int> progress)
{
  using (var fileStream = ...) // Open local file for writing
  using (var ftpStream = ...) // Open FTP stream
  {
    while (true)
    {
      var bytesRead = await ftpStream.ReadAsync(...);
      if (bytesRead == 0)
        return;
      await fileStream.WriteAsync(...);
      if (progress != null)
        progress.Report(bytesRead);
    }
  }
}*/

	}
}
