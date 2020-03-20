Set-Location 'C:\Temp\SQL\Temp';
Clear-Host;
$Servers = (
@{Name='SandBox';Server='S01'},
@{Name='Production';Server='P01'},
@{Name='Demo';Server='D01'},
@{Name='Test 01';Server='T01'},
@{Name='Test 02';Server='T02'},
@{Name='Test 03';Server='T03'},
@{Name='Selenium';Server='T01'}
);
$File='Run1.sql';
$Date=(Get-Date).ToString('yy-MM-dd HH-mm-ss');
Get-Content $File | Out-File -FilePath "Out\$($Date). Sql.txt"
foreach($Server in $Servers){
%{
	$T1=Get-Date 
	'Start: '+$T1;
	Invoke-Sqlcmd -InputFile $File   -ServerInstance $Database.Server 
	$T2=Get-Date
	'End:  '+$T2;
	'Milliseconds: ' + ($T2-$T1).Milliseconds ;
} | Out-File -FilePath "Out\$($Date).$($Database.Name).txt"