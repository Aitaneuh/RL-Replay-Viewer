using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RL_Replay_Viewer
{
    public class Goal
    {
        public int frame { get; set; }
        public string PlayerName { get; set; }
        public int PlayerTeam { get; set; }
    }

    public class Properties
    {
        public string ObjectTypeName { get; set; }
        public int TeamSize { get; set; }
        public int Team0Score { get; set; }
        public int Team1Score { get; set; }
        public double TotalSecondsPlayed { get; set; }
        public long MatchStartEpoch { get; set; }
        public List<Goal> Goals { get; set; }
    }

    public class RootObject
    {
        public int EngineVersion { get; set; }
        public int LicenseeVersion { get; set; }
        public int NetVersion { get; set; }
        public string ReplayClass { get; set; }
        public Properties Properties { get; set; }
    }
}
