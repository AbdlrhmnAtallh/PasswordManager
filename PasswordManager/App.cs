using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    internal class App
    {
        private static Dictionary<string, string> _PasswordEntries = new();
        public static void Run(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Please Select an Option");
                Console.WriteLine("1. List All Passwords");
                Console.WriteLine("2. Add/Change Password");
                Console.WriteLine("3. Get Password");
                Console.WriteLine("4. Delete Password");
                Console.WriteLine("--- --- ---");
                Console.WriteLine("Exit to Exit");
                int SelectedOption = Convert.ToInt32(Console.ReadLine());

                switch (SelectedOption)
                {
                    case 1:
                        ListAllPasswords();
                        break;
                    case 2:
                        AddOrChangePassword();
                        break;
                    case 3: GetPassword();
                        break;
                    case 4: DeletePassword(); 
                        break;
                    default:
                        Console.WriteLine("Unvalid option");
                        break;
                }
            }

        }

        private static void ListAllPasswords()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Passwords.txt");
            StreamReader sr = new(filePath);
            var passwords =sr.ReadToEnd();
            Console.WriteLine(passwords);
            Console.WriteLine("---------");
           StringBuilder sb= EncryptionUtility.Decrypt(passwords);
            Console.WriteLine(sb);

            //if (_PasswordEntries.Count > 0)
            //{
            //    int i = 0;
            //    foreach (var entry in _PasswordEntries)
            //    {
            //        i++;
            //        Console.WriteLine(i+"- "+ entry.Key + " = " + entry.Value);
            //    }
            //    return;
            //}
            //Console.WriteLine("No Passwords");

        }
        private static void AddOrChangePassword()
        {
            Console.WriteLine("Enter App/Site Name");
            var SiteName = Console.ReadLine();
            if(_PasswordEntries.ContainsKey(SiteName))
            {
                string CurrentPassword = _PasswordEntries[SiteName];
                Console.WriteLine("This is the current " + CurrentPassword);
                Console.WriteLine("Press 1 to Change / Exit to Exit ");
                string ChangeOrExit = Convert.ToString(Console.ReadLine());
                if (ChangeOrExit == "Exit")
                {
                    return;
                }
                else
                {
                    int counter = 2;
                    while (counter != 0)
                    {
                        Console.WriteLine("Enter new Password");
                        string Password = Console.ReadLine();
                        Console.WriteLine("Confirm Password");
                        string ConfirmPassword = Console.ReadLine();
                        if (Password != ConfirmPassword)
                        {
                            Console.WriteLine("Password Not Matched, Please try again");
                        }
                        else
                        {
                            _PasswordEntries.Add(SiteName, Password);
                            SavePasswords();
                            var ItemAdded = _PasswordEntries.SingleOrDefault(e => e.Key == SiteName);

                            Console.WriteLine(ItemAdded.Key + " = " + ItemAdded.Value + " Added ");
                           
                            return;
                        }
                        counter--;
                    }
                    Console.WriteLine("Password not saved you've been returned to the home page");
                }
            }
            else
            {
                int counter = 2;
                while (counter != 0)
                {
                    Console.WriteLine("Add Password to " + SiteName);
                    Console.Write(SiteName + " = ");
                    string Password = Console.ReadLine();
                    Console.WriteLine("Confirm Password");
                    Console.Write(SiteName + " = ");
                    string ConfirmPassword = Console.ReadLine();
                    if (Password != ConfirmPassword)
                    {
                        Console.WriteLine("Password Not Matched, Please try again");
                    }
                    else
                    {
                        _PasswordEntries.Add(SiteName, Password);
                        
                        SavePasswords();
                        var ItemAdded = _PasswordEntries.SingleOrDefault(e => e.Key == SiteName);

                        Console.WriteLine( ItemAdded.Key+" = "+ ItemAdded.Value + " Added ");
                        return;
                    }
                    counter--;
                }
                Console.WriteLine("Password not saved you've been returned to the home page");
            }

        }
        private static void GetPassword()
        {
            Console.WriteLine("Enter App Name");
            string AppName = Console.ReadLine();
            if (_PasswordEntries.ContainsKey(AppName))
            {
                string Password = _PasswordEntries.FirstOrDefault(e => e.Key == AppName).Value;
                Console.WriteLine(AppName + " = " + Password);
                return;
            }
            Console.WriteLine("No App match this name, Please try another name");
            int x = 3;
            x--;
            if (x > 0) 
            {
                GetPassword(); 
            }
             return;
        }
        private static void DeletePassword()
        {
            Console.WriteLine("Enter App Name");
            string AppName = Console.ReadLine();// need to decrypt and search abt app name
            if (_PasswordEntries.ContainsKey(AppName))
            {
                 _PasswordEntries.Remove(AppName);
                Console.WriteLine(AppName + " has been deleted");
                SavePasswords();
                return;
            }
            Console.WriteLine("No App match this name");
        }
        private static void SavePasswords()
        {
            var sb = new StringBuilder();
            foreach (var entry in _PasswordEntries)
                sb.Append (EncryptionUtility.Encrypt(entry.Key + " = " + entry.Value));
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Passwords.txt");
            //File.AppendAllText(filePath, sb.ToString());
            File.WriteAllLines(filePath,sb.ToString())
        }

    }
}
