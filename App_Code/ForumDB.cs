using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Class to handle functions for basic interaction with forum database.
/// </summary>
public class ForumDB
{
	public ForumDB()
	{
	}
    /// <summary>
    /// Get the SqlConnection object to interact with forum database.
    /// </summary>
    /// <returns></returns>
    public static SqlConnection GetConnection()
    {

        SqlConnection connection = null;
        connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ForumDatabaseConnectionString"].ConnectionString);
        return connection;
    }
}