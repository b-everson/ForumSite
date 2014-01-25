using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ForumDB
/// </summary>
public class ForumDB
{
	public ForumDB()
	{
	}

    public static SqlConnection GetConnection()
    {

        SqlConnection connection = null;
        connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ForumDatabaseConnectionString"].ConnectionString);
        return connection;
    }
}