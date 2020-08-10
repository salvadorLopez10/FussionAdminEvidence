using FussionAdminEvidence.ViewModels;
using System;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FussionAdminEvidence.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignaturePage : ContentPage
    {
        PedidoViewModel pvm;
        public SignaturePage(PedidoViewModel pvm)
        {
            InitializeComponent();
            this.pvm = pvm;
        }

        private async void ConfirmSignature_Clicked(object sender, EventArgs e)
        {
            Stream firma = await PadView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Jpeg);
            pvm.suscribeFirma(firma);
            //await Application.Current.MainPage.Navigation.PopAsync();
            await App.Navigator.PopAsync();
            
        }
    }
}