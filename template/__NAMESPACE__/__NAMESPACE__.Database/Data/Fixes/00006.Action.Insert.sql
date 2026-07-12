DECLARE @User NVARCHAR(256) = 'administrator';
DECLARE @Date DATETIME = GETDATE();

DECLARE @ParentId UNIQUEIDENTIFIER
DECLARE @ParentCode VARCHAR(64)

DECLARE @DataTable TABLE (
        [Id] INT IDENTITY(1, 1),
        [ParentCode] VARCHAR(64),
        [ModuleCode] VARCHAR(64),
        [Code] VARCHAR(64),
        [Name] VARCHAR(256),
        [Description] VARCHAR(1024)
);

INSERT INTO @DataTable([ParentCode], [ModuleCode], [Code], [Name], [Description])
-- SYSTEM ACTIONS
-- Applications
                  SELECT NULL, 'applications', 'applications.create', 'Create Applications', 'Allows you to create new applications'
UNION ALL SELECT NULL, 'applications', 'applications.edit', 'Edit Applications', 'Allows you to edit existing applications'
UNION ALL SELECT NULL, 'applications', 'applications.delete', 'Delete Applications', 'Allows you to delete existing applications'
UNION ALL SELECT NULL, 'applications', 'applications.search', 'Search Applications', 'Allows you to search applications by criteria'
UNION ALL SELECT NULL, 'applications', 'applications.export', 'Export Applications', 'Allows you to export applications by criteria'
-- Modules & Actions
UNION ALL SELECT NULL, 'modules', 'modules.create', 'Create Modules', 'Allows you to create new modules'
UNION ALL SELECT NULL, 'modules', 'modules.edit', 'Edit Modules', 'Allows you to edit existing modules'
UNION ALL SELECT NULL, 'modules', 'modules.delete', 'Delete Modules', 'Allows you to delete existing modules'
UNION ALL SELECT NULL, 'modules', 'modules.search', 'Search Modules', 'Allows you to search modules by criteria'
UNION ALL SELECT NULL, 'modules', 'modules.export', 'Export Modules', 'Allows you to export modules by criteria'
UNION ALL SELECT NULL, 'modules', 'actions.create', 'Create Actions', 'Allows you to create new actions'
UNION ALL SELECT NULL, 'modules', 'actions.edit', 'Edit Actions', 'Allows you to edit existing actions'
UNION ALL SELECT NULL, 'modules', 'actions.delete', 'Delete Actions', 'Allows you to delete existing actions'
UNION ALL SELECT NULL, 'modules', 'actions.search', 'Search Actions', 'Allows you to search actions by criteria'
UNION ALL SELECT NULL, 'modules', 'actions.export', 'Export Actions', 'Allows you to export actions by criteria'
-- Menu Option
UNION ALL SELECT NULL, 'menu-options', 'menu-options.create', 'Create Menu Options', 'Allows you to create new menu option menu options'
UNION ALL SELECT NULL, 'menu-options', 'menu-options.edit', 'Edit Menu Options', 'Allows you to edit existing menu option menu options'
UNION ALL SELECT NULL, 'menu-options', 'menu-options.delete', 'Delete Menu Options', 'Allows you to delete existing menu option menu options'
UNION ALL SELECT NULL, 'menu-options', 'menu-options.search', 'Search Menu Options', 'Allows you to search menu option menu options by criteria'
UNION ALL SELECT NULL, 'menu-options', 'menu-options.export', 'Export Menu Options', 'Allows you to export menu option menu options by criteria'
-- Roles
UNION ALL SELECT NULL, 'roles', 'roles.create', 'Create Roles', 'Allows you to create new roles'
UNION ALL SELECT NULL, 'roles', 'roles.edit', 'Edit Roles', 'Allows you to edit existing roles'
UNION ALL SELECT NULL, 'roles', 'roles.delete', 'Delete Roles', 'Allows you to delete existing roles'
UNION ALL SELECT NULL, 'roles', 'roles.search', 'Search Roles', 'Allows you to search roles by criteria'
UNION ALL SELECT NULL, 'roles', 'roles.export', 'Export Roles', 'Allows you to export roles by criteria'
UNION ALL SELECT NULL, 'roles', 'roles.permissions', 'Assign Permissions to Roles', 'Allows you to assign permissions to roles'
-- Users
UNION ALL SELECT NULL, 'users', 'users.create', 'Create Users', 'Allows you to create new users'
UNION ALL SELECT NULL, 'users', 'users.edit', 'Edit Users', 'Allows you to edit existing users'
UNION ALL SELECT NULL, 'users', 'users.delete', 'Delete Users', 'Allows you to delete existing users'
UNION ALL SELECT NULL, 'users', 'users.search', 'Search Users', 'Allows you to search users by criteria'
UNION ALL SELECT NULL, 'users', 'users.export', 'Export Users', 'Allows you to export users by criteria'
-- Settings
UNION ALL SELECT NULL, 'settings', 'settings.edit', 'Edit Settings', 'Allows you to edit existing settings'
UNION ALL SELECT NULL, 'settings', 'settings.search', 'Search Settings', 'Allows you to search settings by criteria'



DECLARE @Index INT = 1
DECLARE @Count INT = (SELECT COUNT(1) FROM @DataTable)

WHILE @Index <= @Count
BEGIN
        SET @ParentId = NULL;

        SELECT TOP 1
                @ParentCode     = [ParentCode]
        FROM @DataTable WHERE [Id] = @Index;
        
        SELECT TOP 1
                @ParentId       = [a].[Id]
        FROM [dbo].[Actions] [a]
        WHERE 1 = 1
                AND @ParentCode IS NOT NULL
                AND [a].[Code] = @ParentCode;

        INSERT INTO [dbo].[Actions] (
                [Id],
                [ParentActionId],
                [ModuleId],
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
                @ParentId,
                [m].[Id],
                [dt].[Code],
                [dt].[Name],
                [dt].[Description],
                @User,
                @Date,
                @User,
                @Date,
                1
        FROM @DataTable [dt]
        INNER JOIN [dbo].[Modules] [m] ON [dt].[ModuleCode] = [m].[Code]
        WHERE 1 = 1
                AND [dt].[Id] = @Index
                AND [m].[Id] IS NOT NULL
                AND NOT EXISTS (
                        SELECT 1 FROM [dbo].[Actions] [a]
                        WHERE 1 = 1
                                AND [a].[Code] = [dt].[Code]
                );

        SET @Index = @Index + 1
END
