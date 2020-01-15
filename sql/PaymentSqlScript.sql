IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Payments] (
    [Id] uniqueidentifier NOT NULL,
    [OrderId] uniqueidentifier NOT NULL,
    [Status] varchar(100) NULL,
    [Value] decimal(18,2) NOT NULL,
    [CardName] varchar(100) NULL,
    [CardNumber] varchar(100) NULL,
    [ExpirationCard] varchar(100) NULL,
    [CvvCard] varchar(100) NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Transactions] (
    [Id] uniqueidentifier NOT NULL,
    [OrderId] uniqueidentifier NOT NULL,
    [PaymentId] uniqueidentifier NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    [StatusTransaction] int NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_Payments_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [Payments] ([Id]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [IX_Transactions_PaymentId] ON [Transactions] ([PaymentId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200115231035_InitialMigration', N'3.1.0');

GO

