using FussionAdminEvidence.Models;
using FussionAdminEvidence.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class PedidosViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<Pedido> pedidos;
        #endregion

        #region Properties
        public ObservableCollection<Pedido> Pedidos
        {
            get { return this.pedidos; }
            set { SetValue(ref this.pedidos, value); }

        }
        #endregion

        #region Constructors
        public PedidosViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPedidos();
        }
        #endregion

        #region Methods
        private async void LoadPedidos()
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");

                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            //192.168.0.4/api/apiFussion/Pedidos/getPedidos.php
            var response = await apiService.GetList<Pedido>("http://5e92feff3013.ngrok.io/", "/api/apiFussion/Pedidos", "/getPedidos.php");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Aceptar");
                return;
            }

            var lista = (List<Pedido>)response.Result;
            this.Pedidos = new ObservableCollection<Pedido>(lista);

        }

        #endregion
    }
}
