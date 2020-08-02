using FussionAdminEvidence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class ChoferItemViewModel: Chofer
    {
        #region Commands
        public ICommand SelectChoferCommand
        {
            get
            {
                return new RelayCommand(SelectChofer);
            }
        }

        private async void SelectChofer()
        {
            await Application.Current.MainPage.DisplayAlert("PRUEBA", "MENSAJE TEMPORAL", "Aceptar");

        } 
        #endregion

    }
}
