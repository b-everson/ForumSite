using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Topics : System.Web.UI.Page
{
    PostDB.PostSorts sortMethod = PostDB.PostSorts.ByDate;
    PostList posts;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( Request.QueryString["sortMethod"] != null && Request.QueryString["sortMethod"].Length > 0)
        {
            sortMethod = (PostDB.PostSorts)Convert.ToInt32(Request.QueryString["sortMethod"]);
        }
        loadPosts(sortMethod);
        string timeSort = "";
        string postIDSort = "";
        string userNameSort = "";


        if(posts.Count() > 0)
        {
            timeSort = posts[posts.Count() - 1].TimePosted.ToString().Replace(" ", "+");
            postIDSort = posts[posts.Count() - 1].PostID.ToString().Replace(" ", "+");      
            userNameSort = ForumUserDB.GetUser(posts[posts.Count() - 1].UserID).UserName.Replace(" ", "+");
        }
       // Response.Write("<script language='javascript'>alert('" + HttpContext.Current.Session["userNameSort"] + " " + HttpContext.Current.Session["postIDSort"] + "');setTimeout(function(){document.location = '" + ResolveUrl("~/Posts.aspx?id=") + Request.QueryString["id"] + "'}, 5000)</script>");
        if (timeSort.Length > 0)
        btnNextPostPage.PostBackUrl = "~/Posts.aspx?id=" + Request.QueryString["id"] + "&timeSort=" + timeSort + "&postIDSort=" + postIDSort + "&userNameSort=" + userNameSort + "&sortMethod=" + (int)sortMethod;
        btnTimeSort.PostBackUrl = "~/Posts.aspx?id=" + Request.QueryString["id"] + "&sortMethod=" + (int)PostDB.PostSorts.ByDate;
        btnUserSort.PostBackUrl = "~/Posts.aspx?id=" + Request.QueryString["id"] + "&sortMethod=" + (int)PostDB.PostSorts.ByUserName;
        
     /*   if (IsPostBack)
        {
            Response.Write("<script language='javascript'>alert('Not PostBack');setTimeout('function(){alert(\"balls\")}', 5000)</script>");
            Session.Remove("userNameSort");
            Session.Remove("postIDSort");
            Session.Remove("timeSort");
        }*/
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
    private void loadPosts(PostDB.PostSorts method)
    {

        int topicID = Convert.ToInt32(Request.QueryString["id"]);
        string userNameSort = "";
        if ( Request.QueryString["userNameSort"] != null && Request.QueryString["userNameSort"].ToString().Length > 0)
        {
            userNameSort = Request.QueryString["userNameSort"].ToString().Replace("+", " ");
        }

        int postIDSort = -1;
        if (Request.QueryString["postIDSort"] != null && Request.QueryString["postIDSort"].ToString().Length > 0)
        {
            postIDSort = Convert.ToInt32(Request.QueryString["postIDSort"]);
        }

        DateTime timeSort = new DateTime(1900,1,1);
        if (Request.QueryString["timeSort"] != null)// && Request.QueryString["timeSort"].ToString().Length > 0)
        {
            try
            {
                timeSort = DateTime.Parse(Request.QueryString["timeSort"].ToString().Replace("+", " "));
            }
            catch (Exception) {  }
        }


        
    //    Response.Write("<script language='javascript'>alert('" +userNameSort + " " + timeSort.ToString() + " " +  postIDSort + "')</script>");
        posts = null;
        posts = PostDB.GetPosts(topicID, 10, method, postIDSort, userNameSort, timeSort, true);
      

    //    Response.Write("<script language='javascript'>alert('" + ForumUserDB.GetUser(posts[posts.Count() - 1].UserID).UserName.ToString() + " " + posts[posts.Count() - 1].TimePosted.ToString() + " " + posts[posts.Count() - 1].PostID.ToString() + "')</script>");
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
            nameCell.Text = ForumUserDB.GetUser(posts[i].UserID).UserName + " " + posts[i].PostID.ToString();
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
        if(posts.Count() < 10 )  //need to implement function to determine if last post in list is also the last post of sort method
        btnNextPostPage.PostBackUrl = Request.RawUrl;
    }
}