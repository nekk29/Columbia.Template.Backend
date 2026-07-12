DECLARE @User NVARCHAR(256) = 'administrator';
DECLARE @Date DATETIME = GETDATE();

DECLARE @ActionId UNIQUEIDENTIFIER
DECLARE @ActionCode VARCHAR(64)

DECLARE @ParentId UNIQUEIDENTIFIER
DECLARE @ParentCode VARCHAR(64)

DECLARE @Code VARCHAR(64)
DECLARE @Name VARCHAR(256)
DECLARE @Description VARCHAR(1024)
DECLARE @MenuUri VARCHAR(MAX)
DECLARE @MenuIcon VARCHAR(MAX)
DECLARE @SortOrder INT

DECLARE @ApplicationCode VARCHAR(32) = '__CLIENT_CODE__';
DECLARE @ApplicationId UNIQUEIDENTIFIER = (SELECT TOP 1 [Id] FROM [dbo].[Applications] WHERE [Code] = @ApplicationCode);

DECLARE @DataTable TABLE (
        [Id] INT IDENTITY(1, 1),
        [ActionCode] VARCHAR(64),
        [ParentCode] VARCHAR(64),
        [Code] VARCHAR(64),
        [Name] VARCHAR(256),
        [Description] VARCHAR(1024),
        [MenuUri] VARCHAR(MAX),
        [MenuIcon] VARCHAR(MAX),
        [SortOrder] INT
);

INSERT INTO @DataTable([ActionCode], [ParentCode], [Code], [Name], [Description], [MenuUri], [MenuIcon], [SortOrder])
-- SYSTEM MENUOPTIONS
          SELECT 'applications.search', NULL, 'applications.search', 'Applications', 'Applications Search', '/applications', 'burger-menu-3', 0
UNION ALL SELECT 'modules.search', NULL, 'modules.search', 'Modules', 'Modules Search', '/modules', 'category', 1
UNION ALL SELECT 'menu-options.search', NULL, 'menu-options.search', 'Menu Options', 'Menu Options Search', '/menu-options', 'menu', 2
UNION ALL SELECT 'roles.search', NULL, 'roles.search', 'Roles', 'Roles Search', '/roles', 'user-edit', 3
UNION ALL SELECT 'users.search', NULL, 'users.search', 'Users', 'Users Search', '/users', 'profile-user', 4
UNION ALL SELECT 'settings.search', NULL, 'settings.search', 'Settings', 'Settings Search', '/settings', 'gear', 5


DELETE FROM [dbo].[MenuOptions];

DECLARE @Index INT = 1
DECLARE @Count INT = (SELECT COUNT(1) FROM @DataTable)

WHILE @Index <= @Count
BEGIN
        SELECT TOP 1
                @ActionCode             = [ActionCode],
                @ParentCode             = [ParentCode],
                @Code                   = [Code],
                @Name                   = [Name],
                @Description    = [Description],
                @MenuUri                = [MenuUri],
                @MenuIcon               = [MenuIcon],
                @SortOrder              = [SortOrder]
        FROM @DataTable WHERE [Id] = @Index
        
        SET @ActionId = (SELECT TOP 1 [Id] FROM [Actions]               WHERE [Code] = @ActionCode)
        SET @ParentId = (SELECT TOP 1 [Id] FROM [MenuOptions]   WHERE [Code] = @ParentCode)

        IF @ActionId IS NOT NULL AND @ApplicationId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM [dbo].[MenuOptions] WHERE [Code] = @Code)
        BEGIN
                INSERT INTO [dbo].[MenuOptions] (
                        [Id],
                        [ApplicationId],
                        [ParentMenuOptionId],
                        [ActionId],
                        [Code],
                        [Name],
                        [Description],
                        [MenuUri],
                        [MenuIcon],
                        [SortOrder],
                        [CreationUser],
                        [CreationDate],
                        [UpdateUser],
                        [UpdateDate],
                        [IsActive]
                ) VALUES (
                        NEWID(),
                        @ApplicationId,
                        @ParentId,
                        @ActionId,
                        @Code,
                        @Name,
                        @Description,
                        @MenuUri,
                        @MenuIcon,
                        @SortOrder,
                        @User,
                        @Date,
                        @User,
                        @Date,
                        1
                )
        END

        SET @Index = @Index + 1
END
