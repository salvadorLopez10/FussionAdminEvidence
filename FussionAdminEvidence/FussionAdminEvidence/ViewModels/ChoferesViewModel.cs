using FussionAdminEvidence.Models;
using FussionAdminEvidence.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class ChoferesViewModel: BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<ChoferItemViewModel> choferes;
        private bool isRefreshingChoferes;
        private string filter;
        private List<Chofer> choferesList;
        #endregion

        #region Properties
        public ObservableCollection<ChoferItemViewModel> Choferes
        {
            get { return this.choferes; }
            set { SetValue(ref this.choferes, value); }

        }

        public bool IsRefreshingChoferes
        {
            get { return this.isRefreshingChoferes; }
            set { SetValue(ref this.isRefreshingChoferes, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        #endregion

        public ChoferesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadChoferes();
        }

        private IEnumerable<ChoferItemViewModel> ToChoferItemViewModel()
        {
            return this.choferesList.Select(p => new ChoferItemViewModel
            {
               Nombre=p.Nombre,
               FormattedId=p.FormattedId,
               Identifier=p.Identifier,
               IsValid=p.IsValid,
               LastValidationErrorMessages=p.LastValidationErrorMessages,
               LastValidationErrors=p.LastValidationErrors
            });
        }

        private async void LoadChoferes()
        {
            this.IsRefreshingChoferes = true;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshingChoferes = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");

                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            
            var response = await apiService.GetChoferes("https://apps.fussionweb.com/", "/sietest/Mobile", "/Choferes");
            if (!response.IsSuccess)
            {
                this.IsRefreshingChoferes = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            this.IsRefreshingChoferes = false;

            this.choferesList = (List<Chofer>)response.Result;
            this.Choferes = new ObservableCollection<ChoferItemViewModel>(
                this.ToChoferItemViewModel());

        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Choferes = new ObservableCollection<ChoferItemViewModel>(
                    this.ToChoferItemViewModel());
            }
            else
            {

                this.Choferes = new ObservableCollection<ChoferItemViewModel>(
                    this.ToChoferItemViewModel().Where(
                        c=> c.Nombre.ToLower().Contains(this.Filter.ToLower())
                    ));
            }
        }

    }
}
