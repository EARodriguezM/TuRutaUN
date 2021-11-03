-- **************************** ExternalLoginDB ****************************

USE [ExternalLoginDB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [login_user](
	[login_user_id] NVARCHAR(10) NOT NULL,
	[username] NVARCHAR(20) NOT NULL,
    [password_hash] VARBINARY(128) NULL,
    [password_salt] VARBINARY(128) NULL,
    [status] BIT DEFAULT 1 NOT NULL,
    [last_update] SMALLDATETIME DEFAULT GETDATE(),
    CONSTRAINT [login_user_pk] PRIMARY KEY ([login_user_id]),
    CONSTRAINT [login_user_username_uk] UNIQUE ([username]),
    CONSTRAINT [login_user_status_ck] CHECK([status] = 1 OR [status] = 0)
);
GO
SET ANSI_PADDING OFF
GO

-- **************************** TuRutaUN ****************************
USE [TuRutaUN]
GO
--------------------------------------
-- disability_types : later

--------------------------------------
-- path
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [path](
    [path_id] BIGINT IDENTITY(1,1) NOT NULL,
    [path_name] NVARCHAR(50) NOT NULL,
    [path_gpx_file] XML NOT NULL,
    [last_update] SMALLDATETIME DEFAULT GETDATE(),
    [status] BIT DEFAULT 1 NOT NULL,
    CONSTRAINT [path_pk] PRIMARY KEY ([path_id]),
    CONSTRAINT [path_name_uk] UNIQUE ([path_name]),
    CONSTRAINT [path_status_ck] CHECK([status] = 1 OR [status] = 0) 
);
GO
SET ANSI_PADDING OFF
GO

--------------------------------------
-- stage
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [stage](
    [stage_id] BIGINT IDENTITY(1,1) NOT NULL,
    [stage_name] NVARCHAR(50) NOT NULL,
    [x_coordinate] NVARCHAR(50) NOT NULL,
    [y_coordinate] NVARCHAR(50) NOT NULL,
    [last_update] SMALLDATETIME DEFAULT GETDATE(),
    [status] BIT DEFAULT 1 NOT NULL,
    CONSTRAINT [stage_pk] PRIMARY KEY ([stage_id]),
    CONSTRAINT [stage_name_uk] UNIQUE ([stage_name]),
    CONSTRAINT [stage_status_ck] CHECK([status] = 1 OR [status] = 0) 
);
GO
SET ANSI_PADDING OFF
GO
--------------------------------------
-- route
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [route](
    [route_id] BIGINT IDENTITY(1,1) NOT NULL,
    [route_name] VARCHAR(50) NOT NULL,
    [departure_time] SMALLDATETIME DEFAULT GETDATE(),
    [arrive_time] SMALLDATETIME DEFAULT GETDATE(),
    [last_update] SMALLDATETIME DEFAULT GETDATE(),
    [status] BIT DEFAULT 1 NOT NULL,
    CONSTRAINT [route_pk] PRIMARY KEY ([route_id]),
    CONSTRAINT [route_status_ck] CHECK([status] = 1 OR [status] = 0) 
);
GO
SET ANSI_PADDING OFF
GO
--------------------------------------
-- map
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [map](
    [map_id] BIGINT IDENTITY(1,1) NOT NULL,
    [route_id] BIGINT NOT NULL,
    [path_id] BIGINT NOT NULL,
    [stage_id] BIGINT NOT NULL,
    [last_update] SMALLDATETIME DEFAULT GETDATE(),
    [status] BIT DEFAULT 1 NOT NULL,
    CONSTRAINT [map_pk] PRIMARY KEY ([map_id]),
    CONSTRAINT [map_fk-route] FOREIGN KEY ([route_id]) REFERENCES [route] ([route_id]),
    CONSTRAINT [map_fk-path] FOREIGN KEY ([path_id]) REFERENCES [path] ([path_id]),
    CONSTRAINT [map_fk-stage] FOREIGN KEY ([stage_id]) REFERENCES [stage] ([stage_id]),
    CONSTRAINT [map_status_ck] CHECK([status] = 1 OR [status] = 0) 
);
GO
SET ANSI_PADDING OFF
GO
--------------------------------------
-- bus
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [bus](
    [bus_plate] NVARCHAR(10) NOT NULL,
    [number] NVARCHAR(50) NOT NULL,
	[brand] NVARCHAR(50) NOT NULL,
    [line] NVARCHAR(50) NOT NULL,
	[model] NVARCHAR(4) NOT NULL,
    [color] NVARCHAR(50) NOT NULL,
	[description] NVARCHAR(50) NULL,
    [status] BIT DEFAULT 1 NOT NULL,
    [last_update] SMALLDATETIME DEFAULT GETDATE(),
    CONSTRAINT [bus_pk] PRIMARY KEY ([bus_plate]),
    CONSTRAINT [bus_number_uk] UNIQUE ([number]),
    CONSTRAINT [bus_status_ck] CHECK([status] = 1 OR [status] = 0) 
);
GO
SET ANSI_PADDING OFF
GO
--------------------------------------
-- route_assigment
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [route_assigment](
    [route_assigment_id] BIGINT IDENTITY(1,1) NOT NULL,
    [bus_plate] NVARCHAR(10) NOT NULL,
    [map_id] BIGINT NOT NULL,
    [status] BIT DEFAULT 1 NOT NULL,
    [last_update] SMALLDATETIME DEFAULT GETDATE(),
    CONSTRAINT [route_assigment_pk] PRIMARY KEY ([route_assigment_id]),
    CONSTRAINT [route_assigment_fk-bus] FOREIGN KEY ([bus_plate]) REFERENCES [bus] ([bus_plate]),
    CONSTRAINT [route_assigment_fk-map] FOREIGN KEY ([map_id]) REFERENCES [map] ([map_id]),
    CONSTRAINT [route_assigment_status_ck] CHECK([status] = 1 OR [status] = 0)
);
GO
SET ANSI_PADDING OFF
GO


--------------------------------------
-- bus-disability_types : later

--------------------------------------
-- driver

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [driver](
    [driver_id] NVARCHAR(10) NOT NULL,
    [first_name] NVARCHAR(50) NOT NULL,
    [second_name] NVARCHAR(50) NULL,
	[first_surname] NVARCHAR(50) NOT NULL,
	[second_surname] NVARCHAR(50) NOT NULL,
	[email] NVARCHAR(50) NULL,
    [mobile] NVARCHAR(14) NOT NULL,
	[profile_picture] IMAGE NULL,
    [driver_license] IMAGE NOT NULL,
    [id_card] IMAGE NOT NULL,
    [status] BIT DEFAULT 1 NOT NULL,
    [last_update] SMALLDATETIME DEFAULT GETDATE(),
    CONSTRAINT [driver_pk] PRIMARY KEY ([driver_id]),
    CONSTRAINT [driver_email_uk] UNIQUE ([email]),
    CONSTRAINT [driver_email_ck] CHECK([email] LIKE '%@unal.edu.co%'), --Check if the driver has a valid email
    CONSTRAINT [driver_mobile_uk] UNIQUE ([mobile]),
    CONSTRAINT [driver_mobile_ck] CHECK([mobile] LIKE '+%'),
    CONSTRAINT [driver_status_ck] CHECK([status] = 1 OR [status] = 0) 
);
GO
SET ANSI_PADDING OFF
GO

--------------------------------------
-- user_type

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [user_type](
	[user_type_id] TINYINT NOT NULL,
	[description] NVARCHAR(20) NOT NULL,
    [status] BIT DEFAULT 1 NOT NULL,
    [last_update] SMALLDATETIME DEFAULT GETDATE(),
    CONSTRAINT [user_type_pk] PRIMARY KEY ([user_type_id]),
    CONSTRAINT [user_type_description_uk] UNIQUE ([description]),
    CONSTRAINT [user_type_status_ck] CHECK([status] = 1 OR [status] = 0)
);
GO
SET ANSI_PADDING OFF
GO

--------------------------------------
-- user

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [user](
    [user_id] NVARCHAR(10) NOT NULL,
    [first_name] NVARCHAR(50) NOT NULL,
    [second_name] NVARCHAR(50) NULL,
	[first_surname] NVARCHAR(50) NOT NULL,
	[second_surname] NVARCHAR(50) NOT NULL,
	[email] NVARCHAR(50) NULL,
    [mobile] NVARCHAR(14) NOT NULL,
	[profile_picture] IMAGE NULL,
    [status] BIT DEFAULT 1 NOT NULL,
    [user_type_id] TINYINT NOT NULL,
    [last_update] SMALLDATETIME DEFAULT GETDATE(),
    CONSTRAINT [user_pk] PRIMARY KEY ([user_id]),
    CONSTRAINT [user_fk-user_type] FOREIGN KEY ([user_type_id]) REFERENCES [user_type](user_type_id),
    CONSTRAINT [user_email_uk] UNIQUE ([email]),
    CONSTRAINT [user_email_ck] CHECK([email] LIKE '%@unal.edu.co%'),
    CONSTRAINT [user_mobile_uk] UNIQUE ([mobile]),
    CONSTRAINT [user_mobile_ck] CHECK([mobile] LIKE '+%'),
    CONSTRAINT [user_status_ck] CHECK([status] = 1 OR [status] = 0) 
);
GO
SET ANSI_PADDING OFF
GO