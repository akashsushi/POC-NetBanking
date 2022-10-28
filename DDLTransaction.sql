CREATE TABLE [transaction](
transactionId INT PRIMARY KEY IDENTITY(10000,1),
sourceAccountNo VARCHAR(20) FOREIGN KEY REFERENCES account(accountNumber) not null,
transactionAmount FLOAT not null,
transactionType VARCHAR(10) not null,
transactionDate DATE not null,
destinationAccountNo VARCHAR(20) null,
transactionDescription NVARCHAR(30) not null
)
GO

