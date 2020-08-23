using FussionAdminEvidence.Models;
using FussionAdminEvidence.Services;
using FussionAdminEvidence.Views;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FussionAdminEvidence.ViewModels
{
    public class PedidoViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private Pedido_ pedido;
        private ImageSource imageSource;
        private ImageSource imageSourceTwo;
        private ImageSource imageSourceThree;
        private ImageSource imageSourceFour;
        private ImageSource imageSourceSignature;
        private bool isEnabled;
        private bool isEnabledSave;
        private bool isRunning;
        private GeolocatorService geolocation;
        #endregion

        #region Properties
        public Pedido_ Pedido
        {
            get { return this.pedido; }
            set { SetValue(ref this.pedido, value); }
        }

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }

        public ImageSource ImageSourceTwo
        {
            get { return this.imageSourceTwo; }
            set { SetValue(ref this.imageSourceTwo, value); }
        }

        public ImageSource ImageSourceThree
        {
            get { return this.imageSourceThree; }
            set { SetValue(ref this.imageSourceThree, value); }
        }

        public ImageSource ImageSourceFour
        {
            get { return this.imageSourceFour; }
            set { SetValue(ref this.imageSourceFour, value); }
        }

        public ImageSource ImageSourceSignature
        {
            get { return this.imageSourceSignature; }
            set { SetValue(ref this.imageSourceSignature, value); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetValue(ref isEnabled, value); }
        }

        public bool IsEnabledSave
        {
            get { return isEnabledSave; }
            set { SetValue(ref isEnabledSave, value); }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set { SetValue(ref isRunning, value); }
        }
        #endregion

        #region Constructors

        public PedidoViewModel(Pedido_ pedido)
        {
            this.apiService = new ApiService();
            this.pedido = pedido;
            this.ImageSource = "ic_search_image.png";
            this.ImageSourceTwo = "ic_search_image.png";
            this.ImageSourceThree = "ic_search_image.png";
            this.ImageSourceFour = "ic_search_image.png";
            this.ImageSourceSignature = "ic_search_image.png";
            this.IsEnabled = false;
            this.IsEnabledSave = false;
            this.geolocation = new GeolocatorService();

        }
        #endregion

        #region Commands
        public ICommand TakePictureEvidenceOneCommand
        {
            get
            {
                return new RelayCommand(TakePictureEvidenceOne);
            }
        }

        private async void TakePictureEvidenceOne()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( Cámara no disponible.", "Aceptar");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Fussion_evidencias",
                Name = "evidencia1.jpg",
                PhotoSize = PhotoSize.Small,
            });

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                this.IsEnabled = true;
            }
        }

        public ICommand TakePictureEvidenceTwoCommand
        {
            get
            {
                return new RelayCommand(TakePictureEvidenceTwo);
            }
        }

        private async void TakePictureEvidenceTwo()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( Cámara no disponible.", "Aceptar");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Fussion_evidencias",
                Name = "evidencia2.jpg",
                PhotoSize = PhotoSize.Small,
            });

            if (file != null)
            {
                ImageSourceTwo = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                this.IsEnabled = true;
            }
        }

        public ICommand TakePictureEvidenceThreeCommand
        {
            get
            {
                return new RelayCommand(TakePictureEvidenceThree);
            }
        }

        private async void TakePictureEvidenceThree()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( Cámara no disponible.", "Aceptar");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Fussion_evidencias",
                Name = "evidencia3.jpg",
                PhotoSize = PhotoSize.Small,
            });

            if (file != null)
            {
                ImageSourceThree = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                this.IsEnabled = true;
            }
        }

        public ICommand TakePictureEvidenceFourCommand
        {
            get
            {
                return new RelayCommand(TakePictureEvidenceFour);
            }
        }

        private async void TakePictureEvidenceFour()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( Cámara no disponible.", "Aceptar");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Fussion_evidencias",
                Name = "evidencia4.jpg",
                PhotoSize = PhotoSize.Small,
            });

            if (file != null)
            {
                ImageSourceFour = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
                this.IsEnabled = true;
            }
        }

        public ICommand GoToSignatureCommand
        {
            get
            {
                return new RelayCommand(GoToSignature);
            }
        }

        private async void GoToSignature()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new SignaturePage(this));
            await App.Navigator.PushAsync(new SignaturePage(this));
        }

        public ICommand SavePedidoCommand
        {
            get
            {
                return new RelayCommand(SavePedido);
            }
        }

        private async void SavePedido()
        {
            //Obteniendo ubicación actual
            await this.geolocation.GetLocationAsync();
            var ubicacion = "";
            if (geolocation.Latitude != 0 && geolocation.Longitude != 0)
            {
                Position position = new Position(
                    geolocation.Latitude,
                    geolocation.Longitude);
                ubicacion = geolocation.Latitude.ToString() + "," + geolocation.Longitude.ToString();
            }

            //var byteArray = this.ImageSourceToByteArray(ImageSource);
            //var stringBase64 = Convert.ToBase64String(byteArray);

            //Armando body de petición
            JArray arrPrincipal = new JArray();
            JObject pedido = new JObject();
            pedido["Pedido"] = this.Pedido.Identifier;
            JArray evidencias = new JArray();
            //Armar objetos de evidencias
            JObject objEvidencia = new JObject();
            objEvidencia["Imagen"] = "Evidencia 1";
            objEvidencia["Tipo"] = 2;
            evidencias.Add(objEvidencia);

            JObject objEvidencia2 = new JObject();
            objEvidencia2["Imagen"] = "Evidencia 2";
            objEvidencia2["Tipo"] = 2;
            evidencias.Add(objEvidencia2);

            JObject objEvidencia3 = new JObject();
            objEvidencia3["Imagen"] = "Evidencia 3";
            objEvidencia3["Tipo"] = 2;
            evidencias.Add(objEvidencia3);

            JObject objFirma = new JObject();
            objFirma["Imagen"] = "Evidencia 1";
            objFirma["Tipo"] = 1;
            evidencias.Add(objFirma);

            pedido["Evidencias"] = evidencias;
            pedido["Status"] = 2;//Entregado
            pedido["Ubicacion"] = ubicacion;

            arrPrincipal.Add(pedido);

            this.IsRunning = true;
            this.IsEnabled = false;
            this.IsEnabledSave = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                this.IsEnabledSave = true;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                return;
            }

            var response = await apiService.ActualizarPedido("https://apps.fussionweb.com/", "sietest/Mobile", "/ActualizarPedido", arrPrincipal);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                this.IsEnabledSave = true;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;
            this.IsEnabledSave = true;

            await Application.Current.MainPage.DisplayAlert("Éxito", response.Message, "Aceptar");

            MainViewModel.GetInstace().Pedidos = new PedidosViewModel();
            await App.Navigator.PopAsync();


        }


        #endregion

        #region Methods
        public void suscribeFirma(Stream st)
        {
            ImageSourceSignature = ImageSource.FromStream(() => st);
            IsEnabledSave = true;
        }

        private byte[] ImageSourceToByteArray(ImageSource source)
        {
            StreamImageSource streamImageSource = (StreamImageSource)source;
            System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;

            byte[] b;
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                b = ms.ToArray();
            }

            return b;
        }

        #endregion
    }
}
