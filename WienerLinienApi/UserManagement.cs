using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinienApi
{
    public class UserManagement
    {
        Entities dbEntities = new Entities();

        public bool Login(string username, string password)
        {
            Benutzer b = dbEntities.Benutzers.Where(i => i.Username == username).Single();
            if(b.Password == sha256(password))
            {
                return true;
            }
            return false;
        }

        public bool Signup(string username, string password, string firstname, string lastname)
        {
            Benutzer b = new Benutzer() { Username = username, Password = sha256(password), Vorname = firstname, Nachname = lastname };
            if(dbEntities.Benutzers.Any(i=> i.Username == username))
            {
                return false;
            }
            dbEntities.Benutzers.Add(b);
            dbEntities.SaveChanges();
            return true;
        }
        public bool UsernameAvailable(string username)
        {
            return !(dbEntities.Benutzers.Any(i => i.Username == username));
          
        }
        public void AddStationToUser(Benutzer benutzerToAdd, Haltestellen haltestelleToMarkAsFavourite)
        {
            BenutzerHaltestellen bh = new BenutzerHaltestellen() { Benutzer_ID = benutzerToAdd.Benutzer_ID, Haltestellen_ID = haltestelleToMarkAsFavourite.Haltestellen_ID };
            dbEntities.BenutzerHaltestellens.Add(bh);
            dbEntities.SaveChanges();
        }
        private string sha256(string password)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
