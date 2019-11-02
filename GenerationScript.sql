IF OBJECT_ID('[dbo].[Customers]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Customers];
IF OBJECT_ID('[dbo].[Routes]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Routes];
IF OBJECT_ID('[dbo].[Bookings]', 'U') IS NOT NULL
	DROP TABLE [dbo].[Bookings];
IF OBJECT_ID('[dbo].[TicketCancellation]', 'TR') IS NOT NULL
	DROP TABLE [dbo].[TicketCancellation];
IF OBJECT_ID('[dbo].[TicketUpdating]', 'TR') IS NOT NULL
	DROP TABLE [dbo].[TicketUpdating];
IF OBJECT_ID('[dbo].[TicketBooking]', 'TR') IS NOT NULL
	DROP TABLE [dbo].[TicketBooking];

GO

CREATE TABLE [dbo].[Customers] (
    [Username] VARCHAR (50)  NOT NULL,
    [Password] VARCHAR (MAX)  NOT NULL,
    [Name]     VARCHAR (MAX) NOT NULL,
    [Type]     INT           DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Username] ASC)
);

GO

CREATE TABLE [dbo].[Routes] (
    [Id]          INT          IDENTITY (0, 1) NOT NULL,
    [Source]      VARCHAR (MAX) NOT NULL,
    [Destination] VARCHAR (MAX) NOT NULL,
    [Departure]   DATETIME     DEFAULT (dateadd(hour,floor(rand() * (5 - 1) + 1),getdate())) NOT NULL,
    [Arrival]     DATETIME     DEFAULT (dateadd(hour,floor(rand() * (22 - 14) + 14),getdate())) NOT NULL,
    [Price]       DECIMAL(6,2)   DEFAULT ((875.99)) NOT NULL,
    [BusOperator] VARCHAR (MAX)  DEFAULT ('KSRTC') NOT NULL,
    [Seats]       INT          DEFAULT floor(rand() * (35 - 25) + 25) NOT NULL,
    [ACType]      INT          DEFAULT ((0)) NOT NULL,
    [SleeperType] INT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE TABLE [dbo].[Bookings] (
    [Id]          INT          IDENTITY (0, 1) NOT NULL,
    [RouteId]     INT          NOT NULL,
	[Username]    VARCHAR(50) NOT NULL,
    [Seats]       INT          DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE TRIGGER [dbo].[TicketCancellation]
	ON [dbo].[Bookings]
	AFTER DELETE
	AS
	BEGIN
		SET NOCOUNT ON
		UPDATE [Routes]
		SET [Routes].[Seats]=[Routes].[Seats]+[deleted].[Seats]
		FROM [deleted]
		WHERE [Routes].[Id]=[deleted].[RouteId]
	END

GO

ENABLE TRIGGER [dbo].[TicketCancellation] ON [dbo].[Bookings];

GO

CREATE TRIGGER [dbo].[TicketUpdating]
	ON [dbo].[Bookings]
	AFTER UPDATE
	AS
	BEGIN
		SET NOCOUNT ON
		UPDATE [Routes]
		SET [Routes].[Seats]=[Routes].[Seats]+[deleted].[Seats]-[inserted].[Seats]
		FROM [deleted], [inserted]
		WHERE [Routes].[Id]=[deleted].[RouteId] AND [Routes].[Id]=[inserted].[RouteId]
	END

GO

ENABLE TRIGGER [dbo].[TicketUpdating] ON [dbo].[Bookings];

GO

CREATE TRIGGER [dbo].[TicketBooking]
	ON [dbo].[Bookings]
	AFTER INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		UPDATE [Routes]
		SET [Routes].[Seats]=[Routes].[Seats]-[inserted].[Seats]
		FROM [inserted]
		WHERE [Routes].[Id]=[inserted].[RouteId]
	END

GO

ENABLE TRIGGER [dbo].[TicketBooking] ON [dbo].[Bookings];

GO

INSERT INTO [dbo].[Customers] ([Username], [Password], [Name], [Type]) VALUES (N'admin', N'admin', N'MyBus Admin', 0);
INSERT INTO [dbo].[Customers] ([Username], [Password], [Name], [Type]) VALUES (N'kb', N'kb', N'Krishna Birla', 1);
INSERT INTO [dbo].[Customers] ([Username], [Password], [Name], [Type]) VALUES (N'owner', N'owner', N'MyBus Owner', 0);
INSERT INTO [dbo].[Customers] ([Username], [Password], [Name], [Type]) VALUES (N'vpai', N'vpai', N'Vighnesh Pai', 1);

GO

INSERT INTO [dbo].[Routes] ([Source], [Destination], [BusOperator], [ACType]) VALUES ('Manipal', 'Bangalore', 'KTC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [ACType]) VALUES ('Pune', 'Nasik', 1025.75, 'BMTC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [SleeperType]) VALUES ('Manipal', 'Bangalore', 999.25, 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [SleeperType]) VALUES ('Pune', 'Nasik', 1250.5, 'BMTC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price]) VALUES ('Manipal', 'Bangalore', 600.39);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator]) VALUES ('Pune', 'Nasik', 675.10, 'BMTC');
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [ACType], [SleeperType]) VALUES ('Manipal', 'Bangalore', 1480.80, 'KTC', 1, 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [ACType], [SleeperType]) VALUES ('Pune', 'Nasik', 1645.95, 'BMTC', 1, 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [ACType]) VALUES ('Bangalore', 'Manipal', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [ACType]) VALUES ('Nasik', 'Pune', 1025.75, 'BMTC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [SleeperType]) VALUES ('Bangalore', 'Manipal', 999.25, 'KTC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [SleeperType]) VALUES ('Nasik', 'Pune', 1250.5, 'BMTC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price]) VALUES ('Bangalore', 'Manipal', 600.39);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [BusOperator], [Price]) VALUES ('Nasik', 'Pune', 'BMTC', 675.10);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [ACType], [SleeperType]) VALUES ('Bangalore', 'Manipal', 1480.80, 1, 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [ACType], [SleeperType]) VALUES ('Nasik', 'Pune', 1645.95, 'BMTC', 1, 1);

INSERT INTO [dbo].[Routes] ([Source], [Destination], [BusOperator], [ACType]) VALUES ('Manipal', 'Bangalore', 'KTC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [ACType]) VALUES ('Pune', 'Nasik', 1125.75, 'BMC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [SleeperType]) VALUES ('Manipal', 'Bangalore', 969.25, 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [SleeperType]) VALUES ('Pune', 'Nasik', 1290.5, 'BMC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price]) VALUES ('Manipal', 'Bangalore', 640.39);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator]) VALUES ('Pune', 'Nasik', 615.10, 'BMC');
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [ACType], [SleeperType]) VALUES ('Manipal', 'Bangalore', 1490.80, 'KTC', 1, 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [ACType], [SleeperType]) VALUES ('Pune', 'Nasik', 1545.95, 'BMC', 1, 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [ACType]) VALUES ('Bangalore', 'Manipal', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [ACType]) VALUES ('Nasik', 'Pune', 1125.75, 'BMC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [SleeperType]) VALUES ('Bangalore', 'Manipal', 969.25, 'KTC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [SleeperType]) VALUES ('Nasik', 'Pune', 1290.5, 'BMC', 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price]) VALUES ('Bangalore', 'Manipal', 640.39);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [BusOperator], [Price]) VALUES ('Nasik', 'Pune', 'BMC', 615.10);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [ACType], [SleeperType]) VALUES ('Bangalore', 'Manipal', 1490.80, 1, 1);
INSERT INTO [dbo].[Routes] ([Source], [Destination], [Price], [BusOperator], [ACType], [SleeperType]) VALUES ('Nasik', 'Pune', 1545.95, 'BMC', 1, 1);

GO

INSERT INTO [dbo].[Bookings] ([RouteId], [Username], [Seats]) VALUES (3, 'kb', 2);
INSERT INTO [dbo].[Bookings] ([RouteId], [Username], [Seats]) VALUES (6, 'kb', 3);
INSERT INTO [dbo].[Bookings] ([RouteId], [Username], [Seats]) VALUES (11, 'kb', 2);
INSERT INTO [dbo].[Bookings] ([RouteId], [Username], [Seats]) VALUES (9, 'vpai', 1);
INSERT INTO [dbo].[Bookings] ([RouteId], [Username], [Seats]) VALUES (21, 'vpai', 4);
INSERT INTO [dbo].[Bookings] ([RouteId], [Username], [Seats]) VALUES (26, 'vpai', 3);

GO