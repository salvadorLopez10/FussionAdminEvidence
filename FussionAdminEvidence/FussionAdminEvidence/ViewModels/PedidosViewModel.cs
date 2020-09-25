using FussionAdminEvidence.Models;
using FussionAdminEvidence.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class PedidosViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private PersistenceService persistenceService;
        #endregion

        #region Attributes
        private ObservableCollection<PedidoItemViewModel> pedidos;
        private ObservableCollection<PedidoForRutaItemViewModel> pedidosForRuta;
        public bool isRefreshing;
        public bool isEnabledSearchBar;
        private string filter;
        private List<Pedido_> pedidosList;
        public static RutaViewModel rvm;
        #endregion

        #region Properties
        public ObservableCollection<PedidoItemViewModel> Pedidos
        {
            get { return this.pedidos; }
            set { SetValue(ref this.pedidos, value); }

        }

        public ObservableCollection<PedidoForRutaItemViewModel> PedidosForRuta
        {
            get { return this.pedidosForRuta; }
            set { SetValue(ref this.pedidosForRuta, value); }

        }

        public bool IsRefreshing {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public bool IsEnabledSearchBar
        {
            get { return this.isEnabledSearchBar; }
            set { SetValue(ref this.isEnabledSearchBar, value); }
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
            this.persistenceService = new PersistenceService();
            this.LoadPedidos();
        }

        public PedidosViewModel(string bandera, RutaViewModel rutaVM)
        {
            this.apiService = new ApiService();
            this.persistenceService = new PersistenceService();
            rvm = rutaVM;
            this.LoadPedidosForRuta();
        }
        #endregion

        #region Singleton

        public static RutaViewModel GetInstaceRutaVM()
        {
            return rvm;
        }

        #endregion

        #region Methods

        private IEnumerable<PedidoItemViewModel> ToPedidoItemViewModel()
        {
            return this.pedidosList.Select(p => new PedidoItemViewModel { 
                CardCode=p.CardCode,
                CardName=p.CardName,
                Address2=p.Address2,
                Cajas=p.Cajas,
                Fletera=p.Fletera,
                Items=p.Items,
                FormattedId=p.FormattedId,
                Identifier=p.Identifier,
                IsValid=p.IsValid,
                LastValidationErrorMessages=p.LastValidationErrorMessages,
                LastValidationErrors=p.LastValidationErrors
            });
        }

        private IEnumerable<PedidoForRutaItemViewModel> ToPedidoForRutaItemViewModel()
        {
            return this.pedidosList.Select(p => new PedidoForRutaItemViewModel
            {
                CardCode = p.CardCode,
                CardName = p.CardName,
                Address2 = p.Address2,
                Cajas = p.Cajas,
                Items = p.Items,
                FormattedId = p.FormattedId,
                Identifier = p.Identifier,
                IsValid = p.IsValid,
                LastValidationErrorMessages = p.LastValidationErrorMessages,
                LastValidationErrors = p.LastValidationErrors
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

            //var idUsuario = "47c2e141-55e0-4857-9e15-ab48dd3a862e";
            //var idUsuario = "fb9c3aff-ff58-4e3d-97c1-8dcdb50f02ce";
            var idUsuario = this.persistenceService.GetValuePreference("guid");

                //var response = await apiService.GetList<Pedido_>("https://apps.fussionweb.com/", "/sietest/Mobile", "/Pedidos");
            var response = await apiService.GetPedidos("https://apps.fussionweb.com/", "/sietest/Mobile", "/PedidosChofer?Chofer=" +idUsuario);
            //var response = await apiService.GetPedidos("https://apps.fussionweb.com/", "/sie/Mobile", "/PedidosChofer?Chofer=" +idUsuario);
             if (!response.IsSuccess)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                    return;
                }

             this.IsRefreshing = false;

             this.pedidosList = (List<Pedido_>)response.Result;
             this.Pedidos = new ObservableCollection<PedidoItemViewModel>(
                    this.ToPedidoItemViewModel());

            if (this.Pedidos.Count==0)
            {
                await Application.Current.MainPage.DisplayAlert("Información","Usted no cuenta con pedidos asignados."+ Environment.NewLine+"Por favor contacte al administrador", "Aceptar");
            }
        }

        private async void LoadPedidosForRuta()
        {
            this.IsRefreshing = true;
            this.IsEnabledSearchBar = false;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsEnabledSearchBar = true;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");

                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            
            //var response = await apiService.GetPedidos("https://apps.fussionweb.com/", "/sie/Mobile", "/Pedidos");
            var response = await apiService.GetPedidos("https://apps.fussionweb.com/", "/sietest/Mobile", "/Pedidos");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsEnabledSearchBar = true;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            this.IsRefreshing = false;
            this.IsEnabledSearchBar = true;

            this.pedidosList = (List<Pedido_>)response.Result;
            this.PedidosForRuta = new ObservableCollection<PedidoForRutaItemViewModel>(
                this.ToPedidoForRutaItemViewModel());

            //Comparar PedidosForRuta con el arreglo del RutaViewModel para saber cuales son los que deben de tener check
            if (rvm.DetalleRuta.Count>0){
                foreach (var itemSeleccionado in rvm.DetalleRuta)
                {
                    foreach (var itemFromApi in this.PedidosForRuta)
                    {
                        if (itemFromApi.Identifier==itemSeleccionado.Identifier){
                            itemFromApi.IsChecked = true;
                        }
                    }
                }
            }

        }

        private void Search() 
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Pedidos = new ObservableCollection<PedidoItemViewModel>(
                    this.ToPedidoItemViewModel());

                this.PedidosForRuta = new ObservableCollection<PedidoForRutaItemViewModel>(this.ToPedidoForRutaItemViewModel());
            }
            else
            {

                this.Pedidos = new ObservableCollection<PedidoItemViewModel>(
                    this.ToPedidoItemViewModel().Where(
                        p=>p.FormattedId.ToLower().Contains(this.Filter.ToLower())||
                        p.CardName.ToUpper().Contains(this.Filter.ToUpper())

                    ));

                this.PedidosForRuta = new ObservableCollection<PedidoForRutaItemViewModel>(
                    this.ToPedidoForRutaItemViewModel().Where(
                        p => p.FormattedId.ToLower().Contains(this.Filter.ToLower()) ||
                        p.CardName.ToUpper().Contains(this.Filter.ToUpper())

                    ));

                
            }

            if (rvm != null)
            {
                if (rvm.DetalleRuta.Count > 0)
                {
                    foreach (var itemSeleccionado in rvm.DetalleRuta)
                    {
                        foreach (var itemFromApi in this.PedidosForRuta)
                        {
                            if (itemFromApi.Identifier == itemSeleccionado.Identifier)
                            {
                                itemFromApi.IsChecked = true;
                            }
                        }
                    }
                }
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


        public ICommand AddPedidosToRutaCommand
        {
            get
            {
                return new RelayCommand(AddPedidosToRuta);
            }
        }

        private async void AddPedidosToRuta()
        {
            //Obtener valor de checkbox
            if (rvm.DetalleRuta.Count==0)
            {
                await Application.Current.MainPage.DisplayAlert("Elegir al menos un pedido", "Favor de elegir al menos un pedido de la lista", "Aceptar");
                return;
            }
            //Concatenar contenido del arreglo
            var strDetallePedidos = "";
            foreach (var item in rvm.DetalleRuta)
            {
                strDetallePedidos += "Pedido: " + item.FormattedId + Environment.NewLine + "Cliente: " + item.CardName+ Environment.NewLine+ Environment.NewLine;
            }
            rvm.DetallePedidos = strDetallePedidos;
            rvm.IsVisibleListaPedidos = true;
            rvm.IsEnabled = true;
            await App.Current.MainPage.Navigation.PopModalAsync();
        }


        #endregion
    }
}
