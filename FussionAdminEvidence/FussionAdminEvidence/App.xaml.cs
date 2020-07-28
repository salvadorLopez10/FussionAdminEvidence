using FussionAdminEvidence.Services;
using FussionAdminEvidence.ViewModels;
using FussionAdminEvidence.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FussionAdminEvidence
{
    public partial class App : Application
    {
        private PersistenceService persistenceService;
        public App()
        {
            InitializeComponent();

            persistenceService = new PersistenceService();
            var lista = persistenceService.GetValuesLogin("usuario", "horaLogin");
            var ultimoLogin = (long)lista[1];
            var now = DateTime.Now.Ticks;
            var tiempoTranscurridoUltimoLogin = now - ultimoLogin;
            TimeSpan ts = TimeSpan.FromTicks(tiempoTranscurridoUltimoLogin);
            double minutesFromTs = ts.TotalMinutes;

            //Si pasan más de 30 minutos de inactividad, vuelve a pedir el login
            if (minutesFromTs > 30.0)
            {
                MainPage = new NavigationPage(new LoginPage());
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            }
            else
            {
                MainViewModel.GetInstace().Pedidos = new PedidosViewModel();
                MainPage = new NavigationPage(new PedidosPage());
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            }

            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
