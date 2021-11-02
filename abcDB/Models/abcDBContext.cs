using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace abcDB.Models
{
    [Table("tblUsers")]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Email { get; set; }
        public string Password { get; set; }
        [StringLength(20)]
        public string role { get; set; }

        [StringLength(20)]
        public string Location { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }
    }

    [Table("tblCategory")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        
    }
    [Table("tblMedicines")]
    public class Medicines
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public int Price { get; set; }
        public bool Available { get; set; }
        [StringLength(20)]
        public string Image { get; set; }

        [StringLength(20)]
        public string Description { get; set; }

        [StringLength(20)]
        public string Seller { get; set; }

        public virtual Category Cid { get; set; }

    }
    public class abcDBContext:DbContext
    {
        public abcDBContext(DbContextOptions<abcDBContext> options):base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Medicines> Medicines { get; set; }

    }
}
