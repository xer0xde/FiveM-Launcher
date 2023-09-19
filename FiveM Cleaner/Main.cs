using System;
using System.Diagnostics;
using System.IO;
using System.Net;


class Program
{
    static void Main(string[] args)
    {
        System.Threading.Thread.Sleep(1000);

        while (true)
        {
            Console.WriteLine("Bitte wählen Sie eine Option:");
            Console.WriteLine("1. Zeige aktuelle Uhrzeit");
            Console.WriteLine("2. Lösche den FiveM Datenordner des aktuellen Benutzers");
            Console.WriteLine("3. Deinstalliere TeamSpeak 3 Client");
            Console.WriteLine("4. Lösche den SaltyChat-Plugin-Ordner des aktuellen Benutzers");
            Console.WriteLine("5. Herunterladen und Ausführen der TeamSpeak-Datei");
            Console.WriteLine("6. Saltychat herunterladen und starten");
            Console.WriteLine("7. DNS");
            Console.WriteLine("0. Beenden");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ShowCurrentTime();
                    break;
                case "2":
                    DeleteCurrentUserFiveMDataFolder();
                    break;
                case "4":
                    DeleteSaltyChatPluginFolder();
                    break;
                case "5":
                    DownloadAndExecuteTeamSpeakFile();
                    break;
                case "6":
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Ungültige Option. Bitte erneut wählen.");
                    break;
            }
        }
    }

    static void ShowCurrentTime()
    {
        DateTime now = DateTime.Now;
        Console.WriteLine("Aktuelle Uhrzeit: " + now);
    }

    static void DeleteCurrentUserFiveMDataFolder()
    {
        string userName = Environment.UserName;
        string fivemFolder = @"C:\Users\" + userName + @"\AppData\Local\FiveM\FiveM.app\data";

        if (Directory.Exists(fivemFolder))
        {
            Directory.Delete(fivemFolder, true);
            Console.WriteLine("Der FiveM Datenordner des aktuellen Benutzers wurde gelöscht.");
        }
        else
        {
            Console.WriteLine("Der FiveM Datenordner des aktuellen Benutzers existiert nicht.");
        }
    }
    

    static void DeleteSaltyChatPluginFolder()
    {
        string userName = Environment.UserName;
        string saltyChatPluginFolder = @"C:\Users\" + userName + @"\AppData\Roaming\TS3Client\plugins\SaltyChat";

        if (Directory.Exists(saltyChatPluginFolder))
        {
            Directory.Delete(saltyChatPluginFolder, true);
            Console.WriteLine("Der SaltyChat-Plugin-Ordner des aktuellen Benutzers wurde gelöscht.");
        }
        else
        {
            Console.WriteLine("Der SaltyChat-Plugin-Ordner des aktuellen Benutzers existiert nicht.");
        }
    }
    static void DownloadAndExecuteTeamSpeakFile()
    {
        string url = "https://f69.workupload.com/download/JpEGXkYfnWJ";
        string userName = Environment.UserName;
        string downloadsPath = $@"C:\Users\{userName}\Downloads";
        string fileName = Path.Combine(downloadsPath, "TeamSpeak3-Client-win64-3.6.1.exe");

        if (!Directory.Exists(downloadsPath))
        {
            Directory.CreateDirectory(downloadsPath);
        }

        using (WebClient client = new WebClient())
        {
            client.DownloadFile(url, fileName);
            Console.WriteLine("TeamSpeak-Datei wurde heruntergeladen.");
        }

        if (File.Exists(fileName))
        {
            System.Threading.Thread.Sleep(15000);
            Process.Start(fileName);
            Console.WriteLine("TeamSpeak-Datei wird ausgeführt.");
        }
        else
        {
            Console.WriteLine("Fehler beim Herunterladen der TeamSpeak-Datei.");
        }
    }
}
