param (
[switch]$Clean
)

$msbuild = $env:MSBUILD_PATH
if ($msbuild -eq $null -or -not (Test-Path $msbuild))
{
    $msbuild = (dir 'C:\Program Files (x86)\MSBuild*' -Recurse -Filter MSBuild.exe -ErrorAction Ignore | sort -Property FullName -Descending | select -First 1).FullName
    $env:MSBUILD_PATH = $msbuild
}

$numProcesses = '/m:4'
$verbosity = '/verbosity:minimal'

if (-not ($msbuild -eq $null))
{
    if ($Clean)
    {
        & $msbuild @('InheritBDD.xUnit.sln','/t:Clean', $numProcesses, $verbosity)
    }
    else
    {
        ..\tools\nuget.exe restore
        & $msbuild @('InheritBDD.xUnit.sln','/t:Build', $numProcesses, $verbosity)
    }
}
else
{
    Write-Error 'MSBuild not found in any of the C:\Windows\Microsoft.NET directories.'
}