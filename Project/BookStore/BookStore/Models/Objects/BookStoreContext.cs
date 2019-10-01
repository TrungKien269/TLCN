using System;
using BookStore.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStore.Models.Objects
{
    public partial class BookStoreContext : DbContext
    {
        public BookStoreContext()
        {
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<AuthorBook> AuthorBook { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookCategory> BookCategory { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartBook> CartBook { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<FamousPublisher> FamousPublisher { get; set; }
        public virtual DbSet<Form> Form { get; set; }
        public virtual DbSet<FormBook> FormBook { get; set; }
        public virtual DbSet<ImageBook> ImageBook { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<PromotionDetail> PromotionDetail { get; set; }
        public virtual DbSet<Publisher> Publisher { get; set; }
        public virtual DbSet<PublisherBook> PublisherBook { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SupplierBook> SupplierBook { get; set; }
        public virtual DbSet<User> User { get; set; }

        // Unable to generate entity type for table 'dbo.RawUser'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.RawBook'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SettingHelper.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("U_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("U_Username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(100);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_User");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.AuthorId });

                entity.Property(e => e.BookId)
                    .HasColumnName("Book_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.AuthorId).HasColumnName("Author_ID");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorBook)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthorBook_Author");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.AuthorBook)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthorBook_Book");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.CateId });

                entity.Property(e => e.BookId)
                    .HasColumnName("BookID")
                    .HasMaxLength(20);

                entity.Property(e => e.CateId).HasColumnName("CateID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookCategory)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookCategory_Book");

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.BookCategory)
                    .HasForeignKey(d => d.CateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookCategory_SubCategory");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Cart)
                    .HasForeignKey<Cart>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_User");
            });

            modelBuilder.Entity<CartBook>(entity =>
            {
                entity.HasKey(e => new { e.CartId, e.BookId });

                entity.Property(e => e.CartId).HasColumnName("Cart_ID");

                entity.Property(e => e.BookId)
                    .HasColumnName("Book_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.PickedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.CartBook)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartBook_Book");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartBook)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartBook_Cart");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BookId });

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.BookId)
                    .HasColumnName("Book_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.Comment1)
                    .IsRequired()
                    .HasColumnName("Comment");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Book");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<FamousPublisher>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Form>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<FormBook>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.FormId });

                entity.Property(e => e.BookId)
                    .HasColumnName("Book_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.FormId).HasColumnName("Form_ID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.FormBook)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormBook_Book");

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.FormBook)
                    .HasForeignKey(d => d.FormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormBook_Form");
            });

            modelBuilder.Entity<ImageBook>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.ImageId });

                entity.Property(e => e.BookId)
                    .HasColumnName("Book_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.ImageId).HasColumnName("Image_ID");

                entity.Property(e => e.Path).IsRequired();

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ImageBook)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageBook_Book");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.BookId });

                entity.Property(e => e.OrderId)
                    .HasColumnName("Order_ID")
                    .HasMaxLength(10);

                entity.Property(e => e.BookId)
                    .HasColumnName("Book_ID")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Book");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PromotionDetail>(entity =>
            {
                entity.HasKey(e => new { e.PromotionId, e.BookId });

                entity.Property(e => e.PromotionId).HasColumnName("Promotion_ID");

                entity.Property(e => e.BookId)
                    .HasColumnName("Book_ID")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.PromotionDetail)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PromotionDetail_Book");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.PromotionDetail)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PromotionDetail_Promotion");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<PublisherBook>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.PublisherId });

                entity.Property(e => e.BookId)
                    .HasColumnName("Book_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.PublisherId).HasColumnName("Publisher_ID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.PublisherBook)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublisherBook_Book");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.PublisherBook)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublisherBook_Publisher");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BookId });

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.BookId)
                    .HasColumnName("Book_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_Book");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rating_User");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CateId).HasColumnName("Cate_ID");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.SubCategory)
                    .HasForeignKey(d => d.CateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategory_Category");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SupplierBook>(entity =>
            {
                entity.HasKey(e => new { e.BookId, e.SupplierId });

                entity.Property(e => e.BookId)
                    .HasColumnName("Book_ID")
                    .HasMaxLength(20);

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_ID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.SupplierBook)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierBook_Book");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierBook)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierBook_Supplier");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
