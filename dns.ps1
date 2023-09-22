$primaryDNS = "8.8.8.8"
$secondaryDNS = "8.8.4.4"

$nic = Get-WmiObject Win32_NetworkAdapterConfiguration | Where-Object { $_.IPEnabled -eq $true }

if ($nic -ne $null) {
    $dns = @($primaryDNS, $secondaryDNS)
    $nic.SetDNSServerSearchOrder($dns)
    Write-Host "DNS-Server wurden erfolgreich geändert."
} else {
    Write-Host "Fehler: Netzwerkadapter nicht gefunden."
}
