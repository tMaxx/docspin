
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/13/2015 00:33:53
-- Generated from EDMX file: C:\Users\Nicolas\Dropbox\Projects\docspin\docspin\Models\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-docspin];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RepositoryRepositorSupervisor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RepositorySupervisorSet] DROP CONSTRAINT [FK_RepositoryRepositorSupervisor];
GO
IF OBJECT_ID(N'[dbo].[FK_RepositoryDocumentDocumentVersion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentVersionSet] DROP CONSTRAINT [FK_RepositoryDocumentDocumentVersion];
GO
IF OBJECT_ID(N'[dbo].[FK_RepositoryRepositoryDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RepositoryDocumentSet] DROP CONSTRAINT [FK_RepositoryRepositoryDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRepositorySupervisor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RepositorySupervisorSet] DROP CONSTRAINT [FK_UserRepositorySupervisor];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDocumentVersion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentVersionSet] DROP CONSTRAINT [FK_UserDocumentVersion];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEntity1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentCommentSet] DROP CONSTRAINT [FK_UserEntity1];
GO
IF OBJECT_ID(N'[dbo].[FK_RepositoryDocumentEntity1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentCommentSet] DROP CONSTRAINT [FK_RepositoryDocumentEntity1];
GO
IF OBJECT_ID(N'[dbo].[FK_RepositoryRepositoryACL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RepositoryACLSet] DROP CONSTRAINT [FK_RepositoryRepositoryACL];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRepositoryACL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RepositoryACLSet] DROP CONSTRAINT [FK_UserRepositoryACL];
GO
IF OBJECT_ID(N'[dbo].[FK_RepositoryDocumentDocumentACL]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentACLSet] DROP CONSTRAINT [FK_RepositoryDocumentDocumentACL];
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
IF OBJECT_ID(N'[dbo].[RepositoryDocumentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RepositoryDocumentSet];
GO
IF OBJECT_ID(N'[dbo].[RepositorySupervisorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RepositorySupervisorSet];
GO
IF OBJECT_ID(N'[dbo].[DocumentVersionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentVersionSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[DocumentCommentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentCommentSet];
GO
IF OBJECT_ID(N'[dbo].[RepositoryACLSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RepositoryACLSet];
GO
IF OBJECT_ID(N'[dbo].[DocumentACLSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentACLSet];
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
    [Repository_Id] int  NOT NULL
);
GO

-- Creating table 'RepositorySupervisorSet'
CREATE TABLE [dbo].[RepositorySupervisorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Repository_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'DocumentVersionSet'
CREATE TABLE [dbo].[DocumentVersionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileTimestamp] datetime  NOT NULL,
    [Filename] nvarchar(max)  NOT NULL,
    [OriginalFilename] nvarchar(max)  NOT NULL,
    [UploadTimestamp] datetime  NOT NULL,
    [Document_Id] int  NOT NULL,
    [Author_Id] int  NOT NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Active] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Role] tinyint  NOT NULL
);
GO

-- Creating table 'CommentSet'
CREATE TABLE [dbo].[CommentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Timestamp] datetime  NOT NULL,
    [Author_Id] int  NOT NULL,
    [Document_Id] int  NOT NULL
);
GO

-- Creating table 'RepositoryACLSet'
CREATE TABLE [dbo].[RepositoryACLSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ACS] tinyint  NOT NULL,
    [Repository_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'DocumentACLSet'
CREATE TABLE [dbo].[DocumentACLSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ACS] tinyint  NOT NULL,
    [RepositoryDocument_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'RepositorySupervisorSet'
ALTER TABLE [dbo].[RepositorySupervisorSet]
ADD CONSTRAINT [PK_RepositorySupervisorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocumentVersionSet'
ALTER TABLE [dbo].[DocumentVersionSet]
ADD CONSTRAINT [PK_DocumentVersionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
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

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Repository_Id] in table 'RepositorySupervisorSet'
ALTER TABLE [dbo].[RepositorySupervisorSet]
ADD CONSTRAINT [FK_RepositoryRepositorSupervisor]
    FOREIGN KEY ([Repository_Id])
    REFERENCES [dbo].[RepositorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RepositoryRepositorSupervisor'
CREATE INDEX [IX_FK_RepositoryRepositorSupervisor]
ON [dbo].[RepositorySupervisorSet]
    ([Repository_Id]);
GO

-- Creating foreign key on [Document_Id] in table 'DocumentVersionSet'
ALTER TABLE [dbo].[DocumentVersionSet]
ADD CONSTRAINT [FK_RepositoryDocumentDocumentVersion]
    FOREIGN KEY ([Document_Id])
    REFERENCES [dbo].[DocumentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RepositoryDocumentDocumentVersion'
CREATE INDEX [IX_FK_RepositoryDocumentDocumentVersion]
ON [dbo].[DocumentVersionSet]
    ([Document_Id]);
GO

-- Creating foreign key on [Repository_Id] in table 'DocumentSet'
ALTER TABLE [dbo].[DocumentSet]
ADD CONSTRAINT [FK_RepositoryRepositoryDocument]
    FOREIGN KEY ([Repository_Id])
    REFERENCES [dbo].[RepositorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RepositoryRepositoryDocument'
CREATE INDEX [IX_FK_RepositoryRepositoryDocument]
ON [dbo].[DocumentSet]
    ([Repository_Id]);
GO

-- Creating foreign key on [User_Id] in table 'RepositorySupervisorSet'
ALTER TABLE [dbo].[RepositorySupervisorSet]
ADD CONSTRAINT [FK_UserRepositorySupervisor]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRepositorySupervisor'
CREATE INDEX [IX_FK_UserRepositorySupervisor]
ON [dbo].[RepositorySupervisorSet]
    ([User_Id]);
GO

-- Creating foreign key on [Author_Id] in table 'DocumentVersionSet'
ALTER TABLE [dbo].[DocumentVersionSet]
ADD CONSTRAINT [FK_UserDocumentVersion]
    FOREIGN KEY ([Author_Id])
    REFERENCES [dbo].[UserSet]
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
ADD CONSTRAINT [FK_UserEntity1]
    FOREIGN KEY ([Author_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEntity1'
CREATE INDEX [IX_FK_UserEntity1]
ON [dbo].[CommentSet]
    ([Author_Id]);
GO

-- Creating foreign key on [Document_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_RepositoryDocumentEntity1]
    FOREIGN KEY ([Document_Id])
    REFERENCES [dbo].[DocumentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RepositoryDocumentEntity1'
CREATE INDEX [IX_FK_RepositoryDocumentEntity1]
ON [dbo].[CommentSet]
    ([Document_Id]);
GO

-- Creating foreign key on [Repository_Id] in table 'RepositoryACLSet'
ALTER TABLE [dbo].[RepositoryACLSet]
ADD CONSTRAINT [FK_RepositoryRepositoryACL]
    FOREIGN KEY ([Repository_Id])
    REFERENCES [dbo].[RepositorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RepositoryRepositoryACL'
CREATE INDEX [IX_FK_RepositoryRepositoryACL]
ON [dbo].[RepositoryACLSet]
    ([Repository_Id]);
GO

-- Creating foreign key on [User_Id] in table 'RepositoryACLSet'
ALTER TABLE [dbo].[RepositoryACLSet]
ADD CONSTRAINT [FK_UserRepositoryACL]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRepositoryACL'
CREATE INDEX [IX_FK_UserRepositoryACL]
ON [dbo].[RepositoryACLSet]
    ([User_Id]);
GO

-- Creating foreign key on [RepositoryDocument_Id] in table 'DocumentACLSet'
ALTER TABLE [dbo].[DocumentACLSet]
ADD CONSTRAINT [FK_RepositoryDocumentDocumentACL]
    FOREIGN KEY ([RepositoryDocument_Id])
    REFERENCES [dbo].[DocumentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RepositoryDocumentDocumentACL'
CREATE INDEX [IX_FK_RepositoryDocumentDocumentACL]
ON [dbo].[DocumentACLSet]
    ([RepositoryDocument_Id]);
GO

-- Creating foreign key on [User_Id] in table 'DocumentACLSet'
ALTER TABLE [dbo].[DocumentACLSet]
ADD CONSTRAINT [FK_DocumentACLUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
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