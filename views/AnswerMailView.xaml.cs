using System;

namespace MailClient.views;

public partial class AnswerMailView
{
    public AnswerMailView()
    {
        InitializeComponent();
        var mailInbox = new MailInbox();
        var mailText = mailInbox.SelectedMail();
        ShowSelectedMail(mailText);
    }

    private void ShowSelectedMail(string? mailText)
    {
        SelectedMailText.Text = mailText;
    }
}