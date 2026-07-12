DECLARE @User NVARCHAR(256) = 'administrator';
DECLARE @Date DATETIME = GETDATE();

DECLARE @ApplicationCode VARCHAR(32) = '__CLIENT_CODE__';
DECLARE @ApplicationId UNIQUEIDENTIFIER = (SELECT TOP 1 [Id] FROM [dbo].[Applications] WHERE [Code] = @ApplicationCode);

DECLARE @DataTable TABLE (
        [Id] INT IDENTITY(1, 1),
        [Name] VARCHAR(256)
);

INSERT INTO @DataTable([Name])
          SELECT 'System Admin'


INSERT INTO [dbo].[AspNetRoles] (
    [Id],
    [ApplicationId],
    [Name],
    [NormalizedName],
    [ConcurrencyStamp],
    [CreationUser],
    [CreationDate],
    [UpdateUser],
    [UpdateDate],
    [IsActive]
)
SELECT
    NEWID(),
    @ApplicationId,
    [dt].[Name],
    UPPER([dt].[Name]),
    NEWID(),
    @User,
    @Date,
    @User,
    @Date,
    1
FROM @DataTable [dt]
WHERE NOT EXISTS (
    SELECT TOP 1 1 FROM [dbo].[AspNetRoles] WHERE [Name] = [dt].[Name]
) AND @ApplicationId IS NOT NULL;
