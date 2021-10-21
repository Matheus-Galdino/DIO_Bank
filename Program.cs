using Bank.Models;
using Bank.Models.Enums;
using System;
using System.Collections.Generic;
using static System.Console;

namespace Bank
{
    class Program
    {
        static List<Account> accounts = new List<Account>();
        static Dictionary<string, Action> menuOptions = new Dictionary<string, Action>
        {
            { "1", ListAccounts },
            { "2", CreateAccount },
            { "3", MakeTransfer },
            { "4", Withdraw },
            { "5", Deposit },
            { "C", Clear },
            { "X", () => WriteLine("Obrigado por utilizar nossos serviços.") }
        };


        static void Main(string[] args)
        {
            string option;
            do
            {
                option = GetOption();

                try
                {
                    menuOptions[option]?.Invoke();
                }
                catch (InvalidOperationException e)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(e.Message);
                    WriteLine();
                    ForegroundColor = ConsoleColor.White;
                }
                catch(Exception)
                {
                    continue;
                }

            } while (option.ToUpper() != "X");            
        }

        static string GetOption()
        {
            WriteLine("\nDIO Bank a seu dispor!");
            WriteLine("Informe a opção desejada: ");

            WriteLine("1- Listar contas");
            WriteLine("2- Criar conta");
            WriteLine("3- Transferir");
            WriteLine("4- Sacar");
            WriteLine("5- Depositar");
            WriteLine("C- Limpar tela");
            WriteLine("X- Sair\n");

            var option = ReadLine().ToUpper();

            WriteLine();
            return option;
        }

        static void ListAccounts()
        {
            WriteLine("Listar contas");

            if (accounts.Count == 0)
            {
                WriteLine("Nenhuma conta cadastrada");
                return;
            }

            for (int i = 0; i < accounts.Count; i++)
                WriteLine($"#{i} - {accounts[i]}");

        }

        static void CreateAccount()
        {
            WriteLine("Criar conta: ");

            Write("Digite 1 para Conta Física ou 2 para Jurídica: ");
            var accountType = int.Parse(ReadLine());

            Write("Digite o nome do cliente: ");
            var name = ReadLine();

            Write("Digite o saldo inicial: ");
            var balance = decimal.Parse(ReadLine());

            Write("Digite o crédito: ");
            var credit = decimal.Parse(ReadLine());

            var newAccount = new Account(name, (AccountType)accountType, credit, balance);

            accounts.Add(newAccount);
        }

        private static void MakeTransfer()
        {
            Write("Digite o número da sua conta: ");
            int index = int.Parse(ReadLine());

            Write("Digite o valor a ser transferido: ");
            decimal value = decimal.Parse(ReadLine());

            Write("Digite o número da conta do destinatário: ");
            int indexRecipient = int.Parse(ReadLine());

            accounts[index].MakeTransfer(value, accounts[indexRecipient]);
        }

        private static void Withdraw()
        {
            Write("Digite o número da conta: ");
            int index = int.Parse(ReadLine());

            Write("Digite o valor a ser sacado: ");
            decimal value = decimal.Parse(ReadLine());

            accounts[index].Withdraw(value);
        }

        private static void Deposit()
        {
            Write("Digite o número da conta: ");
            int index = int.Parse(ReadLine());

            Write("Digite o valor a ser depositado: ");
            decimal value = decimal.Parse(ReadLine());

            accounts[index].Deposit(value);
        }
    }
}
