DECLARE @User NVARCHAR(256) = 'administrator';
DECLARE @Date DATETIME = GETDATE();

DECLARE @Code [nvarchar](32) = 'User.Registration';
DECLARE @Language [nvarchar](6) = 'en-US';
DECLARE @Description [nvarchar](256) = 'Mail sent when an application user is registered';
DECLARE @Subject [nvarchar](256) = 'User Registration';

INSERT INTO [dbo].[Emails] (
    [Id],
    [Code],
    [Language],
	[Description],
	[Subject],
	[ToEmails],
	[CcEmails],
	[Body],
	[CreationUser],
	[CreationDate],
	[UpdateUser],
	[UpdateDate],
	[IsActive]
)
SELECT
    NEWID(),
	@Code,
    @Language,
	@Description,
	@Subject,
	NULL,
	NULL,
	'',
    @User,
    @Date,
    @User,
    @Date,
    1
WHERE NOT EXISTS (
	SELECT TOP 1 1 FROM [dbo].[Emails]
	WHERE [Code] = @Code AND [Language] = @Language
);

UPDATE [dbo].[Emails] SET
	[Body] = '<html>
<head>
    <style>
        body {
            text-align: center;
            font-family: monospace;
        }

        .app-logo {
            margin: 15px;
        }

        .app-text {
            margin: 12px;
            color: rgb(15, 20, 25);
            font-weight: 600;
            font-size: 14px;
        }

        .app-text-small {
            margin: 10px;
            color: rgb(15, 20, 25);
            font-weight: 600;
            font-size: 13px;
        }

        .app-credentials > p {
            margin: 8px;
            color: rgb(15, 20, 25);
            font-weight: 500;
            display: inline;
            font-size: 12px;
        }

        .app-credentials label {
            margin: 8px;
            color: rgb(29, 155, 240);
            font-weight: 100;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <div class="app-logo">
        <img width="180" src="{LOGO}" />
    </div>
    <div class="app-text">
        You have been registered as user in the application {APPLICATION}
    </div>
    <div class="app-text-small">
        Your credentials are:
    </div>
    <div class="app-credentials">
        <p>User:</p><label>{USER}</label>
    </div>
    <div class="app-credentials">
        <p>Password:</p><label>{PASSWORD}</label>
    </div>
</body>
</html>'
WHERE 1 = 1
	AND [Code] = @Code
    AND [Language] = @Language;
