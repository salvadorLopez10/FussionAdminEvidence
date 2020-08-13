using FussionAdminEvidence.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class RutaViewModel: Ruta, INotifyPropertyChanged
    {
        #region Properties
        private string rutaNombre;
        private string estado;
        private string nombreChofer;
        private DateTime currentDate;
        private TimeSpan currentHour;
        private bool isEnabled;
        #endregion

        #region Attributes

        public string RutaNombre
        {
            set
            {
                if (rutaNombre != value)
                {
                    rutaNombre = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RutaNombre"));
                }
            }
            get
            {
                return rutaNombre;
            }
        }

        public string Estado
        {
            set
            {
                if (estado != value)
                {
                    estado = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Estado"));
                }
            }
            get
            {
                return estado;
            }
        }

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

        public DateTime CurrentDate
        {
            set
            {
                if (currentDate != value)
                {
                    currentDate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDate"));
                }
            }
            get
            {
                return currentDate;
            }
        }

        public TimeSpan CurrentHour
        {
            set
            {
                if (currentHour != value)
                {
                    currentHour = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentHour"));
                }
            }
            get
            {
                return currentHour;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }

        #endregion

        /*
        public RutaViewModel(Ruta ruta)
        {
            this.NombreChofer = ruta.Chofer;
            this.CurrentDate = DateTime.Now;
            
        }
        

        public RutaViewModel()
        {
            this.CurrentDate = DateTime.Now;
            var hora = DateTime.Now.Hour;
            var minutos = DateTime.Now.Minute;
            var segundos = DateTime.Now.Second;

            CurrentHour = new TimeSpan(hora, minutos, segundos);

        }
        */

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public ICommand GuardarRutaCommand
        {
            get
            {
                return new RelayCommand(GuardarRuta);
            }
        }

        private async void GuardarRuta()
        {
            if (string.IsNullOrEmpty(this.Nombre))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Favor de ingresar el nombre de la ruta", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.NombreChofer))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Favor de ingresar nombre de Chofer asignado", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.NombreChofer))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Favor de ingresar nombre de Chofer asignado", "Aceptar");
                return;
            }

            if (this.KmSalida<=0.0)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Favor de ingresar kilometraje de salida", "Aceptar");
                return;
            }
        }

            #endregion


        }
}
