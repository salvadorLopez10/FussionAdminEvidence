using FussionAdminEvidence.Models;
using FussionAdminEvidence.Services;
using FussionAdminEvidence.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private PersistenceService persistenceService;
        #endregion

        #region Attributes
        private string nombreUsuario;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string NombreUsuario {
            get { return nombreUsuario; }
            set { SetValue(ref nombreUsuario, value); }
        }

        public string Password
        {
            get { return password; }
            set { SetValue(ref password, value); }

        }

        public bool IsRunning
        {
            get { return isRunning; }
            set { SetValue(ref isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetValue(ref isEnabled, value); }
        }

        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsEnabled = true;
            this.NombreUsuario = "luis@interdev.mx";
            this.Password = "Luis123+";
            
            //this.NombreUsuario = "prueba_app@fussionweb.com";
            //this.Password = "Prueba123+";
            this.apiService = new ApiService();
            this.persistenceService = new PersistenceService();
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.NombreUsuario))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Tienes que ingresar el Nombre de Usuario", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Tienes que ingresar una contraseña", "Aceptar");
                return;
            }

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

            var usuario = this.NombreUsuario;
            var pass = this.Password;

            //var jsonData = "{\"model\": {\"UserName\": \"" + usuario + "\",\"Password\": \"" + pass + "\"}}";
            //string json = @"{'model':{'UserName': 'luis@interdev.mx','Password': 'Luis123+'}}";
            //string json= "{\r\n    \"model\": {\r\n        \"UserName\": \"prueba_app@fussionweb.com\",\r\n        \"Password\": \"Prueba123+\"\r\n    }\r\n}";
            string json= "{\r\n    \"model\": {\r\n        \"UserName\": \""+usuario+"\",\r\n        \"Password\": \""+pass+"\"\r\n    }\r\n}";

            var response = await apiService.Login("https://apps.fussionweb.com/", "sietest/Account", "/loginmovile", json);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                this.Password = string.Empty;
                return;
            }


            this.persistenceService.SaveLogin(response.Message,DateTime.Now);
            string[] infoUser = response.Message.Split(' ');
            if (infoUser[0] == "Administrador")
            {
                MainViewModel.GetInstace().Choferes = new ChoferesViewModel();
                Application.Current.MainPage = new MasterPage();
                this.NombreUsuario = string.Empty;
                this.Password = string.Empty;
                this.IsRunning = false;
                this.IsEnabled = true;
            }
            else
            {
                MainViewModel.GetInstace().Pedidos = new PedidosViewModel();
                //await Application.Current.MainPage.Navigation.PushAsync(new PedidosPage());
                //Application.Current.MainPage = new NavigationPage(new PedidosPage());
                Application.Current.MainPage = new MasterPageChofer();
                //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
                //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                this.NombreUsuario = string.Empty;
                this.Password = string.Empty;
                this.IsRunning = false;
                this.IsEnabled = true;
            }
            
        }
        #endregion
    }
}
