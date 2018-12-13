
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/11/2018 16:26:51
-- Generated from EDMX file: C:\Users\thedr\source\repos\epam_developer_net\ManagerCloud\EF.ManagerCloud\ManagerCloudContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ManagerCloud];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ClientSet'
CREATE TABLE [dbo].[ClientSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SaleSet'
CREATE TABLE [dbo].[SaleSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [ClientId] int  NOT NULL,
    [ItemId] int  NOT NULL,
    [SaleSum] float  NOT NULL,
    [DataSourceId] int  NOT NULL
);
GO

-- Creating table 'ItemSet'
CREATE TABLE [dbo].[ItemSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DataSourceSet'
CREATE TABLE [dbo].[DataSourceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ClientSet'
ALTER TABLE [dbo].[ClientSet]
ADD CONSTRAINT [PK_ClientSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SaleSet'
ALTER TABLE [dbo].[SaleSet]
ADD CONSTRAINT [PK_SaleSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemSet'
ALTER TABLE [dbo].[ItemSet]
ADD CONSTRAINT [PK_ItemSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DataSourceSet'
ALTER TABLE [dbo].[DataSourceSet]
ADD CONSTRAINT [PK_DataSourceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClientId] in table 'SaleSet'
ALTER TABLE [dbo].[SaleSet]
ADD CONSTRAINT [FK_ClientSale]
    FOREIGN KEY ([ClientId])
    REFERENCES [dbo].[ClientSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientSale'
CREATE INDEX [IX_FK_ClientSale]
ON [dbo].[SaleSet]
    ([ClientId]);
GO

-- Creating foreign key on [ItemId] in table 'SaleSet'
ALTER TABLE [dbo].[SaleSet]
ADD CONSTRAINT [FK_ItemSale]
    FOREIGN KEY ([ItemId])
    REFERENCES [dbo].[ItemSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemSale'
CREATE INDEX [IX_FK_ItemSale]
ON [dbo].[SaleSet]
    ([ItemId]);
GO

-- Creating foreign key on [DataSourceId] in table 'SaleSet'
ALTER TABLE [dbo].[SaleSet]
ADD CONSTRAINT [FK_DataSourceSale]
    FOREIGN KEY ([DataSourceId])
    REFERENCES [dbo].[DataSourceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DataSourceSale'
CREATE INDEX [IX_FK_DataSourceSale]
ON [dbo].[SaleSet]
    ([DataSourceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------