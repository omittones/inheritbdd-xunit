set bin="C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
set adapter=/TestAdapterPath:"%~dp0bin\Debug"

::%bin% /ListDiscoverers %adapter%
::%bin% %adapter% /ListTests:".\bin\Debug\InheritBDD.xUnit.exe"
%bin% .\bin\Debug\InheritBDD.xUnit.exe %adapter%