using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GamesWPF.Model
{
    public partial class PlayerData
    {
        public Response Response { get; set; }
    }

    public partial class Response
    {
        public Player[] Players { get; set; }
    }

    public partial class Player
    {
        public string Steamid { get; set; }
        public long Communityvisibilitystate { get; set; }
        public long Profilestate { get; set; }
        public string Personaname { get; set; }
        public long Commentpermission { get; set; }
        public Uri Profileurl { get; set; }
        public Uri Avatar { get; set; }
        public Uri Avatarmedium { get; set; }
        public Uri Avatarfull { get; set; }
        public long Lastlogoff { get; set; }
        public long Personastate { get; set; }
        public string Realname { get; set; }
        public string Primaryclanid { get; set; }
        public long Timecreated { get; set; }
        public long Personastateflags { get; set; }
        public string Loccountrycode { get; set; }
    }
}
