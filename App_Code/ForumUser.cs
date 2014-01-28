using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for User
/// </summary>
public class ForumUser
{
	public ForumUser()
	{
		
	}

    public ForumUser(int userID, string userName, string password, string firstname, string lastname, string email, string phone, string street1, string street2, string city, string state, string zip, int permissions)
    {
        this.UserID = userID;
        this.UserName = userName;
        this.Password = password;
        this.FirstName = firstname;
        this.LastName = lastname;
        this.Email = email;
        this.Phone = phone;
        this.Street1 = street1;
        this.Street2 = street2;
        this.City = city;
        this.State = state;
        this.Zip = zip;
        this.Permissions = permissions;
    }
   

    public int UserID { get; private set; }

    public string UserName { get ; set; } //validation created

    public string Password { get; set; }

    public string FirstName { get; set; } //validation created
    public string LastName { get; set; } //validation created
    public string Email { get; set; } //validation created
    public string Phone { get; set; }
    public string Street1	{ get; set; }
    public string Street2 { get; set; }
    public string City { get; set; }
    public string State { get; set; } //validation created
    public string Zip { get; set; }
    public int Permissions { get; private set; }

    /// <summary>
    /// Returns false if user name is taken.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool ValidUserName(string name)
    {
        bool valid = true;
        if (ForumUserDB.UserExists(name))
        {
            valid = false;
        }

        return valid;
    }
    /// <summary>
    /// Returns false if email contains whitespace, if there is not a single @ symbol at least
    /// two characters before the first period, if the @ symbol is the first character, or a period
    /// is the last character. Also returns false for any email address currently in database.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool ValidEmail(string email)
    {
        bool valid = false;
        if (!email.Contains(' '))
        {
            if (email.Contains('@') && email.Contains('.') && email.IndexOf('@') == email.LastIndexOf('@')
                && email.IndexOf('@') > 0 && email.LastIndexOf('.') < email.Length - 1)
            {
                if (email.IndexOf('@') < email.IndexOf('.') && email.IndexOf('@') < email.IndexOf('.') - 1)
                {
                    valid = true;
                }
            }
        }

        if (valid)
        {
            valid = !ForumUserDB.UserEmailTaken(email);
        }

        return valid;
    }

    /// <summary>
    /// Return false if states number of non whitespace characters is not 2
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static bool ValidState(string state)
    {
        return state.Trim().Length == 2;
    }

    /// <summary>
    /// Return false if no white space characters.
    /// </summary>
    /// <param name="first"></param>
    /// <param name="last"></param>
    /// <returns></returns>
    public static bool ValidName(string first, string last)
    {
        return first.Trim().Length > 0 && last.Trim().Length > 0;
    }

    /// <summary>
    /// Returns false if street2 is not empty but street1 is.
    /// </summary>
    /// <param name="street1"></param>
    /// <param name="street2"></param>
    /// <returns></returns>
    public static bool ValidStreet(string street1, string street2)
    {
        return street1.Trim().Length > 0 || street2.Trim().Length == 0;
    }
}