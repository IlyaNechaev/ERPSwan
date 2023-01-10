using ES.Web.Models.DAO;

namespace ES.Web.Services;

public interface IBookService
{
    public Task<Book> AddBook(string debitCode, string creditCode, float sum, Order order);
}
