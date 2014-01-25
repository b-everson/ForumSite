using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ForumUserDB
/// </summary>
public static class ForumUserDB
{

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

    public static ForumUser CreateUser(string userName, string password, string firstname, string lastname, string email, string phone = null, string street1 = null, string street2 = null, string city = null, string state = null, string zip = null)
    {
        //int userID, string userName, string password, string firstname, string lastname, string email, string phone, string street1, string street2, string city, string state, string zip, int permissions)

        SqlConnection connection = ForumDB.GetConnection();
        
        SqlCommand insertCommand = new SqlCommand("INSERT INTO [User] ([UserName],[Password],[FirstName],[LastName], [Email],[Phone],[Street1],[Street2],[City], [State],[Zip], [Permissions]) VALUES (@userName,@password,@firstName,@lastName,@email,@phone,@street1,@street2,@city,@state,@zip, @permissions); SELECT SCOPE_IDENTITY()", connection);
        insertCommand.Parameters.AddWithValue("@userName", userName);
        insertCommand.Parameters.AddWithValue("@password", password);
        insertCommand.Parameters.AddWithValue("@firstName", firstname);
        insertCommand.Parameters.AddWithValue("@lastName", lastname);
        insertCommand.Parameters.AddWithValue("@email", email);
        if (phone.Length > 0)
        {
            insertCommand.Parameters.AddWithValue("@phone", phone);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@phone", DBNull.Value);
        }
        if (street1.Length > 0)
        {
            insertCommand.Parameters.AddWithValue("@street1", street1);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@street1", DBNull.Value);
        }
        if (street2.Length > 0)
        {
            insertCommand.Parameters.AddWithValue("@street2", street2);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@street2", DBNull.Value);
        }
        if (city.Length > 0)
        {
            insertCommand.Parameters.AddWithValue("@city", city);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@city", DBNull.Value);
        }
        if (state.Length > 0)
        {
            insertCommand.Parameters.AddWithValue("@state", state);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@state", DBNull.Value);
        }
        if (zip.Length > 0)
        {
            insertCommand.Parameters.AddWithValue("@zip", zip);
        }
        else
        {
            insertCommand.Parameters.AddWithValue("@zip", DBNull.Value);
        }
        insertCommand.Parameters.AddWithValue("@permissions", GetPermissionsLevel("user"));

        connection.Open();
        SqlDataReader reader = insertCommand.ExecuteReader();
        int userID = -1;
        if (reader.Read())
        {
            userID = Convert.ToInt32(reader[0]);
        }
        connection.Close();
        
        ForumUser user = GetUser(userID);
        return user;
    }

    public static int GetPermissionsLevel(string permissionsType)
    {
        SqlConnection connection = ForumDB.GetConnection();
        SqlCommand selectCommand = new SqlCommand("SELECT [PermissionsID] FROM [Permissions] WHERE PermissionsLabel = @permissionsLabel", connection);
        selectCommand.Parameters.AddWithValue("@permissionsLabel", permissionsType);
        connection.Open();
        SqlDataReader reader = selectCommand.ExecuteReader();
        int permissionsID = -1;
        if (reader.Read())
        {
            permissionsID = Convert.ToInt32(reader[0]);
        }
        return permissionsID;
    }
}