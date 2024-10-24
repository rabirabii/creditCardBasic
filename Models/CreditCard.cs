using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Models
{
    public class CreditCardClass
    {
        private string cardNumber;
        private string cardHolderName;
        private DateTime expiryDate;
        private int cvv;
        private decimal balance;
        private bool isActive;

        public CreditCardClass(string cardNumber, string cardHolderName, DateTime expiryDate, int cvv, decimal balance)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length != 16) 
                throw new ArgumentException("Nomor Kartu harus 16 digit!");

            if (string.IsNullOrWhiteSpace(cardHolderName))
                throw new ArgumentException("Nama pemegang kartu tidak boleh kosong!");

            if (expiryDate <= DateTime.Now)
                throw new ArgumentException("Tanggal Kadaluarsa tidak valid!");

            if (cvv < 100 || cvv > 999)
                throw new ArgumentException("CVV harus 3 Digit!");

            this.cardNumber = cardNumber;
            this.cardHolderName = cardHolderName;
            this.expiryDate = expiryDate;
            this.cvv = cvv;
            this.balance = 0;
            this.isActive = true;
        }

        public void Deposit(decimal amount) {

            if (!isActive)
                throw new InvalidOperationException("Kartu tidak Aktif!");

            if (amount <= 0)
                throw new ArgumentException("Jumlah deposit harus lebih dari 0!");

            balance += amount;
        }

        public bool Withdraw(decimal amount) {
            if (!isActive)
                throw new InvalidOperationException("Kartu tidak Aktif!");

            if (amount <= 0)
                throw new ArgumentException("Jumlah penarikan harus lebih dari 0!");

            if (amount > balance)
                return false;

            balance -= amount;
            return true;
        }

        public decimal getBalance()
        {
            return balance;
        }

        public void DeactiveCard()
        {
            isActive = false;
        }

        public void ActiveCard ()
        {
            if (isActive)
                throw new InvalidOperationException("Kartu sudah aktif");

            isActive = true;
        }

        public string getCardInfo()
        {
            return $"Informasi Kartu : \n" + 
                   $"Nomor Kartu : **** **** **** {cardNumber.Substring(12)}\n" +
                   $"Nama Pemegang Kartu : {cardHolderName}\n" +
                   $"Tanggal Kadaluarsa : {expiryDate:MM/YY}\n" +
                   $"Saldo : {balance:F2}\n" +
                   $"Status : { (isActive ? "Active" : "Inactive" )}"
                ;
        }

        public static bool ValidateCardNumber (string number)
        {
            if (string.IsNullOrWhiteSpace(number) || !number.All(char.IsDigit) || number.Length != 16)
                return false;

            int sum = 0;

            bool isEven = false;

            for (int i = number.Length -1; i >= 0; i--)
            {
                int digit = number[i]-'0';

                if (isEven)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                isEven = !isEven;
            }
            return sum % 10 == 0;
        }
    }
}
