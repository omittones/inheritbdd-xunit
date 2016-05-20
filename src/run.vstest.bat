set bin=C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe
set project=InheritBDD.xUnit.Testing
set adapter=/TestAdapterPath:"%~dp0%project%\bin\Debug"

"%bin%" ".\%project%\bin\Debug\%project%.exe" %adapter%