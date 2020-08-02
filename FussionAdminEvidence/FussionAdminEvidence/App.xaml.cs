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

        public static NavigationPage Navigator { get; internal set; }

        public static MasterPage Master { get; internal set; }

        public App()
        {
            InitializeComponent();

            persistenceService = new PersistenceService();
            double minutesFromTs = 20.0;
            var lista = persistenceService.GetValuesLogin("usuario", "horaLogin");
            if (lista.Count>0)
            {
                var ultimoLogin = (long)lista[1];
                var now = DateTime.Now.Ticks;

                //double minutesFromTs = calculaTiempoInactividad(now,ultimoLogin);
                

            }
            /*
            var ultimoLogin = (long)lista[1];
            var now = DateTime.Now.Ticks;

            //double minutesFromTs = calculaTiempoInactividad(now,ultimoLogin);
            double minutesFromTs = 20.0;
            */
            //Si pasan más de 30 minutos de inactividad, vuelve a pedir el login
            if (minutesFromTs > 30.0)
            {
                MainPage = new NavigationPage(new LoginPage());
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            }
            else
            {
                /*
                MainViewModel.GetInstace().Pedidos = new PedidosViewModel();
                MainPage = new NavigationPage(new PedidosPage());
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                */
                MainPage = new MasterPage();
            }

            
        }

        public double calculaTiempoInactividad(long loginActual, long ultimoLogin)
        {
            var tiempoTranscurridoUltimoLogin = loginActual - ultimoLogin;
            TimeSpan ts = TimeSpan.FromTicks(tiempoTranscurridoUltimoLogin);
            double minutesFromTs = ts.TotalMinutes;

            return minutesFromTs;
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
