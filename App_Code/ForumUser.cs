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

    public string UserName { get ; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email	{ get; set; }
    public string Phone { get; set; }
    public string Street1	{ get; set; }
    public string Street2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public int Permissions { get; private set; }

}