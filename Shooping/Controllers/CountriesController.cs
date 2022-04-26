#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shooping.Data;
using Shooping.Data.Entities;
using Shooping.Models;

namespace Shooping.Controllers
{
	public class CountriesController : Controller
	{
		private readonly DataContext _context;

		public CountriesController(DataContext context)
		{
			_context = context;
		}

		// GET: Countries
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _context.Countries.Include(c => c.States).ToListAsync());
		}

		// GET: Countries/Details/5
		[HttpGet]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Country country = await _context.Countries
				.Include(c => c.States)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (country == null)
			{
				return NotFound();
			}

			return View(country);
		}

		// GET: Countries/Create
		[HttpGet]
		public IActionResult Create()
		{
			Country country = new() { States = new List<State>() };
			return View();
		}

		// POST: Countries/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Country country)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.Add(country);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				catch (DbUpdateException dbUpdateException)
				{
					if (dbUpdateException.InnerException.Message.Contains("duplicate"))
					{
						ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
					}
					else
					{
						ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
					}
				}
				catch (Exception exception)
				{
					ModelState.AddModelError(string.Empty, exception.Message);
				}
			}
			return View(country);
		}
		// GET: States/Create
		[HttpGet]
		public async Task<IActionResult> AddState(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Country country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
				return NotFound();
            }
			StateViewModel model = new()
			{
				CountryId = country.Id,
			};
			return View(model);
		}

		// POST: States/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddState(StateViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					State state = new()
					{
						Cities = new List<City>(),
						Country = await _context.Countries.FindAsync(model.CountryId),
						Name = model.Name,
					};
					_context.Add(state);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Details), new {Id = model.CountryId });
				}
				catch (DbUpdateException dbUpdateException)
				{
					if (dbUpdateException.InnerException.Message.Contains("duplicate"))
					{
						ModelState.AddModelError(string.Empty, "Ya existe un Departamento/Estado con el mismo nombre en este País.");
					}
					else
					{
						ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
					}
				}
				catch (Exception exception)
				{
					ModelState.AddModelError(string.Empty, exception.Message);
				}
			}
			return View(model);
		}
		// GET: Countries/Edit/5
		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Country country = await _context.Countries.FindAsync(id);
			if (country == null)
			{
				return NotFound();
			}
			return View(country);
		}

		// POST: Countries/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Country country)
		{
			if (id != country.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(country);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				catch (DbUpdateException dbUpdateException)
				{
					if (dbUpdateException.InnerException.Message.Contains("duplicate"))
					{
						ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
					}
					else
					{
						ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
					}
				}
				catch (Exception exception)
				{
					ModelState.AddModelError(string.Empty, exception.Message);
				}

			}
			return View(country);
		}
		// GET: Countries/Edit/5
		[HttpGet]
		public async Task<IActionResult> EditState(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			State state = await _context.States
				.Include(s => s.Country)
				.FirstOrDefaultAsync(s => s.Id == id);
			if (state == null)
			{
				return NotFound();
			}
			StateViewModel model = new()
			{
				CountryId = state.Country.Id,
				Id = state.Id,
				Name = state.Name,
			};
			return View(model);
		}

		// POST: Countries/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditState(int id, StateViewModel model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					State state = new()
					{
						Id = model.Id,
						Name = model.Name,
					};
					_context.Update(state);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Details), new {Id = model.CountryId});
				}
				catch (DbUpdateException dbUpdateException)
				{
					if (dbUpdateException.InnerException.Message.Contains("duplicate"))
					{
						ModelState.AddModelError(string.Empty, "Ya existe un Departamento/Estado con el mismo nombre en este País.");
					}
					else
					{
						ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
					}
				}
				catch (Exception exception)
				{
					ModelState.AddModelError(string.Empty, exception.Message);
				}

			}
			return View(model);
		}

		// GET: Countries/Delete/5
		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Country country = await _context.Countries
				.Include(c => c.States)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (country == null)
			{
				return NotFound();
			}

			return View(country);
		}

		// POST: Countries/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var country = await _context.Countries.FindAsync(id);
			_context.Countries.Remove(country);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
