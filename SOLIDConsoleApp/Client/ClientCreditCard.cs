using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SOLIDConsoleApp.Client
{
    internal class ClientCreditCard : ICreditCard
    {
        internal event Message _message;
        public ClientCreditCard()
        {
            this.CardNumber = null;
            this.ExpirationDate = DateTime.Now;
            this.CVVcode = 0;
            this.PIN = 0;
            this.Fortune = 0.0;

            this._message += MessageOfCreatedCreditCard;
            this._message("Created new credit card with standard data");
        }

        public ClientCreditCard(string? cardNumber, DateTime expirationDate, int CVV, int pin, double fortune, TransactionHistory transactions) 
        {
            this.CardNumber = cardNumber;
            this.ExpirationDate = expirationDate;
            this.CVVcode = CVV;
            this.PIN = pin;
            this.Fortune = fortune;
            this.transactions = transactions;

            this._message += MessageOfCreatedCreditCard;
            this._message("Created new credit card : \n" + this);
        }

        public string? CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVVcode { get; set; }
        public int PIN { get; set; }
        public double Fortune { get; set; }

        [NotMapped]
        public ITransactionHistory? transactions { get; set; }

        public int BalanceID { get; set; } // Foreign key

        [Key]
        public int CreditCardID { get; set; } // Primary key
        public ClientBalance Balance { get; set; }
        public ICollection<Transaction> TransactionsForDB { get; set; }
        
        public override string? ToString()
        {
            return  $"Card number : {CardNumber}\n" +
                    $"Expiration date : {ExpirationDate}\n" +
                    $"CVV code  : {CVVcode}\n" +
                    $"PIN : {PIN}\n" +
                    $"Fortune {Fortune}\n" +
                    $"========================\n";
        }

        void MessageOfCreatedCreditCard(string message) => Console.WriteLine(message);
    }
}
