using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ES.Web.Contracts.V1;
using ExamManager.Filters;
using ES.Web.Data;
using Microsoft.EntityFrameworkCore;
using ES.Web.Models.ViewModels;
using ES.Web.Models.DAO;
using ES.Web.Models.EditModels;

namespace ES.Web.Controllers
{
    [Route(ApiRoutes.Root)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ESDbContext _context;
        public OrderController(ESDbContext context)
        {
            _context = context;
        }

        [HttpGet(ApiRoutes.Order.GetOrder)]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .AsQueryable()
                .Include(o => o.Parts)
                .ThenInclude(p => p.Materials)
                .ThenInclude(m => m.Material)
                .FirstOrDefaultAsync(o => o.ObjectID == id);

            if (order is null)
            {
                return BadRequest(new { error = $"Заказ {id} не существует" });
            }

            var orderView = new OrderView
            {
                id = order.ObjectID,
                name = order.Name,
                date_reg = order.RegDate,
                is_approved = order.IsApproved,
                is_completed = order.IsCompleted,
                is_checked = order.IsChecked,
                parts = order.Parts.Select(op =>
                new OrderPartView
                {
                    id = op.ObjectID,
                    date_end = op.EndDate,
                    is_completed = op.IsCompleted,
                    order_num = op.OrderNum,
                    storelist = op.Materials.Select(m =>
                    new OrderStoreView
                    {
                        id = m.MaterialID,
                        count = m.Count,
                        name = m.Material.Name
                    }).ToArray()
                })
                .ToArray()
            };

            return Ok(orderView);
        }

        [HttpGet(ApiRoutes.Order.GetOrderList)]
        public async Task<IActionResult> GetOrderList(string? name)
        {
            var ordersQuery = _context.Orders
                .AsNoTracking()
                .AsQueryable();

            if (name is not null)
            {
                ordersQuery = ordersQuery.Where(o => o.Name.Contains(name));
            }

            var ordersView = new OrderListView
            {
                list = ordersQuery.Select(o =>
                new OrderListView.OrderView
                {
                    id = o.ObjectID,
                    name = o.Name,
                    date_reg = o.RegDate,
                    is_completed = o.IsCompleted
                }).ToArray()
            };

            return Ok(ordersView);
        }

        [HttpPost(ApiRoutes.Order.CreateOrder)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderEditModel model)
        {
            var order = new Order
            {
                Name = model.name,
                RegDate = DateTime.Now,
                IsApproved = false,
                IsChecked = false,
                IsCompleted = false
            };

            await _context.Orders.AddAsync(order);

            var orderParts = model.parts.Select(p => new OrderPart
            {
                EndDate = p.date_end,
                OrderID = order.ObjectID,
                IsCompleted = false,
                OrderNum = p.order_num,
                Materials = p.storelist.Select(m => new OrderMaterial
                {
                    MaterialID = m.id,
                    Count = m.count
                }).ToList()
            });

            await _context.OrderParts.AddRangeAsync(orderParts);

            await _context.SaveChangesAsync();

            return Ok(new { id = order.ObjectID });
        }

        [HttpGet(ApiRoutes.Order.ModifyOrder)]
        public async Task<IActionResult> ModifyOrder([FromBody] OrderEditModel model)
        {
            var order = new Order
            {
                Name = model.name,
                RegDate = DateTime.Now,
                IsApproved = false,
                IsChecked = false,
                IsCompleted = false
            };

            _context.Orders.Update(order);

            var orderParts = model.parts.Select(p => new OrderPart
            {
                EndDate = p.date_end,
                OrderID = order.ObjectID,
                IsCompleted = false,
                OrderNum = p.order_num,
                Materials = p.storelist.Select(m => new OrderMaterial
                {
                    MaterialID = m.id,
                    Count = m.count
                }).ToList()
            });

            _context.OrderParts.UpdateRange(orderParts);

            await _context.SaveChangesAsync();

            return Ok(new { id = order.ObjectID });
        }

        [HttpGet(ApiRoutes.Order.DeleteOrder)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.ObjectID == id);

            if (order is null)
            {
                throw new ArgumentException($"Производственного заказа {id} не существует");
            }

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Success" });
        }
    }
}
