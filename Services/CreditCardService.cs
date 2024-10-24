using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCard.Models;


namespace CreditCard.Services
{
    public class CreditCardService
    {
        private List<CreditCardClass> cards;

        public CreditCardService() {
        
            cards = new List<CreditCardClass>();
        }

        public CreditCardClass CreateCard(string cardNumber, string cardHolderName, DateTime expiryDate , int cvv, decimal balance) 
        { 
        
            try {

                var card = new CreditCardClass(cardNumber, cardHolderName, expiryDate, cvv, 0);
                cards.Add(card);
                return card;
            }

            catch(ArgumentException ex) {
                throw new Exception($"Failed to create cad : {ex.Message}");
            }
        }

        public List<CreditCardClass> GetAllCards()
        {
            return cards;
        }

        public bool ProcessTransaction(CreditCardClass card, decimal amount, bool isDeposit)
        {
            try
            {
                if (isDeposit)
                {
                    card.Deposit(amount);
                    return true;
                }
                else
                {
                    return card.Withdraw(amount);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
