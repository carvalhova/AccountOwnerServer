--Delete the Owner database if it exists.  
IF EXISTS(SELECT * from sys.databases WHERE name='AccountOwner')
BEGIN  
    DROP DATABASE AccountOwner;  
END  

USE [AccountOwner]
--
-- Table structure for table owner
--

CREATE TABLE Owner (
  OwnerId 		UNIQUEIDENTIFIER NOT NULL,
  Name 			VARCHAR(60) NOT NULL,
  DateOfBirth 	DATE NOT NULL,
  Address 		VARCHAR(100) NOT NULL,
  CONSTRAINT PK_Owner_OwnerId PRIMARY KEY (OwnerId)
)

--
-- Dumping data for table owner
--

INSERT INTO Owner 
VALUES ('24fd81f8-d58a-4bcc-9f35-dc6cd5641906','John Keen','1980-12-05','61 Wellfield Road'),
	   ('261e1685-cf26-494c-b17c-3546e65f5620','Anna Bosh','1974-11-14','27 Colored Row'),
	   ('66774006-2371-4d5b-8518-2177bcf3f73e','Nick Somion','1998-12-15','North sunny address 102'),
	   ('a3c1880c-674c-4d18-8f91-5d3608a2c937','Sam Query','1990-04-22','91 Western Roads'),
	   ('f98e4d74-0f68-4aac-89fd-047f1aaca6b6','Martin Miller','1983-05-21','3 Edgar Buildings');

--
-- Table structure for table Account
--
CREATE TABLE Account (
  AccountId 	UNIQUEIDENTIFIER NOT NULL,
  DateCreated 	DATE NOT NULL,
  AccountType 	VARCHAR(45) NOT NULL,
  OwnerId 		UNIQUEIDENTIFIER NOT NULL,
  CONSTRAINT PK_Account_AccountId 
	PRIMARY KEY (AccountId),
  CONSTRAINT FK_Owner_Account_OwnerId FOREIGN KEY (OwnerId)
	REFERENCES Owner(OwnerId)  
) 
--
-- Dumping data for table account
--

INSERT INTO Account 
VALUES ('03e91478-5608-4132-a753-d494dafce00b','2003-12-15','Domestic','f98e4d74-0f68-4aac-89fd-047f1aaca6b6'),
	   ('356a5a9b-64bf-4de0-bc84-5395a1fdc9c4','1996-02-15','Domestic','261e1685-cf26-494c-b17c-3546e65f5620'),
	   ('371b93f2-f8c5-4a32-894a-fc672741aa5b','1999-05-04','Domestic','24fd81f8-d58a-4bcc-9f35-dc6cd5641906'),
	   ('670775db-ecc0-4b90-a9ab-37cd0d8e2801','1999-12-21','Savings','24fd81f8-d58a-4bcc-9f35-dc6cd5641906'),
	   ('a3fbad0b-7f48-4feb-8ac0-6d3bbc997bfc','2010-05-28','Domestic','a3c1880c-674c-4d18-8f91-5d3608a2c937'),
	   ('aa15f658-04bb-4f73-82af-82db49d0fbef','1999-05-12','Foreign','24fd81f8-d58a-4bcc-9f35-dc6cd5641906'),
	   ('c6066eb0-53ca-43e1-97aa-3c2169eec659','1996-02-16','Foreign','261e1685-cf26-494c-b17c-3546e65f5620'),
	   ('eccadf79-85fe-402f-893c-32d3f03ed9b1','2010-06-20','Foreign','a3c1880c-674c-4d18-8f91-5d3608a2c937');

-- Dump completed on 2017-12-24 15:53:17