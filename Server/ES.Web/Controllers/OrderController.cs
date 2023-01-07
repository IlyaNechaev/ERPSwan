using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ES.Web.Contracts.V1;
using ES.Web.Data;
using ES.Web.Models.ViewModels;
using ES.Web.Models.DAO;
using ES.Web.Models.EditModels;

namespace ES.Web.Controllers;

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
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderEditModel model)
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

    //[HttpPost(ApiRoutes.Order.ModifyOrder)]
    //public async Task<IActionResult> ModifyOrder([FromBody] ModifyOrderEditModel model)
    //{
    //    if (model.orders is not null && model.orders.Length > 0)
    //    {
    //        foreach (var modifiedOrder in model.orders)
    //        {
    //            var order = await _context.Orders.FirstOrDefaultAsync(o => o.ObjectID == modifiedOrder.id);

    //            if (order is not null)
    //            {
    //                order.Name = modifiedOrder.name ?? order.Name;
    //            }
    //        }
    //    }
    //    if (model.parts is not null && model.parts.Length > 0)
    //    {
    //        foreach (var modifiedPart in model.parts)
    //        {
    //            var part = await _context.OrderParts.FirstOrDefaultAsync(p => p.ObjectID == modifiedPart.id);

    //            if (part is null)
    //            {
    //                continue;
    //            }

    //            part.OrderNum = modifiedPart.order_num ?? part.OrderNum;
    //            part.EndDate = modifiedPart.date_end ?? part.EndDate;

    //            if (modifiedPart.storelist is not null && modifiedPart.storelist.Length > 0)
    //            {
    //                var materialsToDelete = _context.OrderMaterials.AsQueryable().Where(s => !modifiedPart.storelist.Select(s => s.id).Contains(s.MaterialID) && s.PartID == modifiedPart.id).ToArray();
    //                var materialsToCreate = modifiedPart.storelist.Where(s => !materialsToDelete.Select(d => d.MaterialID).Contains(s.id));

    //                _context.OrderMaterials.RemoveRange(materialsToDelete);
    //                var newMaterials = materialsToCreate.Select(m => new OrderMaterial
    //                {
    //                    MaterialID = m.id,
    //                    PartID = modifiedPart.id,
    //                    Count = m.count ?? 0
    //                }).ToArray();

    //                await _context.OrderMaterials.AddRangeAsync(newMaterials);
    //            }
    //        }
    //    }

    //    await _context.SaveChangesAsync();

    //    return Ok(new { message = "success" });
    //}

    [HttpPost(ApiRoutes.Order.ApproveOrder)]
    public async Task<IActionResult> ApproveOrder([FromQuery(Name = "id")] Guid orderId)
    {
        var order = await _context.Orders.SingleOrDefaultAsync(o => o.ObjectID == orderId);

        if (order is null)
        {
            return BadRequest(new { error = $"Производственный заказ {orderId} не существует" });
        }

        order.IsApproved = true;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Success" });
    }

    [HttpPost(ApiRoutes.Order.CompleteOrder)]
    public async Task<IActionResult> CompleteOrder([FromQuery(Name = "id")] Guid partId)
    {
        var currentPart = await _context.OrderParts.SingleOrDefaultAsync(p => p.ObjectID == partId);

        if (currentPart is null)
        {
            return BadRequest(new { error = $"Производственный заказ {partId} не существует" });
        }

        var orderParts = _context.OrderParts
            .AsQueryable()
            .Where(p => p.OrderID == currentPart.OrderID)
            .ToList();

        if (currentPart.OrderNum > 1)
        {
            var previousParts = orderParts.Where(p => p.OrderNum < currentPart.OrderNum);

            if (previousParts.Any(p => !p.IsCompleted))
            {
                return BadRequest(new { error = $"Не удалось подтвердить выполнение части {partId}" });
            }
        }

        currentPart.IsCompleted = true;
        Order order;
        if (orderParts.Select(p => p.IsCompleted).Aggregate((p1, p2) => p1 && p2))
        {
            order = await _context.Orders.FirstAsync(o => o.ObjectID == currentPart.OrderID);
            order!.IsCompleted = true;
        }

        await _context.SaveChangesAsync();

        return Ok(new { message = "Success" });
    }

    [HttpPost(ApiRoutes.Order.CheckOrder)]
    public async Task<IActionResult> CheckOrder([FromQuery(Name = "id")] Guid orderId)
    {
        var order = await _context.Orders.SingleOrDefaultAsync(o => o.ObjectID == orderId);

        if (order is null)
        {
            return BadRequest(new { error = $"Производственный заказ {orderId} не существует" });
        }

        order.IsChecked = true;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Success" });
    }

    [HttpDelete(ApiRoutes.Order.DeleteOrder)]
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
