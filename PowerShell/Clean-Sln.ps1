function Delete{
param([string[]] $Items)
cls;
$itemsToDelete = Get-ChildItem . -recurse -force -include $Items;
if ($itemsToDelete) {
  foreach ($item in $itemsToDelete) {
    Remove-Item $item.FullName -Force -Recurse -ErrorAction SilentlyContinue;
    Write-Host "Deleted" $item.FullName;
  }
}
Write-Host "Press any key to continue . . .";
}

Delete ("*.suo","*.user","*.log","_ReSharper.*","bin","obj","node_modules","packages");