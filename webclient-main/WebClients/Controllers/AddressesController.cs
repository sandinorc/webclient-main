using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebClients.Models;

namespace WebClients.Controllers
{
	public class AddressesController : Controller
	{
		private readonly myDBContext _context;

		public AddressesController(myDBContext context)
		{
			_context = context;
		}

		// GET: Addresses
		public async Task<IActionResult> Index()
		{
			var myDBContext = _context.Addresses.Include(a => a.Client);
			return View(await myDBContext.ToListAsync());
		}

		// GET: Addresses/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var address = await _context.Addresses
				.Include(a => a.Client)
				.FirstOrDefaultAsync(m => m.id == id);
			if (address == null)
			{
				return NotFound();
			}

			return View(address);
		}
		// ********************************************************************************
		// GET: Addresses/Create
		// or
		// GET: Addresses/Create/1 
		// id here is used to specify the client owner of the address
		// ********************************************************************************
		public IActionResult Create(int? clientID)
		{
			// check if id is sended
			if (clientID != null)
			{
				// if id is defined force only one id can be used
				ViewData["ClientID"] = new SelectList(_context.Clients.Where(p=>p.id== clientID), "id", "id");
			}
			else
			{	
				// else return the complete list of clients
				ViewData["ClientID"] = new SelectList(_context.Clients, "id", "id");
			}
			return View();
		}

		// POST: Addresses/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("id,Street,Number,City,State,Country,ClientID")] Address address)
		{
			if (ModelState.IsValid)
			{
				_context.Add(address);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["ClientID"] = new SelectList(_context.Clients, "id", "id", address.ClientID);
			return View(address);
		}

		// GET: Addresses/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var address = await _context.Addresses.FindAsync(id);
			if (address == null)
			{
				return NotFound();
			}
			ViewData["ClientID"] = new SelectList(_context.Clients, "id", "id", address.ClientID);
			return View(address);
		}

		// POST: Addresses/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("id,Street,Number,City,State,Country,ClientID")] Address address)
		{
			if (id != address.id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(address);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AddressExists(address.id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["ClientID"] = new SelectList(_context.Clients, "id", "id", address.ClientID);
			return View(address);
		}

		// GET: Addresses/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var address = await _context.Addresses
				.Include(a => a.Client)
				.FirstOrDefaultAsync(m => m.id == id);
			if (address == null)
			{
				return NotFound();
			}

			return View(address);
		}

		// POST: Addresses/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var address = await _context.Addresses.FindAsync(id);
			_context.Addresses.Remove(address);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool AddressExists(int id)
		{
			return _context.Addresses.Any(e => e.id == id);
		}
	}
}
