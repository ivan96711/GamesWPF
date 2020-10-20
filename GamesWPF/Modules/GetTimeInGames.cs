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
    static class TimeInGame
    {
        public static Time GetTime(string key, long SteamId)
        {
            Time time = new Time();
            Uri uri = new Uri("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + key + "&steamid=" + SteamId.ToString() + "&format=json");
            WebClient webClient = new WebClient();

            string json = Encoding.UTF8.GetString(webClient.DownloadData(uri));

            time = JsonConvert.DeserializeObject<Time>(json);

            return time;
        }
    }
}
