using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string NombreUsuario { get; set; }

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
            this.isEnabled = false;

            if (this.NombreUsuario != "Chava" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Nombre de usuario o Contraseña incorrectos", "Aceptar");
                this.Password = string.Empty;
                return;
            }

            await Application.Current.MainPage.DisplayAlert("MUY BIEN", "USUARIO Y PASS BIEN", "Aceptar");

        }
        #endregion
    }
}
