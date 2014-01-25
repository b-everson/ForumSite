using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    TextBox txtUserName;
    TextBox txtPassword;
    CheckBox cbStaySignedIn;
    Label lblMessage;
    ForumUser user;


    protected void Page_Load(object sender, EventArgs e)
    {

        loadPostsPanel();
        loadUserPanel();
    }

    private void loadUserPanel()
    {
        lblMessage = new Label();

        int userID = Convert.ToInt32(HttpContext.Current.Session["UserID"]);

        //for user selection if user not found return val is -1, if user exits but password is incorrect returnval is -2
        //otherwise, log the user in, saving UserID in session state
        switch (userID)
        {
            case -2:
                lblMessage.Text = "Incorrect password for that user.";
                break;
            case -1:
                lblMessage.Text = "User not found.";
                break;
            case 0:
                lblMessage.Text = "";
                break;
            default:

                break;
        }
        
        if (userID <= 0)
        {
            lblMessage.CssClass = "label";

            Label lblUserName = new Label();
            lblUserName.Text = "User Name:";
            lblUserName.CssClass = "label";
            txtUserName = new TextBox();
            txtUserName.CssClass = "textbox";

            Label lblPassword = new Label();
            lblPassword.CssClass = "label";
            lblPassword.Text = "Password:";
            txtPassword = new TextBox();
            txtPassword.TextMode = TextBoxMode.Password;
            txtPassword.CssClass = "textbox";

            cbStaySignedIn = new CheckBox();
            cbStaySignedIn.Checked = false;
            cbStaySignedIn.Text = "Stay signed in?";
            cbStaySignedIn.CssClass = "checkbox";

            Button btnSignIn = new Button();
            btnSignIn.Text = "Sign In";
            btnSignIn.CssClass = "button";

            HyperLink lnkCreateAccount = new HyperLink();
            lnkCreateAccount.Text = "Create Account";
            lnkCreateAccount.CssClass = "label";
            lnkCreateAccount.NavigateUrl = "/CreateAccount.aspx";

            pnlUser.Controls.Add(lblUserName);
            pnlUser.Controls.Add(txtUserName);
            pnlUser.Controls.Add(lblPassword);
            pnlUser.Controls.Add(txtPassword);
            pnlUser.Controls.Add(cbStaySignedIn);
            pnlUser.Controls.Add(btnSignIn);
            pnlUser.Controls.Add(lnkCreateAccount);
            pnlUser.Controls.Add(lblMessage);


            btnSignIn.Click += btnSignIn_Click;

        }
        else
        {
            pnlUser.CssClass = "userLoggedIn";

            if (Trace.IsEnabled)
                Trace.Write("Begin database read");

            user = ForumUser.GetUser(Convert.ToInt32(HttpContext.Current.Session["UserID"]));
            
            if (Trace.IsEnabled)
                Trace.Warn("End database read");
            lblMessage.Text = "Welcome, " + user.FirstName;
            pnlUser.Controls.Add(lblMessage);

            Button btnLogout = new Button();
            btnLogout.Text = "Log Out";
            pnlUser.Controls.Add(btnLogout);

            btnLogout.Click += btnLogout_Click;
            
        }
        
    }

    //remove user from session, refresh page
    void btnLogout_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Remove("UserID");  
        Response.Redirect(Request.RawUrl);
    }

    void btnSignIn_Click(object sender, EventArgs e)
    {
        //for user selection if user not found return val is -1, if user exits but password is incorrect returnval is -2
        //otherwise, log the user in, saving UserID in session state
        hiddenUserName.Value = txtUserName.Text;
        hiddenPassword.Value = txtPassword.Text;
        DataView UserData = (DataView)sdsUser.Select(DataSourceSelectArguments.Empty);
        

        DataRowView UserRow = (DataRowView)UserData[0]; 
        
        int userID = int.Parse(UserRow[0].ToString());
        Session.Add("UserID", userID);

        hiddenPassword.Value = "";
        hiddenUserName.Value = "";
        Response.Redirect(Request.RawUrl);
    }

    private void loadPostsPanel()
    {
        DataView view = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

        for (int i = 0; i < view.Count; i++)
        {

            DataRowView row = (DataRowView)view[i];
            Panel pnlNext = new Panel();
            pnlPosts.Controls.Add(pnlNext);
            HyperLink hlTitle = new HyperLink();
            hlTitle.Text = row["Title"].ToString();
            hlTitle.NavigateUrl = "~/Posts.aspx?id=" + Convert.ToInt32(row["TopicID"]);
            pnlNext.Controls.Add(hlTitle);
            Label lblContent = new Label();
            lblContent.Text = row["Content"].ToString();
            pnlNext.Controls.Add(lblContent);
            pnlNext.CssClass = "pnlNext";

        } 
    }
}