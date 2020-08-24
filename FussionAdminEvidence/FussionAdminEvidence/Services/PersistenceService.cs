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
            
            string[] palabras = usuario.Split(' ');
            Preferences.Set("usuario", palabras[0]);
            Preferences.Set("guid", palabras[1]);
            Preferences.Set("horaLogin", horaFecha.Ticks);
        }

        public string GetValuePreference(string key)
        {
            var usuario = "";
            if (Preferences.ContainsKey(key))
            {
                usuario = Preferences.Get(key, "0");
            }
            return usuario;
        }

        public List<Object> GetValuesLogin(string keyUsuario,string keyHoraLogin,string guid)
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
            if (Preferences.ContainsKey(guid))
            {
                var idUsuario = Preferences.Get(guid, "0");
                lista.Add(idUsuario);
            }

            return lista;
        }

        public void RestoreKeysPersistance(string key1, string key2,string key3)
        {
            if (Preferences.ContainsKey(key1))
            {
                Preferences.Remove(key1);
            }
            if (Preferences.ContainsKey(key2))
            {
                Preferences.Remove(key2);
            }
            if (Preferences.ContainsKey(key3))
            {
                Preferences.Remove(key3);
            }
        }

        #endregion
    }
}
