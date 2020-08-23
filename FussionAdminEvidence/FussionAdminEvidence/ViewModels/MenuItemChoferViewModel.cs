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
    public class MenuItemChoferViewModel
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
            App.MasterChofer.IsPresented = false;
            switch (this.PageName)
            {
                case "ProfilePage":
                    App.Navigator.PushAsync(new ProfilePage());
                    break;
                case "PedidosPage":
                    MainViewModel.GetInstace().Pedidos = new PedidosViewModel();
                    //Application.Current.MainPage = new NavigationPage(new PedidosPage());
                    App.Navigator.PushAsync(new PedidosPage());
                    break;
                case "LoginPage":
                    PersistenceService.GetPersistenceService().RestoreKeysPersistance("usuario", "horaLogin");
                    Application.Current.MainPage = new LoginPage();
                    break;
                default:
                    break;

            }
        }
    }
}
