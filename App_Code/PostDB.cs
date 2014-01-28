using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for PostDB
/// </summary>
public class PostDB
{
	public PostDB()
	{

	}
    
    //TODO: Create function to get a limited number of posts

    /// <summary>
    /// Create and return a post.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="userID"></param>
    /// <param name="topicID"></param>
    /// <returns></returns>
    public static Post CreatePost(string title, string content, int userID, int topicID)
    {
        Post post = null;

        SqlConnection connection = ForumDB.GetConnection();
        SqlCommand createCommand = new SqlCommand("INSERT INTO [Post]([Title],[Content],[TimePosted],[UserID],[TopicID]) VALUES (@title,@content,GetDate(),@userID,@topicID); SELECT SCOPE_IDENTITY()",connection);
        createCommand.Parameters.AddWithValue("@title", title);
        createCommand.Parameters.AddWithValue("@content", content);
        createCommand.Parameters.AddWithValue("@userID", userID);
        createCommand.Parameters.AddWithValue("@topicID", topicID);

        int postID = -1;
        connection.Open();
        SqlDataReader reader = createCommand.ExecuteReader();

        if (reader.Read())
        {
            postID = Convert.ToInt32(reader[0]);
        }

        connection.Close();
        post = PostDB.GetPost(postID);

        
        return post;
    }

    /// <summary>
    /// Get a post by its id.
    /// </summary>
    /// <param name="postID"></param>
    /// <returns></returns>
    private static Post GetPost(int postID)
    {
        Post post = null;

        SqlConnection connection = ForumDB.GetConnection();
        SqlCommand command = new SqlCommand("SELECT [Title],[Content],[TimePosted],[UserID],[TopicID] FROM [Post] WHERE [PostID] = @postID ", connection);
        command.Parameters.AddWithValue("@postID", postID);

        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        string title = "";
        string content = "";
        DateTime timePosted = DateTime.Now.AddDays(3);
        int userId = -1;
        int topicID = -1;
        
        if (reader.Read())
        {
            title = reader["Title"].ToString();
            content = reader["Content"].ToString();
            timePosted = DateTime.Parse(reader["TimePosted"].ToString());
            userId = Convert.ToInt32(reader["UserID"]);
            topicID = Convert.ToInt32(reader["TopicID"]);
        }

        post = new Post(postID, title, content, timePosted, userId, topicID);

        return post;
    }

}