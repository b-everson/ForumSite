using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Posts : System.Web.UI.Page
{
    int postID = -1;
    Post post = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            postID = Convert.ToInt32(Request.QueryString["postID"]);
        }catch(InvalidCastException){
            ErrorWithRedirect();
        }
        post = PostDB.GetPost(postID);
        if (post != null)
        {
            lblPostTitle.Text = post.Title;
            lblPostContent.Text = post.Content;
            ForumUser author = ForumUserDB.GetUser(post.UserID);
            lblPostAuthor.Text = author.UserName;
            lblPostTime.Text = post.TimePosted.ToString();
            LoadReplies();

            Panel replyActionPanel = new Panel();
            replyActionPanel.CssClass = "actionPanel";
            HyperLink hlReply = new HyperLink();
            hlReply.Text = "Create Reply";
            hlReply.NavigateUrl = "~/CreateReply.aspx?responseToID=" + post.PostID + "&responseToTable=" + Reply.ResponsesToTable.Post;
            replyActionPanel.Controls.Add(hlReply);
            pnlReplies.Controls.Add(replyActionPanel);
        }
        else
        {
            ErrorWithRedirect();
        }

    }

    private void ErrorWithRedirect()
    {
        Response.Write("<script language='javascript'>alert('Invalid page, redirecting to main forum page.'); setTimeout(function(){ document.location = '" + ResolveUrl("~/Forums.aspx") + "'} ,100)</script>");
    }

    public void LoadReplies()
    {
        ReplyList list = ReplyDB.GetReplies( postID, Reply.ResponsesToTable.Post);
        if (list.Count() > 0)
        {
            AddReplyListPanel(pnlReplies, list);
        }
        else
        {
            Label noReplyLabel = new Label();
            noReplyLabel.Text = "No replies yet";
            pnlReplies.Controls.Add(noReplyLabel);
        }
    }

    private void AddReplyListPanel(Panel pnlParent, ReplyList replies)
    {
        if (replies.Count() > 0)
        {           
            for (int i = 0; i < replies.Count(); i++)
            {
                Panel pnlChild = new Panel();
                pnlChild.CssClass = "replyPanel";
                pnlParent.Controls.Add(pnlChild);
                Label lblContent = new Label();
                Label lblTimePosted = new Label();
                Label lblUser = new Label();
                lblContent.Text = replies[i].Content;
                lblTimePosted.Text = replies[i].TimePosted.ToString();
                lblUser.Text = ForumUserDB.GetUser(replies[i].Sender).UserName;
                AddReplyActionPanel(pnlChild, replies[i]);


                ReplyList childReplies = ReplyDB.GetReplies(replies[i].ResponseToID, Reply.ResponsesToTable.Reply);
                AddReplyListPanel(pnlChild, childReplies);
            }
        }
    }

    private void AddReplyActionPanel(Panel pnlParent, Reply reply)
    {
        Panel replyActionPanel = new Panel();
        replyActionPanel.CssClass = "actionPanel";
        HyperLink hlReply = new HyperLink();
        hlReply.NavigateUrl = "~/CreateReply.aspx?responseToID=" + reply.ReplyID + "&responseToTable=" + Reply.ResponsesToTable.Reply;
        replyActionPanel.Controls.Add(hlReply);
    }

}