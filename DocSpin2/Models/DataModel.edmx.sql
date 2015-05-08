
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/08/2015 19:46:04
-- Generated from EDMX file: C:\Users\Nicolas\OneDrive\Projects\docspin\DocSpin2\Models\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [docspinv2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RepositorSupervisor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupervisorSet] DROP CONSTRAINT [FK_RepositorSupervisor];
GO
IF OBJECT_ID(N'[dbo].[FK_RepositoryDocumentVersion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentVersionSet] DROP CONSTRAINT [FK_RepositoryDocumentVersion];
GO
IF OBJECT_ID(N'[dbo].[FK_RepositoryDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentSet] DROP CONSTRAINT [FK_RepositoryDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRepositorySupervisor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupervisorSet] DROP CONSTRAINT [FK_UserRepositorySupervisor];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDocumentVersion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentVersionSet] DROP CONSTRAINT [FK_UserDocumentVersion];
GO
IF OBJECT_ID(N'[dbo].[FK_UserComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentSet] DROP CONSTRAINT [FK_UserComment];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentSet] DROP CONSTRAINT [FK_DocumentComment];
GO
IF OBJECT_ID(N'[dbo].[FK_ACLRepository]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RepositoryACLSet] DROP CONSTRAINT [FK_ACLRepository];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRepositoryACL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RepositoryACLSet] DROP CONSTRAINT [FK_UserRepositoryACL];
GO
IF OBJECT_ID(N'[dbo].[FK_ACLDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentACLSet] DROP CONSTRAINT [FK_ACLDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentACLUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentACLSet] DROP CONSTRAINT [FK_DocumentACLUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[RepositorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RepositorySet];
GO
IF OBJECT_ID(N'[dbo].[DocumentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentSet];
GO
IF OBJECT_ID(N'[dbo].[SupervisorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupervisorSet];
GO
IF OBJECT_ID(N'[dbo].[DocumentVersionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentVersionSet];
GO
IF OBJECT_ID(N'[dbo].[CommentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentSet];
GO
IF OBJECT_ID(N'[dbo].[RepositoryACLSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RepositoryACLSet];
GO
IF OBJECT_ID(N'[dbo].[DocumentACLSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentACLSet];
GO
IF OBJECT_ID(N'[dbo].[ApplicationUserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApplicationUserSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'RepositorySet'
CREATE TABLE [dbo].[RepositorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ACS] tinyint  NOT NULL
);
GO

-- Creating table 'DocumentSet'
CREATE TABLE [dbo].[DocumentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] smallint  NULL,
    [TsCreated] datetime  NOT NULL,
    [TsModified] datetime  NOT NULL,
    [ACS] tinyint  NOT NULL,
    [IsRemoved] bit  NOT NULL,
    [Repository_Id] int  NOT NULL
);
GO

-- Creating table 'SupervisorSet'
CREATE TABLE [dbo].[SupervisorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Repository_Id] int  NOT NULL,
    [User_Id] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DocumentVersionSet'
CREATE TABLE [dbo].[DocumentVersionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileTimestamp] datetime  NOT NULL,
    [Filename] nvarchar(max)  NOT NULL,
    [OriginalFilename] nvarchar(max)  NOT NULL,
    [UploadTimestamp] datetime  NOT NULL,
    [IsRemoved] bit  NOT NULL,
    [Hash] nvarchar(max)  NOT NULL,
    [Document_Id] int  NOT NULL,
    [Author_Id] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CommentSet'
CREATE TABLE [dbo].[CommentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Timestamp] datetime  NOT NULL,
    [IsRemoved] bit  NOT NULL,
    [Author_Id] nvarchar(max)  NOT NULL,
    [Document_Id] int  NOT NULL
);
GO

-- Creating table 'RepositoryACLSet'
CREATE TABLE [dbo].[RepositoryACLSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ACS] tinyint  NOT NULL,
    [Repository_Id] int  NOT NULL,
    [User_Id] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DocumentACLSet'
CREATE TABLE [dbo].[DocumentACLSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ACS] tinyint  NOT NULL,
    [Document_Id] int  NOT NULL,
    [User_Id] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ApplicationUserSet'
CREATE TABLE [dbo].[ApplicationUserSet] (
    [Id] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [Active] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Role] tinyint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'RepositorySet'
ALTER TABLE [dbo].[RepositorySet]
ADD CONSTRAINT [PK_RepositorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocumentSet'
ALTER TABLE [dbo].[DocumentSet]
ADD CONSTRAINT [PK_DocumentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupervisorSet'
ALTER TABLE [dbo].[SupervisorSet]
ADD CONSTRAINT [PK_SupervisorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocumentVersionSet'
ALTER TABLE [dbo].[DocumentVersionSet]
ADD CONSTRAINT [PK_DocumentVersionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [PK_CommentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RepositoryACLSet'
ALTER TABLE [dbo].[RepositoryACLSet]
ADD CONSTRAINT [PK_RepositoryACLSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocumentACLSet'
ALTER TABLE [dbo].[DocumentACLSet]
ADD CONSTRAINT [PK_DocumentACLSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ApplicationUserSet'
ALTER TABLE [dbo].[ApplicationUserSet]
ADD CONSTRAINT [PK_ApplicationUserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Repository_Id] in table 'SupervisorSet'
ALTER TABLE [dbo].[SupervisorSet]
ADD CONSTRAINT [FK_RepositorSupervisor]
    FOREIGN KEY ([Repository_Id])
    REFERENCES [dbo].[RepositorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RepositorSupervisor'
CREATE INDEX [IX_FK_RepositorSupervisor]
ON [dbo].[SupervisorSet]
    ([Repository_Id]);
GO

-- Creating foreign key on [Document_Id] in table 'DocumentVersionSet'
ALTER TABLE [dbo].[DocumentVersionSet]
ADD CONSTRAINT [FK_RepositoryDocumentVersion]
    FOREIGN KEY ([Document_Id])
    REFERENCES [dbo].[DocumentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RepositoryDocumentVersion'
CREATE INDEX [IX_FK_RepositoryDocumentVersion]
ON [dbo].[DocumentVersionSet]
    ([Document_Id]);
GO

-- Creating foreign key on [Repository_Id] in table 'DocumentSet'
ALTER TABLE [dbo].[DocumentSet]
ADD CONSTRAINT [FK_RepositoryDocument]
    FOREIGN KEY ([Repository_Id])
    REFERENCES [dbo].[RepositorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RepositoryDocument'
CREATE INDEX [IX_FK_RepositoryDocument]
ON [dbo].[DocumentSet]
    ([Repository_Id]);
GO

-- Creating foreign key on [User_Id] in table 'SupervisorSet'
ALTER TABLE [dbo].[SupervisorSet]
ADD CONSTRAINT [FK_UserRepositorySupervisor]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[ApplicationUserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRepositorySupervisor'
CREATE INDEX [IX_FK_UserRepositorySupervisor]
ON [dbo].[SupervisorSet]
    ([User_Id]);
GO

-- Creating foreign key on [Author_Id] in table 'DocumentVersionSet'
ALTER TABLE [dbo].[DocumentVersionSet]
ADD CONSTRAINT [FK_UserDocumentVersion]
    FOREIGN KEY ([Author_Id])
    REFERENCES [dbo].[ApplicationUserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDocumentVersion'
CREATE INDEX [IX_FK_UserDocumentVersion]
ON [dbo].[DocumentVersionSet]
    ([Author_Id]);
GO

-- Creating foreign key on [Author_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_UserComment]
    FOREIGN KEY ([Author_Id])
    REFERENCES [dbo].[ApplicationUserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserComment'
CREATE INDEX [IX_FK_UserComment]
ON [dbo].[CommentSet]
    ([Author_Id]);
GO

-- Creating foreign key on [Document_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_DocumentComment]
    FOREIGN KEY ([Document_Id])
    REFERENCES [dbo].[DocumentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentComment'
CREATE INDEX [IX_FK_DocumentComment]
ON [dbo].[CommentSet]
    ([Document_Id]);
GO

-- Creating foreign key on [Repository_Id] in table 'RepositoryACLSet'
ALTER TABLE [dbo].[RepositoryACLSet]
ADD CONSTRAINT [FK_ACLRepository]
    FOREIGN KEY ([Repository_Id])
    REFERENCES [dbo].[RepositorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACLRepository'
CREATE INDEX [IX_FK_ACLRepository]
ON [dbo].[RepositoryACLSet]
    ([Repository_Id]);
GO

-- Creating foreign key on [User_Id] in table 'RepositoryACLSet'
ALTER TABLE [dbo].[RepositoryACLSet]
ADD CONSTRAINT [FK_UserRepositoryACL]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[ApplicationUserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRepositoryACL'
CREATE INDEX [IX_FK_UserRepositoryACL]
ON [dbo].[RepositoryACLSet]
    ([User_Id]);
GO

-- Creating foreign key on [Document_Id] in table 'DocumentACLSet'
ALTER TABLE [dbo].[DocumentACLSet]
ADD CONSTRAINT [FK_ACLDocument]
    FOREIGN KEY ([Document_Id])
    REFERENCES [dbo].[DocumentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACLDocument'
CREATE INDEX [IX_FK_ACLDocument]
ON [dbo].[DocumentACLSet]
    ([Document_Id]);
GO

-- Creating foreign key on [User_Id] in table 'DocumentACLSet'
ALTER TABLE [dbo].[DocumentACLSet]
ADD CONSTRAINT [FK_DocumentACLUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[ApplicationUserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentACLUser'
CREATE INDEX [IX_FK_DocumentACLUser]
ON [dbo].[DocumentACLSet]
    ([User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------