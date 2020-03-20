Set-Location 'C:\Temp\SQL\Temp';
Clear-Host;
$Servers = (
@{Name='SandBox';Server='S01'},
@{Name='Production';Server='P01'},
@{Name='Demo';Server='D01'},
@{Name='Test 01';Server='T01'},
@{Name='Test 02';Server='T02'},
@{Name='Test 03';Server='T03'},
@{Name='Selenium';Server='SEL'}
);
$File='Run1.sql';
foreach($Server in $Servers){
    $T1=Get-Date 
    Write-Host  $($Server.Name);
    Write-Host  'Start: '$T1;
    Invoke-Sqlcmd -InputFile $File -ServerInstance $Server.Server 
    $T2=Get-Date
    Write-Host  'End  : '$T2;
    Write-Host  'Milliseconds: ' ($T2-$T1).Milliseconds ;
    Write-Host  '#####################################################################################'
}