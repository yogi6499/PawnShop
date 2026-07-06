using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Domain.Entities;
using PawnShop.Infrastructure.DBContext;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Repositories.CommandRepository;

public class CapitalCommandRepository : ICapitalCommandRepository
{
    private readonly PawnShopDbContext _context;

    public CapitalCommandRepository(PawnShopDbContext context) => _context = context;

    public Task AddTransactionAsync(CapitalTransaction transaction)
    {
        _context.CapitalTransactions.Add(transaction);
        return _context.SaveChangesAsync();
    }
}
