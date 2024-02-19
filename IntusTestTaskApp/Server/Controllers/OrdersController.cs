using IntusTestTaskApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntusTestTaskApp.Server.Controllers
{
    
    [ApiController]
    [Route("api/orders")]

    public class OrdersController : ControllerBase
    {
        private readonly SalesAndOrderDbContext context;
        public OrdersController(SalesAndOrderDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            return await context.Orders.ToListAsync();
        }


        [HttpGet("{id}")]
        public async  Task<ActionResult<Order>> Get(int id)
        {
            var order = await context.Orders
                .Include(x=>x.Windows)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order == null) { return NotFound(); }

            if (order.Windows == null)
            {
                // Load the Windows explicitly if it's null
                context.Entry(order).Collection(o => o.Windows).Load();
            }
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            return order.Id;
        }
        [HttpPut]
        public async Task<ActionResult> Put(Order order)
        {
            context.Entry(order).State = EntityState.Modified;

            foreach (var windows in order.Windows)
            {
                if (windows.Id != 0)
                {
                    context.Entry(windows).State = EntityState.Modified;
                }
                else
                {
                    context.Entry(windows).State = EntityState.Added;
                }
            }

            var idsOfWindows = order.Windows.Select(x => x.Id).ToList();
            var windowsToDelete = await context
                .Windows
                .Where(x => !idsOfWindows.Contains(x.Id) && x.OrderId == order.Id)
                .ToListAsync();

            context.RemoveRange(windowsToDelete);

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
