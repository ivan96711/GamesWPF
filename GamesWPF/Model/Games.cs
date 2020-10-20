using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesWPF.Model
{
    public class Games : BaseVM
    {
        public string InstallGameId { get; set; }
        public string InstallGameName { get; set; }
        public string LibraryHero { get; set; }
        public string LibraryHeroBlur { get; set; }
        public string Header { get; set; }
        public string Icon { get; set; }
        public string Library { get; set; }
        public string Logo { get; set; }
        public long PlaytimeForever { get; set; }
        public long Playtime2Weeks { get; set; }
    }
}
