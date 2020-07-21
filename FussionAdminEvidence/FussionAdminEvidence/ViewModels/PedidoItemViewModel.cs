using FussionAdminEvidence.Models;
using FussionAdminEvidence.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class PedidoItemViewModel: Pedido
    {
        #region Commands
        public ICommand SelectPedidoCommand
        {
            get
            {
                return new RelayCommand(SelectPedido);
            }
        }

        private async void SelectPedido()
        {
            MainViewModel.GetInstace().Pedido = new PedidoViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new PedidoPage());

        }
        #endregion
    }
}
