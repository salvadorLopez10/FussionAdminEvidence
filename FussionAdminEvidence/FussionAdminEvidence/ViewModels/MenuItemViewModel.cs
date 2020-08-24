using FussionAdminEvidence.Services;
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

        #region Services
        //private PersistenceService persistenceService;
        #endregion

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
                    MainViewModel.GetInstace().Rutas = new RutasViewModel();
                    App.Navigator.PushAsync(new RutasPage());
                    break;
                case "ChoferesPage":
                    MainViewModel.GetInstace().Choferes = new ChoferesViewModel();
                    App.Navigator.PushAsync(new ChoferesPage());
                    break;
                case "LoginPage":
                    //persistenceService = new PersistenceService();
                    PersistenceService.GetPersistenceService().RestoreKeysPersistance("usuario", "horaLogin", "guid");
                    //persistenceService.RestoreKeysPersistance("usuario", "horaLogin");
                    Application.Current.MainPage = new LoginPage();
                    break;
                default:
                    break;
                        
            }
        }
    }
}
