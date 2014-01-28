using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Topics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        loadPosts();
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
        SqlConnection connection = ForumDB.GetConnection();
        SqlCommand postsCommand = new SqlCommand("SELECT p.[PostID], p.[Title], p.[Content], p.[TimePosted], p.[UserID], p.[TopicID], u.UserName FROM [Post] p JOIN [User] u ON p.UserID = u.UserID WHERE [TopicID] = @topicID", connection);
        postsCommand.Parameters.AddWithValue("@topicID", topicID);

        connection.Open();
        SqlDataReader reader = postsCommand.ExecuteReader();
        int counter = 0;
        while (reader.Read())
        {
            
            int postID = Convert.ToInt32(reader["PostID"]);
            string title = reader["Title"].ToString();
            string content = reader["Content"].ToString();
            DateTime timePosted = DateTime.Parse(reader["TimePosted"].ToString());
            int userID = Convert.ToInt32(reader["UserID"]);

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
            hlTitle.Text = title;
            hlTitle.NavigateUrl = "~/nofile";
            titleCell.Controls.Add(hlTitle);
            topRow.Cells.Add(titleCell);

            TableCell descCell = new TableCell();
            //descCell.RowSpan = 30;
            descCell.ColumnSpan = 2;
            descCell.Wrap = true;
            if (content.Length > 150)
            {
                content = content.Substring(0, 150) + "...";
            }
            descCell.Text = content;
            descCell.CssClass = "descCell";
            topRow.Cells.Add(descCell);

            TableCell nameCell = new TableCell();
            nameCell.Text = reader["UserName"].ToString();
            nameCell.CssClass = "nameCell";
            topRow.Cells.Add(nameCell);

            TableCell dateCell = new TableCell();
            dateCell.Text = timePosted.ToString();
            topRow.Cells.Add(dateCell);
            dateCell.CssClass = "dateCell";
            tblPosts.Rows.Add(topRow);        
            
        }
           
        connection.Close();
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CreatePost.aspx?topicID=" + Request.QueryString["id"]);
    }
}