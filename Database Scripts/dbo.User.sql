CREATE TABLE [dbo].[User] (
    [ID]            INT          IDENTITY (1, 1) NOT NULL,
    [email]         VARCHAR (30) NOT NULL,
    [password]      VARCHAR (30) NOT NULL,
    [lastname]      VARCHAR (30) NULL,
    [firstname]     VARCHAR (30) NULL,
    [username]      VARCHAR (30) NULL,
    [address]       VARCHAR (30) NULL,
    [tel]           INT          NULL,
    [owner]         INT          NULL,
    [IDTeam]        INT          NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [PK_User1] FOREIGN KEY ([IDTeam]) REFERENCES [dbo].[Team] ([ID]),
);

