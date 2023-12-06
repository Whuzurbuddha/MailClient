using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MailClient.DataController;
using MailClient.views;

namespace MailClient.viewmodels;

public class SendMailViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _recipient;
    private string? _regarding;
    private string? _mailText;
    private static readonly SendMailView.Files Files = new SendMailView.Files();
    private string? _fileSource = Files.ToString();

    public string? Recipient
    {
        get => _recipient;
        set
        {
            _recipient = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recipient)));
        }
    }

    public string? Regarding
    {
        get => _regarding;
        set
        {
            _regarding = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Regarding)));
        }
    }

    public string? MailText
    {
        get => _mailText;
        set
        {
            _mailText = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailText)));
        }
    }

    public string? FileSource
    {
        get => _fileSource;
        set
        {
            _fileSource = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(FileSource));
        }
    }

    private bool _mailStatus;
    private readonly string? _recipientMissing = "Empfängeradresse fehlt";
    private readonly string? _sendingSuccess = "Mail erfolgreich versendet";
    public async Task<string?> SendMail()
    {
        Console.WriteLine(FileSource);
        /*if(Recipient != null && MailText != null) _mailStatus = await EmailSender.SendingMail(Recipient, Regarding, MailText, FileSource);
        return Recipient == null ? _recipientMissing : _sendingSuccess;*/
        return null;
    }
}