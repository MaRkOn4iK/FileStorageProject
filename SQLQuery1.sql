--Create database FileStorage
--Create table [User]
--(
--Id int primary key identity(1,1),
--[Login] nvarchar(100),
--[Password] nvarchar(100),
--[Name] nvarchar(100),
--[LastName] nvarchar(100),
--[Email] nvarchar(100)
--)

--Create table FileType
--(
--Id int primary key identity(1,1),
--TypeName nvarchar(100)
--)

--Create table [File]
--(
--Id int primary key identity(1,1),
--[FileName] nvarchar(100),
--[FileStreamCol] varbinary(max),
--[FileCreateDate] datetime,
--[FileTypeId] int foreign key references FileType(Id)
--)

--Create table FileSecureLevel
--(
--Id int primary key identity(1,1),
--SecureLevelName nvarchar(100)
--)

--Create table [FullFileInfo]
--(
--Id int primary key identity(1,1),
--[FileId] int foreign key references [File](Id),
--[UserId] int foreign key references [User](Id),
--[FileSecureLevelId] int foreign key references FileSecureLevel(Id)
--)