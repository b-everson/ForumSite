using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PostList
/// </summary>
public class PostList 
{
    private List<Post> posts;

	public PostList()
	{
        posts = new List<Post>();
	}

    public int Count()
    {
        return posts.Count;
    }

    public Post this[int i]
    {
        get
        {
            if (i < 0 || i >= posts.Count)
            {
                throw new ArgumentOutOfRangeException(i.ToString());
            }
            return posts[i];
        }

        set
        {
            posts[i] = value;
        }
    }

    public void Add(Post post)
    {
        posts.Add(post);
    }

    public void Remove(Post post)
    {
        posts.Remove(post);
    }

    public void RemoveAt(int index)
    {
        posts.RemoveAt(index);
    }

    /// <summary>
    /// Sorts posts by time posted.
    /// </summary>
    public void Sort()
    {
        posts.Sort();
    }

 
    public void SortByUserName()
    {
        posts.Sort(Post.ComparePostsByUser);
    }

}