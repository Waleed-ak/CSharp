

function Pause
{
	Write-Host -NoNewLine "Press any key to continue . . . ";
	[Console]::ReadKey($true) | Out-Null;
	Write-Host;
}

function Set-Version{
    param([object[]] $Items,[string[]] $Nodes)
    $Year =(Get-Date -UFormat %Y);
    $Week= (Get-Date -UFormat %V);
    $Ver=10;

    if ($Items) {
        foreach ($item in $Items) {
            $file = $item.FullName;
            $xml = New-Object XML;
            $xml.Load($file);
            $flag=1;
            foreach ($node in $Nodes) {
                $element =  $xml.SelectSingleNode("//$node");
                if($element){
                if($flag){
                    Write-Host "Path $($item.FullName)";
                    $flag=0;
                 }
                $Counter=1;
                $Values=$element.InnerText.Split('.') | Foreach{ [int]::Parse($_)}
                if($Values.Length -eq 4 -and  $Values[2] -eq $Week){
                    $Counter =$Values[3]+1;
                }
                Write-Host "Node $node  ($Year.$ver.$Week.$Counter)";
                $element.InnerText ="$Year.$ver.$Week.$Counter";
                }
            }
            $xml.Save($file);
        }
    }
}

cls

$itemsToWprk = Get-ChildItem . -recurse -force -include ("*.csproj")
$Nodes = ("AssemblyVersion","FileVersion","Version")

Set-Version $itemsToWprk  $Nodes

$itemsToWprk = Get-ChildItem . -recurse -force -include ("*.nuspec")
$Nodes = ("version")

Set-Version $itemsToWprk  $Nodes
Write-Host -NoNewLine "Done any key to continue . . . ";
