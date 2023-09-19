using System;
using System.Diagnostics;
using System.IO;
using System.Net;


class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Bitte wählen Sie eine Option:");
            Console.WriteLine("1. Zeige aktuelle Uhrzeit");
            Console.WriteLine("2. Lösche den FiveM Datenordner des aktuellen Benutzers");
            Console.WriteLine("3. Deinstalliere TeamSpeak 3 Client");
            Console.WriteLine("4. Lösche den SaltyChat-Plugin-Ordner des aktuellen Benutzers");
            Console.WriteLine("5. Herunterladen und Ausführen der TeamSpeak-Datei");
            Console.WriteLine("6. Setze DNS-Server auf Google DNS");
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
                case "3":
                    UninstallTeamSpeak3Client();
                    break;
                case "4":
                    DeleteSaltyChatPluginFolder();
                    break;
                case "5":
                    DownloadAndExecuteTeamSpeakFile();
                    break;
                case "6":
                    SetGoogleDNS();
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

    static void UninstallTeamSpeak3Client()
    {
        Process.Start(@"C:\Program Files\TeamSpeak 3 Client\Uninstall.exe");
        Console.WriteLine("Der TeamSpeak 3 Client wird deinstalliert.");
    }
    static void SetGoogleDNS()
    {
        Process process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/C netsh interface ipv4 set dnsservers name=\"Ethernet\" static 8.8.8.8 primary validate=no";
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        process.WaitForExit();

        process.StartInfo.Arguments = "/C netsh interface ipv4 add dnsservers name=\"Ethernet\" 8.8.4.4 index=2 validate=no";
        process.Start();
        process.WaitForExit();

        Console.WriteLine("DNS-Server wurde auf Google DNS (8.8.8.8 und 8.8.4.4) geändert.");
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
        string url = "https://workupload.com/start/JpEGXkYfnWJ";
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
            Process.Start(fileName);
            Console.WriteLine("TeamSpeak-Datei wird ausgeführt.");
        }
        else
        {
            Console.WriteLine("Fehler beim Herunterladen der TeamSpeak-Datei.");
        }
    }
}
