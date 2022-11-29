CREATE TABLE [dbo].[Team] (
    [ID]       INT          IDENTITY (1, 1) NOT NULL,
    [name]      VARCHAR (30) NULL,
    [address]     VARCHAR (10) NULL,
    CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED ([ID] ASC),
);

