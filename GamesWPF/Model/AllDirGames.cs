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
    class AllDirGames : BaseVM
    {
        public string[] SteamFolders { get; set; }
        public string[] InstallGamesAppmanifestDir { get; set; }
        public string[] InstallGamesId { get; set; }
        public string[] InstallGamesName { get; set; }
    }
}
