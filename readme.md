run  kubectl apply -f ./k8s within src folder
hit http://localhost:6500/


docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=6Tpeople" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest