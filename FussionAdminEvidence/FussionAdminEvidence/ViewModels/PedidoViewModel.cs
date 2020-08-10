using FussionAdminEvidence.Models;
using FussionAdminEvidence.Views;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Net.Http.Headers;
using System.Windows.Input;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class PedidoViewModel : BaseViewModel
    {
        #region Attributes
        private Pedido_ pedido;
        private ImageSource imageSource;
        private ImageSource imageSourceTwo;
        private ImageSource imageSourceThree;
        private ImageSource imageSourceFour;
        private ImageSource imageSourceSignature;
        private bool isEnabled;
        private bool isEnabledSave;
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
        #endregion

        #region Constructors

        public PedidoViewModel(Pedido_ pedido)
        {
            this.pedido = pedido;
            this.ImageSource = "ic_search_image.png";
            this.ImageSourceTwo = "ic_search_image.png";
            this.ImageSourceThree = "ic_search_image.png";
            this.ImageSourceFour = "ic_search_image.png";
            this.ImageSourceSignature = "ic_search_image.png";
            this.IsEnabled = false;
            this.IsEnabledSave = false;

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


        #endregion

        #region Methods
        public void suscribeFirma(Stream st)
        {
            ImageSourceSignature = ImageSource.FromStream(() => st);
            IsEnabledSave = true;
        }

        #endregion
    }
}
