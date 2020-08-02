using FussionAdminEvidence.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FussionAdminEvidence.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }

        public PedidosViewModel Pedidos { get; set; }

        public PedidoViewModel Pedido { get; set; }

        public ChoferesViewModel Choferes { get; set; }

        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            //this.Pedidos = new PedidosViewModel();
            //this.Choferes = new ChoferesViewModel();
            this.LoadMenu();
        }

        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstace()
        {
            if (instance == null)
            {
                return new MainViewModel();
            } 

            return instance;    
        }

        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();
            this.Menu.Add(new MenuItemViewModel
            {
                Icon="ruta_icon",
                PageName="RutasPage",
                Title="Rutas"
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "chofer_icon",
                PageName = "ChoferesPage",
                Title = "Choferes"
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_close",
                PageName = "LoginPage",
                Title = "Cerrar Sesión"
            });
        }
        #endregion
    }
}
