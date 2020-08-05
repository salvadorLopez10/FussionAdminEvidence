using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Essentials;
namespace FussionAdminEvidence.Services
{
    public class PersistenceService
    {
        #region Singleton
        private static PersistenceService ps;

        public static PersistenceService GetPersistenceService()
        {
            if (ps == null)
            {
                return new PersistenceService();
            }

            return ps;
        } 
        #endregion

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
                var usuario = Preferences.Get(keyUsuario, "Supervisor");
                lista.Add(usuario);
            }

            if (Preferences.ContainsKey(keyHoraLogin))
            {
                var horaFecha = Preferences.Get(keyHoraLogin, DateTime.MinValue.Ticks);
                lista.Add(horaFecha);
            }

            return lista;
        }

        public void RestoreKeysPersistance(string key1, string key2)
        {
            if (Preferences.ContainsKey(key1))
            {
                Preferences.Remove(key1);
            }
            if (Preferences.ContainsKey(key2))
            {
                Preferences.Remove(key2);
            }
        }

        #endregion
    }
}
