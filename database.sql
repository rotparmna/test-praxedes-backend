-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- praxedestestdb.dbo.ActivitiesApi definition

-- Drop table

-- DROP TABLE praxedestestdb.dbo.ActivitiesApi;

CREATE TABLE praxedestestdb.dbo.ActivitiesApi (
	IdActivityApi varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Resource varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Method] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Path] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	HttpStatusCode varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Exception] varchar(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Datetime] datetimeoffset(0) NULL,
	CONSTRAINT ActivitiesApi_PK PRIMARY KEY (IdActivityApi)
);


-- praxedestestdb.dbo.Posts definition

-- Drop table

-- DROP TABLE praxedestestdb.dbo.Posts;

CREATE TABLE praxedestestdb.dbo.Posts (
	IdUser int NOT NULL,
	Title varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Body varchar(2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IdPost int NOT NULL,
	CONSTRAINT Posts_PK PRIMARY KEY (IdPost)
);


-- praxedestestdb.dbo.Users definition

-- Drop table

-- DROP TABLE praxedestestdb.dbo.Users;

CREATE TABLE praxedestestdb.dbo.Users (
	UserId int IDENTITY(0,1) NOT NULL,
	DocumentNumber varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Names varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	LastName varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Gender varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Birthdate date NOT NULL,
	CONSTRAINT Users_PK PRIMARY KEY (UserId),
	CONSTRAINT Users_UNIQUE UNIQUE (DocumentNumber)
);


-- praxedestestdb.dbo.Comments definition

-- Drop table

-- DROP TABLE praxedestestdb.dbo.Comments;

CREATE TABLE praxedestestdb.dbo.Comments (
	IdPost int NOT NULL,
	Name varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Email varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Body varchar(1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	IdComment int NOT NULL,
	CONSTRAINT Comments_PK PRIMARY KEY (IdComment),
	CONSTRAINT Comments_Posts_FK FOREIGN KEY (IdPost) REFERENCES praxedestestdb.dbo.Posts(IdPost)
);


-- praxedestestdb.dbo.UsersRelationship definition

-- Drop table

-- DROP TABLE praxedestestdb.dbo.UsersRelationship;

CREATE TABLE praxedestestdb.dbo.UsersRelationship (
	UserRelationShipId int IDENTITY(0,1) NOT NULL,
	UserIdParent int NOT NULL,
	UserIdChild int NOT NULL,
	Relationship varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT UsersRelationship_PK PRIMARY KEY (UserRelationShipId),
	CONSTRAINT UsersRelationship_Users_FK FOREIGN KEY (UserIdParent) REFERENCES praxedestestdb.dbo.Users(UserId),
	CONSTRAINT UsersRelationship_Users_FK_1 FOREIGN KEY (UserIdChild) REFERENCES praxedestestdb.dbo.Users(UserId)
);

CREATE PROCEDURE dbo.spDeleteComment
@IdComment int
AS 
DELETE FROM dbo.Comments
WHERE IdComment=@IdComment;

CREATE PROCEDURE dbo.spDeletePost
@IdPost int
AS 
DELETE FROM dbo.Posts
WHERE IdPost=@IdPost;

CREATE PROCEDURE dbo.spDeleteUser
@UserId int
AS 
DELETE FROM dbo.UsersRelationship WHERE UserIdParent = @UserId OR UserIdChild = @UserId;
DELETE FROM dbo.Users WHERE UserId = @UserId;

CREATE PROCEDURE dbo.spGetCommentById
@IdComment int
AS 
SELECT IdComment, IdPost, Name, Email, Body
FROM dbo.Comments
WHERE IdComment=@IdComment;

CREATE PROCEDURE dbo.spGetComments
AS 
SELECT IdComment, IdPost, Name, Email, Body
FROM dbo.Comments;

CREATE PROCEDURE dbo.spGetCommentsByIdPost
@IdPost int
AS 
SELECT IdComment, IdPost, Name, Email, Body
FROM dbo.Comments
WHERE IdPost=@IdPost;

CREATE PROCEDURE dbo.spGetFamilyGroup
@UserId INT
AS 
SELECT u.UserId,
u.DocumentNumber,
u.Names,
u.LastName,
u.Gender,
u.Birthdate,
ur.Relationship
FROM dbo.Users u
INNER JOIN dbo.UsersRelationship ur on u.UserId = ur.UserIdParent
WHERE u.UserId = @UserId
RETURN;

CREATE PROCEDURE dbo.spGetPostById
@IdPost int
AS 
SELECT IdPost, IdUser, Title, Body
FROM dbo.Posts
WHERE IdPost=@IdPost;

CREATE PROCEDURE dbo.spGetPosts
AS 
SELECT IdPost, IdUser, Title, Body
FROM dbo.Posts;

CREATE PROCEDURE dbo.spGetUser
@DocumentNumber varchar(100)
AS 
SELECT UserId, DocumentNumber, Names, LastName, Gender, Birthdate
FROM dbo.Users
WHERE DocumentNumber = @DocumentNumber
RETURN;

CREATE PROCEDURE dbo.spGetUserById
@UserId int
AS 
SELECT UserId, DocumentNumber, Names, LastName, Gender, Birthdate
FROM dbo.Users
WHERE UserId = @UserId
RETURN;

CREATE PROCEDURE dbo.spGetUsers
AS 
SELECT UserId, DocumentNumber, Names, LastName, Gender, Birthdate
FROM dbo.Users
RETURN;

CREATE PROCEDURE dbo.spInsertActivityApi
@IdActivityApi varchar(100),
@Resource varchar(100),
@Method varchar(100),
@Path varchar(100),
@HttpStatusCode varchar(5),
@Exception varchar(1000)
AS 
BEGIN
	DECLARE @fechaActual DATETIMEOFFSET;
	SET @fechaActual = GETDATE();
	INSERT INTO dbo.ActivitiesApi
	(IdActivityApi, Resource, [Method], [Path], HttpStatusCode, [Exception], [Datetime])
	VALUES(@IdActivityApi, @Resource, @Method, @Path, @HttpStatusCode, @Exception, @fechaActual);
END;

CREATE PROCEDURE dbo.spInsertComment
@IdPost int,
@Name varchar(100),
@Email varchar(100),
@Body varchar(2000)
AS 
INSERT INTO dbo.Comments
(IdComment, IdPost, Name, Email, Body)
VALUES((SELECT COUNT(1)+1 FROM dbo.Comments), @IdPost, @Name, @Email, @Body);

CREATE PROCEDURE dbo.spInsertPost
@IdUser int,
@Title varchar(100),
@Body varchar(2000)
AS 
INSERT INTO dbo.Posts
(IdPost, IdUser, Title, Body)
VALUES((SELECT COUNT(1)+1 FROM dbo.Posts), @IdUser, @Title, @Body);

CREATE PROCEDURE dbo.spInsertUser
@DocumentNumber varchar(100),
@Names varchar(100),
@LastName varchar(100),
@Gender varchar(100),
@Birthdate date,
@UserId int OUT
AS 
INSERT INTO dbo.Users
(DocumentNumber, Names, LastName, Gender, Birthdate)
VALUES(@DocumentNumber, @Names, @LastName, @Gender, @Birthdate);
SET @UserId = SCOPE_IDENTITY();

CREATE PROCEDURE dbo.spInsertUserRelationship
@UserIdParent int,
@UserIdChild int,
@Relationship varchar(100)
AS 
INSERT INTO dbo.UsersRelationship
(UserIdParent, UserIdChild, Relationship)
VALUES(@UserIdParent, @UserIdChild, @Relationship);

CREATE PROCEDURE dbo.spUpdateActivityApi
@IdActivityApi varchar(100),
@HttpStatusCode varchar(5),
@Exception varchar(1000)
AS 
UPDATE praxedestestdb.dbo.ActivitiesApi
SET HttpStatusCode=@HttpStatusCode, 
[Exception]=@Exception
WHERE IdActivityApi=@IdActivityApi;

CREATE PROCEDURE dbo.spUpdateComment
@IdComment int,
@IdPost int,
@Name varchar(100),
@Email varchar(100),
@Body varchar(2000)
AS 
UPDATE dbo.Comments
SET IdPost=@IdPost, Name=@Name, Email=@Email, Body=@Body
WHERE IdComment=@IdComment;

CREATE PROCEDURE dbo.spUpdatePost
@IdPost int,
@IdUser int,
@Title varchar(100),
@Body varchar(2000)
AS 
UPDATE dbo.Posts
SET IdUser=@IdUser, Title=@Title, Body=@Body
WHERE IdPost=@IdPost;

CREATE PROCEDURE dbo.spUpdateUser
@UserId int,
@Names varchar(100),
@LastName varchar(100),
@Gender varchar(100),
@Birthdate date
AS 
UPDATE praxedestestdb.dbo.Users
SET Names=@Names, 
LastName=@LastName, 
Gender=@Gender, 
Birthdate=@Birthdate
WHERE UserId=@UserId;