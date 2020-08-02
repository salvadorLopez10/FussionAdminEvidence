﻿using FussionAdminEvidence.Models;
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
            string json = @"{'model':{'UserName': 'luis@interdev.mx','Password': 'Luis123+'}}";

            JObject jsonData = JObject.Parse(json);

            var response = await apiService.Login("https://apps.fussionweb.com/", "sietest/Account", "/loginmovile", jsonData.ToString());

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                this.NombreUsuario = string.Empty;
                this.Password = string.Empty;
                return;
            }

            this.persistenceService.SaveLogin(this.NombreUsuario,DateTime.Now);
            MainViewModel.GetInstace().Pedidos = new PedidosViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PedidosPage());
            this.NombreUsuario = string.Empty;
            this.Password = string.Empty;
            this.IsRunning = false;
            this.IsEnabled = true;
        }
        #endregion
    }
}
