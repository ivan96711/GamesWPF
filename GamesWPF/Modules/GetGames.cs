using GamesWPF.Model;
using GamesWPF.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesWPF.Modules
{
    static class GetGames
    {
        public static ObservableCollection<Games> LoadData(string steamDir, long steamID, string key)
        {
            ObservableCollection<Games> Games = new ObservableCollection<Games>();

            var steamGamesDir = SteamGamesDir.GetGames(steamDir);
            var timeInGame = TimeInGame.GetTime(key, steamID);

            for (int i = 0; i < steamGamesDir.InstallGamesId.Count(); i++)
            {
                AllGameImage allGameImage = SteamGameImage.GetImage(steamDir, steamGamesDir.InstallGamesId[i]);

                long playTimeForever = 0;
                long playTime2Weeks = 0;
                foreach (var game in timeInGame.Response.Games)
                {
                    if (game.Appid.ToString() == steamGamesDir.InstallGamesId[i])
                    {
                        playTimeForever = game.PlaytimeForever / 60;
                        playTime2Weeks = game.Playtime2Weeks / 60;
                    }
                }

                Games.Add(new Games
                {
                    InstallGameId = steamGamesDir.InstallGamesId[i],
                    InstallGameName = steamGamesDir.InstallGamesName[i],
                    LibraryHero = allGameImage.LibraryHero,
                    LibraryHeroBlur = allGameImage.LibraryHeroBlur,
                    Header = allGameImage.Header,
                    Icon = allGameImage.Icon,
                    Library = allGameImage.Library,
                    Logo = allGameImage.Logo,
                    PlaytimeForever = playTimeForever,
                    Playtime2Weeks = playTime2Weeks
                });
            }

            return Games;
        }

        public static List<long[]> RefreshTime(ObservableCollection<Games> Games, long steamID, string key)
        {
            var timeInGame = TimeInGame.GetTime(key, steamID);

            List<long[]> time = new List<long[]>();

            foreach (Games game in Games)
            {
                long playTimeForever = 0;
                long playTime2Weeks = 0;
                foreach (var g in timeInGame.Response.Games)
                {
                    if (g.Appid.ToString() == game.InstallGameId)
                    {
                        playTimeForever = g.PlaytimeForever / 60;
                        playTime2Weeks = g.Playtime2Weeks / 60;
                    }
                }

                time.Add(new long[] { playTimeForever, playTime2Weeks });
            }

            return time;
        }
    }
}
