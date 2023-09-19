using System;
using System.Diagnostics;
using System.IO;
using System.Net;

class Program
{
    static void Main()
    {
        Console.WriteLine("Bitte wählen Sie eine Option:");
        Console.WriteLine("1. Zeige aktuelle Uhrzeit");
        Console.WriteLine("2. Lösche den FiveM Datenordner des aktuellen Benutzers");
        Console.WriteLine("3. Deinstalliere TeamSpeak 3 Client");
        Console.WriteLine("4. Lösche den SaltyChat-Plugin-Ordner des aktuellen Benutzers");
        Console.WriteLine("5. Herunterladen und Ausführen der TeamSpeak-Datei");
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
            case "0":
                return;
            default:
                Console.WriteLine("Ungültige Option. Bitte erneut wählen.");
                break;
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
        using (WebClient client = new WebClient())
        {
            string url = "https://files.teamspeak-services.com/releases/client/3.6.1/TeamSpeak3-Client-win64-3.6.1.exe"; // Hier die tatsächliche URL einfügen
            string fileName = "TeamSpeak3-Client-win64-3.6.1.exe"; // Hier den gewünschten Dateinamen festlegen

            client.DownloadFile(url, fileName);

            Process.Start(fileName);
        }
    }
}
