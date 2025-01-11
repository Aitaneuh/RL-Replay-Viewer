using Newtonsoft.Json;
using RocketRP;
using RocketRP.Serializers;
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

namespace RL_Replay_Viewer
{
    public partial class ReplayStats : Page
    {
        public ReplayStats(string replayFilePath)
        {
            InitializeComponent();

            LoadData(replayFilePath);
        }

        private void LoadData(string replayFilePath)
        {
            bool parseNetstream = true;
            bool enforceCRC = true;

            Replay replay = Replay.Deserialize(replayFilePath, parseNetstream, enforceCRC);

            ReplayJsonSerializer serializer = new ReplayJsonSerializer();
            string jsonData = serializer.Serialize(replay, false);

            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonData);

            EngineVersionText.Text = rootObject.EngineVersion.ToString();
            LicenseeVersionText.Text = rootObject.LicenseeVersion.ToString();
            ReplayClassText.Text = rootObject.ReplayClass;
            Team0ScoreText.Text = rootObject.Properties.Team0Score.ToString();
            Team1ScoreText.Text = rootObject.Properties.Team1Score.ToString();

            GoalsList.ItemsSource = rootObject.Properties.Goals;
        }
    }
}
