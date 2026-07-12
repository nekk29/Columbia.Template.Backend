DECLARE @ClientId VARCHAR(200) = '__CLIENT_CODE__'

DECLARE @ColumnValue VARCHAR(MAX)

DECLARE @DataTable TABLE (
	[Id] INT IDENTITY(1, 1),
	[Name] VARCHAR(256)
);

INSERT INTO @DataTable([Name])
-- Web App - Local
		  SELECT 'https://localhost:4200/auth/signin'
UNION ALL SELECT 'https://localhost:4200/silent-refresh.html'
-- Web App - Dev
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-dev.azurewebsites.net/auth/signin'
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-dev.azurewebsites.net/silent-refresh.html'
-- Web App - Test
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-test.azurewebsites.net/auth/signin'
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-test.azurewebsites.net/silent-refresh.html'
-- Web App - Stage
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-stage.azurewebsites.net/auth/signin'
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-stage.azurewebsites.net/silent-refresh.html'
-- Web App - Prod
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-prod.azurewebsites.net/auth/signin'
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-prod.azurewebsites.net/silent-refresh.html'
-- Web Apis - Local
UNION ALL SELECT 'https://localhost:7202/oauth2-redirect.html'
UNION ALL SELECT 'https://myapp.local:7202/oauth2-redirect.html'
-- Web Apis - Dev
UNION ALL SELECT 'https://app-__CLIENT_CODE__-apis-dev.azurewebsites.net/oauth2-redirect.html'
-- Web Apis - Test
UNION ALL SELECT 'https://app-__CLIENT_CODE__-apis-test.azurewebsites.net/oauth2-redirect.html'
-- Web Apis - Stage
UNION ALL SELECT 'https://app-__CLIENT_CODE__-apis-stage.azurewebsites.net/oauth2-redirect.html'
-- Web Apis - Prod
UNION ALL SELECT 'https://app-__CLIENT_CODE__-apis-prod.azurewebsites.net/oauth2-redirect.html'

SELECT
	@ColumnValue = '[' + STRING_AGG('"' + [Name] + '"', ',') + ']'
FROM @DataTable

UPDATE [dbo].[OpenIddictApplications] SET
	[RedirectUris] = @ColumnValue
WHERE [ClientId] = @ClientId
