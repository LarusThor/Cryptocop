using Cryptocop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cryptocop.Repositories.Data;

public class CryptocopDbContext : DbContext
{
    public CryptocopDbContext(DbContextOptions<CryptocopDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>()
            .HasMany(c => c.ShoppingCartItems)    
            .WithOne(i => i.ShoppingCart)         
            .HasForeignKey(i => i.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    public DbSet<Address> Addresses { get; set;}
    public DbSet<JwtToken> JwtTokens { get; set;}
    public DbSet<Order> Orders { get; set;}
    public DbSet<OrderItem> OrderItems { get; set;}
    public DbSet<PaymentCard> PaymentCards { get; set;}
    public DbSet<ShoppingCart> ShoppingCarts { get; set;}
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;}
    public DbSet<User> Users { get; set;}
}