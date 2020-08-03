using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Validation
{
    static class AddUserValidation
    {

        //Static variables made to store usefull data after validation.
        public static string dateOfBirth = "";
        public static string expirationDate = "";
        public static string registrationNumber = "";
        public static string dateOfIssue = "";

        public static bool Validate(User e)
        {
            bool cancel = false;
            //JMBG validation starts here.
            string day = "";
            string month = "";
            string year = "";

            while (true)
            {
                DateTime correct = new DateTime();

                if (e.Username.Length == 13 && e.Username.All(Char.IsDigit) && e.Username != null && e.Username != "")
                {
                    //creating date of birth of user is realised below.
                    day = e.Username[0].ToString() + e.Username[1].ToString();
                    month = e.Username[2].ToString() + e.Username[3].ToString();
                    year = e.Username[4].ToString() + e.Username[5].ToString() + e.Username[6].ToString();

                    if (int.Parse(year) <= 99)
                    {
                        year = "2" + year;
                    }
                    else
                    {
                        year = "1" + year;
                    }

                    dateOfBirth = year + "-" + month + "-" + day;
                    dateOfIssue = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                    //validation if date of birth is in correct format and value.
                    if (!DateTime.TryParse(dateOfBirth, out correct))
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("JMBG is not correct, due to incorrect date of birth, please try again.", "Notification");
                        cancel = true;
                        break;
                    }
                    if (cancel) break;
                }
                else
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("JMBG input is not correct, try again.", "Notification");
                    cancel = true;
                    break;
                }
                if (cancel) { break; }

                //Validations consernig date of birth are realised below.
                if (int.Parse(year) > int.Parse(DateTime.Now.ToString("yyyy")))
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Date of birth suggests that you are born in the future, please try again.", "Notification");
                    cancel = true;
                    break;
                }
                else if (int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(year) > 100)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Your date of birth suggests that you lived longer than 100 years, please try again.", "Notification");
                    cancel = true;
                    break;
                }
                else if (int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(year) < 18)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Your date of birth suggests are under aged (less than 18 y/o), please try again.", "Notification");
                    cancel = true;
                    break;
                }
                else if(e.Password != "Gost")
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect password, value must be Gost", "Notification");
                    cancel = true;
                    break;
                }
                else
                {
                    break;
                }
            }

            if (!cancel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
