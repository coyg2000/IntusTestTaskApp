using IntusTestTaskApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntusTestTaskApp.Server.Controllers
{
    [ApiController]
    [Route("api/windows")]
    public class WindowsController : Controller
    {
        private readonly SalesAndOrderDbContext context;
        public WindowsController(SalesAndOrderDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Window>>> Get()
        {
            return await context.Windows.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Window>> Get(int id)
        {
            var window = await context.Windows
                .Include(x => x.SubElements)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (window == null) { return NotFound(); }

            return window;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Window window)
        {
            context.Windows.Add(window);
            await context.SaveChangesAsync();
            return window.Id;
        }
        [HttpPut]
        public async Task<ActionResult> Put(Window window)
        {
            context.Entry(window).State = EntityState.Modified;

            foreach (var subElement in window.SubElements)
            {
                if (subElement.Id != 0)
                {
                    context.Entry(subElement).State = EntityState.Modified;
                }
                else
                {
                    context.Entry(subElement).State = EntityState.Added;
                }
            }

            var idsOfSubElements = window.SubElements.Select(x => x.Id).ToList();
            var SubElementsToDelete = await context
                .SubElements
                .Where(x => !idsOfSubElements.Contains(x.Id) && x.WindowId == window.Id)
                .ToListAsync();

            context.RemoveRange(SubElementsToDelete);

            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}
