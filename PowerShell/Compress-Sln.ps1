function Zipfile{
    $Date=(Get-Date).ToString("yy-MM-dd-HHmm");
    $Folder=(Get-Item .).Name;
    return "C:/Development/Application/VS-Zip/$Folder-$Date.zip";
}
function Compress{
    $Backup ="../VS-Zip/$((Get-Item .).Name)Back";
    robocopy . $Backup /S /XD "node_modules" "obj" "bin" "wwwroot" ".vs" ".nuget" ".git" "packages" "*.Old" /XF "*.png" "*.jpg" "*.dll" "*.user" "*.data" "*.dbmdl" "*.cache" "*Backup*.rdl" "UpgradeLog*.htm" > null
    Compress-Archive -Path "$Backup/*" -DestinationPath (Zipfile) > null;
    Remove-Item $Backup  -Force -Recurse;
    Write-Host "Done..";
}
cls;
Compress;
