
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/23/2024 11:32:35
-- Generated from EDMX file: D:\Projects_Vaival\Waats\Waats\waats\Models\data\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [waats];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[BrainFitness]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BrainFitness];
GO
IF OBJECT_ID(N'[dbo].[EOT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EOT];
GO
IF OBJECT_ID(N'[dbo].[EST]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EST];
GO
IF OBJECT_ID(N'[dbo].[GenderT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GenderT];
GO
IF OBJECT_ID(N'[dbo].[GratitudeJournal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GratitudeJournal];
GO
IF OBJECT_ID(N'[dbo].[Logs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logs];
GO
IF OBJECT_ID(N'[dbo].[MemoryMaker]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MemoryMaker];
GO
IF OBJECT_ID(N'[dbo].[MindFullAttention]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MindFullAttention];
GO
IF OBJECT_ID(N'[dbo].[ProfilePicT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProfilePicT];
GO
IF OBJECT_ID(N'[dbo].[ScheduleTask]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScheduleTask];
GO
IF OBJECT_ID(N'[dbo].[SubScheduleTask]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubScheduleTask];
GO
IF OBJECT_ID(N'[dbo].[Trigger]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trigger];
GO
IF OBJECT_ID(N'[dbo].[waatsForm]', 'U') IS NOT NULL
    DROP TABLE [dbo].[waatsForm];
GO
IF OBJECT_ID(N'[waatsModelStoreContainer].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [waatsModelStoreContainer].[AspNetUserRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [Activationcode] nvarchar(256)  NULL,
    [Firstname] nvarchar(256)  NULL,
    [Lastname] nvarchar(256)  NULL,
    [Dob] nvarchar(25)  NULL,
    [Gender] nvarchar(25)  NULL,
    [EthnicOrigin] nvarchar(50)  NULL,
    [EmploymentStatus] nvarchar(50)  NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [ADUserName] nvarchar(256)  NULL,
    [AccountType] int  NOT NULL,
    [ConsentToSharedata] bit  NULL,
    [ProfilePic] bit  NULL,
    [AgreeToADHD] bit  NULL,
    [Dateinserted] datetime  NULL,
    [bDeleted] bit  NULL
);
GO

-- Creating table 'BrainFitness'
CREATE TABLE [dbo].[BrainFitness] (
    [BrainFitnessID] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NULL,
    [MarkAsCompleted] bit  NULL,
    [CompletionDate] datetime  NULL,
    [bDeleted] bit  NULL,
    [StartingNumber] int  NULL,
    [Q_Add] int  NULL,
    [Q_Take] int  NULL,
    [Q_A] int  NULL,
    [Q_NoofAttempts] int  NULL
);
GO

-- Creating table 'EOT'
CREATE TABLE [dbo].[EOT] (
    [LocalId] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(100)  NULL
);
GO

-- Creating table 'EST'
CREATE TABLE [dbo].[EST] (
    [LocalId] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(100)  NULL
);
GO

-- Creating table 'GenderT'
CREATE TABLE [dbo].[GenderT] (
    [LocalId] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(100)  NULL
);
GO

-- Creating table 'GratitudeJournal'
CREATE TABLE [dbo].[GratitudeJournal] (
    [GratitudeJouralID] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NULL,
    [thingsiamgratfullfortoday] nvarchar(4000)  NULL,
    [AddedDate] datetime  NULL,
    [MarkAsCompleted] bit  NULL,
    [EditDate] datetime  NULL,
    [CompletionDate] datetime  NULL,
    [bDeleted] bit  NULL
);
GO

-- Creating table 'Logs'
CREATE TABLE [dbo].[Logs] (
    [localId] int IDENTITY(1,1) NOT NULL,
    [WaatsFormId] nchar(10)  NULL,
    [Description] nvarchar(4000)  NULL,
    [CreationDate] nvarchar(20)  NULL
);
GO

-- Creating table 'MemoryMaker'
CREATE TABLE [dbo].[MemoryMaker] (
    [memoryId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NULL,
    [One] nvarchar(4000)  NULL,
    [two] nvarchar(4000)  NULL,
    [three] nvarchar(4000)  NULL,
    [four] nvarchar(4000)  NULL,
    [AddedDate] datetime  NULL,
    [MarkAsCompleted] bit  NULL,
    [EditDate] datetime  NULL,
    [CompletionDate] datetime  NULL,
    [bDeleted] bit  NULL
);
GO

-- Creating table 'MindFullAttention'
CREATE TABLE [dbo].[MindFullAttention] (
    [MindFulID] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NULL,
    [thepersonilistenedtospokeabout] nvarchar(4000)  NULL,
    [AddedDate] datetime  NULL,
    [MarkAsCompleted] bit  NULL,
    [EditDate] datetime  NULL,
    [CompletionDate] datetime  NULL,
    [bDeleted] bit  NULL
);
GO

-- Creating table 'ProfilePicT'
CREATE TABLE [dbo].[ProfilePicT] (
    [LocalId] int IDENTITY(1,1) NOT NULL,
    [UserGUID] nvarchar(128)  NULL,
    [FileData] varbinary(max)  NULL,
    [FileName] nvarchar(200)  NULL,
    [FileType] nvarchar(200)  NULL,
    [FileUploadDateTime] datetime  NULL,
    [bDeleted] bit  NULL
);
GO

-- Creating table 'ScheduleTask'
CREATE TABLE [dbo].[ScheduleTask] (
    [LocalID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NULL,
    [Description] nvarchar(4000)  NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [AddedDate] datetime  NULL,
    [MarkAsCompleted] bit  NULL,
    [EditDate] datetime  NULL,
    [bDeleted] bit  NULL,
    [SubTask] bit  NULL,
    [CompletionDate] datetime  NULL
);
GO

-- Creating table 'SubScheduleTask'
CREATE TABLE [dbo].[SubScheduleTask] (
    [SubLocalID] int IDENTITY(1,1) NOT NULL,
    [MainTaskID] int  NOT NULL,
    [Title] nvarchar(250)  NULL,
    [Description] nvarchar(4000)  NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [AddedDate] datetime  NULL,
    [MarkAsCompleted] bit  NULL,
    [EditDate] datetime  NULL,
    [CompletionDate] datetime  NULL,
    [bDeleted] bit  NULL
);
GO

-- Creating table 'Trigger'
CREATE TABLE [dbo].[Trigger] (
    [TriggerID] int IDENTITY(1,1) NOT NULL,
    [whatwasthetriggerevent] nvarchar(4000)  NULL,
    [whatwastheemotionthatyoufelt] nvarchar(4000)  NULL,
    [howdiditfeelinyourbody] nvarchar(4000)  NULL,
    [whymightthisbeatriggerforyou_thinkaboutyourpast] nvarchar(4000)  NULL,
    [howcanyouavoidthesituationand_ormanageyourresponsetoitinfuture] nvarchar(4000)  NULL,
    [UserId] nvarchar(128)  NULL,
    [AddedDate] datetime  NULL,
    [MarkAsCompleted] bit  NULL,
    [EditDate] datetime  NULL,
    [CompletionDate] datetime  NULL,
    [bDeleted] bit  NULL
);
GO

-- Creating table 'waatsForm'
CREATE TABLE [dbo].[waatsForm] (
    [localID] int IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(300)  NULL,
    [Surname] nvarchar(300)  NULL,
    [DoB] nvarchar(20)  NULL,
    [Age] int  NULL,
    [Gender] nvarchar(20)  NULL,
    [Email] nvarchar(50)  NULL,
    [GPName] nvarchar(500)  NULL,
    [HistoryofMentalIllness] nvarchar(1000)  NULL,
    [ReferredtoMentalHealth] nvarchar(1000)  NULL,
    [HaveYouEverHadAnyThoughtsorMadeAttemptstoHarmYourself] nvarchar(10)  NULL,
    [HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmYourself] nvarchar(10)  NULL,
    [HaveYouCurrentlyorrecentlyAnyThoughtsorMadeAttemptstoHarmAnyoneElse] nvarchar(10)  NULL,
    [CompleteProjects] nvarchar(50)  NULL,
    [RequiresOrganization] nvarchar(50)  NULL,
    [keepAppointments] nvarchar(50)  NULL,
    [StartTasksOnTime] nvarchar(50)  NULL,
    [FidgetAndSquirm] nvarchar(50)  NULL,
    [OverlyActiveAndCompelled] nvarchar(50)  NULL,
    [MakeCarelessMistakes] nvarchar(50)  NULL,
    [DifficultyKeepingMyAttention] nvarchar(50)  NULL,
    [DifficultyConcentrating] nvarchar(50)  NULL,
    [DifficultyFinding] nvarchar(50)  NULL,
    [EasilyDistracted] nvarchar(50)  NULL,
    [LeaveMySeatInMeetings] nvarchar(50)  NULL,
    [DifficultyRelaxingOrUnwinding] nvarchar(50)  NULL,
    [TalkingTooMuch] nvarchar(50)  NULL,
    [FinishingTheSentences] nvarchar(50)  NULL,
    [DifficultyWaitingMyTurn] nvarchar(50)  NULL,
    [InterruptOtherPeople] nvarchar(50)  NULL,
    [ReceiveWarningsAndPersistent] nchar(10)  NULL,
    [DisciplinedOrAacked] nchar(10)  NULL,
    [EverExperiencedABreakdown] nchar(10)  NULL,
    [Completedby] nvarchar(300)  NULL,
    [CompletionDate] nvarchar(20)  NULL,
    [Action] nvarchar(500)  NULL,
    [PracticeName] nvarchar(500)  NULL,
    [Postcode] nvarchar(500)  NULL,
    [Address] nvarchar(500)  NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] nvarchar(128)  NOT NULL,
    [RoleId] nvarchar(128)  NOT NULL,
    [localid] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [BrainFitnessID] in table 'BrainFitness'
ALTER TABLE [dbo].[BrainFitness]
ADD CONSTRAINT [PK_BrainFitness]
    PRIMARY KEY CLUSTERED ([BrainFitnessID] ASC);
GO

-- Creating primary key on [LocalId] in table 'EOT'
ALTER TABLE [dbo].[EOT]
ADD CONSTRAINT [PK_EOT]
    PRIMARY KEY CLUSTERED ([LocalId] ASC);
GO

-- Creating primary key on [LocalId] in table 'EST'
ALTER TABLE [dbo].[EST]
ADD CONSTRAINT [PK_EST]
    PRIMARY KEY CLUSTERED ([LocalId] ASC);
GO

-- Creating primary key on [LocalId] in table 'GenderT'
ALTER TABLE [dbo].[GenderT]
ADD CONSTRAINT [PK_GenderT]
    PRIMARY KEY CLUSTERED ([LocalId] ASC);
GO

-- Creating primary key on [GratitudeJouralID] in table 'GratitudeJournal'
ALTER TABLE [dbo].[GratitudeJournal]
ADD CONSTRAINT [PK_GratitudeJournal]
    PRIMARY KEY CLUSTERED ([GratitudeJouralID] ASC);
GO

-- Creating primary key on [localId] in table 'Logs'
ALTER TABLE [dbo].[Logs]
ADD CONSTRAINT [PK_Logs]
    PRIMARY KEY CLUSTERED ([localId] ASC);
GO

-- Creating primary key on [memoryId] in table 'MemoryMaker'
ALTER TABLE [dbo].[MemoryMaker]
ADD CONSTRAINT [PK_MemoryMaker]
    PRIMARY KEY CLUSTERED ([memoryId] ASC);
GO

-- Creating primary key on [MindFulID] in table 'MindFullAttention'
ALTER TABLE [dbo].[MindFullAttention]
ADD CONSTRAINT [PK_MindFullAttention]
    PRIMARY KEY CLUSTERED ([MindFulID] ASC);
GO

-- Creating primary key on [LocalId] in table 'ProfilePicT'
ALTER TABLE [dbo].[ProfilePicT]
ADD CONSTRAINT [PK_ProfilePicT]
    PRIMARY KEY CLUSTERED ([LocalId] ASC);
GO

-- Creating primary key on [LocalID] in table 'ScheduleTask'
ALTER TABLE [dbo].[ScheduleTask]
ADD CONSTRAINT [PK_ScheduleTask]
    PRIMARY KEY CLUSTERED ([LocalID] ASC);
GO

-- Creating primary key on [SubLocalID] in table 'SubScheduleTask'
ALTER TABLE [dbo].[SubScheduleTask]
ADD CONSTRAINT [PK_SubScheduleTask]
    PRIMARY KEY CLUSTERED ([SubLocalID] ASC);
GO

-- Creating primary key on [TriggerID] in table 'Trigger'
ALTER TABLE [dbo].[Trigger]
ADD CONSTRAINT [PK_Trigger]
    PRIMARY KEY CLUSTERED ([TriggerID] ASC);
GO

-- Creating primary key on [localID] in table 'waatsForm'
ALTER TABLE [dbo].[waatsForm]
ADD CONSTRAINT [PK_waatsForm]
    PRIMARY KEY CLUSTERED ([localID] ASC);
GO

-- Creating primary key on [UserId], [RoleId], [localid] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([UserId], [RoleId], [localid] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------