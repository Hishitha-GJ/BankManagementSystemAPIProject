using BankManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BankManagementSystemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BANK

            modelBuilder.Entity<Bank>()
                .HasKey(b => b.BankId);

            modelBuilder.Entity<Bank>()
                .Property(b => b.BankName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Bank>()
                .Property(b => b.Branch).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Bank>()
                .Property(b => b.Address).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Bank>()
                .HasMany(b => b.Customers)
                .WithOne(c => c.Bank)
                .HasForeignKey(c => c.BankId)
                .OnDelete(DeleteBehavior.Restrict);

            //CUSTOMER

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerName).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email).IsUnique();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Phone).HasMaxLength(15);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Accounts)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<User>(u => u.CustomerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            // ACCOUNT

            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountId);

            modelBuilder.Entity<Account>()
                .Property(a => a.AccountNumber).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Account>()
                .Property(a => a.Balance).HasColumnType("decimal(18,2)").IsRequired();

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // TRANSACTION

            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.TransactionId);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount).HasColumnType("decimal(18,2)").IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionDate).IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Type).IsRequired();


            // USER

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Username).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Password).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<User>()
                .Property(u => u.Role).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Bank>().HasData(
                new Bank { BankId = 1, BankName = "Central Bnak", Branch = "CRP", Address = "Hassan" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Jnana", Email = "jnana@gmail.com", Phone = "9000000001", BankId = 1 },
                new Customer { CustomerId = 2, CustomerName = "Moksha", Email = "moksha@gmail.com", Phone = "9000000002", BankId = 1 }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account { AccountId = 1, AccountNumber = "AC1001", Balance = 5000, CustomerId = 1 },
                new Account { AccountId = 2, AccountNumber = "AC2001", Balance = 2500, CustomerId = 2 }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin", CustomerId = null },
                new User { Id = 2, Username = "Jnana D N", Password = "12345", Role = "User", CustomerId = 1 },
                new User { Id = 3, Username = "Moksha M N", Password = "45678", Role = "User", CustomerId = 2 }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { TransactionId = 1, AccountId = 1, Amount = 5000, TransactionDate = new DateTime(2023, 01, 01), Type = TransactionType.Deposit },
                new Transaction { TransactionId = 2, AccountId = 2, Amount = 2500, TransactionDate = new DateTime(2023, 01, 12), Type = TransactionType.Deposit }
            );
        }
    }
}