using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.Client
{
    internal class CreditData : ICreditData
    {
        internal event Message _message;
        public CreditData(double creditSum, CreditType creditType, CreditStatus creditStatus, DateTime creditEndDate)
        {
            this.CreditSum = creditSum;
            this.CreditType = creditType;
            this.CreditStatus = creditStatus;
            this.CreditEndDate = creditEndDate;

            this._message += MessageOfCreatedCredit;
            this._message("Created credit : " + this);
        }
        public double CreditSum { get; set; }
        public CreditType CreditType { get; set; }
        public CreditStatus CreditStatus { get; set; }
        public DateTime CreditEndDate { get; set;  }
        public int BalanceID { get; set; } // Foreign key
        public int CreditID { get; set; } // Primary key

        public override string? ToString()
        {
            return $"Credit sum: {CreditSum}\n" + 
                $"Credit type: {CreditType.ToString()}" +
                $"Credit status: {CreditStatus.ToString()}\n" + 
                $"Credit end date: {CreditEndDate}" +
                $"========================\n";
        }

        void MessageOfCreatedCredit(string message) => Console.WriteLine(message);
    }

    enum CreditType
    {
        REVOLVING_CREDIT, INSTALLMENT_CREDIT, OPEN_CREDIT
    }

    enum CreditStatus
    {
        ACTIVE, OVERDUE, CLOSED
    }
}
