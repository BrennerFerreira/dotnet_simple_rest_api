using Microsoft.EntityFrameworkCore;

namespace CardNumber.Models
{
    public class CardNumberContext : DbContext
    {
        public CardNumberContext(DbContextOptions<CardNumberContext> options)
            : base(options)
        {
        }

        public DbSet<CardNumberItem> CardNumberItems { get; set; }
    }
}