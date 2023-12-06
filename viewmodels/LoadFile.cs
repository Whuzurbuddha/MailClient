using Microsoft.Win32;

namespace MailClient.viewmodels;

public class LoadFile
{
    public static string OpenDirectory()
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == true)
        {
            return openFileDialog.FileName;
        }

        return string.Empty;
    }
}