using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WienerLinienApi
{
    public class UserManagement
    {
        Entities1 dbEntities = new Entities1();

        public Benutzer Login(string username, string password)
        {
            Benutzer b = dbEntities.Benutzers.Where(i => i.Username == username).Single();
            dbEntities.Haltestellens.ToList()[0].Name.Replace("\"", "");
            dbEntities.SaveChanges();
            if (b.Password == sha256(password))
            {
                return b;
            }
            return null;
        }

        public bool Signup(string username, string password, string firstname, string lastname)
        {
            Benutzer b = new Benutzer() { Username = username, Password = sha256(password), Vorname = firstname, Nachname = lastname };
            if (dbEntities.Benutzers.Any(i => i.Username == username))
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
        public void AddStationToUser(Benutzer benutzerToAdd, int HaltestellenId, string linie, string richtung)
        {
            BenutzerHaltestellen bh = new BenutzerHaltestellen() { Benutzer_ID = benutzerToAdd.Benutzer_ID, Haltestellen_ID = HaltestellenId, Linie = linie, Richtung = richtung };
            dbEntities.BenutzerHaltestellens.Add(bh);
            dbEntities.SaveChanges();
        }
        public List<BenutzerHaltestellen> GetFavouriteStopsFromUser(Benutzer b)
        {
            return dbEntities.BenutzerHaltestellens.Where(i => i.Benutzer_ID == b.Benutzer_ID).ToList();
        }
        public bool ChangeUsername(Benutzer b, string newUsername)
        {
            Benutzer ben = dbEntities.Benutzers.First(i => i.Username == b.Username);
           
            if (UsernameAvailable(newUsername)) {
                ben.Username = newUsername;
            } else {
                return false;
            }
            dbEntities.SaveChanges();
            if (UsernameAvailable(newUsername) == false)
            {
                return true;
            }
            return false;
        }
        public bool ChangePassword(Benutzer b, string newPassword)
        {
            Benutzer ben = dbEntities.Benutzers.First(i => i.Username == b.Username);
            var pw = sha256(newPassword);
            if (pw != b.Password)
            {
                ben.Password = pw;
            }
            else
            {
                return false;
            }
            dbEntities.SaveChanges();
            return (Login(b.Username, newPassword) != null);
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
