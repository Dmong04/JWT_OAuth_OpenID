use usersdb
go

create table Countries (
	country_id int not null primary key identity,
	[name] varchar(20) not null,
)

CREATE TABLE Users(
	[id] [int] IDENTITY(1,1) NOT NULL primary key,
	[username] [nvarchar](30) NOT NULL,
	[password] [nvarchar](80) NOT NULL,
	[role] [nvarchar](10) NOT NULL,
	[country_id] [int] not null
)