using FussionAdminEvidence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using System.ComponentModel;
using FussionAdminEvidence.Services;
using FussionAdminEvidence.Views;

namespace FussionAdminEvidence.ViewModels
{
    public class ChoferItemViewModel: Chofer,INotifyPropertyChanged
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Properties
        private string nombreChofer;
        private string apellido;
        private bool isRunning;
        #endregion

        #region Attributes
        public string NombreChofer
        {
            set
            {
                if (nombreChofer != value)
                {
                    nombreChofer = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NombreChofer"));
                }
            }
            get
            {
                return nombreChofer;
            }
        }

        public string Apellido
        {
            set
            {
                if (apellido != value)
                {
                    apellido = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Apellido"));
                }
            }
            get
            {
                return apellido;
            }
        }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }
        #endregion

        public ChoferItemViewModel()
        {
            this.apiService = new ApiService();
        }

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
            bool respuesta = await Application.Current.MainPage.DisplayAlert("Asignar ruta", "¿Desea asignar ruta al chofer "+this.Nombre+"?", "SI", "NO");
            if (respuesta)
            {
                var hora = DateTime.Now.Hour;
                var minutos = DateTime.Now.Minute;
                var segundos = DateTime.Now.Second;


                RutaViewModel objRuta = new RutaViewModel();
                objRuta.Identifier = 0;
                objRuta.Nombre= "Ruta de: " + this.Nombre;
                objRuta.Fecha = DateTime.Now;
                objRuta.TsHoraLlegada = new TimeSpan(hora, minutos, segundos);
                objRuta.TsHoraSalida = new TimeSpan(hora, minutos, segundos);
                objRuta.KmSalida = 0.0m;
                objRuta.KmLlegada = 0.0m;
                objRuta.Chofer = this;
                objRuta.NombreChofer = this.Nombre;
                objRuta.Status = "1";
                objRuta.DetalleRuta = new List<Pedido_>();

                MainViewModel.GetInstace().Ruta = new RutaViewModel(objRuta);
                await App.Navigator.PushAsync(new RutaPage());
            }
            else
            {
                return;
            }

        }

        public ICommand AddNewChoferCommand
        {
            get
            {
                return new RelayCommand(AddNewChofer);
            }
        }

        private async void AddNewChofer()
        {

            if (string.IsNullOrEmpty(this.NombreChofer))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Tienes que ingresar el nombre del chofer", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Apellido))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Tienes que ingresar el apellido del chofer", "Aceptar");
                return;
            }

            this.IsRunning = true;

            Chofer elchofer = new Chofer
            {
                Nombre=this.NombreChofer + " " + this.Apellido
            };

            //var response= await this.apiService.Post<Chofer>("https://apps.fussionweb.com/", "sie/Mobile", "/AgregarChofer", elchofer);
            var response= await this.apiService.Post<Chofer>("https://apps.fussionweb.com/", "sietest/Mobile", "/AgregarChofer", elchofer);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                this.NombreChofer = string.Empty;
                this.Apellido = string.Empty;
                return;
            }

            this.IsRunning = false;

            await Application.Current.MainPage.DisplayAlert("Éxito",
                "El chofer "+this.NombreChofer+" "+this.Apellido+" ha sido guardado con éxito",
                "Aceptar");

            MainViewModel.GetInstace().Choferes = new ChoferesViewModel();
            await App.Navigator.PushAsync(new ChoferesPage());
            this.NombreChofer = string.Empty;
            this.Apellido = string.Empty;

        }


        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

    }
}
