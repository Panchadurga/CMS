IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210916114022_cms')
BEGIN
    CREATE TABLE [Doctor] (
        [DoctorId] int NOT NULL IDENTITY,
        [Firstname] nvarchar(max) NOT NULL,
        [Lastname] nvarchar(max) NOT NULL,
        [Sex] nvarchar(max) NOT NULL,
        [Specialization] nvarchar(max) NOT NULL,
        [StartTime] nvarchar(max) NOT NULL,
        [EndTime] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Doctor] PRIMARY KEY ([DoctorId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210916114022_cms')
BEGIN
    CREATE TABLE [Patient] (
        [PatientId] int NOT NULL IDENTITY,
        [Firstname] nvarchar(max) NOT NULL,
        [Lastname] nvarchar(max) NOT NULL,
        [Sex] nvarchar(max) NOT NULL,
        [age] int NOT NULL,
        [DOB] datetime2 NOT NULL,
        CONSTRAINT [PK_Patient] PRIMARY KEY ([PatientId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210916114022_cms')
BEGIN
    CREATE TABLE [Schedule] (
        [AppointmentId] int NOT NULL IDENTITY,
        [PatientId] int NOT NULL,
        [SelectSpeciality] nvarchar(max) NOT NULL,
        [DoctorName] nvarchar(max) NOT NULL,
        [VisitDate] datetime2 NOT NULL,
        [StartTime] nvarchar(max) NOT NULL,
        [EndTime] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Schedule] PRIMARY KEY ([AppointmentId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210916114022_cms')
BEGIN
    CREATE TABLE [UserSetup] (
        [Username] nvarchar(450) NOT NULL,
        [Firstname] nvarchar(max) NOT NULL,
        [Lastname] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_UserSetup] PRIMARY KEY ([Username])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210916114022_cms')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210916114022_cms', N'5.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210929113720_modifyclass-docschedule')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Schedule]') AND [c].[name] = N'EndTime');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Schedule] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Schedule] DROP COLUMN [EndTime];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210929113720_modifyclass-docschedule')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Doctor]') AND [c].[name] = N'EndTime');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Doctor] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Doctor] DROP COLUMN [EndTime];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210929113720_modifyclass-docschedule')
BEGIN
    EXEC sp_rename N'[Schedule].[StartTime]', N'Specialization', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210929113720_modifyclass-docschedule')
BEGIN
    EXEC sp_rename N'[Schedule].[SelectSpeciality]', N'AppointmentTime', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210929113720_modifyclass-docschedule')
BEGIN
    EXEC sp_rename N'[Doctor].[StartTime]', N'VisitingHour', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210929113720_modifyclass-docschedule')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210929113720_modifyclass-docschedule', N'5.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210929154217_modifycls-patient')
BEGIN
    EXEC sp_rename N'[Patient].[age]', N'Age', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210929154217_modifycls-patient')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210929154217_modifycls-patient', N'5.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211005112734_doc')
BEGIN
    EXEC sp_rename N'[Doctor].[Start]', N'StartTime', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211005112734_doc')
BEGIN
    EXEC sp_rename N'[Doctor].[End]', N'EndTime', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211005112734_doc')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211005112734_doc', N'5.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220129101839_modify-usersetup')
BEGIN
    ALTER TABLE [UserSetup] ADD [CreationDate] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220129101839_modify-usersetup')
BEGIN
    ALTER TABLE [UserSetup] ADD [Email] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220129101839_modify-usersetup')
BEGIN
    ALTER TABLE [UserSetup] ADD [SecurityCode] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220129101839_modify-usersetup')
BEGIN
    ALTER TABLE [UserSetup] ADD [Status] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220129101839_modify-usersetup')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220129101839_modify-usersetup', N'5.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130081030_new')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserSetup]') AND [c].[name] = N'CreationDate');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [UserSetup] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [UserSetup] ALTER COLUMN [CreationDate] datetime2 NOT NULL;
    ALTER TABLE [UserSetup] ADD DEFAULT '0001-01-01T00:00:00.0000000' FOR [CreationDate];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130081030_new')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220130081030_new', N'5.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130081346_datetime')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserSetup]') AND [c].[name] = N'CreationDate');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [UserSetup] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [UserSetup] ALTER COLUMN [CreationDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220130081346_datetime')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220130081346_datetime', N'5.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205132217_reg')
BEGIN
    DROP TABLE [UserSetup];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205132217_reg')
BEGIN
    CREATE TABLE [Registration] (
        [Username] nvarchar(450) NOT NULL,
        [Firstname] nvarchar(max) NOT NULL,
        [Lastname] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [SecurityQuestion] nvarchar(max) NOT NULL,
        [Answer] nvarchar(max) NOT NULL,
        [Status] bit NOT NULL,
        [SecurityCode] nvarchar(max) NULL,
        [CreationDate] datetime2 NULL,
        CONSTRAINT [PK_Registration] PRIMARY KEY ([Username])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220205132217_reg')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220205132217_reg', N'5.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220209061710_modifiedregclass')
BEGIN
    ALTER TABLE [Registration] ADD [ConfirmPassword] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220209061710_modifiedregclass')
BEGIN
    ALTER TABLE [Registration] ADD [MobileNo] nvarchar(13) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220209061710_modifiedregclass')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220209061710_modifiedregclass', N'5.0.10');
END;
GO

COMMIT;
GO

