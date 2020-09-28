# Payments API

Welcome to the Payments API!

### Steps to run
- To run the payments api you will need ".net core 3.1" and a "mysql" instace
- The payments api uses a mysql connection, at the root folder we have a docker-compose file to help you to create a instace, 
if you want to use a custom instance, you can edit the string connection at "./Payments.Api/Properties/launchSettings.json" environmens.
`````
$ docker-compose up
`````
- Than you can restore packages, build and run
`````
$ dotnet restore
$ dotnet run --project Payments.Api
`````

### Routes:

To use Payments Api you can use this two routes:

`````
Route: https://localhost:5001/Payment
Method: Post
Body:
{
    "nome": string
    "valorOriginal": decimal
    "dataPagamento": date
    "dataVencimento": date
}
`````

`````
Route: https://localhost:5001/Payment
Method: Get
Returns:
{
    "nome": string
    "valor": decimal
    "valorCorrigido": decimal
    "dataPagamento": date
    "dataVencimento": date
}
`````
