using Gameloop.Vdf;
using GamesWPF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesWPF.Modules
{
    static class SteamGamesDir
    {
        public static AllDirGames GetGames(string startSteamDir)
        {
            AllDirGames dirs = new AllDirGames();
            dirs.SteamFolders = SteamFolders(startSteamDir);
            dirs.InstallGamesAppmanifestDir = InstallGamesAppmanifestDir(dirs.SteamFolders);
            dirs.InstallGamesId = InstallGamesId(dirs.InstallGamesAppmanifestDir);
            dirs.InstallGamesName = InstallGamesName(dirs.InstallGamesAppmanifestDir);
            return dirs;
        }

        /// <summary>
        /// Получаем все папки стима на ПК из libraryfolders.vdf
        /// </summary>
        /// <param name="startSteamDir"></param>
        private static string[] SteamFolders(string startSteamDir)
        {
            string[] libraryfolders = File.ReadAllLines(startSteamDir + @"\steamapps\libraryfolders.vdf");

            List<string> folders = new List<string>();
            folders.Add(startSteamDir);

            //Все папки пронумерованы от 1, поэтому считаем i
            int i = 1;
            bool a = true;
            while (a == true)
            {
                bool b = false;
                foreach (string sub in libraryfolders)
                {
                    //проверяем, существует ли следуещее значение i в файле. 
                    //Если нашли, то делим строку по кавычкам и достаем четвертое значение.
                    //Если ничего не нашли, то прерываем цикл.
                    if (sub.Contains("\"" + i.ToString() + "\""))
                    {
                        folders.Add(sub.Split('"')[3].Replace(@"\\", @"\"));
                        b = true;
                        i++;
                    }

                }
                if (b == false)
                    a = false;
            }

            return folders.ToArray();
        }

        /// <summary>
        /// Поиск файлов Appmanifest
        /// </summary>
        /// <param name="steamFolders"></param>
        private static string[] InstallGamesAppmanifestDir(string[] steamFolders)
        {
            List<string> files = new List<string>();
            foreach (string dir in steamFolders)
            {
                string[] fileEntries = Directory.GetFiles(dir + @"\steamapps", "appmanifest_*.acf");
                foreach (string file in fileEntries)
                {
                    files.Add(file);
                }
            }

            return files.ToArray();
        }

        /// <summary>
        /// Получение id приложений из файла Appmanifest
        /// </summary>
        /// <param name="InstallGamesAppmanifestDir"></param>
        /// <returns></returns>
        private static string[] InstallGamesId(string[] InstallGamesAppmanifestDir)
        {
            List<string> id = new List<string>();
            foreach (string dir in InstallGamesAppmanifestDir)
            {
                //с помощью Gameloop.Vdf достаем id из файла
                dynamic i = VdfConvert.Deserialize(File.ReadAllText(dir));
                id.Add(Convert.ToString(i.Value.appid));
            }
            return id.ToArray();
        }

        /// <summary>
        /// Получение названия приложений из файла Appmanifest
        /// </summary>
        /// <param name="InstallGamesAppmanifestDir"></param>
        /// <returns></returns>
        private static string[] InstallGamesName(string[] InstallGamesAppmanifestDir)
        {
            List<string> name = new List<string>();
            foreach (string dir in InstallGamesAppmanifestDir)
            {
                //с помощью Gameloop.Vdf достаем id из файла
                dynamic i = VdfConvert.Deserialize(File.ReadAllText(dir));
                name.Add(Convert.ToString(i.Value.name));
            }
            return name.ToArray();
        }
    }
}
