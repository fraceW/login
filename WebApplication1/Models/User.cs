using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
	public class User
    {
		[Key]
		public int Id { get; set; }

		[DisplayName("Firstname")]
		[Required(ErrorMessage = "")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Please enter the correct format")]
		public string Firstname { get; set; }


		[DisplayName("Password")]
		[Required(ErrorMessage = "Please enter the Password")]
		[MaxLength(10, ErrorMessage = "MaxLength 10 words")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}

    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("BbsConnection")
        {
        }

        public DbSet<UserPerson> UserPerson { get; set; }
    }

    [Table("person")]
    public class UserPerson
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal Age { get; set; }
        public string Job { get; set; }
    }

    public class RoleContext : DbContext
    {
        public RoleContext()
            : base("BbsConnection")
        {
        }

        public DbSet<Role> Role { get; set; }
    }

    [Table("Teacher")]
    public class Role
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string firstname { get; set; }
        public string Password { get; set; }
    }
}