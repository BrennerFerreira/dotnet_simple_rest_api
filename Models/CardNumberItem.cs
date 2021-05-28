using System;

namespace CardNumber.Models
{
    public class CardNumberItem
    {
        private readonly Random random = new Random();

        public long Id { get; set; }
        public string CardNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Único construtor público da classe, gera o número aleatório para
        /// ser usado na instância criada.
        /// </summary>
        /// <param name="email"><c>string</c> contendo o e-mail a ser utilizado
        /// na instância</param>
        public CardNumberItem(string email)
        {
            Email = email;
            CardNumber = generateCardNumber();
            CreatedAt = DateTime.Now;
        }
        private int generateRandomInt()
        {
            return random.Next(10);
        }

        /// <summary>
        /// Este método gera 16 dígitos entre 0 e 9 para ser utilizado como
        /// número do cartão gerado.
        /// </summary>
        /// <returns>Uma string com uma sequência de 16 dígitos.</returns>
        private string generateCardNumber()
        {
            string cardNumber = "";
            while (cardNumber.Length < 16)
            {
                int newNumber = generateRandomInt();
                cardNumber += newNumber.ToString();
            }

            return cardNumber;
        }
    }
}