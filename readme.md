run  kubectl apply -f ./k8s within src folder
hit http://localhost:6500/


docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=6Tpeople" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest


root@d35b5181c3d9:/# .. opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 6Tpeople 
select name from sys.Databases
go
use FootballDb
go
select * from Players
go


docker build -t squigs/annoyingapi:1.0 .

docker run -d -p 8080:80 --name ballsup squigs/annoyingclient:1.0