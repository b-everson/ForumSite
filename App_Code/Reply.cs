using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Reply
/// </summary>
public class Reply : IComparable<Reply>
{
    public enum ResponsesToTable
    {
        Message,
        Reply,
        Post
    };

    private ResponsesToTable responseToTable;
    private int replyID;

	public Reply(int id, string content, DateTime timePosted, int sender, int responseID , ResponsesToTable table)
	{
        replyID = id;
        Content = content;
        TimePosted = timePosted;
        Sender = sender;
        ResponseToID = responseID;
        responseToTable = table;
	}

    public int ReplyID { get { return replyID; } }
    public string Content { get; set; }
    public DateTime TimePosted { get; set; }
    public int Sender { get; set; }
    public int ResponseToID { get; set; }
    public ResponsesToTable ResponseToTable { get { return responseToTable; } set { responseToTable = value;} }

    public int CompareTo(Reply other)
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

    public static bool operator == (Reply r1, Reply r2)
    {
        if (Object.ReferenceEquals(r1, r2)){
            return true;
        }

        if ( (Object)r1 == null || (Object)r2 == null)
        {
            return false;
        }

        return r1.ReplyID == r2.ReplyID;
    }

    public static bool operator !=(Reply r1, Reply r2)
    {
        return !(r1 == r2);
    }
}