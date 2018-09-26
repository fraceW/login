using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
	public class ContactModel : DbContext
	{
		public ContactModel()
			: base("BbsConnection")
		{
		}
		public DbSet<Contact> contact { get; set; }
		
	}

	public class Contact
	{
		[Key]
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public string ID { get; set; }
		public string Date { get; set; }
		public string Num { get; set; }
		public string Name { get; set; }
		public string KQQK { get; set; }
	}


}