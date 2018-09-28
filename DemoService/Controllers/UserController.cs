using DemoService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoService.Controllers
{
	[EnableCors("MyPolicy")]
	[Route("[controller]")]
	public class UserController
    {
		private readonly DemoServiceContext _context;

		public UserController(DemoServiceContext context)
		{
			_context = context;
		}
		[HttpGet("Details/{id}")]
		public async Task<Userdetails> Get(int id)
		{
			if (id == null)
			{
				return null;
			}

			var userdetails = await _context.Userdetails
				.FirstOrDefaultAsync(m => m.UserId == id);
			if (userdetails == null)
			{
				return null;
			}

			return userdetails;
		}
		[HttpGet("Details")]
		public async Task<List<Userdetails>> Get()
		{
			return await _context.Userdetails.ToListAsync();
		}

		[HttpPost("Add")]
		public async Task<bool> Post([Bind("UserId,FirstName,LastName,EmailId")] Userdetails userdetails)
		{
			_context.Add(userdetails);
			await _context.SaveChangesAsync();
			return true;
		}

		[HttpDelete("Delete/{id}")]
		public async Task<bool> Delete(int id)
		{
			var userdetails = await _context.Userdetails.FindAsync(id);
			if (userdetails != null)
			{
				_context.Userdetails.Remove(userdetails);
				await _context.SaveChangesAsync();
			}
			return true;
		}

		[HttpPut("Update/{id}")]
		public async Task<bool> Update(int id, [Bind("UserId,FirstName,LastName,EmailId")] Userdetails userdetails)
		{
			if (id != userdetails.UserId)
			{
				return false;
			}
	
			try
			{
				_context.Update(userdetails);
				await _context.SaveChangesAsync();
				}
			catch (DbUpdateConcurrencyException)
			{
			    if (!UserdetailsExists(userdetails.UserId))
				{
					return false;
				}
			}	
			return true;
		}

		private bool UserdetailsExists(int id)
		{
			return _context.Userdetails.Any(e => e.UserId == id);
		}
	}

}
