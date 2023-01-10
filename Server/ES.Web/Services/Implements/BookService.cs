using ES.Web.Models.DAO;
using ES.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ES.Web.Services;

public class BookService : IBookService
{
    ESDbContext _context;
    public BookService(ESDbContext context)
    {
        _context = context;
    }

    public async Task<Book> AddBook(string debitCode, string creditCode, float sum, Order order)
    {
        var entries = await _context.BookEntries
            .AsQueryable()
            .Where(be => be.Code == debitCode || be.Code == creditCode)
            .ToArrayAsync();

        var book = new Book()
        {
            DebetEntry = entries.Single(be => be.Code == debitCode),
            CreditEntry = entries.Single(be => be.Code == creditCode),
            Sum = sum,
            Order = order,
            RegDate = DateTime.Now
        };

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return book;
    }
}
