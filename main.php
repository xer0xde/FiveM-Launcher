<?php
$licenses = [
    [
        "key" => "SC1",
        "ip" => "192.168.0.1",
        "discord_id" => "1234567890"
    ],
    [
        "key" => "DEF456",
        "ip" => "192.168.0.2",
        "discord_id" => "0987654321"
    ]
    // Hier weitere Lizenzen hinzufügen...
];

$licenseKey = $_POST['license_key'];
$clientIP = $_SERVER['REMOTE_ADDR'];
$discordID = $_POST['discord_id']; // Die Discord-ID des Benutzers

$validLicense = false;

foreach ($licenses as $license) {
    if ($license['key'] === $licenseKey && $license['ip'] === $clientIP) {
        $validLicense = true;
        break;
    }
}

if ($validLicense) {
    $currentTime = date("Y-m-d H:i:s");
    $embedData = [
        "content" => "<@{$discordID}> hat sich erfolgreich eingeloggt!",
        "embeds" => [
            [
                "title" => "Erfolgreicher Login",
                "description" => "IP-Adresse: {$clientIP}\nUhrzeit: {$currentTime}",
                "color" => hexdec("00FF00")
            ]
        ]
    ];

    $webhookURL = "https://discord.com/api/webhooks/1114661796431802428/NxHNnaDlSOGJlAZq-IZsJ3K5nRxvcrODfBy6428GYooyAeIf-1EwJ7BoK_HbHGm6U_bS"; // Deine Discord-Webhook-URL
    $ch = curl_init($webhookURL);
    curl_setopt($ch, CURLOPT_HTTPHEADER, ["Content-Type: application/json"]);
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($embedData));
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_exec($ch);
    curl_close($ch);

    echo "Erfolgreich eingeloggt!";
} else {
    echo "Ungültige Lizenz oder IP.";
}
?>
