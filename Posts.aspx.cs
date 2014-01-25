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
    private void loadPosts()
    {
        int topicID = Convert.ToInt32(Request.QueryString["id"]);
        SqlConnection connection = ForumDB.GetConnection();
        SqlCommand postsCommand = new SqlCommand("SELECT [PostID], [Title], [Content], [TimePosted], [UserID], [TopicID] FROM [Post] WHERE [TopicID] = @topicID", connection);
        postsCommand.Parameters.AddWithValue("@topicID", topicID);

        connection.Open();
        SqlDataReader reader = postsCommand.ExecuteReader();

        while (reader.Read())
        {
            Panel pnlNext = new Panel();
            int postID = Convert.ToInt32(reader["PostID"]);
            string title = reader["Title"].ToString();
            string content = reader["Content"].ToString();
            DateTime timePosted = DateTime.Parse(reader["TimePosted"].ToString());
            int userID = Convert.ToInt32(reader["UserID"]);
            HyperLink hlTitle = new HyperLink();
            hlTitle.Text = title;
            hlTitle.NavigateUrl = "http://www.reddit.com";

            pnlNext.Controls.Add(hlTitle);
            Label lblDate = new Label();
            lblDate.Text = timePosted.ToString();
            lblDate.ID = "lblDate";
            pnlNext.Controls.Add(lblDate);
            Label lblContent = new Label();
            lblContent.CssClass = "lblContent";
            lblContent.Text = content.Substring(0, 150) + "...";
            

            pnlNext.Controls.Add(lblContent);
            pnlNext.CssClass = "pnlNext";
            
            pnlPosts.Controls.Add(pnlNext);
        }
           
        connection.Close();
    }
}