# Tool installation
dotnet tool restore
dotnet new tool-manifest
dotnet tool install dotnet-ef --version 10.0.7 --global

# Run migrations
dotnet ef migrations add CoreMigration_InitialMigration --context CoreDbContext
dotnet ef database update --context CoreDbContext

# Run Shakermaker.SqlServer
dotnet tool install --global Shakermaker.SqlServer
Shakermaker.SqlServer --source-directory "__NAMESPACE__.Database" --release "0.0.1" --environment "Dev" --connection-string "Server=tcp:sql-han-__CLIENT_CODE__-dev.database.windows.net,1433;Initial Catalog=sqldb-han-__CLIENT_CODE__-dev;Persist Security Info=False;User ID=han-__CLIENT_CODE__-admin;Password=c3uf3cCLq6KzXs9kKCpj;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# Local domain configuration
# Add to hosts file
127.0.0.1	myapp.local

# Update launchSettings.json
"applicationUrl": "https://myapp.local:7202;http://myapp.local:5202"

# Create self-signed certificate for myapp.local
mkcert -pkcs12 myapp.local

# Update appsettings.json
{
  "Kestrel": {
    "Endpoints": {
      "Https": {
        "Url": "https://myapp.local:7202",
        "Certificate": {
          "Path": "[PATH_TO_CERTIFICATE]/myapp.local.p12",
          "Password": "changeit"
        }
      }
    }
  }
}