CREATE TABLE [dbo].[Clients] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (MAX) NULL,
    [MiddleName]  NVARCHAR (MAX) NULL,
    [LastName]    NVARCHAR (MAX) NULL,
    [PhoneNumber] NVARCHAR (MAX) NULL,
    [Email]       NVARCHAR (MAX) NULL,
	[BalanceID]	  INT	   NULL
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

