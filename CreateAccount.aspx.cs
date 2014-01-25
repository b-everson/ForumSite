using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class CreateAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;

        ListItem[] states = { new ListItem("", ""),
                              new ListItem("ALABAMA", "AL"), 
                              new ListItem("ALASKA", "AK"),
                              new ListItem("AMERICAN SAMOA", "AS"),
                              new ListItem("ARIZONA", "AZ"),
                              new ListItem("ARKANSAS", "AR"),
                              new ListItem("CALIFORNIA", "CA"),
                              new ListItem("COLORADO", "CO"),
                              new ListItem("CONNECTICUT", "CT"),
                              new ListItem("DELAWARE", "DE"),
                              new ListItem("DISTRICT OF COLUMBIA", "DC"),
                              new ListItem("FEDERATED STATES OF MICRONESIA", 	"FM"),
                              new ListItem("FLORIDA", "FL"),
                              new ListItem("GEORGIA", "GA"),
                              new ListItem("GUAM", "GU"),
                              new ListItem("HAWAII", "HI"),
                              new ListItem("IDAHO", "ID"),
                              new ListItem("ILLINOIS", "IL"),
                              new ListItem("INDIANA", "IN"),
                              new ListItem("IOWA", "IA"),
                              new ListItem("KANSAS", "KS"),
                              new ListItem("KENTUCKY", "KY"),
                              new ListItem("LOUISIANA", "LA"),
                              new ListItem("MAINE", "ME"),
                              new ListItem("MARSHALL ISLANDS", "MH"),
                              new ListItem("MARYLAND", "MD"),
                              new ListItem("MASSACHUSETTS", "MA"),
                              new ListItem("MICHIGAN", "MI"),
                              new ListItem("MINNESOTA", "MN"),
                              new ListItem("MISSISSIPPI", "MS"),
                              new ListItem("MISSOURI", "MO"),
                              new ListItem("MONTANA", "MT"),
                              new ListItem("NEBRASKA", "NE"),
                              new ListItem("NEVADA", "NV"),
                              new ListItem("NEW HAMPSHIRE", "NH"),
                              new ListItem("NEW JERSEY", "NJ"),
                              new ListItem("NEW MEXICO", "NM"),
                              new ListItem("NEW YORK", "NY"),
                              new ListItem("NORTH CAROLINA", "NC"),
                              new ListItem("NORTH DAKOTA", "ND"),
                              new ListItem("NORTHERN MARIANA ISLANDS", "MP"),
                              new ListItem("OHIO", "OH"),
                              new ListItem("OKLAHOMA", "OK"),
                              new ListItem("OREGON", "OR"),
                              new ListItem("PALAU", "PW"),
                              new ListItem("PENNSYLVANIA", "PA"),
                              new ListItem("PUERTO RICO", "PR"),
                              new ListItem("RHODE ISLAND", "RI"),
                              new ListItem("SOUTH CAROLINA", "SC"),
                              new ListItem("SOUTH DAKOTA", "SD"),
                              new ListItem("TENNESSEE", "TN"),
                              new ListItem("TEXAS", 	"TX"),
                              new ListItem("UTAH", "UT"),
                              new ListItem("VERMONT", "VT"),
                              new ListItem("VIRGIN ISLANDS", "VI"),
                              new ListItem("VIRGINIA", "VA"),
                              new ListItem("WASHINGTON", "WA"),
                              new ListItem("WEST VIRGINIA", "WV"),
                              new ListItem("WISCONSIN", "WI"),
                              new ListItem("WYOMING", "WY") 
                            };

        for (int i = 0; i < states.Length; i++)
        {
            ddlState.Items.Add(states[i]);
        }
        ddlState.SelectedIndex = -1;        

        btnCreate.Click += btnCreate_Click;
    }

    void btnCreate_Click(object sender, EventArgs e)
    {
        if (ValidInfo())
        {
            /*create new ForumUser with textfield values
              save clientID into session state
              redirect to ~/default.aspx
             */
            try
            {
                ForumUser user = ForumUserDB.CreateUser(txtUserName.Text, txtPassword.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPhone.Text, txtStreet1.Text, txtStreet2.Text, txtCity.Text, ddlState.SelectedValue, txtZip.Text);
                lblMessage.Text = "";
                Session["UserID"] = user.UserID;
                Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
            }
        }
        else
        {
            lblMessage.Text = "Invalid/Missing fields";
            lblMessage.ForeColor = Color.Red;
        }
    }

    /// <summary>
    /// Returns false if required fields are missing or username is already taken.
    /// </summary>
    /// <returns></returns>
    private bool ValidInfo()
    {
        bool valid = true;

        //if user name is taken, inform user
        if (ForumUserDB.UserExists(txtUserName.Text))
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "There is already a user by that name.";
            valid = false;
        }

        if (txtPassword.Text != txtConfirmPassword.Text)
        {
            valid = false;
        }
        


        if (txtFirstName.Text == "" || txtLastName.Text == "" || txtEmail.Text == "")
        {
            valid = false;
        }


        return valid;
    }


}