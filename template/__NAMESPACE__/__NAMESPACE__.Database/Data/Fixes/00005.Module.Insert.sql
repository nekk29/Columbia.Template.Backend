DECLARE @User NVARCHAR(256) = 'administrator';
DECLARE @Date DATETIME = GETDATE();

DECLARE @ApplicationCode VARCHAR(32) = '__CLIENT_CODE__';
DECLARE @ApplicationId UNIQUEIDENTIFIER = (SELECT TOP 1 [Id] FROM [dbo].[Applications] WHERE [Code] = @ApplicationCode);

DECLARE @DataTable TABLE (
        [Id] INT IDENTITY(1, 1),
        [Code] VARCHAR(64),
        [Name] VARCHAR(256),
        [Description] VARCHAR(1024)
);

INSERT INTO @DataTable([Code], [Name], [Description])
-- SYSTEM MODULES
          SELECT 'applications', 'Applications', 'Applications management module'
UNION ALL SELECT 'modules', 'Modules', 'Modules management module'
UNION ALL SELECT 'menu-options', 'Menu Options', 'Menu options management module'
UNION ALL SELECT 'roles', 'Roles', 'Roles management module'
UNION ALL SELECT 'users', 'Users', 'Users management module'
UNION ALL SELECT 'settings', 'Settings', 'Settings management module'


INSERT INTO [dbo].[Modules] (
        [Id],
    [ApplicationId],
        [Code],
        [Name],
        [Description],
    [CreationUser],
    [CreationDate],
    [UpdateUser],
    [UpdateDate],
    [IsActive]
)
SELECT
        NEWID(),
    @ApplicationId,
        [Code],
        [Name],
        [Description],
    @User,
    @Date,
    @User,
    @Date,
    1
FROM @DataTable [dt]
WHERE NOT EXISTS (
        SELECT 1 FROM [dbo].[Modules] [m]
        WHERE 1 = 1
                AND [m].[Code] = [dt].[Code]
) AND @ApplicationId IS NOT NULL;
