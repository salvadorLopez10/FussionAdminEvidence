using FussionAdminEvidence.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FussionAdminEvidence
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new LoginPage());
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
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
