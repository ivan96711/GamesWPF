using Gameloop.Vdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GamesWPF.Model
{
    class SteamProfileData : BaseVM
    {
        public string AccountName { get; set; }
        public string PersonaName { get; set; }
        public long SteamId { get; set; }
    }
}
