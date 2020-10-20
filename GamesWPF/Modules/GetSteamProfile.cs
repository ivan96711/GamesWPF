using Gameloop.Vdf;
using GamesWPF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GamesWPF.Modules
{
    static class SteamProfile
    {
        /// <summary>
        /// Данные последнего авторизированного пользователя steam
        /// </summary>
        /// <param name="startSteamDir"></param>
        /// <returns></returns>
        public static SteamProfileData GetProfile(string startSteamDir)
        {
            SteamProfileData Profile = new SteamProfileData();

            string loginusers = File.ReadAllText(startSteamDir + @"\config\loginusers.vdf");

            string[] allusers = allUsers(loginusers);
            int profileNumber = currentProfileNumber(allusers);

            Profile.AccountName = AccountName(allusers[profileNumber]);
            Profile.PersonaName = PersonaName(allusers[profileNumber]);
            Profile.SteamId = SteamId(allusers[profileNumber]);

            return Profile;
        }

        /// <summary>
        /// Перечень всех учетных записей
        /// </summary>
        /// <param name="loginusers"></param>
        /// <returns></returns>
        private static string[] allUsers(string loginusers)
        {
            List<string> j = new List<string>();

            //регуляркой разделяем текст из loginusers. Должны получить подобный шаблон:      
            //  "'steamID из чисел'"
            //      {
            //          'любое количество строк'
            //      }
            //каждый шаблон характеризует отдельную учетку, которая была авторизована на пк 
            Regex regex = new Regex("\"[0-9]+\"\n\t{(\n.*)*?(\n\t})");
            MatchCollection matches = regex.Matches(loginusers);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                    j.Add(match.Value);
            }

            return j.ToArray();
        }

        /// <summary>
        /// Порядковый номер последней использованной учетки
        /// </summary>
        /// <param name="loginusers"></param>
        /// <returns></returns>
        private static int currentProfileNumber(string[] allusers)
        {
            int k = -1;
            //последняя учетка имеет параметр mostrecent равным 1
            for (int i = 0; i < allusers.Length; i++)
            {
                dynamic g = VdfConvert.Deserialize(allusers[i]);
                if (Convert.ToString(g.Value.mostrecent) == "1")
                {
                    k = i;
                    break;
                }
            }
            return k;
        }

        /// <summary>
        /// Логин
        /// </summary>
        /// <param name="loginuser"></param>
        /// <returns></returns>
        private static string AccountName(string loginuser)
        {
            //с помощью Gameloop.Vdf достаем имя из файла
            dynamic i = VdfConvert.Deserialize(loginuser);
            return Convert.ToString(i.Value.AccountName);
        }

        /// <summary>
        /// Ник
        /// </summary>
        /// <param name="loginuser"></param>
        /// <returns></returns>
        private static string PersonaName(string loginuser)
        {
            //с помощью Gameloop.Vdf достаем имя из файла
            dynamic i = VdfConvert.Deserialize(loginuser);
            return Convert.ToString(i.Value.PersonaName);
        }

        /// <summary>
        /// Steam id
        /// </summary>
        /// <param name="loginuser"></param>
        /// <returns></returns>
        private static long SteamId(string loginuser)
        {
            long result = 0;
            Regex regex = new Regex("[0-9]+");
            MatchCollection matches = regex.Matches(loginuser);
            if (matches.Count > 0)
                result = Convert.ToInt64(matches[0].Value);
            return result;
        }
    }
}
