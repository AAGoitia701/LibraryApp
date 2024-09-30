using Library.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Data
{
    public class ApplicationDBContext : IdentityDbContext //We changed DbContext for this
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; } //Table Category
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//added this so we dont get error because of the keys of identity tables, cos' they're mapped onModelCreating

            modelBuilder.Entity<Category>().HasData( //In table 'category' you will add the rows viewed here -> it is data
                new Category { Id = 1, Name= "Action", DisplayOrder=1},
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            
            modelBuilder.Entity<Product>().HasData( //In table 'category' you will add the rows viewed here -> it is data
                new Product { Id = 1, Title = "Harry Potter And The Philosopher'S Stone", Description = "The hope and wonder of Harry Potter's world will make you want to escape to Hogwarts again and again. The magic starts here!", ISBN= "1408855658", Author="J.K Rowling", ListPrice=11.90 , ListPrice30= 7.53, ListPriceHigher30 = 5.55, CategoryId= 2, ImageURL=""},
                new Product { Id = 2, Title = "The Silmarillion", Description = "It is the ancient drama to which the characters in The Lord of the Rings look back, and in whose events some of them such as Elrond and Galadriel took part.", ISBN = "9780261102736", Author = "J.R.R Tolkien", ListPrice =8 , ListPrice30=6.23, ListPriceHigher30=4.65, CategoryId = 2, ImageURL="" },
                new Product { Id = 3, Title = "I, Robot", Description = "In these stories Isaac Asimov creates the Three Laws of Robotics and ushers in the Robot Age.\r\n\r\nEarth is ruled by master-machines but the Three Laws of Robotics have been designed to ensure humans maintain the upper hand.", ISBN = "9780007532278", Author = "Isaac Asimov", ListPrice =10 , ListPrice30=7.43 , ListPriceHigher30= 5.25, CategoryId = 2, ImageURL="" }
                );
        }
    }
}
