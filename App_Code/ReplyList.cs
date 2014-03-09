using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReplyList
/// </summary>
public class ReplyList
{
    private List<Reply> replies;

	public ReplyList()
	{
        replies = new List<Reply>();
	}

    public int Count()
    {
        return replies.Count;
    }

    public Reply this[int i]
    {
        get
        {
            if (i < 0 || i >= replies.Count)
            {
                throw new ArgumentOutOfRangeException(i.ToString());
            }
            return replies[i];
        }

        set
        {
            replies[i] = value;
        }
    }

    public void Add(Reply reply)
    {
        replies.Add(reply);
    }

    public void Add(Reply reply, bool sorted)
    {
        if (sorted)
        {

            /*loop through list, 
            find index of first position where the replyList reply is greater than the incoming reply 
             insert new reply to selected position*/
            for (int i = 0; i < Count(); i++)
            {
                if (reply.CompareTo(replies[i]) > -1)
                {
                    replies.Insert(i, reply);
                    break;
                }
            }
        }
        else
        {
            Add(reply);
        }
    }


    public void Remove(Reply reply)
    {
        replies.Remove(reply);
    }

    public void RemoveAt(int index)
    {
        replies.RemoveAt(index);
    }

    /// <summary>
    /// Sorts replies by time.
    /// </summary>
    public void Sort()
    {
        replies.Sort();
    }
}