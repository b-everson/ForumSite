using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Topics : System.Web.UI.Page
{
    PostList posts;

    protected void Page_Load(object sender, EventArgs e)
    {
        loadPosts();
        Session.Remove("timeSort");
        Session.Remove("userNameSort");
    }

    /* 
     * use query string to get relevant posts
     * save posts in list
     * loop through list
     *     display title as link
     *     display first 150 characters of content for each post
     *     display date created
     */ 
    /// <summary>
    /// Populate the posts panel.
    /// </summary>
    private void loadPosts()
    {

        int topicID = Convert.ToInt32(Request.QueryString["id"]);
        string userNameSort = "";
        if (HttpContext.Current.Session["userNameSort"] != null)
        {
            userNameSort = HttpContext.Current.Session["userNameSort"].ToString();
        }

        DateTime timeSort = DateTime.Now.AddDays(2);
        if (HttpContext.Current.Session["timeSort"] != null)
        {
            timeSort = DateTime.Parse(HttpContext.Current.Session["timeSort"].ToString());
        }


        
        //Response.Write("<script language='javascript'>alert('" +userNameSort + " " + timeSort.ToString() + "')</script>");
        if (timeSort < DateTime.Now)
        {
            posts = PostDB.GetPosts(topicID, 10, PostDB.PostSorts.ByDate, userNameSort, timeSort, true);
        }
        else
        {
            posts = PostDB.GetPosts(topicID, DateTime.Now.AddDays(2));
        }
        int counter = 0;
        for (int i = 0; i < posts.Count(); i++){
            TableRow topRow = new TableRow();
            if (counter++ % 2 == 1)
            {
                topRow.CssClass = "oddRow";
            }
            else
            {
                topRow.CssClass = "evenRow";
            }
            

            TableCell titleCell = new TableCell();
            titleCell.CssClass = "titleCell";
            //titleCell.ColumnSpan = 2;
            
            HyperLink hlTitle = new HyperLink();
            hlTitle.Text = posts[i].Title;
            hlTitle.NavigateUrl = "~/nofile";
            titleCell.Controls.Add(hlTitle);
            topRow.Cells.Add(titleCell);

            TableCell descCell = new TableCell();
            //descCell.RowSpan = 30;
            descCell.ColumnSpan = 2;
            descCell.Wrap = true;
            string content = posts[i].Content;
            if (content.Length > 150)
            {
                content = content.Substring(0, 150) + "...";
            }
            descCell.Text = content;
            descCell.CssClass = "descCell";
            topRow.Cells.Add(descCell);

            TableCell nameCell = new TableCell();
            nameCell.Text = ForumUserDB.GetUser(posts[i].UserID).UserName;
            nameCell.CssClass = "nameCell";
            topRow.Cells.Add(nameCell);

            TableCell dateCell = new TableCell();
            dateCell.Text = posts[i].TimePosted.ToString();
            topRow.Cells.Add(dateCell);
            dateCell.CssClass = "dateCell";
            tblPosts.Rows.Add(topRow);        
            
        }
           
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CreatePost.aspx?topicID=" + Request.QueryString["id"]);
    }


    protected void btnNextPostPage_Click(object sender, EventArgs e)
    {
        Session["timeSort"] = posts[posts.Count() - 1];
        Response.Redirect(Request.RawUrl);
    }
}