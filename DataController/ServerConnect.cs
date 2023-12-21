using System.Threading.Tasks;
using MailKit.Net.Imap;

namespace MailClient.DataController;

public class ServerConnect
{
    public class User
    {
        private static readonly ReadJson.UserContent UserContent = ReadJson.GetUserContent();
        public static readonly string? UserMail = UserContent.User;
        private static readonly string? EncryptedPasswd = UserContent.EncryptedPasswd;
        public static readonly string? Password = ContentManager.DecryptedPasswd(EncryptedPasswd);
        public static readonly string? Imap = UserContent.Imap;
    }

    public class Client : ImapClient
    {
        public static readonly ImapClient Imap = new ImapClient();
        public static async Task Connect()
        {
            await Imap.ConnectAsync(User.Imap, 933, true);
            await Imap.AuthenticateAsync(User.UserMail, User.Password);
        }

        public async Task Disconnect()
        {
            await Imap.DisconnectAsync(true);
        }

        public void KillClient()
        {
            Imap.Dispose();
        }
    }
}