using System;
using Bank.Models.Enums;

namespace Bank.Models
{
    public class Account
    {
        public string Name { get; set; }
        public decimal Credit { get; private set; }
        public decimal Balance { get; private set; }
        public AccountType AccountType { get; set; }

        public Account(string name, AccountType accountType, decimal credit = 0, decimal balance = 0)
        {
            Name = name;
            Credit = credit;
            Balance = balance;
            AccountType = accountType;
        }

        public void Withdraw(decimal value)
        {
            if (Balance - value < Credit * -1)
                throw new InvalidOperationException("Saldo insuficiente!");

            Balance -= value;
        }

        public void Deposit(decimal value)
        {
            if (value <= 0)
                throw new InvalidOperationException("Valor de depósito deve ser maior que zero");

            Balance += value;
        }

        public void MakeTransfer(decimal value, Account recipient)
        {
            if (Balance - value < Credit * -1)
                throw new InvalidOperationException("Saldo insuficiente!");

            Withdraw(value);

            recipient.Deposit(value);
        }

        public override string ToString() => $"Tipo conta: {AccountType} - Nome: {Name} - Saldo: {Balance} - Crédito: {Credit}";
    }
}
