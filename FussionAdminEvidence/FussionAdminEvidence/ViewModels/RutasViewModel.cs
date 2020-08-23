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
    public class RutasViewModel: BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<RutaItemViewModel> rutas;
        public bool isRefreshing;
        private string filter;
        private List<Ruta> rutasList;
        public static RutaViewModel rvm;
        #endregion

        #region Properties
        public ObservableCollection<RutaItemViewModel> Rutas
        {
            get { return this.rutas; }
            set { SetValue(ref this.rutas, value); }

        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        #endregion

        public RutasViewModel()
        {
            this.apiService = new ApiService();
            this.LoadRutas();
        }

        private IEnumerable<RutaItemViewModel> ToRutaItemViewModel()
        {
            return this.rutasList.Select(r=> new RutaItemViewModel
            {
                FormattedId=r.FormattedId,
                Identifier=r.Identifier,
                Nombre= r.Nombre,
                Fecha=r.Fecha,
                HoraLlegada=r.HoraLlegada,
                HoraSalida=r.HoraSalida,
                KmSalida=r.KmSalida,
                KmLlegada=r.KmLlegada,
                Status=r.Status,
                Chofer=r.Chofer,
                DetalleRuta=r.DetalleRuta,
                Pedidos=r.Pedidos
            });
        }

        private async void LoadRutas()
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

            var response = await apiService.GetRutas("https://apps.fussionweb.com/", "/sietest/Mobile", "/Rutas");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            this.IsRefreshing = false;

            this.rutasList = (List<Ruta>)response.Result;
            this.Rutas = new ObservableCollection<RutaItemViewModel>(
                   this.ToRutaItemViewModel());
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Rutas = new ObservableCollection<RutaItemViewModel>(
                    this.ToRutaItemViewModel());
            }
            else
            {

                this.Rutas = new ObservableCollection<RutaItemViewModel>(
                    this.ToRutaItemViewModel().Where(
                         r => r.Nombre.ToLower().Contains(this.Filter.ToLower()) ||
                         r.Chofer.Nombre.ToUpper().Contains(this.Filter.ToUpper())
                    ));
            }
        }

        public ICommand SearchRutaCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadRutas);
            }
        }
    }
}
