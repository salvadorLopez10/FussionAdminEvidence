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

        public string imgEv1Base64;
        public string imgEv2Base64;
        public string imgEv3Base64;
        public string imgEv4Base64;
        public string imgFirmaBase64;

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

            //Se establecen vacias las cadenas que guardarán base64 de las imagenes de evidencia
            this.imgEv1Base64 = "";
            this.imgEv2Base64 = "";
            this.imgEv3Base64 = "";
            this.imgEv4Base64 = "";
            this.imgFirmaBase64 = "";

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

            var streamEvidencia = file.GetStream();
            var bytes= new byte[streamEvidencia.Length];
            await streamEvidencia.ReadAsync(bytes, 0, (int)streamEvidencia.Length);
            this.imgEv1Base64 = System.Convert.ToBase64String(bytes);

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

            var streamEvidencia = file.GetStream();
            var bytes = new byte[streamEvidencia.Length];
            await streamEvidencia.ReadAsync(bytes, 0, (int)streamEvidencia.Length);
            this.imgEv2Base64 = System.Convert.ToBase64String(bytes);

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

            var streamEvidencia = file.GetStream();
            var bytes = new byte[streamEvidencia.Length];
            await streamEvidencia.ReadAsync(bytes, 0, (int)streamEvidencia.Length);
            this.imgEv3Base64 = System.Convert.ToBase64String(bytes);

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

            var streamEvidencia = file.GetStream();
            var bytes = new byte[streamEvidencia.Length];
            await streamEvidencia.ReadAsync(bytes, 0, (int)streamEvidencia.Length);
            this.imgEv4Base64 = System.Convert.ToBase64String(bytes);

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
            this.IsRunning = true;
            this.IsEnabled = false;
            this.IsEnabledSave = false;
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
            if (this.imgEv1Base64!=""){
                JObject objEvidencia = new JObject();
                objEvidencia["Imagen"] = this.imgEv1Base64;
                objEvidencia["Tipo"] = 2;
                evidencias.Add(objEvidencia);
            }

            if (this.imgEv2Base64!="")
            {
                JObject objEvidencia2 = new JObject();
                objEvidencia2["Imagen"] =this.imgEv2Base64;
                objEvidencia2["Tipo"] = 2;
                evidencias.Add(objEvidencia2);
            }

            if (this.imgEv3Base64!="")
            {
                JObject objEvidencia3 = new JObject();
                objEvidencia3["Imagen"] = this.imgEv3Base64;
                objEvidencia3["Tipo"] = 2;
                evidencias.Add(objEvidencia3);
            }

            if (this.imgEv4Base64!="")
            {
                JObject objEvidencia4 = new JObject();
                objEvidencia4["Imagen"] = this.imgEv4Base64;
                objEvidencia4["Tipo"] = 2;
                evidencias.Add(objEvidencia4);
            }

            //Objeto firma
            if (this.imgFirmaBase64!="")
            {
                JObject objFirma = new JObject();
                objFirma["Imagen"] =this.imgFirmaBase64;
                objFirma["Tipo"] = 1;
                evidencias.Add(objFirma);
            }
            

            pedido["Evidencias"] = evidencias;
            pedido["Status"] = 2;//Entregado
            pedido["Ubicacion"] = ubicacion;

            arrPrincipal.Add(pedido);

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
        public async void suscribeFirma(Stream st)
        {
            ImageSourceSignature = ImageSource.FromStream(() => st);

            var bytes = new byte[st.Length];
            await st.ReadAsync(bytes, 0, (int)st.Length);
            this.imgFirmaBase64 = System.Convert.ToBase64String(bytes);

            IsEnabledSave = true;
        }
        #endregion
    }
}
