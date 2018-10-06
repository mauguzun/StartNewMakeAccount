using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartNewMakeAccount.Models.Email
{
    public class ReadEmailFromList : DataProvider
    {

        public string FileName { get; set; } = "gmailAcc.txt";

        public override string GetString(string prettyName)
        {

            var emails = File.ReadAllLines(FileName).ToList();
            string email = emails.FirstOrDefault();
            emails.Remove(email);
            File.WriteAllLines(FileName, emails);
            return email; 
        }

    }
}
