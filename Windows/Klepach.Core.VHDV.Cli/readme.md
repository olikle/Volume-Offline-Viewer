# config
dotnet add Klepach.Core.VHDV.Cli.csproj package System.Configuration.ConfigurationManager

## dotnet channel
https://www.youtube.com/channel/UCvtT19MZW8dq5Wwfu6B0oxw

https://www.bing.com/videos/search?q=c%23+core+entity+framework&docid=608031154313571468&mid=4D424A023E581F8F3F2B4D424A023E581F8F3F2B&view=detail&FORM=VIRE
https://www.youtube.com/results?search_query=csharp+fritz+entity
https://www.youtube.com/watch?v=4LRUWCfGLIs

## add ef packages
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SQLite
dotnet add package Microsoft.EntityFrameworkCore.Tools

## list tools
dotnet tool list
dotnet tool list --global
## add ef tool
dotnet tool install dotnet-ef --global

## add changes
dotnet ef migrations add AddDiskPartitionAndFile
## remove the last change
dotnet ef migrations remove 

## list the structure
dotnet ef dbcontext list
dotnet ef dbcontext list


## generate table
dotnet ef database create
dotnet ef database update