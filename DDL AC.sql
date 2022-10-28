CREATE TABLE account(
accountNumber VARCHAR(20) PRIMARY KEY,
customerId VARCHAR(20) FOREIGN KEY REFERENCES customer(customerId),
accountType VARCHAR(20) not null,
balance FLOAT not null,
DOC DATE not null,
TIN VARCHAR(20) not null,
IFSC VARCHAR(20) not null,
isDeleted bit not null
)
GO

CREATE TABLE currentAccount(
ind INT PRIMARY KEY ,
withdrawlLimit FLOAT not null,
minimumBalance FLOAT not null
)
GO

CREATE TABLE corporatetAccount(
ind INT PRIMARY KEY ,
withdrawlLimit FLOAT not null,
minimumBalance FLOAT not null
)
GO

CREATE TABLE savingsAccount(
ind INT PRIMARY KEY ,
withdrawlLimit FLOAT not null,
minimumBalance FLOAT not null
)
GO

CREATE SEQUENCE [dbo].[SequenceCounter]
 AS INT
 START WITH 1
 INCREMENT BY 1
 GO

INSERT INTO savingsAccount VALUES (NEXT VALUE FOR [dbo].[SequenceCounter],100000,1000);

INSERT INTO corporatetAccount VALUES (NEXT VALUE FOR [dbo].[SequenceCounter],200000,0);

INSERT INTO currentAccount VALUES (NEXT VALUE FOR [dbo].[SequenceCounter],500000,5000);
