using FussionAdminEvidence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FussionAdminEvidence.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }

        public PedidosViewModel Pedidos { get; set; }

        public PedidoViewModel Pedido { get; set; }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            //this.Pedidos = new PedidosViewModel();
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
    }
}
