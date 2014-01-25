using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
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
    /// <summary>
    /// Takes a user ID number, returns a ForumUser object if user ID found. Otherwise, returns null.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    static public ForumUser GetUser(int userId)
    {
        ForumUser myUser = null;

        String commandString = String.Format("Select [UserID],[UserName], [Password], [FirstName], [LastName], [email], [Phone], [Street1], [Street2], [City], [State], [Zip], [Permissions] From dbo.[User] Where [UserID] = {0}", userId.ToString());
        SqlCommand command = new SqlCommand(commandString, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ForumDatabaseConnectionString"].ConnectionString));
        
        command.CommandType = CommandType.Text;
        command.Connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        reader.Read();
        IDataRecord dr = (IDataRecord)reader;
        try
        {
            {
                Object[] dataValues = new Object[dr.FieldCount];
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    dataValues[i] = dr[i];
                }

                myUser = new ForumUser(Convert.ToInt32(dataValues[0]), dataValues[1].ToString(), dataValues[2].ToString(), dataValues[3].ToString(), dataValues[4].ToString(), dataValues[5].ToString(), dataValues[6].ToString(), dataValues[7].ToString(), dataValues[8].ToString(), dataValues[9].ToString(), dataValues[10].ToString(), dataValues[11].ToString(), Convert.ToInt32(dataValues[12]));
            }
        }
        catch (InvalidOperationException)
        {
            //nothing to do
        }

        reader.Close();
        command.Connection.Close();

        return myUser;     
    }


    static public bool UserExists(string username)
    {
        return GetUser(username) != null;
    }
    /// <summary>
    /// Takes a user name as string, returns a ForumUser object if username found. Otherwise, returns null.
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    static public ForumUser GetUser(string userName)
    {

        ForumUser myUser = null;

        String commandString = String.Format("Select [UserID],[UserName], [Password], [FirstName], [LastName], [email], [Phone], [Street1], [Street2], [City], [State], [Zip], [Permissions] From dbo.[User] Where [UserName] Like '{0}'", userName.ToString());
        SqlCommand command = new SqlCommand(commandString, new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ForumDatabaseConnectionString"].ConnectionString));

        command.CommandType = CommandType.Text;
        command.Connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        reader.Read();
        IDataRecord dr = (IDataRecord)reader;
        try
        {
            {
                Object[] dataValues = new Object[dr.FieldCount];
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    dataValues[i] = dr[i];
                }
                myUser = new ForumUser(Convert.ToInt32(dataValues[0]), dataValues[1].ToString(), dataValues[2].ToString(), dataValues[3].ToString(), dataValues[4].ToString(), dataValues[5].ToString(), dataValues[6].ToString(), dataValues[7].ToString(), dataValues[8].ToString(), dataValues[9].ToString(), dataValues[10].ToString(), dataValues[11].ToString(), Convert.ToInt32(dataValues[12]));
            }
        }

        catch (InvalidOperationException ioEx)
        {
            //nothing to do
        }

        reader.Close();
        command.Connection.Close();


        return myUser;
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