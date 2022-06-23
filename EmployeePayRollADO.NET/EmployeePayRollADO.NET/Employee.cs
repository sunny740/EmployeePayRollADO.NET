﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollADO.NET
{
    public class Employee
    {
        public static string connection = "Data Source = RAJVARDHAN; Initial Catalog = Payroll_Service";
        SqlConnection sqlConnection = new SqlConnection(connection);

        public void SetConnection()
        {
            if (sqlConnection != null && sqlConnection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    sqlConnection.Open();
                }
                catch (Exception)
                {
                    throw new EmployeeException(EmployeeException.ExceptionType.Connection_Failed, "Connection Failed");
                }
            }
        }
        public void Close()
        {
            if (sqlConnection != null && !sqlConnection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    sqlConnection.Close();
                }
                catch (Exception)
                {
                    throw new EmployeeException(EmployeeException.ExceptionType.Connection_Failed, "Connection Failed");
                }
            }
        }
        EmployeeData employeeData = new EmployeeData();
        public void GetSqlData()
        {
            sqlConnection.Open();
            string query = "select * from employee_payroll";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    employeeData.ID = sqlDataReader.GetInt32(0);
                    employeeData.Name = sqlDataReader["Name"].ToString();
                    employeeData.Salary = Convert.ToDouble(sqlDataReader["Salary"]);
                    employeeData.StartDate = Convert.ToDateTime(sqlDataReader["StartDate"]);
                    employeeData.Gender = Convert.ToChar(sqlDataReader["Gender"]);
                    employeeData.PhoneNumber = (long)Convert.ToDouble(sqlDataReader["PhoneNumber"]);
                    employeeData.Address = sqlDataReader["Address"].ToString();
                    employeeData.Department = sqlDataReader["Department"].ToString();
                    employeeData.Basic_Pay = Convert.ToDouble(sqlDataReader["BasicPay"]);
                    employeeData.Deductions = Convert.ToDouble(sqlDataReader["Deduction"]);
                    employeeData.Income_Tax = Convert.ToDouble(sqlDataReader["IncomeTax"]);
                    employeeData.Taxable_Pay = Convert.ToDouble(sqlDataReader["TaxablePay"]);
                    employeeData.Net_Pay = Convert.ToDouble(sqlDataReader["NetPay"]);

                    Console.WriteLine("ID" + employeeData.ID + "\n" +
                        "Name" + employeeData.Name + "\n" +
                        "Salary" + employeeData.Salary +
                        "Start Date" + employeeData.StartDate +
                        "Gender" + employeeData.Gender +
                        "Phone Number" + employeeData.PhoneNumber +
                        "Address" + employeeData.Address +
                        "Department" + employeeData.Department +
                        "Basic Pay" + employeeData.Basic_Pay +
                        "Deductions" + employeeData.Deductions +
                        "Income Tax" + employeeData.Income_Tax +
                        "Taxable Pay" + employeeData.Taxable_Pay +
                        "Net Pay" + employeeData.Net_Pay);
                }
                sqlDataReader.Close();
            }
            sqlConnection.Close();
        }
        public void UpdateSalary()
        {
            sqlConnection.Open();
            string query = "update employee_payroll set BasicPay=3000000 where EmployeeName= 'Terrrisa'";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            int result = sqlCommand.ExecuteNonQuery();
            if (result != 0)
            {
                Console.WriteLine("Updated");
            }
            else
            {
                Console.WriteLine("Not Updated");
            }
            sqlConnection.Close();
            GetSqlData();
        }
        public int UpdateSalary(EmployeeData employeeData)
        {
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdateDetails", this.sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@salary", employeeData.Basic_Pay);
                    sqlCommand.Parameters.AddWithValue("@Name", employeeData.Name);
                    sqlCommand.Parameters.AddWithValue("@Id", employeeData.ID);

                    sqlConnection.Open();
                    result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Updated");
                    }
                    else
                    {
                        Console.WriteLine("Not Updated");
                    }

                }
            }
            catch (Exception)
            {
                throw new EmployeeException(EmployeeException.ExceptionType.No_data_found, "Cannot Update");
            }
            return result;
        }
        public string DataBasedOnDateRange()
        {
            string nameList = "";
            try
            {
                using (sqlConnection)
                {
                    string query = @"select * from employee_payroll where StartDate BETWEEN '2022-01-12' and GetDate()";
                    SqlCommand command = new SqlCommand(query, this.sqlConnection);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            DisplayEmployeeData(sqlDataReader);
                            nameList += sqlDataReader["Name"].ToString();
                        }
                    }
                    sqlDataReader.Close();
                }

            }
            catch (Exception)
            {
                throw new EmployeeException(EmployeeException.ExceptionType.No_data_found, "No Data Found");
            }
            sqlConnection.Close();
            return nameList;

        }
        public void DisplayEmployeeData(SqlDataReader sqlDataReader)
        {
            employeeData.ID = sqlDataReader.GetInt32(0);
            employeeData.Name = sqlDataReader["Name"].ToString();
            employeeData.Salary = Convert.ToDouble(sqlDataReader["Salary"]);
            employeeData.StartDate = Convert.ToDateTime(sqlDataReader["StartDate"]);
            employeeData.Gender = Convert.ToChar(sqlDataReader["Gender"]);
            employeeData.PhoneNumber = Convert.ToDouble(sqlDataReader["PhoneNumber"]);
            employeeData.Address = sqlDataReader["Address"].ToString();
            employeeData.Department = sqlDataReader["Department"].ToString();
            employeeData.Basic_Pay = Convert.ToDouble(sqlDataReader["BasicPay"]);
            employeeData.Deductions = Convert.ToDouble(sqlDataReader["Deduction"]);
            employeeData.Income_Tax = Convert.ToDouble(sqlDataReader["IncomeTax"]);
            employeeData.Taxable_Pay = Convert.ToDouble(sqlDataReader["TaxablePay"]);
            employeeData.Net_Pay = Convert.ToDouble(sqlDataReader["NetPay"]);

            Console.WriteLine("ID" + employeeData.ID + "\n" +
                "Name" + employeeData.Name + "\n" +
                "Salary" + employeeData.Salary +
                "Start Date" + employeeData.StartDate +
                "Gender" + employeeData.Gender +
                "Phone Number" + employeeData.PhoneNumber +
                "Address" + employeeData.Address +
                "Department" + employeeData.Department +
                "Basic Pay" + employeeData.Basic_Pay +
                "Deductions" + employeeData.Deductions +
                "Income Tax" + employeeData.Income_Tax +
                "Taxable Pay" + employeeData.Taxable_Pay +
                "Net Pay" + employeeData.Net_Pay);
        }
    }
}
