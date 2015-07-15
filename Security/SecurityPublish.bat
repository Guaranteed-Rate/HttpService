del *.nupkg
nuget pack Security.csproj -Prop Platform=AnyCPU -Prop Configuration=Release
nuget push *.nupkg