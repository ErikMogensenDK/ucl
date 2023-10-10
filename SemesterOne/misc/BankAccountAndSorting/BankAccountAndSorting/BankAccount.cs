using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountAndSorting
{
    public class BankAccount
    {
        private double balance;
        public string Name { get; set; }
        public double Balance { get => balance; }
        private bool locked;
        public BankAccount(double balance)
        {
            this.balance = balance;
        }
        public BankAccount(string name, double balance) : this(balance)
        {
            Name = name;
        }
        public BankAccount(string name, double balance, bool locked) : this(name, balance)
        {
            this.locked = locked;
        }

        public void Deposit(double amount)
        {
            if (!locked)
                balance += amount;
        }
        public void Withdraw(double amount)
        {
            if (!locked && amount <= balance)
                balance -= amount;
        }
        public void ChangeLockState()
        {
            if (locked)
                locked = false;
            else 
                locked = true;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Balance: {Balance}";
        }

    }
}