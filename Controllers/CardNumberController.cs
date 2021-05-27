using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardNumber.Models;

namespace VaiVoaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardNumberController : ControllerBase
    {
        private readonly CardNumberContext _context;

        public CardNumberController(CardNumberContext context)
        {
            _context = context;
        }

        /// GET: api/CardNumber
        /// <summary>
        /// Este endpoint recebe um <param>Email</param> como
        /// parâmetro na query da url e procura todos os cartões associados
        /// a este e-mail. Caso não encontre nenhum cartão, retorna uma lista
        /// vazia.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardNumberItem>>> GetCardNumbersByEmail([FromQuery(Name = "Email")] string Email)
        {
            List<CardNumberItem> cardNumbers = await _context
                .CardNumberItems
                .Where(card => card.Email.Equals(Email)).ToListAsync();

            if (cardNumbers == null)
            {
                return NotFound();
            }

            return cardNumbers;
        }

        /// POST: api/CardNumber
        /// <summary>
        /// Este endpoint recebe um <param>Email</param> na forma
        /// form-urlencoded, gera uma sequência aleatória de 16 dígitos, salva
        /// o cartão criado na memória e retona este objeto.
        /// </summary>
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<CardNumberItem>> PostCardNumberItem([FromForm] string Email)
        {
            CardNumberItem cardNumber = getUniqueCardNumber(Email);
            _context.CardNumberItems.Add(cardNumber);
            await _context.SaveChangesAsync();

            return cardNumber;
        }

        /// <summary>
        /// Este método recebe um e-mail e gera uma instância da classe
        /// <c>CardNumberItem</c>, com um número de cartão aleatório. Caso já
        /// exista este número no banco de dados, ele gera, de forma recursiva,
        /// um novo número, até gerar um valor único.
        /// </summary>
        /// <param name="Email"><c>string</c> utilizada para gerar a nova
        /// instância de <c>CardNumberItem</c>.</param>
        /// <returns>Uma nova instância de <c>CardNumberItem</c> com um número
        /// único.</returns>
        private CardNumberItem getUniqueCardNumber(string Email)
        {
            CardNumberItem cardNumberItem = new CardNumberItem(Email);
            if (CardNumberItemExists(cardNumberItem.CardNumber))
            {
                getUniqueCardNumber(Email);
            }
            return cardNumberItem;

        }

        /// <summary>
        /// Este método recebe <param>cardNumber</param> que é uma
        /// <c>string</c> contendo uma sequência de 16 dígitos e verifica se
        /// esta sequência já existe no banco de dados.
        /// <returns>true caso existe e false caso contrário. </returns>
        /// </summary>
        private bool CardNumberItemExists(string cardNumber)
        {
            return _context.CardNumberItems.Any(e => e.CardNumber == cardNumber);
        }
    }
}
