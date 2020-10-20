using GamesWPF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesWPF.Modules
{
    static class SteamGameImage
    {
        public static AllGameImage GetImage(string startSteamDir, string gameId)
        {
            AllGameImage Image = new AllGameImage();

            if (File.Exists(startSteamDir + "\\appcache\\librarycache\\" + gameId + "_library_hero.jpg"))
                Image.LibraryHero = startSteamDir + "\\appcache\\librarycache\\" + gameId + "_library_hero.jpg";

            if (File.Exists(startSteamDir + "\\appcache\\librarycache\\" + gameId + "_library_hero_blur.jpg"))
                Image.LibraryHeroBlur = startSteamDir + "\\appcache\\librarycache\\" + gameId + "_library_hero_blur.jpg";

            if (File.Exists(startSteamDir + "\\appcache\\librarycache\\" + gameId + "_header.jpg"))
                Image.Header = startSteamDir + "\\appcache\\librarycache\\" + gameId + "_header.jpg";

            if (File.Exists(startSteamDir + "\\appcache\\librarycache\\" + gameId + "_icon.jpg"))
                Image.Icon = startSteamDir + "\\appcache\\librarycache\\" + gameId + "_icon.jpg";

            if (File.Exists(startSteamDir + "\\appcache\\librarycache\\" + gameId + "_library_600x900.jpg"))
                Image.Library = startSteamDir + "\\appcache\\librarycache\\" + gameId + "_library_600x900.jpg";

            if (File.Exists(startSteamDir + "\\appcache\\librarycache\\" + gameId + "_logo.png"))
                Image.Logo = startSteamDir + "\\appcache\\librarycache\\" + gameId + "_logo.png";

            return Image;
        }
    }
}
