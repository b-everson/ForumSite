using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// This class is used to interact with the forum database concerning the Reply objects and table.
/// </summary>
public class ReplyDB
{

    public static ReplyList GetReplies(int responseToID, Reply.ResponsesToTable responseToTable)
    {
        ReplyList replies = new ReplyList();
        string table = "";
        
        if (responseToTable == Reply.ResponsesToTable.Message)
        {
            table = "message";
        }
        else if (responseToTable == Reply.ResponsesToTable.Post)
        {
            table = "post";
        }
        else if (responseToTable == Reply.ResponsesToTable.Reply)
        {
            table = "reply";
        }

        int id = -1;
        string content = "";
        DateTime time = DateTime.Now.AddDays(3);
        int sender = -1;
       

        SqlConnection connection = ForumDB.GetConnection();
        SqlCommand command = new SqlCommand("SELECT [ReplyID], [Content], [TimePosted], [Sender], [ResponseToID], [ResponseToTable] FROM [Reply] WHERE [ResponseToTable] = @table AND [ResponseToID] = @responseToID", connection);
        command.Parameters.AddWithValue("@table", table);
        command.Parameters.AddWithValue("@responseToID", responseToID);


        connection.Open();
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            id = Convert.ToInt32(reader["ReplyID"]);
            content = reader["Content"].ToString();
            sender = Convert.ToInt32(reader["Sender"]);
            time = Convert.ToDateTime(reader["TimePosted"]);
            Reply newReply = new Reply(id, content, time, sender, responseToID, responseToTable);
            replies.Add(newReply);
        }

        connection.Close();

        return replies;
    }

    public static Reply GetReply(int id)
    {
        Reply reply = null;

        SqlConnection connection = ForumDB.GetConnection();
        SqlCommand command = new SqlCommand("SELECT [ReplyID] ,[Content],[TimePosted], [Sender], [ResponseToID], [ResponseToTable] FROM [Reply] WHERE [ReplyID] = @reply", connection);
        command.Parameters.AddWithValue("@reply", id);

        int replyID = -1;
        string content = "";
        DateTime timePosted = DateTime.Now.AddDays(3);
        int sender = -1;
        int responseToID = -1;
        Reply.ResponsesToTable responseToTable = Reply.ResponsesToTable.Post;

        connection.Open();
        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            replyID = Convert.ToInt32(reader["ReplyID"]);
            content = reader["Content"].ToString();
            timePosted = Convert.ToDateTime(reader["TimePosted"]);
            sender = Convert.ToInt32(reader["Sender"]);
            responseToID = Convert.ToInt32(reader["ResponseToID"]);
            string table = reader["ResponseToTable"].ToString();
            if (table.ToLower().Equals("post"))
            {
                responseToTable = Reply.ResponsesToTable.Post;
            }
            else if (table.ToLower().Equals("message"))
            {
                responseToTable = Reply.ResponsesToTable.Message;
            }
            else if (table.ToLower().Equals("reply"))
            {
                responseToTable = Reply.ResponsesToTable.Reply;
            }
        }

        connection.Close();

        if (replyID > -1)
        {
            reply = new Reply(replyID, content, timePosted, sender, responseToID, responseToTable);
        }
        return reply;
    }

    public static Reply CreateReply(string content, DateTime timePosted, int sender, int responseToID, Reply.ResponsesToTable responseToTable)
    {
        Reply reply = null;

        string table = "";
        //select applicable sql string from Reply.ResponsesToTable enumerator
        if (responseToTable == Reply.ResponsesToTable.Message)
        {
            table = "message";
        }
        else if (responseToTable == Reply.ResponsesToTable.Post)
        {
            table = "post";
        }
        else if (responseToTable == Reply.ResponsesToTable.Reply)
        {
            table = "reply";
        }

        SqlConnection connection = ForumDB.GetConnection();
        SqlCommand command = new SqlCommand("INSERT INTO [Reply] ([Content],[TimePosted],[Sender],[ResponseToID],[ResponseToTable]) VALUES (@content, @timePosted, @sender, @responseToID, @responseToTable); SELECT SCOPE_IDENTITY() ", connection);
        command.Parameters.AddWithValue("@content", content);
        command.Parameters.AddWithValue("@timePosted", timePosted);
        command.Parameters.AddWithValue("@sender", sender);
        command.Parameters.AddWithValue("@responseToID", responseToID);
        command.Parameters.AddWithValue("@responseToTable", table);

        int id = -1;
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            id = Convert.ToInt32(reader[0]);
        }

        connection.Close();

        if (id > -1)
        {
            reply = new Reply(id, content, timePosted, sender, responseToID, responseToTable);
        }

        return reply;
    }
}