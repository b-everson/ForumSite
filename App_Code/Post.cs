using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Post
/// </summary>
public class Post : IComparable<Post>
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


    public int CompareTo(Post other)
    {
        if (other == null)
        {
            return 1;
        }
        else
        {
            return TimePosted.CompareTo(other.TimePosted);
        }

    }
    /*
    public int ComparePostsByUser(Post other)
    {
        if (other == null)
        {
            return 1;
        }
        else
        {
            return ForumUserDB.GetUser(UserID).UserName.CompareTo(ForumUserDB.GetUser(other.UserID));
        }
    }*/
    
    public static int ComparePostsByUser(Post x, Post y)
    {

        if (x == null)
        {
            if (y == null)
            {
                return 0;
            }
            else
            {
                //if x is null and y is not, y is greater
                return -1;
            }
        }
        else
        {
            if (y == null)
            {
                //if y null and x is not, x is greater
                return 1;
            }
            else
            {
                return ForumUserDB.GetUser(x.UserID).UserName.CompareTo(ForumUserDB.GetUser(y.UserID).UserName);
            }
        }
    }
}