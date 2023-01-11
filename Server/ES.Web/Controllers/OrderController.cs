using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ES.Web.Contracts.V1;
using ES.Web.Data;
using ES.Web.Models.ViewModels;
using ES.Web.Models.DAO;
using ES.Web.Models.EditModels;
using ES.Web.Services;
using ExamManager.Filters;

namespace ES.Web.Controllers;

[ApiController]
[JwtAuthorize]
public class OrderController : ControllerBase
{
    ESDbContext _context;
    IBookService _bookService;
    IStoreService _storeService;

    public OrderController(ESDbContext context, IBookService bookService, IStoreService storeService)
    {
        _context = context;
        _bookService = bookService;
        _storeService = storeService;
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
            .Include(o => o.Foreman)
            .FirstOrDefaultAsync(o => o.ObjectID == id);

        if (order is null)
        {
            return Ok(new { error = $"Заказ {id} не существует" });
        }

        var orderView = new OrderView
        {
            id = order.ObjectID,
            number = order.Number,
            date_reg = order.RegDate,
            is_approved = order.IsApproved,
            is_completed = order.IsCompleted,
            is_checked = order.IsChecked,
            foreman = new ForemanView
            {
                id = order.Foreman.ObjectID,
                fullname = $"{order.Foreman.LastName} {order.Foreman.FirstName}"
            },
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
    public async Task<IActionResult> GetOrderList(string? number)
    {
        var ordersQuery = _context.Orders
            .AsNoTracking()
            .AsQueryable();

        if (number is not null)
        {
            ordersQuery = ordersQuery.Where(o => o.Number.ToString().Contains(number));
        }

        var ordersView = new OrderListView
        {
            list = ordersQuery.Select(o =>
            new OrderListView.OrderView
            {
                id = o.ObjectID,
                number = o.Number,
                date_reg = o.RegDate,
                is_completed = o.IsCompleted
            }).ToArray()
        };

        return Ok(ordersView);
    }

    [HttpPost(ApiRoutes.Order.CreateOrder)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderEditModel model)
    {
        var foreman = await _context.Users.FirstOrDefaultAsync(u => u.ObjectID == model.foremanId);
        if (foreman is null)
        {
            return Ok(new { error = $"Пользователь {model.foremanId} не найден" });
        }

        var order = new Order
        {
            Number = model.number,
            RegDate = DateTime.Now,
            IsApproved = false,
            IsChecked = false,
            IsCompleted = false,
            Foreman = foreman
        };

        await _context.Orders.AddAsync(order);

        var orderParts = model.parts.Select(p => new OrderPart
        {
            EndDate = p.date_end,
            OrderID = order.ObjectID,
            IsCompleted = false,
            OrderNum = p.order_num
        }).ToArray();

        await _context.OrderParts.AddRangeAsync(orderParts);
        await _context.SaveChangesAsync();        

        // Резервируем материалы
        foreach (var part in orderParts)
        {
            var stores = model.parts.First(p => p.order_num == part.OrderNum).storelist;

            foreach (var store in stores)
            {
                await _storeService.ReserveMaterialsAsync(part, store.id, store.count);
            }
        }

        return Ok(new { id = order.ObjectID });
    }

    //[HttpPost(ApiRoutes.Order.ModifyOrder)]
    //public async Task<IActionResult> ModifyOrder([FromBody] ModifyOrdersEditModel model)
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
        var order = await _context.Orders
            .AsQueryable()
            .SingleOrDefaultAsync(o => o.ObjectID == orderId);

        if (order is null)
        {
            return BadRequest(new { error = $"Производственный заказ {orderId} не существует" });
        }

        if (order.IsCanceled)
        {
            return BadRequest(new { error = $"Производственный заказ {orderId}({order.Number}) был отменен" });
        }        

        var orderPart = await _context.OrderParts
            .AsQueryable()
            .Where(p => p.OrderID == orderId)
            .Include(p => p.Materials)
            .OrderBy(p => p.OrderNum)
            .FirstAsync();

        var transaction = await _context.Database.BeginTransactionAsync();
        order.IsApproved = true;
        try
        {
            await _storeService.AllocateMaterialsAsync(orderPart.ObjectID);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Ok(new { error = ex.Message });
        }

        var sum = orderPart.Materials.Select(om => om.Sum ?? 0).Sum();
        await _bookService.AddBook(Constants.BookEntryCode.MAIN_PRODUCTION, Constants.BookEntryCode.MATERIALS, sum, order);

        await transaction.CommitAsync();
        await _context.SaveChangesAsync();

        return Ok(new { message = "Success" });
    }

    [HttpPost(ApiRoutes.Order.CompleteOrder)]
    public async Task<IActionResult> CompleteOrder([FromQuery(Name = "id")] Guid partId)
    {
        var currentPart = await _context.OrderParts
            .AsQueryable()
            .Include(p => p.Order)
            .SingleOrDefaultAsync(p => p.ObjectID == partId);

        if (currentPart is null)
        {
            return BadRequest(new { error = $"Производственный заказ {partId} не существует" });
        }

        if (currentPart.Order.IsCanceled || !currentPart.Order.IsApproved)
        {
            return BadRequest(new { error = $"Производственный заказ {currentPart.Order.ObjectID} не был утвержден" });
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
        else
        {
            var nextPart = orderParts.First(p => p.OrderNum == currentPart.OrderNum + 1);
            await _storeService.AllocateMaterialsAsync(nextPart.ObjectID);
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

        if (!order.IsCompleted)
        {
            return BadRequest(new { error = $"Производственный заказ {orderId}({order.Number}) не был завершен" });
        }

        order.IsChecked = true;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Success" });
    }

    [HttpPost(ApiRoutes.Order.CancelOrder)]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.ObjectID == id);

        if (order is null)
        {
            throw new ArgumentException($"Производственного заказа {id} не существует");
        }

        order.IsCanceled = true;
        await _storeService.UnreserveMaterialsAsync(order.ObjectID);

        await _context.SaveChangesAsync();

        return Ok(new { message = "Success" });
    }
}
