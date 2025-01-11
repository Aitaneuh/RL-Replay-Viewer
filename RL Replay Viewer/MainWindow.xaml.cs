using Microsoft.Win32;
using System.Text;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace RL_Replay_Viewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_select_file_Click(object sender, RoutedEventArgs e)
        {
            string userDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string baseDirectory = Path.Combine(userDocumentsPath, "My Games", "Rocket League", "TAGame");

            string demosFolder = Path.Combine(baseDirectory, "Demos");
            string demosEpicFolder = Path.Combine(baseDirectory, "DemosEpic");

            string selectedFolder = GetMostRecentFolder(demosFolder, demosEpicFolder);

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a Replay folder",
                Filter = "Replay Files (*.replay)|*.replay",
                InitialDirectory = selectedFolder
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                ReplayStats replayStatsPage = new ReplayStats(selectedFilePath);

                var frame = new Frame();
                frame.Content = replayStatsPage;
                this.Content = frame;
            }
        }

        private string GetMostRecentFolder(string folder1, string folder2)
        {
            DateTime GetLastWriteTime(string folder)
            {
                if (Directory.Exists(folder))
                {
                    return Directory.EnumerateFiles(folder, "*.replay", SearchOption.TopDirectoryOnly)
                                    .Select(f => File.GetLastWriteTime(f))
                                    .DefaultIfEmpty(DateTime.MinValue)
                                    .Max();
                }
                return DateTime.MinValue;
            }

            DateTime lastWriteTime1 = GetLastWriteTime(folder1);
            DateTime lastWriteTime2 = GetLastWriteTime(folder2);

            if (lastWriteTime1 > lastWriteTime2)
                return folder1;
            if (lastWriteTime2 > lastWriteTime1)
                return folder2;

            return null;
        }
    }
}