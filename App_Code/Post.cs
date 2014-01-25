using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Post
/// </summary>
public class Post
{

    public Post(int postID, string title, string content, DateTime timePosted, int userID, int topicID)
    {
        this.PostID = postID;
        this.Title = title;
        this.Content = content;
        this.TimePosted = timePosted;
        this.UserID = userID;
        this.TopicID = topicID;
    }

    public int PostID { get; private set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime TimePosted { get; private set; }
    public int UserID { get; private set; }
    public int TopicID { get; private set; }
}