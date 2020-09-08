using FussionAdminEvidence.Models;
using FussionAdminEvidence.Services;
using FussionAdminEvidence.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json.Linq;
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
        #region Services
        private ApiService apiService;
        #endregion


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

        private decimal kmSalidaInicial;
        private decimal kmLlegadaInicial;

        private TimeSpan tsHoraLlegada;
        private TimeSpan tsHoraSalida;

        private bool actualizar;
        private bool isRunning;
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
                    this.ActualizarRegistro(statusRutaSeleccionado,value.Valor);
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

        /*
        public RutaViewModel(Ruta ruta)
        {
            this.NombreChofer = ruta.Chofer;
            this.CurrentDate = DateTime.Now;
            
        }
        */
        public RutaViewModel(RutaViewModel rvm)
        {
            this.apiService = new ApiService();
            IsVisibleListaPedidos = false;
            IsVisibleRelatePedidos = true;
            Nombre = rvm.Nombre;
            Fecha = rvm.Fecha;
            HoraLlegada = rvm.HoraLlegada;
            HoraSalida = rvm.HoraSalida;
            KmSalida = rvm.KmSalida;
            KmLlegada = rvm.KmLlegada;
            Chofer = rvm.Chofer;
            NombreChofer = rvm.Chofer.Nombre;
            StatusRuta = new List<StatusRuta>();
            StatusRuta.Add(new StatusRuta { Id = "1", Valor = "Abierta" });
            StatusRuta.Add(new StatusRuta { Id = "2", Valor = "Cerrada" });
            Status = rvm.Status;

            if (rvm.Status == "1")
            {
                //StringStatus = "Abierta";
                StatusRutaSeleccionado = StatusRuta[0];
            }
            else
            {
                //StringStatus = "Cerrada";
                StatusRutaSeleccionado = StatusRuta[1];
            }

            DetalleRuta = rvm.DetalleRuta;
            //DetallePedidos = "PEDIDO 1" + Environment.NewLine + "Pedido 2" + Environment.NewLine + "Pedido 3" + Environment.NewLine;

        }
        public RutaViewModel(Ruta ruta)
        {
            this.apiService = new ApiService();
            this.actualizar = false;
            IsVisibleListaPedidos = true;
            IsVisibleRelatePedidos = false;
            IsEnabled = true;
            Identifier = ruta.Identifier;
            Nombre = ruta.Nombre;
            Fecha = ruta.Fecha;
            HoraLlegada = ruta.HoraLlegada;
            HoraSalida = ruta.HoraSalida;

            KmSalida = ruta.KmSalida;
            kmSalidaInicial = ruta.KmSalida;
            
            KmLlegada = ruta.KmLlegada;
            kmLlegadaInicial = ruta.KmLlegada;
            
            Chofer = ruta.Chofer;
            NombreChofer = ruta.Chofer.Nombre;
            StatusRuta = new List<StatusRuta>();
            StatusRuta.Add(new StatusRuta { Id = "1", Valor = "Abierta" });
            StatusRuta.Add(new StatusRuta { Id = "2", Valor = "Cerrada" });
            Status = ruta.Status;

            if (ruta.Status == "1")
            {
                StatusRutaSeleccionado = StatusRuta[0];
            }
            else
            {
                StatusRutaSeleccionado = StatusRuta[1];
            }
            DetalleRuta = ruta.DetalleRuta;
            Pedidos = ruta.Pedidos;
            DetallePedidos = "";
            if (Pedidos.Count > 0)
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

            if (this.Identifier!=0)
            {
                if (this.KmLlegada <= 0.0m)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Favor de ingresar kilometraje de llegada", "Aceptar");
                    return;
                }

                if (this.KmLlegada <= this.KmSalida)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Kilometraje de llegada no puede ser menor o igual a kilometraje de salida", "Aceptar");
                    return;
                }
            }
            

            this.ActualizarRegistroPorKm(this.kmSalidaInicial, KmSalida);
            this.ActualizarRegistroPorKm(this.kmLlegadaInicial, KmLlegada);

            //Al tener el Identifier igual a 0, quiere decir que es creación de Ruta
            if (this.Identifier == 0) {
                //Armar el cuerpo para petición
                JObject ruta = new JObject();
                ruta["Nombre"] = this.Nombre;
                ruta["Fecha"] = this.Fecha.ToString("yyyy/MM/dd") ;
                ruta["HoraLLegada"] = string.Format("{0:hh\\:mm\\:ss}", this.TsHoraLlegada);
                ruta["HoraSalida"] = string.Format("{0:hh\\:mm\\:ss}", this.TsHoraSalida);
                ruta["KmSalida"] = this.KmSalida;
                ruta["KmLlegada"] = this.KmLlegada;
                JObject objChofer = new JObject();
                objChofer["Identifier"] = this.Chofer.Identifier;
                ruta["Chofer"] = objChofer;
                ruta["Status"] = 1;
                JArray arrPedidos = new JArray();
                
                //ruta["DetalleRuta"]= new JArray();
                if (this.DetalleRuta.Count>0)
                {
                    foreach (var item in this.DetalleRuta)
                    {
                        JObject itemPedido = new JObject();
                        itemPedido["Pedido"] = item.Identifier;
                        itemPedido["Status"] = 1;
                        arrPedidos.Add(itemPedido);
                    }
                    ruta["DetalleRuta"] = arrPedidos;
                    
                }

                //await Application.Current.MainPage.DisplayAlert("Info", ruta.ToString(), "Aceptar");
                //Comienza petición para crear Ruta
                this.IsRunning = true;
                this.IsEnabled = false;

                var connection = await this.apiService.CheckConnection();
                if (!connection.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                    return;
                }

                //Validar que la ruta que se intenta crear no contenga un nombre duplicado
                //var responseRutas = await apiService.GetRutas("https://apps.fussionweb.com/", "/sietest/Mobile", "/Rutas");
                var responseRutas = await apiService.GetRutas("https://apps.fussionweb.com/", "/sie/Mobile", "/Rutas");
                var rutasList = (List<Ruta>)responseRutas.Result;
                var existeNombreRuta = false;
                var choferAsignadoConRuta = false;
                if (rutasList.Count>0)
                {
                    foreach (var item in rutasList)
                    {
                        if (string.Equals(item.Nombre,this.Nombre))
                        {
                            existeNombreRuta = true;
                        }

                        if (string.Equals(this.Chofer.FormattedId, item.Chofer.FormattedId) && string.Equals(item.Status, "1"))
                        {
                            choferAsignadoConRuta = true;
                        }
                    }

                    if (existeNombreRuta)
                    {
                        this.IsRunning = false;
                        this.IsEnabled = true;
                        await Application.Current.MainPage.DisplayAlert("Error", "El nombre de la ruta ya existe "+Environment.NewLine+"Favor de elegir uno nuevo", "Aceptar");
                        return;
                    }

                    if (choferAsignadoConRuta)
                    {
                        this.IsRunning = false;
                        this.IsEnabled = true;
                        await Application.Current.MainPage.DisplayAlert("Error", "El chofer asignado ya cuenta con Rutas Abiertas" + Environment.NewLine + "Favor de elegir uno nuevo y/o cerrar las rutas asignadas a "+this.Chofer.Nombre, "Aceptar");
                        await App.Navigator.PopAsync();
                        return;
                    }
                }


                var response = await apiService.InsertarRuta("https://apps.fussionweb.com/", "sie/Mobile", "/InsertarRuta", ruta);
                //var response = await apiService.InsertarRuta("https://apps.fussionweb.com/", "sietest/Mobile", "/InsertarRuta", ruta);
                
                if (!response.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                    return;
                }
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert("Éxito", response.Message, "Aceptar");
                //MainViewModel.GetInstace().Rutas = new RutasViewModel();
                //await App.Navigator.PushAsync(new RutasPage());
                MainViewModel.GetInstace().Choferes = new ChoferesViewModel();
                Application.Current.MainPage = new MasterPage();



            }
            else
            {
                if (this.actualizar)
                {
                    //Mandar peticion

                    this.IsRunning = true;
                    this.IsEnabled = false;
                    
                   var connection = await this.apiService.CheckConnection();
                    if (!connection.IsSuccess)
                    {
                        this.IsRunning = false;
                        this.IsEnabled = true;
                        await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                        return;
                    }

                    //string json = "{\r\n\t\"Identifier\": \""+this.Identifier+"\",\r\n\t\"HoraLLegada\":\""+this.TsHoraLlegada.ToString()+"\",\r\n\t\"KmSalida\": "+this.KmSalida+ ",\r\n\t\"KmLlegada\": " + this.KmLlegada + ",\r\n\t\"Status\": "+this.StatusRutaSeleccionado.Id+"\r\n}";
                    JObject ruta = new JObject();
                    ruta["Identifier"] = this.Identifier;
                    ruta["HoraLLegada"] = string.Format("{0:hh\\:mm\\:ss}", this.TsHoraLlegada);
                    ruta["KmSalida"] = this.KmSalida;
                    ruta["KmLlegada"] =this.KmLlegada;
                    ruta["Status"] = int.Parse(this.StatusRutaSeleccionado.Id);

                    var response = await apiService.ActualizaRuta("https://apps.fussionweb.com/", "/sie/Mobile", "/ActualizarRuta", ruta);
                    //var response = await apiService.ActualizaRuta("https://apps.fussionweb.com/", "/sietest/Mobile", "/ActualizarRuta", ruta);
                    if (!response.IsSuccess)
                    {
                        this.IsRunning = false;
                        this.IsEnabled = true;
                        await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                        return;
                    }

                    this.IsRunning = false;
                    this.IsEnabled = true;

                    await Application.Current.MainPage.DisplayAlert("Éxito", response.Message, "Aceptar");
                    MainViewModel.GetInstace().Rutas = new RutasViewModel();
                    await App.Navigator.PopAsync();

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Información", "No ha cambiado la información", "Aceptar");
                    await App.Navigator.PopAsync();
                }

            }

            

            //this.IsEnabled = false;
            
        }

        public ICommand GotoSelectPedidosCommand
        {
            get
            {
                return new RelayCommand(GotoSelectPedidos, DisableButton);
            }
        }

        private bool DisableButton(){
            var puedeEjecutar = true;
            if (this.FormattedId != null)
            {
                IsVisibleRelatePedidos = false;
                puedeEjecutar = false;
            }

            return puedeEjecutar;
           
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

        public void ActualizarRegistro(StatusRuta objStatus, string valorNuevo)
        {
            if (objStatus != null && objStatus.Valor != valorNuevo )
            {
                this.actualizar = true;
            }
            
        }

        public void ActualizarRegistroPorKm(decimal valorActual, decimal valorNuevo)
        {
            if (valorActual != 0.0m && valorActual != valorNuevo)
            {
                this.actualizar = true;
            }

        }
        #endregion
    }
}
