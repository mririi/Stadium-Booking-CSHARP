CREATE TABLE [dbo].[Stadiums] (
    [ID]             INT          IDENTITY (1, 1) NOT NULL,
    [name]           VARCHAR (30) NOT NULL,
    [description]    VARCHAR (30) NOT NULL,
    [IDStadiumOwner] INT          NOT NULL,
    CONSTRAINT [PK_Stadiums] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [PK_Stadiums2] FOREIGN KEY ([IDStadiumOwner]) REFERENCES [dbo].[User] ([ID])
);

