using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Essentials;
namespace FussionAdminEvidence.Services
{
    public class PersistenceService
    {


        #region Methods
        public void SaveLogin(string usuario, DateTime horaFecha)
        {
            Preferences.Set("usuario", usuario);
            Preferences.Set("horaLogin", horaFecha.Ticks);
        }

        public List<Object> GetValuesLogin(string keyUsuario,string keyHoraLogin)
        {
            var lista = new List<Object>();
            if (Preferences.ContainsKey(keyUsuario))
            {
                var usuario = Preferences.Get(keyUsuario, "luis@interdev.mx");
                lista.Add(usuario);
            }

            if (Preferences.ContainsKey(keyHoraLogin))
            {
                var horaFecha = Preferences.Get(keyHoraLogin, DateTime.MinValue.Ticks);
                lista.Add(horaFecha);
            }

            return lista;
        }

        #endregion
    }
}
