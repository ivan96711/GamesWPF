using DevExpress.Mvvm;
using GamesWPF.Model;
using GamesWPF.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace GamesWPF.ViewModel
{
    public class MainWindowViewModel : BaseVM
    {
        // steam web api key https://steamcommunity.com/dev/apikey
        string key = "";

        private DispatcherTimer timer;        
        string steamDir = "";
        public MainWindowViewModel()
        {
            Microsoft.Win32.RegistryKey objRestistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");
            steamDir = (string)objRestistryKey.GetValue("SteamPath");

            LoadData();
        }

        public int RunningAppID { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileName { get; set; }
        public string ProfileRealName { get; set; }
        public string SteamIsRunning { get; set; }
        public ICollectionView GamesView { get; set; }
        public ObservableCollection<Games> Games { get; set; }
        private Games _SelectedGame;
        public Games SelectedGame
        {
            get { return _SelectedGame; }
            set
            {
                _SelectedGame = value;
                RaisePropertyChanged("SelectedGame");
            }
        }

        /// <summary>
        /// Полная загрузка всех данных
        /// </summary>
        private void LoadData()
        {
            var steamProfile = SteamProfile.GetProfile(steamDir);
            var playerSummaries = PlayerSummaries.GetSummaries(key, steamProfile.SteamId);

            ProfileImage = playerSummaries.Response.Players[0].Avatarmedium.ToString();
            ProfileName = steamProfile.PersonaName;
            ProfileRealName = playerSummaries.Response.Players[0].Realname;

            if(Games != null)
                Games.Clear();
            Games = GetGames.LoadData(steamDir, steamProfile.SteamId, key);
            
            SelectedGame = Games.FirstOrDefault();
            BindingOperations.EnableCollectionSynchronization(Games, new object());

            //ставим таймер для проверки статуса steam
            timer = new DispatcherTimer();
            timer.Tick += CheckInfo_Tick;
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
            CheckInfo_Tick(null, null);
        }

        /// <summary>
        /// Обновление времени после закрытия приложения
        /// </summary>
        private void RefreshTime()
        {
            var newTime = GetGames.RefreshTime(Games, SteamProfile.GetProfile(steamDir).SteamId, key);
            for(int i = 0; i < newTime.Count(); i++)
            {
                Games[i].PlaytimeForever = newTime[i][0];
                Games[i].Playtime2Weeks = newTime[i][1];
                if(SelectedGame.InstallGameId == Games[i].InstallGameId)
                {
                    SelectedGame.PlaytimeForever = newTime[i][0];
                    SelectedGame.Playtime2Weeks = newTime[i][1];
                    RaisePropertyChanged("SelectedGame");
                }
            }
        }

        /// <summary>
        /// Проверка запущенного стима
        /// Проверка запущенной игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckInfo_Tick(object sender, EventArgs e)
        {
            //bool steamIsRunningChangeed = false;

            //if (Steamworks.SteamAPI.IsSteamRunning())
            //{
            //    if (SteamIsRunning != "")
            //        steamIsRunningChangeed = true;
            //    SteamIsRunning = "";
            //}
            //else
            //{
            //    if (SteamIsRunning == "")
            //        steamIsRunningChangeed = true;
            //    SteamIsRunning = "Steam не запущен";
            //}

            //if (steamIsRunningChangeed)
            //    RaisePropertyChanged("SteamIsRunning");

            Microsoft.Win32.RegistryKey objRestistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Valve\\Steam");
            if (RunningAppID != (int)objRestistryKey.GetValue("RunningAppID"))
            {
                RunningAppID = (int)objRestistryKey.GetValue("RunningAppID");
                RaisePropertyChanged("RunningAppID");

                if (RunningAppID != 0)
                    ProfileName = SteamProfile.GetProfile(steamDir).PersonaName + " в игре " + Games.FirstOrDefault(x => x.InstallGameId == RunningAppID.ToString()).InstallGameName;
                else
                {
                    ProfileName = SteamProfile.GetProfile(steamDir).PersonaName;
                    RefreshTime();
                }
                RaisePropertyChanged("ProfileName");
            }
        }

        public ICommand StartGame
        {
            get
            {
                return new DelegateCommand<int>((id) =>
                {
                    Process.Start("steam://run/" + SelectedGame.InstallGameId);
                }, (id) => id == 0);
            }
        }

        public ICommand Friends
        {
            get
            {
                return new DelegateCommand<string>((status) =>
                {
                    Process.Start("steam://open/friends");
                }, 
                (status) => status != "");
            }
        }
    }
}
