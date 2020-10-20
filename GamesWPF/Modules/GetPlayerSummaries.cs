using GamesWPF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GamesWPF.Modules
{
    static class PlayerSummaries
    {
        public static PlayerData GetSummaries(string key, long SteamId)
        {
            PlayerData Summaries = new PlayerData();

            Uri uri = new Uri("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + key + "&steamids=" + SteamId.ToString() + "&format=json");
            WebClient webClient = new WebClient();


            string json = Encoding.UTF8.GetString(webClient.DownloadData(uri));

            Summaries = JsonConvert.DeserializeObject<PlayerData>(json);

            return Summaries;
        }
    }
}
