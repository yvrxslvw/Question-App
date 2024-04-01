CREATE TABLE [dbo].[Questions] (
    [Id]      INT         IDENTITY (1, 1) NOT NULL,
    [TestId]  INT         NOT NULL,
    [Content] NCHAR (256) NOT NULL,
    [Answer]  NCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TestId]) REFERENCES [dbo].[Tests] ([Id])
);

