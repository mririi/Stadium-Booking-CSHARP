CREATE TABLE [dbo].[Reservation] (
    [ID]        INT          IDENTITY (1, 1) NOT NULL,
    [starttime] INT          NOT NULL,
    [endtime]   INT          NOT NULL,
    [IDPlayer]  INT          NOT NULL,
    [IDStadium] INT          NOT NULL,
    CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [PK_Reservation1] FOREIGN KEY ([IDStadium]) REFERENCES [dbo].[Stadiums] ([ID]),
    CONSTRAINT [PK_Reservation2] FOREIGN KEY ([IDPlayer]) REFERENCES [dbo].[User] ([ID])
);

