using System.ComponentModel;
using System.Threading.Tasks;
using MailClient.DataController;

namespace MailClient.viewmodels;

public class SendMailViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _recipient;
    private string? _regarding;
    private string? _mailText;

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

    private bool _mailStatus;
    private readonly string? _recipientMissing = "Empfängeradresse fehlt";
    private readonly string? _sendingSuccess = "Mail erfolgreich versendet";
    public async Task<string?> SendMail()
    {
        if(Recipient != null && MailText != null) _mailStatus = await EmailController.SendingMail(Recipient, Regarding, MailText);
        return Recipient == null ? _recipientMissing : _sendingSuccess;
    }
}