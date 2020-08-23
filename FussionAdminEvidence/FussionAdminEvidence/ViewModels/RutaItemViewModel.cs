using FussionAdminEvidence.Models;
using FussionAdminEvidence.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FussionAdminEvidence.ViewModels
{
    public class RutaItemViewModel: Ruta
    {
        #region Commands
        public ICommand SelectRutaCommand
        {
            get
            {
                return new RelayCommand(SelectRuta);
            }
        }

        private async void SelectRuta()
        {
            MainViewModel.GetInstace().Ruta = new RutaViewModel(this);
            //await Application.Current.MainPage.Navigation.PushAsync(new PedidoPage());
            await App.Navigator.PushAsync(new RutaPage());
        }
        #endregion
    }
}
