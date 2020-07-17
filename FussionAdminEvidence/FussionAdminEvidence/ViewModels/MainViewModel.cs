﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FussionAdminEvidence.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }

        public PedidosViewModel Pedidos { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstace()
        {
            if (instance==null)
            {
                return new MainViewModel();
            }

            return instance;    
        }
        #endregion
    }
}
