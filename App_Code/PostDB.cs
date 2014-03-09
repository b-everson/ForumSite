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

    public enum PostSorts
    {
        ByUserName, ByDate
    };
    
    /// <summary>
    /// Get a set number of posts by Topic ID, using a sort, 
    /// from a record determined by either time posted or 
    /// time posted and username, with an option to use 
    /// ascending or descending order.
    /// </summary>
    /// <param name="topicID"></param>
    /// <param name="number"></param>
    /// <param name="sort"></param>
    /// <param name="userName"></param>
    /// <param name="time"></param>
    /// <param name="ascending"></param>
    /// <returns></returns>
    public static PostList GetPosts(int topicID, int number, PostSorts sort, int postIDSort, string userName, DateTime time, bool ascending)
    {   //take the topicID, number, and sort to determine query options
        PostList posts = new PostList();

        string numberOfRows = "";
        string orderBy = "";
        string ascendingOrder = "";
        string whereClauseMatching = "";
        string whereClauseElse = "";
        string relation = "";
        string union = "";

        if (number > 0)
        {
            numberOfRows = "TOP " + number.ToString() + " ";
        }

        if (ascending)
        {
            ascendingOrder = "ASC";
            relation = ">";
        }
        else
        {
            ascendingOrder = "DESC";
            relation = "<";
        }

        if (sort == PostSorts.ByDate)
        {
            orderBy = " ORDER BY [TimePosted] " + ascendingOrder + ", [UserName], [PostID] ";  //DateAdd(MILLISECOND, 999 ,@time))
            whereClauseMatching = " AND UserName = @userName AND ([TimePosted] " + relation + " DateAdd(MILLISECOND, 999, @timePosted) OR [TimePosted] = @timePosted AND PostID > @postID)";
            whereClauseElse = " AND [TimePosted] " + relation + " @timePosted AND Username != @userName OR [TimePosted] = @timePosted AND UserName > @userName";
        }
        else if (sort == PostSorts.ByUserName)
        {
            orderBy = " ORDER BY [UserName]" + ascendingOrder + ", [TimePosted], [PostID] ";
            whereClauseMatching = " And UserName = @userName AND ([TimePosted] = @timePosted AND PostID > @postID OR [TimePosted] > @timePosted) ";
            whereClauseElse = " AND UserName " + relation + " @userName ";
        }

        union = "SELECT p.[PostID], p.[Title], p.[Content], p.[TimePosted], p.[UserID], p.[topicID], u.UserName FROM [Post] p JOIN [User] u ON p.UserID = u.UserID WHERE [topicID] = @topicID " + whereClauseMatching + " UNION ";
         
        SqlConnection connection = ForumDB.GetConnection();
        SqlCommand postsCommand = new SqlCommand("SELECT " + numberOfRows + " [PostID], [Title], [Content], [TimePosted], [UserID], [topicID], [UserName]  FROM (" + union + "SELECT " + numberOfRows + " p.[PostID], p.[Title], p.[Content], p.[TimePosted], p.[UserID], p.[topicID], u.UserName FROM [Post] p JOIN [User] u ON p.UserID = u.UserID WHERE [topicID] = @topicID" + whereClauseElse + orderBy + ") as rows" + " WHERE [topicID] = @topicID"  + orderBy, connection);
        postsCommand.Parameters.AddWithValue("@topicID", topicID);
        postsCommand.Parameters.AddWithValue("@userName", userName);
        postsCommand.Parameters.AddWithValue("@timePosted", time);
        postsCommand.Parameters.AddWithValue("@postID", postIDSort);
        string derp = postsCommand.CommandText;


        connection.Open();
        SqlDataReader reader = postsCommand.ExecuteReader();
        while (reader.Read())
        {

            int postID = Convert.ToInt32(reader["PostID"]);
            string title = reader["Title"].ToString();
            string content = reader["Content"].ToString();
            DateTime timePosted = DateTime.Parse(reader["TimePosted"].ToString());
            int userID = Convert.ToInt32(reader["UserID"]);
            Post post = new Post(postID, title, content, timePosted, userID, topicID);
            posts.Add(post);
        }

        connection.Close();

        return posts;
    }

    /// <summary>
    /// Get top 10 posts by topic ID, sorts by time posted in ascending order.
    /// </summary>
    /// <param name="topicID"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static PostList GetPosts(int topicID, DateTime time)
    {
        return GetPosts(topicID, 10, PostSorts.ByUserName, -1 , "", time, true);
    }


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
        SqlCommand createCommand = new SqlCommand("INSERT INTO [Post]([Title],[Content],[TimePosted],[UserID],[topicID]) VALUES (@title,@content,GetDate(),@userID,@topicID); SELECT SCOPE_IDENTITY()",connection);
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
    /// Get a post by its id, returns null if invalid post ID.
    /// </summary>
    /// <param name="postID"></param>
    /// <returns></returns>
    public static Post GetPost(int postID)
    {
        Post post = null;

        SqlConnection connection = ForumDB.GetConnection();
        SqlCommand command = new SqlCommand("SELECT [Title],[Content],[TimePosted],[UserID],[topicID] FROM [Post] WHERE [PostID] = @postID ", connection);
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
            topicID = Convert.ToInt32(reader["topicID"]);
        }

        connection.Close();
        if (userId > -1)
        {
            post = new Post(postID, title, content, timePosted, userId, topicID);
        }

        return post;
    }

    /// <summary>
    /// Return the last post of a particular topic and sort.
    /// </summary>
    /// <param name="topicID"></param>
    /// <param name="sort"></param>
    /// <param name="ascending"></param>
    /// <returns></returns>
    public static Post GetLastPost(int topicID, PostSorts sort, bool ascending)
    {
        Post post = null;
        SqlConnection connection = ForumDB.GetConnection();
        string orderby = "";
        string reverseAscending = "";

        if (ascending)
        {
            reverseAscending = "DESC";
        }
        else
        {
            reverseAscending = "ASC";
        }

        if (sort == PostSorts.ByDate)
        {
            orderby = " ORDER BY [TimePosted] " + reverseAscending + ", [UserName] DESC, [PostID] DESC ";
        }
        else if(sort == PostSorts.ByUserName)
        {
            orderby = " ORDER BY [UserName] " + reverseAscending + ", [TimePosted] DESC, [PostID] DESC ";
        }


        int postID = -1;
        string title = "";
        string content = "";
        DateTime timePosted = DateTime.Now.AddDays(10);
        int userID = -1;
        

        SqlCommand selectCommand = new SqlCommand("SELECT TOP 1 p.[PostID], p.[Title], p.[Content], p.[TimePosted], p.[UserID], p.[topicID], u.UserName FROM [Post] p Join [User] u on p.UserID = u.UserID WHERE topicID = @topicID " + orderby, connection);
        selectCommand.Parameters.AddWithValue("@topicID", topicID);
        connection.Open();

        SqlDataReader reader = selectCommand.ExecuteReader();
        if (reader.Read())
        {
            postID = Convert.ToInt32(reader["PostID"]);
            title = reader["Title"].ToString();
            content = reader["Content"].ToString();
            timePosted = DateTime.Parse(reader["TimePosted"].ToString());
            userID = Convert.ToInt32(reader["UserID"]);
        }

        connection.Close();

        post = new Post(postID, title, content, timePosted, userID, topicID);

        return post;
    }

    public static Post GetParentPost(Reply reply)
    {
        Post post = null;

        if (reply.ResponseToTable == Reply.ResponsesToTable.Post)
        {
            post = GetPost(reply.ResponseToID);
        }
        else if (reply.ResponseToTable == Reply.ResponsesToTable.Reply)
        {
            Reply childReply = ReplyDB.GetReply(reply.ResponseToID);
            post = GetParentPost(childReply);
        }

        return post;
    }
}