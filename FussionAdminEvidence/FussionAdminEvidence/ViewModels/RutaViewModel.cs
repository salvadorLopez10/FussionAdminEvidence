using FussionAdminEvidence.Models;
using FussionAdminEvidence.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class RutaViewModel : Ruta, INotifyPropertyChanged
    {
        #region Properties
        private string rutaNombre;
        private string estado;
        private string nombreChofer;
        private List<StatusRuta> statusRuta;
        private string stringStatus;
        private StatusRuta statusRutaSeleccionado;
        private DateTime currentDate;
        private TimeSpan currentHour;
        private bool isEnabled; 
        private bool isVisibleRelatePedidos; 
        private bool isVisibleListaPedidos;
        private string detallePedidos;

        private TimeSpan tsHoraLlegada;
        private TimeSpan tsHoraSalida;
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

        public List<StatusRuta> StatusRuta
        {
            set
            {
                if (statusRuta != value)
                {
                    statusRuta = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusRuta"));
                }
            }
            get
            {
                return statusRuta;
            }
        }

        public StatusRuta StatusRutaSeleccionado
        {
            set
            {
                if (statusRutaSeleccionado != value)
                {
                    statusRutaSeleccionado = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusRutaSeleccionado"));
                }
            }
            get
            {
                return statusRutaSeleccionado;
            }
        }

        public string StringStatus
        {
            set
            {
                if (stringStatus != value)
                {
                    stringStatus = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StringStatus"));
                }
            }
            get
            {
                return stringStatus;
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

        public bool IsVisibleRelatePedidos
        {
            set
            {
                if (isVisibleRelatePedidos != value)
                {
                    isVisibleRelatePedidos = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsVisibleRelatePedidos"));
                }
            }
            get
            {
                return isVisibleRelatePedidos;
            }
        }
        public bool IsVisibleListaPedidos
        {
            set
            {
                if (isVisibleListaPedidos != value)
                {
                    isVisibleListaPedidos = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsVisibleListaPedidos"));
                }
            }
            get
            {
                return isVisibleListaPedidos;
            }
        }

        public string DetallePedidos
        {
            set
            {
                if (detallePedidos != value)
                {
                    detallePedidos = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DetallePedidos"));
                }
            }
            get
            {
                return detallePedidos;
            }
        }

        public TimeSpan TsHoraLlegada
        {
            set
            {
                if (tsHoraLlegada != value)
                {
                    tsHoraLlegada = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TsHoraLlegada"));
                }
            }
            get
            {
                return tsHoraLlegada;
            }
        }

        public TimeSpan TsHoraSalida
        {
            set
            {
                if (tsHoraSalida != value)
                {
                    tsHoraSalida = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TsHoraSalida"));
                }
            }
            get
            {
                return tsHoraSalida;
            }
        }

        #endregion

        /*
        public RutaViewModel(Ruta ruta)
        {
            this.NombreChofer = ruta.Chofer;
            this.CurrentDate = DateTime.Now;
            
        }
        */
        public RutaViewModel(RutaViewModel rvm)
        {
            IsVisibleListaPedidos = false;
            Nombre = rvm.Nombre;
            Fecha = rvm.Fecha;
            HoraLlegada = rvm.HoraLlegada;
            HoraSalida = rvm.HoraSalida;
            KmSalida = rvm.KmSalida;
            KmLlegada = rvm.KmLlegada;
            Chofer = rvm.Chofer;
            NombreChofer = rvm.NombreChofer;
            Status = rvm.Status;
            DetalleRuta = rvm.DetalleRuta;
            //DetallePedidos = "PEDIDO 1" + Environment.NewLine + "Pedido 2" + Environment.NewLine + "Pedido 3" + Environment.NewLine;

        }
        public RutaViewModel(Ruta ruta)
        {
            IsVisibleListaPedidos = true;
            IsVisibleRelatePedidos = false;
            Nombre = ruta.Nombre;
            Fecha = ruta.Fecha;
            HoraLlegada = ruta.HoraLlegada;
            HoraSalida = ruta.HoraSalida;
            KmSalida = ruta.KmSalida;
            KmLlegada = ruta.KmLlegada;
            Chofer = ruta.Chofer;
            NombreChofer = ruta.Chofer.Nombre;
            StatusRuta = new List<StatusRuta>();
            StatusRuta.Add(new StatusRuta { Id="1",Valor="Abierta"});
            StatusRuta.Add(new StatusRuta { Id="2",Valor="Cerrada"});
            Status = ruta.Status;
            
            if (ruta.Status=="1")
            {
                //StringStatus = "Abierta";
                StatusRutaSeleccionado = StatusRuta[0];
            }
            else
            {
                StatusRutaSeleccionado = StatusRuta[1];
            }
            DetalleRuta = ruta.DetalleRuta;
            Pedidos = ruta.Pedidos;
            DetallePedidos = "";
            if (Pedidos.Count>0)
            {
                var strDetallePedidos = "";
                foreach (var item in Pedidos)
                {
                    strDetallePedidos += "Pedido: " + item.FormattedId + Environment.NewLine + "Cliente: " + item.CardName + Environment.NewLine + Environment.NewLine;
                }
                DetallePedidos = strDetallePedidos;
            }
           

        }
        public RutaViewModel()
        {

        }
        #region Singleton
        private static RutaViewModel instanceRuta;

        public static RutaViewModel GetInstaceRuta()
        {
            if (instanceRuta == null)
            {
                return new RutaViewModel();
            }

            return instanceRuta;
        }

        #endregion

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

            if (this.KmSalida <= 0.0m)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Favor de ingresar kilometraje de salida", "Aceptar");
                return;
            }
        }

        public ICommand GotoSelectPedidosCommand
        {
            get
            {
                return new RelayCommand(GotoSelectPedidos);
            }
        }

        private async void GotoSelectPedidos()
        {
            MainViewModel.GetInstace().PedidosForRuta = new PedidosViewModel("flag",this);
            //await App.Navigator.PushAsync(new PedidosForRutasPage());
            await App.Current.MainPage.Navigation.PushModalAsync(new PedidosForRutasPage());
        }

        #endregion

        #region Methods
        public void addListPedidos(Pedido_ pedido)
        {
            DetalleRuta.Add(pedido);
            IsEnabled = true;
        }

        #endregion
    }
}
