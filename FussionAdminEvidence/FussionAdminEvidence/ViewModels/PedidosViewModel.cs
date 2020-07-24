using FussionAdminEvidence.Models;
using FussionAdminEvidence.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class PedidosViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<PedidoItemViewModel> pedidos;
        public bool isRefreshing;
        private string filter;
        private List<Pedido> pedidosList;
        #endregion

        #region Properties
        public ObservableCollection<PedidoItemViewModel> Pedidos
        {
            get { return this.pedidos; }
            set { SetValue(ref this.pedidos, value); }

        }

        public bool IsRefreshing {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter {
            get { return this.filter; }
            set {
                SetValue(ref this.filter, value);
                this.Search();
            }
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

        private IEnumerable<PedidoItemViewModel> ToPedidoItemViewModel()
        {
            return this.pedidosList.Select(p => new PedidoItemViewModel
            {
                Id=p.Id,
                NumeroPedido=p.NumeroPedido,
                FechaPedido=p.FechaPedido,
                CodigoCliente=p.CodigoCliente,
                NombreCliente=p.NombreCliente,
                DireccionEntrega=p.DireccionEntrega,
                NumeroCajas=p.NumeroCajas,
                FolioFactura=p.FolioFactura,
                Detalle=p.Detalle,
                ItemCode=p.ItemCode,
                Quantity=p.Quantity,
                Comentarios=p.Comentarios
            });
        }

        private async void LoadPedidos()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");

                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await apiService.GetList<Pedido>("http://5e92feff3013.ngrok.io/", "/api/apiFussion/Pedidos", "/getPedidos.php");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error",response.Message,"Aceptar");
                return;
            }

            this.IsRefreshing = false;
            this.pedidosList = (List<Pedido>)response.Result;
            this.Pedidos = new ObservableCollection<PedidoItemViewModel>(
                this.ToPedidoItemViewModel());

        }

        private void Search() 
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Pedidos = new ObservableCollection<PedidoItemViewModel>(
                    this.ToPedidoItemViewModel());
            }
            else
            {
                this.Pedidos = new ObservableCollection<PedidoItemViewModel>(
                    this.ToPedidoItemViewModel().Where(
                        l => l.NumeroPedido.Contains(this.Filter) ||
                             l.NombreCliente.ToLower().Contains(this.Filter.ToLower())
                        ));
            }
        }

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get {
                return new RelayCommand(LoadPedidos);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }
        #endregion
    }
}
