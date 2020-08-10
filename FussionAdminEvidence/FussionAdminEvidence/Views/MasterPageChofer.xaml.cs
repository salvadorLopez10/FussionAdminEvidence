using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FussionAdminEvidence.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageChofer : MasterDetailPage
    {
        public MasterPageChofer()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.MasterChofer = this;
            App.Navigator = Navigator;
        }
    }
}