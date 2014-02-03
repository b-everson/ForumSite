using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class _Forum : System.Web.UI.Page
{



    protected void Page_Load(object sender, EventArgs e)
    {
        loadTopicsPanel();
    }
    /// <summary>
    /// Populate the selection of topics
    /// </summary>
    private void loadTopicsPanel()
    {
        DataView view = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

        for (int i = 0; i < view.Count; i++)
        {
            DataRowView row = (DataRowView)view[i];
            Panel pnlNext = new Panel();
            pnlPosts.Controls.Add(pnlNext);
            HyperLink hlTitle = new HyperLink();
            hlTitle.Text = row["Title"].ToString();
            hlTitle.NavigateUrl = "~/Topics.aspx?topicID=" + Convert.ToInt32(row["topicID"]);
            pnlNext.Controls.Add(hlTitle);
            Label lblContent = new Label();
            lblContent.Text = row["Content"].ToString();
            pnlNext.Controls.Add(lblContent);
            pnlNext.CssClass = "pnlNext";
        } 
    }
}