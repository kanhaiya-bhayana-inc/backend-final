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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE TABLE [advisorClients] (
        [ID] int NOT NULL IDENTITY,
        [AdvisorID] int NOT NULL,
        [ClientID] int NOT NULL,
        CONSTRAINT [PK_advisorClients] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE TABLE [AdvisorInfos] (
        [AdvisorId] int NOT NULL IDENTITY,
        [Username] VARCHAR(50) NOT NULL,
        [Email] VARCHAR(50) NOT NULL,
        [PasswordHash] varbinary(max) NOT NULL,
        [PasswordSalt] varbinary(max) NOT NULL,
        [VerificationToken] nvarchar(max) NULL,
        [VerifiedAt] datetime2 NULL,
        [PasswordResetToken] nvarchar(max) NULL,
        [ResetTokenExpires] datetime2 NULL,
        CONSTRAINT [PK_AdvisorInfos] PRIMARY KEY ([AdvisorId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE TABLE [InvestmentTypes] (
        [InvestmentTypeID] int NOT NULL IDENTITY,
        [InvestmentTypeName] VARCHAR(250) NULL,
        [CreatedDate] DateTime2 NOT NULL,
        [ModifiedBy] VARCHAR(50) NULL,
        [ModifiedDate] DateTime2 NULL,
        [DeletedFlag] int NOT NULL,
        CONSTRAINT [PK_InvestmentTypes] PRIMARY KEY ([InvestmentTypeID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE TABLE [Roles] (
        [RoleID] int NOT NULL IDENTITY,
        [RoleName] VARCHAR(15) NOT NULL,
        [Active] Tinyint NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE TABLE [Usersd] (
        [UserID] int NOT NULL IDENTITY,
        [RoleID] int NOT NULL,
        [Address] VARCHAR(200) NULL,
        [City] VARCHAR(30) NULL,
        [State] VARCHAR(20) NULL,
        [Phone] VARCHAR(40) NOT NULL,
        [Email] VARCHAR(30) NOT NULL,
        [AdvisorID] VARCHAR(6) NULL,
        [AgentID] VARCHAR(6) NULL,
        [ClientID] VARCHAR(6) NULL,
        [LastName] VARCHAR(50) NOT NULL,
        [FirstName] VARCHAR(50) NOT NULL,
        [Company] VARCHAR(150) NULL,
        [SortName] VARCHAR(100) NOT NULL,
        [Active] Tinyint NOT NULL,
        [CreatedDate] DateTime2 NOT NULL,
        [ModifiedBy] VARCHAR(50) NULL,
        [ModifiedDate] DateTime2 NULL,
        [DeletedFlag] int NOT NULL,
        [PasswordHash] varbinary(max) NOT NULL,
        [PasswordSalt] varbinary(max) NOT NULL,
        [VerificationToken] nvarchar(max) NULL,
        [VerifiedAt] datetime2 NULL,
        [PasswordResetToken] nvarchar(max) NULL,
        [ResetTokenExpires] datetime2 NULL,
        CONSTRAINT [PK_Usersd] PRIMARY KEY ([UserID]),
        CONSTRAINT [FK_Usersd_Roles_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Roles] ([RoleID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE TABLE [InvestorInfos] (
        [InvestorInfoID] int NOT NULL IDENTITY,
        [UserID] int NOT NULL,
        [InvestmentName] VARCHAR(200) NULL,
        [Active] Tinyint NOT NULL,
        [CreatedDate] DateTime2 NOT NULL,
        [ModifiedBy] VARCHAR(50) NULL,
        [ModifiedDate] DateTime2 NULL,
        [DeletedFlag] int NOT NULL,
        CONSTRAINT [PK_InvestorInfos] PRIMARY KEY ([InvestorInfoID]),
        CONSTRAINT [FK_InvestorInfos_Usersd_UserID] FOREIGN KEY ([UserID]) REFERENCES [Usersd] ([UserID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE TABLE [investmentStrategies] (
        [InvestmentStrategyID] int NOT NULL IDENTITY,
        [UserID] int NOT NULL,
        [StrategyName] VARCHAR(200) NOT NULL,
        [AccountID] VARCHAR(6) NOT NULL,
        [ModelAPLID] VARCHAR(6) NOT NULL,
        [InvestmentAmount] decimal(17,5) NOT NULL,
        [InvestmentTypeID] int NOT NULL,
        [ModifiedBy] VARCHAR(50) NULL,
        [ModifiedDate] DateTime2 NULL,
        [DeletedFlag] int NOT NULL,
        [InvestorInfoID] int NULL,
        CONSTRAINT [PK_investmentStrategies] PRIMARY KEY ([InvestmentStrategyID]),
        CONSTRAINT [FK_investmentStrategies_InvestmentTypes_InvestmentTypeID] FOREIGN KEY ([InvestmentTypeID]) REFERENCES [InvestmentTypes] ([InvestmentTypeID]) ON DELETE CASCADE,
        CONSTRAINT [FK_investmentStrategies_InvestorInfos_InvestorInfoID] FOREIGN KEY ([InvestorInfoID]) REFERENCES [InvestorInfos] ([InvestorInfoID]),
        CONSTRAINT [FK_investmentStrategies_Usersd_UserID] FOREIGN KEY ([UserID]) REFERENCES [Usersd] ([UserID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE INDEX [IX_investmentStrategies_InvestmentTypeID] ON [investmentStrategies] ([InvestmentTypeID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE INDEX [IX_investmentStrategies_InvestorInfoID] ON [investmentStrategies] ([InvestorInfoID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE INDEX [IX_investmentStrategies_UserID] ON [investmentStrategies] ([UserID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE INDEX [IX_InvestorInfos_UserID] ON [InvestorInfos] ([UserID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    CREATE INDEX [IX_Usersd_RoleID] ON [Usersd] ([RoleID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218072146_initials')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230218072146_initials', N'7.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218160910_ipda')
BEGIN
    ALTER TABLE [investmentStrategies] DROP CONSTRAINT [FK_investmentStrategies_InvestorInfos_InvestorInfoID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218160910_ipda')
BEGIN
    ALTER TABLE [investmentStrategies] DROP CONSTRAINT [FK_investmentStrategies_Usersd_UserID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218160910_ipda')
BEGIN
    DROP INDEX [IX_investmentStrategies_UserID] ON [investmentStrategies];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218160910_ipda')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[investmentStrategies]') AND [c].[name] = N'UserID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [investmentStrategies] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [investmentStrategies] DROP COLUMN [UserID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218160910_ipda')
BEGIN
    DROP INDEX [IX_investmentStrategies_InvestorInfoID] ON [investmentStrategies];
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[investmentStrategies]') AND [c].[name] = N'InvestorInfoID');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [investmentStrategies] DROP CONSTRAINT [' + @var1 + '];');
    UPDATE [investmentStrategies] SET [InvestorInfoID] = 0 WHERE [InvestorInfoID] IS NULL;
    ALTER TABLE [investmentStrategies] ALTER COLUMN [InvestorInfoID] int NOT NULL;
    ALTER TABLE [investmentStrategies] ADD DEFAULT 0 FOR [InvestorInfoID];
    CREATE INDEX [IX_investmentStrategies_InvestorInfoID] ON [investmentStrategies] ([InvestorInfoID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218160910_ipda')
BEGIN
    ALTER TABLE [investmentStrategies] ADD CONSTRAINT [FK_investmentStrategies_InvestorInfos_InvestorInfoID] FOREIGN KEY ([InvestorInfoID]) REFERENCES [InvestorInfos] ([InvestorInfoID]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218160910_ipda')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230218160910_ipda', N'7.0.2');
END;
GO

COMMIT;
GO

