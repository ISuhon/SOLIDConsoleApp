﻿using SOLIDConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDConsoleApp.Client
{
    internal class Transaction : ITransaction
    {
        internal event Message _message;
        public Transaction(string? payee, int receiverID, DateTime transactionDate, double transactionAmount) 
        {
            this.Payee = payee;
            this.ReceiverID = receiverID;
            this.TransactionDate = transactionDate; 
            this.TransactionAmount = transactionAmount;

            this._message += MessageOfSuccessfulTransaction;
            this._message("Transaction done successful");
        }
        public string? Payee { get; set; }
        public int ReceiverID { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set;}

        public override string? ToString()
        {
            return $"Payee : {Payee}\n" +
                $"ReceiverID : {ReceiverID}\n" +
                $"Transaction Date : {TransactionDate}\n" +
                $"Transaction Amount : {TransactionAmount}";
        }

        void MessageOfSuccessfulTransaction(string message) => Console.WriteLine(message);
    }
}