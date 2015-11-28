USE TimeSeries;
GO

DROP TABLE Series;
GO
CREATE TABLE Series
(
	ID int identity (1, 1) primary key,
	dateVisit date not null,
	visitors int not null
)