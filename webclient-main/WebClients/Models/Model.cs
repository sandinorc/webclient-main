using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebClients.Models
{
	/**************************************************************************
	 *  Entity Framework DB ContextClass 
	 * Code first 
	 **************************************************************************/
	public class myDBContext : DbContext
	{
		public myDBContext(DbContextOptions<myDBContext> options) : base(options)
		{

		}
		public DbSet<Client> Clients { get; set; }
		public DbSet<Address> Addresses { get; set; }
	}
	/********************************************
	 * Client Class
	 * 
	 *******************************************/
	public class Client
	{
		public int id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		// Navigation Properties 
		public ICollection<Address> Addresses { get; set; }
	}
	/************************************************
	 * Phisical Client Address 
	 * 
	 ***********************************************/
	public class Address
	{
		public int id { get; set; }
		public string Street { get; set; }
		public int Number { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		// Navigation Properties 
		public int ClientID { get; set; }
		public Client Client { get; set; }
		//

	}


}
