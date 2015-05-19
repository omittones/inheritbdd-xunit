::"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" /TestAdapterPath:"%~dp0..\..\packages\xunit.runner.visualstudio.2.0.0\build\_common\xunit.runner.visualstudio.testadapter.dll" %*

set bin="C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" 

%bin% /ListTests:".\bin\Debug\Perform.EvaluationRating.Tests.exe" "/TestAdapterPath:%~dp0..\..\packages\xunit.runner.visualstudio.2.0.0\build\_common\"
::%bin% /ListDiscoverers "/TestAdapterPath:%~dp0..\..\packages\xunit.runner.visualstudio.2.0.0\build\_common\"
::%bin% .\bin\Debug\Perform.EvaluationRating.Tests.exe "/TestAdapterPath:%~dp0..\..\packages\xunit.runner.visualstudio.2.0.0\build\_common\"
