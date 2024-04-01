CREATE TABLE [dbo].[Tests] (
    [Id]    INT        IDENTITY (1, 1) NOT NULL,
    [Name]  NCHAR (24) NOT NULL,
    [Timer] FLOAT (53) DEFAULT ((5)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

