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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225060717_Initial')
BEGIN
    CREATE TABLE [Categories] (
        [CategoryId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225060717_Initial')
BEGIN
    CREATE TABLE [Posts] (
        [PostId] uniqueidentifier NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UpdatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([PostId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225060717_Initial')
BEGIN
    CREATE TABLE [Tags] (
        [TagId] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Tags] PRIMARY KEY ([TagId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225060717_Initial')
BEGIN
    CREATE TABLE [Comments] (
        [CommentId] uniqueidentifier NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [CreateDate] datetime2 NOT NULL,
        [PostId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([CommentId]),
        CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([PostId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225060717_Initial')
BEGIN
    CREATE TABLE [PostCategories] (
        [PostId] uniqueidentifier NOT NULL,
        [CategoryId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_PostCategories] PRIMARY KEY ([PostId], [CategoryId]),
        CONSTRAINT [FK_PostCategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE,
        CONSTRAINT [FK_PostCategories_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([PostId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225060717_Initial')
BEGIN
    CREATE TABLE [PostTags] (
        [PostId] uniqueidentifier NOT NULL,
        [TagId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_PostTags] PRIMARY KEY ([PostId], [TagId]),
        CONSTRAINT [FK_PostTags_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([PostId]) ON DELETE CASCADE,
        CONSTRAINT [FK_PostTags_Tags_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tags] ([TagId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225060717_Initial')
BEGIN
    CREATE INDEX [IX_Comments_PostId] ON [Comments] ([PostId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225060717_Initial')
BEGIN
    CREATE INDEX [IX_PostCategories_CategoryId] ON [PostCategories] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225060717_Initial')
BEGIN
    CREATE INDEX [IX_PostTags_TagId] ON [PostTags] ([TagId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225060717_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240225060717_Initial', N'7.0.16');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225064816_RefToUser')
BEGIN
    ALTER TABLE [Posts] ADD [UserId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225064816_RefToUser')
BEGIN
    ALTER TABLE [Posts] ADD [UserId1] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225064816_RefToUser')
BEGIN
    ALTER TABLE [Comments] ADD [UserId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225064816_RefToUser')
BEGIN
    ALTER TABLE [Comments] ADD [UserId1] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225064816_RefToUser')
BEGIN
    CREATE TABLE [IdentityUser] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(max) NULL,
        [NormalizedUserName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [NormalizedEmail] nvarchar(max) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_IdentityUser] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225064816_RefToUser')
BEGIN
    CREATE INDEX [IX_Posts_UserId1] ON [Posts] ([UserId1]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225064816_RefToUser')
BEGIN
    CREATE INDEX [IX_Comments_UserId1] ON [Comments] ([UserId1]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225064816_RefToUser')
BEGIN
    ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_IdentityUser_UserId1] FOREIGN KEY ([UserId1]) REFERENCES [IdentityUser] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225064816_RefToUser')
BEGIN
    ALTER TABLE [Posts] ADD CONSTRAINT [FK_Posts_IdentityUser_UserId1] FOREIGN KEY ([UserId1]) REFERENCES [IdentityUser] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240225064816_RefToUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240225064816_RefToUser', N'7.0.16');
END;
GO

COMMIT;
GO

