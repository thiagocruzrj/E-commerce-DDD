IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Category] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(250) NOT NULL,
    [Code] int NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [CategoryId] uniqueidentifier NOT NULL,
    [Name] varchar(250) NOT NULL,
    [Description] varchar(500) NOT NULL,
    [Active] bit NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    [RegisterDate] datetime2 NOT NULL,
    [Image] varchar(250) NOT NULL,
    [StockQuantity] int NOT NULL,
    [Height] int NULL,
    [Width] int NULL,
    [Depth] int NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Products_CategoryId] ON [Products] ([CategoryId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200104183911_Initial', N'3.1.0');

GO

