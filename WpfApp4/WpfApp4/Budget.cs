using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace WpfApp4
{
    public class Budget
    {
        public string name;
        public string record_type;
        public double amount;
        public bool is_income;
        public DateTime date;
        //public static List<Budget> budget = new List<Budget>();
        /*
        public Budget(string name, string record_type, double amount, bool is_income, DateTime date)
        {
            this.name = name;
            this.record_type = record_type;
            this.amount = amount;
            this.is_income = is_income;
            this.date = date;
        }*/

        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string RecordType
        {
            get { return record_type; }
            set { 
                record_type = value;
                
            }
        }
        public double Amount
        {
            get { return amount; }
            set
            {
                amount = value;
            }
        }
        public bool Is_income
        {
            get { return is_income; }
            set
            {
                is_income= value;
                
            }

        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
            }
        }




    }
}
