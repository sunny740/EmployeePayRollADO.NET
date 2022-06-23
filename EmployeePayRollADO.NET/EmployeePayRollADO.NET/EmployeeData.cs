using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollADO.NET
{
    public class EmployeeData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public DateTime StartDate { get; set; }
        public char Gender { get; set; }
        public Int64 PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public double Basic_Pay { get; set; }
        public double Deductions { get; set; }
        public double Income_Tax { get; set; }
        public double Taxable_Pay { get; set; }
        public double Net_Pay { get; set; }
    }
}
