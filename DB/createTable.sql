USE TimeSeries;
GO

DROP TABLE Series;
GO
CREATE TABLE Series
(
	--ID int identity (1, 1) primary key,
	dateVisit datetime not null primary key,
	visitors int not null
)