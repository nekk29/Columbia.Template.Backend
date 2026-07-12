DECLARE @ClientId VARCHAR(200) = '__CLIENT_CODE__'

DECLARE @ColumnValue VARCHAR(MAX)

DECLARE @DataTable TABLE (
	[Id] INT IDENTITY(1, 1),
	[Name] VARCHAR(256)
);

INSERT INTO @DataTable([Name])
-- Web App - Local
		  SELECT 'https://localhost:4200/auth/signout'
-- Web App - Dev
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-dev.azurewebsites.net/auth/signout'
-- Web App - Test
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-test.azurewebsites.net/auth/signout'
-- Web App - Stage
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-stage.azurewebsites.net/auth/signout'
-- Web App - Prod
UNION ALL SELECT 'https://app-__CLIENT_CODE__-web-prod.azurewebsites.net/auth/signout'

SELECT
	@ColumnValue = '[' + STRING_AGG('"' + [Name] + '"', ',') + ']'
FROM @DataTable

UPDATE [dbo].[OpenIddictApplications] SET
	[PostLogoutRedirectUris] = @ColumnValue
WHERE [ClientId] = @ClientId
