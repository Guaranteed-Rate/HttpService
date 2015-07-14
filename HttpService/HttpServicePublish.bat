del *.nupkg
nuget pack HttpService.csproj -Prop Platform=AnyCPU -Prop Configuration=Release
nuget push *.nupkg