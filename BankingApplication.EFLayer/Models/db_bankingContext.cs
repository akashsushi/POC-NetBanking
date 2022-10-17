using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BankingApplication.EFLayer.Models
{
    public partial class db_bankingContext : DbContext
    {
        public db_bankingContext()
        {
        }

        public db_bankingContext(DbContextOptions<db_bankingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<CorporatetAccount> CorporatetAccounts { get; set; }
        public virtual DbSet<CurrentAccount> CurrentAccounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<ManagerCustomerRelation> ManagerCustomerRelations { get; set; }
        public virtual DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=AKASH-SU;Initial Catalog=db_banking;Integrated Security=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountNumber)
                    .HasName("PK__account__17D0878B572E791D");

                entity.ToTable("account");

                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("accountNumber");

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("accountType");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("customerId");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("DOC");

                entity.Property(e => e.Ifsc)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IFSC");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Tin)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TIN");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__account__custome__6FE99F9F");
            });

            modelBuilder.Entity<CorporatetAccount>(entity =>
            {
                entity.HasKey(e => e.Ind)
                    .HasName("PK__corporat__DC50F6C8DDAE0321");

                entity.ToTable("corporatetAccount");

                entity.Property(e => e.Ind)
                    .ValueGeneratedNever()
                    .HasColumnName("ind");

                entity.Property(e => e.MinimumBalance).HasColumnName("minimumBalance");

                entity.Property(e => e.WithdrawlLimit).HasColumnName("withdrawlLimit");
            });

            modelBuilder.Entity<CurrentAccount>(entity =>
            {
                entity.HasKey(e => e.Ind)
                    .HasName("PK__currentA__DC50F6C8BF3CAAED");

                entity.ToTable("currentAccount");

                entity.Property(e => e.Ind)
                    .ValueGeneratedNever()
                    .HasColumnName("ind");

                entity.Property(e => e.MinimumBalance).HasColumnName("minimumBalance");

                entity.Property(e => e.WithdrawlLimit).HasColumnName("withdrawlLimit");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("customerId")
                    .HasDefaultValueSql("(format(NEXT VALUE FOR [CustomerSequence],'C-000000#'))");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("emailId");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("lastName");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("managerId");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("mobileNumber");

                entity.Property(e => e.Pincode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("pincode");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__customer__manage__5535A963");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("manager");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("managerId")
                    .HasDefaultValueSql("(format(NEXT VALUE FOR [ManagerSequence],'M-000000#'))");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .HasColumnName("emailId");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("firstName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .HasColumnName("lastName");

                entity.Property(e => e.ManagerPassword)
                    .HasMaxLength(60)
                    .HasColumnName("managerPassword");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(10)
                    .HasColumnName("mobileNumber");
            });

            modelBuilder.Entity<ManagerCustomerRelation>(entity =>
            {
                entity.HasKey(e => e.RelationId);

                entity.ToTable("ManagerCustomerRelation");

                entity.Property(e => e.RelationId).HasColumnName("relationId");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("customerId");

                entity.Property(e => e.ManagerId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("managerId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ManagerCustomerRelations)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ManagerCu__custo__3C34F16F");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.ManagerCustomerRelations)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ManagerCu__manag__3B40CD36");
            });

            modelBuilder.Entity<SavingsAccount>(entity =>
            {
                entity.HasKey(e => e.Ind)
                    .HasName("PK__savingsA__DC50F6C8D13E81C5");

                entity.ToTable("savingsAccount");

                entity.Property(e => e.Ind)
                    .ValueGeneratedNever()
                    .HasColumnName("ind");

                entity.Property(e => e.MinimumBalance).HasColumnName("minimumBalance");

                entity.Property(e => e.WithdrawlLimit).HasColumnName("withdrawlLimit");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transaction");

                entity.Property(e => e.TransactionId).HasColumnName("transactionId");

                entity.Property(e => e.DestinationAccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("destinationAccountNo");

                entity.Property(e => e.SourceAccountNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sourceAccountNo");

                entity.Property(e => e.TransactionAmount).HasColumnName("transactionAmount");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("transactionDate");

                entity.Property(e => e.TransactionDescription)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("transactionDescription");

                entity.Property(e => e.TransactionType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("transactionType");

                entity.HasOne(d => d.SourceAccountNoNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SourceAccountNo)
                    .HasConstraintName("FK__transacti__sourc__160F4887");
            });

            modelBuilder.HasSequence<int>("CorporateSequence");

            modelBuilder.HasSequence<int>("CurrentSequence");

            modelBuilder.HasSequence<int>("CustomerSequence");

            modelBuilder.HasSequence<int>("ManagerSequence");

            modelBuilder.HasSequence<int>("SavingsSequence");

            modelBuilder.HasSequence<int>("SequenceCounter");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public int GetSavingsSequence()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Database.ExecuteSqlRaw(
                       "SELECT @result = (NEXT VALUE FOR SavingsSequence)", result);

            return (int)result.Value;
        }

        public int GetCorporateSequence()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Database.ExecuteSqlRaw(
                       "SELECT @result = (NEXT VALUE FOR CorporateSequence)", result);

            return (int)result.Value;
        }

        public int GetCurrentSequence()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Database.ExecuteSqlRaw(
                       "SELECT @result = (NEXT VALUE FOR CurrentSequence)", result);

            return (int)result.Value;
        }
    }
}
