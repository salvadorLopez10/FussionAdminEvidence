using FussionAdminEvidence.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class MenuItemViewModel
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        } 
        #endregion

        private void Navigate()
        {
            App.Master.IsPresented = false;
            switch (this.PageName)
            { 
                case "RutasPage":
                    App.Navigator.PushAsync(new RutasPage());
                    break;
                case "ChoferesPage":
                    MainViewModel.GetInstace().Choferes = new ChoferesViewModel();
                    App.Navigator.PushAsync(new ChoferesPage());
                    break;
                case "LoginPage":
                    Application.Current.MainPage = new LoginPage();
                    break;
                default:
                    break;
                        
            }
        }
    }
}
