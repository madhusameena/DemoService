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
	public class ProjectController
	{
		private readonly DemoServiceContext _context;

		public ProjectController(DemoServiceContext context)
		{
			_context = context;
		}
		[HttpGet("Details/{id}")]
		public async Task<Project> Get(int id)
		{
			return await _context.Project.FindAsync(id);
		}
		[HttpGet("Details")]
		public async Task<List<Project>> Get()
		{
			return await _context.Project.ToListAsync();
		}

		// POST: Projects/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<bool> Create([Bind("ProjectId,Name,CreatedDate,ModifiedDate,Img,UserId")] Project project)
		{	
			_context.Add(project);
			await _context.SaveChangesAsync();
			return true;	
		}

		[HttpDelete("Delete/{id}")]
		public async Task<bool> Delete(int id)
		{
			var project = await _context.Project.FindAsync(id);
			_context.Project.Remove(project);
			await _context.SaveChangesAsync();
			return true;
		}

		[HttpPut("Update/{id}")]
		public async Task<bool> Edit(int id, [Bind("ProjectId,Name,CreatedDate,ModifiedDate,Img,UserId")] Project project)
		{
			if (id != project.ProjectId)
			{
				return false;
			}		
			try
			{
				_context.Update(project);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProjectExists(project.ProjectId))
				{
						return false;
				}
			}
			return true;
		  }
		private bool ProjectExists(int id)
		{
			return _context.Project.Any(e => e.ProjectId == id);
		}
	}
	

}
