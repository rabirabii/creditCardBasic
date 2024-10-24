using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreditCard.View;

namespace CreditCard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menu = new CardCreditUi();
            menu.Start();
        }
    }
}
