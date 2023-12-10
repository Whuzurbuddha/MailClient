using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Navigation;
using MailClient.DataController;
using MailClient.views;

namespace MailClient.viewmodels;

public class MailContentViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? _recipient;
    private string? _regarding;
    private string? _mailText;
    private static string? _filePath;

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

    public string? FilePath
    {
        get => _filePath;
        set
        {
            _filePath = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilePath)));
        }
    }

    private bool _mailStatus;
    private readonly string? _recipientMissing = "Empfängeradresse fehlt";
    private readonly string? _sendingSuccess = "Mail erfolgreich versendet";
    private readonly string? _sendingFailed = "Versandt fehlgeschlagen";

    public void GetFilePath()
    {
        FilePath = LoadFile.OpenDirectory();
    }
    public async Task<string?> SendMail()
    {
        if (Recipient == string.Empty) return _recipientMissing;
        _mailStatus = await EmailSender.SendingMail(Recipient, Regarding, MailText, FilePath);
        return _mailStatus ? _sendingSuccess : _sendingFailed;
    }
}