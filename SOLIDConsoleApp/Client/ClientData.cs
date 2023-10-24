using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SOLIDConsoleApp.Interfaces;
using SOLIDConsoleApp.MainProgram;

namespace SOLIDConsoleApp.Client
{
    internal delegate void Message(string message);
    internal class ClientData : IClientData
    {
        internal event Message? _message;
        public ClientData() 
        {
            this.Id = 0;
            this.FirstName = null;
            this.MiddleName = null;
            this.LastName = null;
            this.PhoneNumber = null;
            this.Email = null;
            this.Balance = null;

            this._message += MessageOfCreatedClient;
            this._message("Successfully created client with standard data");

            InputHelper.inputWait();
        }
        public ClientData(int ID) 
        {
            this.Id = ID;

            this._message += MessageOfCreatedClient;
            this._message("Successfully created client with ID");

            InputHelper.inputWait();
        }
        public ClientData(int ID, string? firstName, string? middleName, string? lastName, string? phone, 
            string? email, ClientBalance? clientBalance) 
        {
            this.Id = ID;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.PhoneNumber = phone;
            this.Email = email;
            this.Balance = clientBalance;

            this._message += MessageOfCreatedClient;
            this._message("Successfully created client : \n" + this);

            InputHelper.inputWait();
        }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public IClientBalance? Balance { get; set; }
        public override string ToString()
        {
            return $"ID : {Id}\n" +
                $"First name : {FirstName}\n" +
                $"Middle name : {MiddleName}\n" +
                $"Last name : {LastName}\n" +
                $"Phone number : {PhoneNumber}\n" +
                $"e-Mail : {Email}" +
                $"========================\n";
        }

        void MessageOfCreatedClient(string message) => Console.WriteLine(message);
    }
}