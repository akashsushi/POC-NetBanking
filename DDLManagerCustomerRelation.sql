CREATE TABLE ManagerCustomerRelation(
managerId varchar(20) foreign key references manager(managerId) not null,
customerId VARCHAR(20) FOREIGN KEY REFERENCES customer(customerId) not null
);
GO