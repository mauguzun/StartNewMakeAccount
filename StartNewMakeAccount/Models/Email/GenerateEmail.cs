using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartNewMakeAccount.Models.Email
{
    public class GenerateEmail : DataProvider
    {
        public override string GetString(string prettyName)
        {
            Random random = new Random();
            string[] emails = File.ReadAllLines("gmail.txt");
            return $"{emails[random.Next(0, emails.Length)]}+{prettyName}{random.Next(0, 10).ToString()}@gmail.com";
        }
    }
}
