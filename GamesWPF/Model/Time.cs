using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GamesWPF.Model
{
    public partial class Time
    {
        public Response Response { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("game_count")]
        public long GameCount { get; set; }

        [JsonProperty("games")]
        public Game[] Games { get; set; }
    }

    public partial class Game
    {
        [JsonProperty("appid")]
        public long Appid { get; set; }

        [JsonProperty("playtime_forever")]
        public long PlaytimeForever { get; set; }

        [JsonProperty("playtime_windows_forever")]
        public long PlaytimeWindowsForever { get; set; }

        [JsonProperty("playtime_mac_forever")]
        public long PlaytimeMacForever { get; set; }

        [JsonProperty("playtime_linux_forever")]
        public long PlaytimeLinuxForever { get; set; }

        [JsonProperty("playtime_2weeks", NullValueHandling = NullValueHandling.Ignore)]
        public long Playtime2Weeks { get; set; }
    }
}
