using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Management;
class Program
{
    private const string OvoticUrl = "https://cdn.ovotic.de/license/main.php";

    static void Main(string[] args)
    {
        Console.Title = "Launching...";

        Console.WriteLine("[i] Installing ");
        System.Threading.Thread.Sleep(1000);

        Console.WriteLine("[i] Fetching Informations ");
        System.Threading.Thread.Sleep(1500);
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("[i] Connecting to License System (Frankfurt) ");
        if (CheckConnectionToOvotic())
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("[i] Verbindung zu Server #1 erfolgreich.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("[i] Verbindung zu ovotic.de fehlgeschlagen.");
        }
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("[i] Getting PC Infos...");
        System.Threading.Thread.Sleep(1000);

        if (IsAdmin())
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("[i] Admin Access allowed");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;


            Console.Clear();

        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Title = "Execute as Admin!!!";

            Console.WriteLine("[i] Please execute as Admin! The CMD will shutdown!");
            System.Threading.Thread.Sleep(1000);

            Console.ForegroundColor = ConsoleColor.White;

            return; 
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.Title = "Gib deine Daten ein...";

        Console.Write("Bitte Lizenz eingeben: ");
        string license = Console.ReadLine();

        Console.Write("Bitte ID eingeben: ");
        int id = Convert.ToInt32(Console.ReadLine());

        string clientIp = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();

        if (CheckLicense(license, id, clientIp))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "Lizenz gültig!";

            Console.WriteLine("Lizenz gültig. Zugriff erlaubt.");
            Console.ForegroundColor = ConsoleColor.White;

            ShowOptions();

        }
        else
        {
            Console.Title = "Lizenz ungültig!";

            Console.ForegroundColor = ConsoleColor.Red;

            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("Ungültige Lizenz, ID oder IP. Zugriff verweigert.");
            Console.ForegroundColor = ConsoleColor.White;

        }
    }

    static bool CheckLicense(string license, int id, string clientIp)
    {
        string url = $"https://cdn.ovotic.de/license/main.php?license={license}&id={id}&ip={clientIp}";
        string phpResponse;
        using (WebClient client = new WebClient())
        {
            phpResponse = client.DownloadString(url);
        }

        return bool.Parse(phpResponse);
    }
    static bool CheckConnectionToOvotic()
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(OvoticUrl);
                return !string.IsNullOrEmpty(response);
            }
        }
        catch (Exception)
        {
            return false;
        }
        
    }
    static bool IsAdmin()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new WindowsPrincipal(identity);

        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }



    static void ShowOptions()
    {
        string clientIp = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();
        string description = GetWindowsVersion();

        while (true)
        {
            Console.Title = "SURVIVALCITY ON TOP";

            System.Threading.Thread.Sleep(5000);
            Console.Clear();
            DateTime currentDateTime = DateTime.Now;
            Console.WriteLine("Aktuelles Datum: " + DateTime.Now);
            string pc = " " + System.Environment.MachineName;
            Console.WriteLine("Eingeloggt als: " + pc  +"/" + clientIp);
            Console.WriteLine("Version" + description);
            Console.WriteLine("===========================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Bitte wählen Sie eine Option:");
            Console.WriteLine("1. Zeige aktuelle Uhrzeit");
            Console.WriteLine("2. Lösche den FiveM Datenordner des aktuellen Benutzers");
            Console.WriteLine("3. Deinstalliere TeamSpeak 3 Client");
            Console.WriteLine("4. Lösche den SaltyChat-Plugin-Ordner des aktuellen Benutzers");
            Console.WriteLine("5. Herunterladen und Ausführen der TeamSpeak-Datei");
            Console.WriteLine("6. Saltychat herunterladen und starten");
            Console.WriteLine("7. DNS setzen");
            Console.WriteLine("9. Auf SurvivalCity connecten");
            Console.WriteLine("8. Zeige IP-Adresse");

            Console.WriteLine("0. Beenden");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("===========================");



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
                    UninstallTeamSpeak();
                    break;
                case "4":
                    DeleteSaltyChatPluginFolder();
                    break;
                case "5":
                    DownloadAndExecuteTeamSpeakFile();
                    break;
                case "6":
                    DownloadAndExecuteSaltychat();
                    break;
                case "7":
                    SetDNSServers("8.8.8.8", "8.8.4.4");
                    break;
                case "8":
                    Console.WriteLine(clientIp);
                    break;
                case "9":
                    Console.WriteLine("Connecte auf SurvivalCity...");
                    Process.Start("ts3server://survivalcity");
                    Process.Start("fivem://dbjpmj");

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
        string fivemFolder = $@"C:\Users\{userName}\AppData\Local\FiveM\FiveM.app\data";

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
        string saltyChatPluginFolder = $@"C:\Users\{userName}\AppData\Roaming\TS3Client\plugins\SaltyChat"; 
        Console.WriteLine("Closing Teamspeak...");
        CloseTeamSpeak();

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
    static void UninstallTeamSpeak()
    {
        try
        {
            string uninstallPath = @"C:\Program Files\TeamSpeak 3 Client\Uninstall.exe";

            if (System.IO.File.Exists(uninstallPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = uninstallPath;
                Process.Start(startInfo);
                Console.WriteLine("Deinstallationsprogramm gestartet.");
            }
            else
            {
                Console.WriteLine("Das Deinstallationsprogramm wurde nicht gefunden.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Ausführen des Deinstallationsprogramms: " + ex.Message);
        }
    }
    static void DownloadAndExecuteSaltychat()
    {
        try
        {
            Console.WriteLine("Starte den Download von Saltychat");

            string downloadUrl = "https://cdn.ovotic.de/license/SaltyChat.ts3_plugin";
            string fileName = "SaltyChat.ts3_plugin";
            string downloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(downloadUrl, downloadPath);
                Console.WriteLine("Download abgeschlossen.");
                System.Threading.Thread.Sleep(3000);

                Process.Start("ts3server://survivalcity");

            }

            Console.WriteLine("Starte die Installation von Saltychat...");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = downloadPath;
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Herunterladen/Installieren des TeamSpeak 3 Clients: " + ex.Message);
        }
    }
    static void DownloadAndExecuteTeamSpeakFile()
    {
        try
        {
            Console.WriteLine("Starte den Download des TeamSpeak 3 Clients...");

            string downloadUrl = "https://cdn.ovotic.de/license/Teamspeak.exe";
            string fileName = "Teamspeak.exe";
            string downloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(downloadUrl, downloadPath);
                Console.WriteLine("Download abgeschlossen.");
                Process.Start("ts3server://survivalcity");

            }

            Console.WriteLine("Starte die Installation des TeamSpeak 3 Clients...");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = downloadPath;
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Herunterladen/Installieren des TeamSpeak 3 Clients: " + ex.Message);
        }
    }

    static void SetDNSServers(string primaryDNS, string secondaryDNS)
    {
        // PowerShell-Skript erstellen und speichern
        string scriptContent = @"
Set-ExecutionPolicy RemoteSigned -Scope Process

$primaryDNS = '8.8.8.8'
$secondaryDNS = '8.8.4.4'

$nic = Get-WmiObject Win32_NetworkAdapterConfiguration | Where-Object { $_.IPEnabled -eq $true }

if ($nic -ne $null) {
    $dns = @($primaryDNS, $secondaryDNS)
    $nic.SetDNSServerSearchOrder($dns)
    Write-Host 'DNS-Server wurden erfolgreich geändert.'
} else {
    Write-Host 'Fehler: Netzwerkadapter nicht gefunden.'
}
";

        // Pfad zum PowerShell-Skript
        string scriptPath = Path.Combine(Path.GetTempPath(), "TempScript.ps1");

        // Skript im temporären Verzeichnis speichern
        File.WriteAllText(scriptPath, scriptContent);

        // Starte PowerShell mit dem Skript
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-ExecutionPolicy Bypass -File \"{scriptPath}\"",
            Verb = "runas", // Starte mit Administratorrechten
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process
        {
            StartInfo = startInfo
        };

        process.Start();

        // Warte auf das Ende der PowerShell-Ausführung
        process.WaitForExit();

        // Lösche das PowerShell-Skript
        File.Delete(scriptPath);

        // Gib die Ausgabe der PowerShell aus (falls benötigt)
        string output = process.StandardOutput.ReadToEnd();
        Console.WriteLine(output);

        // Prüfe den Exit-Code (0 bedeutet Erfolg)
        int exitCode = process.ExitCode;
        Console.WriteLine($"Exit-Code: {exitCode}");

        // Überprüfe den Exit-Code und handle entsprechend
        if (exitCode == 0)
        {
            Console.WriteLine("Erfolgreich!");
        }
        else
        {
            Console.WriteLine("Fehler!");
        }
    }
    
    
    
        
    static void CloseTeamSpeak()
    {
        Process[] processes = Process.GetProcessesByName("ts3client_win64");

        foreach (Process process in processes)
        {
            process.Kill();
        }

        Console.WriteLine("TeamSpeak 3 Client wurde geschlossen.");
    }
    static string GetWindowsVersion()
    {
        OperatingSystem os = Environment.OSVersion;
        
        if (os.Platform == PlatformID.Win32NT)
        {
            Version version = os.Version;

            if (version.Major == 10 && version.Minor == 0)
            {
                return "10";
            }
            else if (version.Major == 6 && version.Minor == 3)
            {
                return "8.1";
            }
            else if (version.Major == 6 && version.Minor == 2)
            {
                return "8";
            }
            else if (version.Major == 6 && version.Minor == 1)
            {
                return "7";
            }
            else if (version.Major == 6 && version.Minor == 0)
            {
                return "Vista";
            }
            else if (version.Major == 5 && version.Minor == 1)
            {
                return "XP";
            }
        }

        return null;
    }
}

    
