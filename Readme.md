### Game Store Api

## Starting SQL Server

```powershell

$sa_password="msqlpass"

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=msqlpass" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest

```
