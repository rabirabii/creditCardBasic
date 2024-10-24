using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCard.Services;

namespace CreditCard.View
{
    public class CardCreditUi
    {
        private readonly CreditCardService _service;

        public CardCreditUi()
        {
            _service = new CreditCardService();
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Credit Card Management System ===");
                Console.WriteLine("1. Create New Card");
                Console.WriteLine("2. View All Cards");
                Console.WriteLine("3. Process Transaction");
                Console.WriteLine("4. Exit");
                Console.Write("\nSelect option (1-4): ");

                switch (Console.ReadLine()) {
                    case "1":
                        CreateNewCard();
                        break;
                    case "2":
                        ViewAllCards();
                        break;
                    case "3":
                        ProcessTransaction();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
        private void CreateNewCard() {

            Console.WriteLine("\n=== Buat Kartu Baru ===");

            Console.Write("Masukkan Nomor Kartu (16 digits): ");
            string cardNumber = Console.ReadLine();

            Console.Write("Masukkan Nama Pemegang Kartu: ");
            string cardHolderName = Console.ReadLine();

            Console.Write("Masukkan Tanggal kadaluarsa (MM/yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime expiryDate))
            {
                Console.WriteLine("Invalid date Format!");
                return;
            }

            Console.Write("Masukkan CVV (3 Digits): ");
            if (!int.TryParse(Console.ReadLine(), out int cvv))
            {
                Console.WriteLine("Invalid CVV!");
                return;
            }

            

            try { 
                var card = _service.CreateCard(cardNumber, cardHolderName, expiryDate, cvv, 0);
                Console.WriteLine("\nKartu Berhasil dibuat!");
                Console.WriteLine(card.getCardInfo());
            }

            catch (Exception ex) {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        
        }
        private void ViewAllCards() {
            Console.WriteLine("\n=== Semua Kartu ===");

            var cards = _service.GetAllCards();

            if (cards.Count == 0)
            {
                Console.WriteLine(
                    "Tidak ada Kartu!"
                    );
                return;
            }
        
            foreach (var card in cards)
            {
                Console.WriteLine($"\n {card.getCardInfo()}");
                Console.WriteLine("----------------------");
            }
        }

        private void ProcessTransaction() {

            Console.WriteLine("\n=== Process Transaction ===");
            var cards = _service.GetAllCards();

            if (cards.Count == 0)
            {
                Console.WriteLine("Tidak ada kartu yang tersedia! ");
                return;
            }

            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine($"\n{i + 1}. {cards[i].getCardInfo()}");
            }

            Console.Write("\nSilahkan Pilih Nomor Kartu");
            if (!int.TryParse(Console.ReadLine(), out int cardIndex) || cardIndex < 1 || cardIndex > cards.Count
                ) {
                Console.WriteLine("Invalid Card Selection!");
                return;
            }

            Console.WriteLine("\n1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.Write("Silahkan pilih jenis Transaksi :");

            string choice = Console.ReadLine();
            bool isDeposit = choice == "1";

            if (choice != "1" && choice != "2")
            {
                Console.WriteLine("Invalid Transaction type!");
                return;
            }

            Console.Write("Masukan Nominal: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Nominal tidak valid");
                return;
            }

            var result = _service.ProcessTransaction(cards[cardIndex - 1], amount, isDeposit);

            Console.WriteLine(result ? "Transaksi Berhasil" : "Transaksi Gagal");
        }
    }
}
