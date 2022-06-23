create database EmpPayRoll
use EmpPayRoll

create table EmpPayRoll
(
    ID int,
	Name varchar(20),
	Salary float,
	StartDate Date,
	Gender varchar(1),
	PhoneNumber bigint,
	Address varchar(200),
	Department varchar(100),
	BasicPay bigint,
	Deductions bigint,
	TaxablePay bigint,
	IncomeTax bigint,
	NetPay bigint
)
select* from EmpPayRoll
--use [Payroll_Service]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE InsertDetails

	@ID int,
	@Name varchar(20),
	@Salary float,
	@StartDate Date,
	@Gender varchar(1),
	@PhoneNumber bigint,
	@Address varchar(200),
	@Department varchar(100),
	@BasicPay bigint,
	@Deductions bigint,
	@TaxablePay bigint,
	@IncomeTax bigint,
	@NetPay bigint
AS
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @new_identity INTEGER = 0;
	DECLARE @result bit = 0;
	INSERT INTO employee_payroll(Name, Salary, StartDate, Gender) VALUES (@Name, @Salary, @StartDate, @Gender)
	SELECT @new_identity = @@IDENTITY;
	INSERT INTO Departmenttable(Departmenttable.ID, Department) VALUES (@new_identity, @Department)
	INSERT INTO Payrollofemployee(Payrollofemployee.ID, BasicPay, IncomeTax) VALUES (@new_identity, @BasicPay, @IncomeTax)
	COMMIT TRANSACTION
	SET @result = 1;
	RETURN @result;
	END TRY
	BEGIN CATCH

	IF(XACT_STATE()) = -1
		BEGIN
		PRINT
		'Transaction is uncommitable' + ' Rolling back transaction'
		ROLLBACK TRANSACTION;
		RETURN @result;		
		END
	ELSE IF(XACT_STATE()) = 1
		BEGIN
		PRINT
		'Transaction is commitable' + ' Commiting back transaction'
		COMMIT TRANSACTION;
		SET @result = 1;
	    RETURN @result;
	END;
	END CATCH
END
GO