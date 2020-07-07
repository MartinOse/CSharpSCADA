
CREATE TABLE [ALARM_DATA]
( 
	[AlarmId]            int  NOT NULL ,
	[ActivationTime]     datetime  NULL ,
	[AlarmType]          varchar(20)  NULL ,
	[TagId]              int  NULL ,
	[AlarmLimits]        float  NULL ,
	[Description]        varchar(30)  NULL ,
	[AcknowledgeTime]    datetime  NULL ,
	[OpUsername]         varchar(30)  NULL ,
	[DataId]             int  NULL 
)
go

ALTER TABLE [ALARM_DATA]
	ADD CONSTRAINT [XPKALARM_DATA] PRIMARY KEY  CLUSTERED ([AlarmId] ASC)
go

CREATE TABLE [OPERATOR]
( 
	[OperatorId]         int  NOT NULL ,
	[OpUsername]         varchar(30)  NULL ,
	[OpPassword]         varchar(30)  NULL 
)
go

ALTER TABLE [OPERATOR]
	ADD CONSTRAINT [XPKOPERATOR] PRIMARY KEY  CLUSTERED ([OperatorId] ASC)
go

CREATE TABLE [OPERATOR_LOG]
( 
	[ActionId]           int  NOT NULL ,
	[OperatorId]         int  NULL ,
	[OpUsername]         varchar(30)  NULL ,
	[Action]             varchar(30)  NULL ,
	[TagId]              int  NULL 
)
go

ALTER TABLE [OPERATOR_LOG]
	ADD CONSTRAINT [XPKOPERATOR_LOG] PRIMARY KEY  CLUSTERED ([ActionId] ASC)
go

CREATE TABLE [TAG_CONFIG]
( 
	[TagId]              int  NOT NULL ,
	[TagName]            varchar(20)  NULL ,
	[ItemId]             int  NULL ,
	[ItemUrl]            varchar(30)  NULL ,
	[Type]               varchar(20)  NULL ,
	[Setpoint]           float  NULL 
)
go

ALTER TABLE [TAG_CONFIG]
	ADD CONSTRAINT [XPKTAG_CONFIG] PRIMARY KEY  CLUSTERED ([TagId] ASC)
go

CREATE TABLE [TAG_DATA]
( 
	[DataId]             int  NOT NULL ,
	[TagId]              int  NULL ,
	[Temperature]        float  NULL ,
	[Timestamp]          datetime  NULL ,
	[Status]             varchar(20)  NULL ,
	[Setpoint]           float  NULL ,
	[Signal]             float  NULL 
)
go

ALTER TABLE [TAG_DATA]
	ADD CONSTRAINT [XPKTAG_DATA] PRIMARY KEY  CLUSTERED ([DataId] ASC)
go


ALTER TABLE [ALARM_DATA]
	ADD CONSTRAINT [R_2] FOREIGN KEY ([DataId]) REFERENCES [TAG_DATA]([DataId])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go

ALTER TABLE [ALARM_DATA]
	ADD CONSTRAINT [R_3] FOREIGN KEY ([TagId]) REFERENCES [TAG_CONFIG]([TagId])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go


ALTER TABLE [OPERATOR_LOG]
	ADD CONSTRAINT [R_4] FOREIGN KEY ([OperatorId]) REFERENCES [OPERATOR]([OperatorId])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go

ALTER TABLE [OPERATOR_LOG]
	ADD CONSTRAINT [R_18] FOREIGN KEY ([TagId]) REFERENCES [TAG_CONFIG]([TagId])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go


ALTER TABLE [TAG_DATA]
	ADD CONSTRAINT [R_1] FOREIGN KEY ([TagId]) REFERENCES [TAG_CONFIG]([TagId])
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go
