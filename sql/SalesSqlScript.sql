IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE SEQUENCE [MySequence] AS int START WITH 1000 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;

GO

CREATE TABLE [Vouchers] (
    [Id] uniqueidentifier NOT NULL,
    [Code] varchar(100) NOT NULL,
    [Percentage] decimal(18,2) NULL,
    [DiscountValue] decimal(18,2) NULL,
    [Quantity] int NOT NULL,
    [VoucherDiscountType] int NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [DateUse] datetime2 NULL,
    [DateValidate] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    [Used] bit NOT NULL,
    CONSTRAINT [PK_Vouchers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Orders] (
    [Id] uniqueidentifier NOT NULL,
    [Code] int NOT NULL DEFAULT (NEXT VALUE FOR MySequence),
    [ClientId] uniqueidentifier NOT NULL,
    [VoucherId] uniqueidentifier NULL,
    [VoucherUsed] bit NOT NULL,
    [Discount] decimal(18,2) NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [RegisterDate] datetime2 NOT NULL,
    [OrderStatus] int NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Vouchers_VoucherId] FOREIGN KEY ([VoucherId]) REFERENCES [Vouchers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [OrderItems] (
    [Id] uniqueidentifier NOT NULL,
    [OrderId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [ProductName] varchar(250) NOT NULL,
    [Quantity] int NOT NULL,
    [UnitValue] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);

GO

CREATE INDEX [IX_Orders_VoucherId] ON [Orders] ([VoucherId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200110031137_InitialCreate', N'3.1.0');

GO

