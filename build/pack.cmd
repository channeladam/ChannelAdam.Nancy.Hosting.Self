SET msbuild="C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"

%msbuild% ..\src\ChannelAdam.Nancy.Hosting.Self\ChannelAdam.Nancy.Hosting.Self.csproj /t:Rebuild /p:Configuration=Release;TargetFrameworkVersion=v4.0;OutDir=bin\Release\net40

..\tools\nuget\nuget.exe pack ..\src\ChannelAdam.Nancy.Hosting.Self\ChannelAdam.Nancy.Hosting.Self.nuspec -Symbols

pause
