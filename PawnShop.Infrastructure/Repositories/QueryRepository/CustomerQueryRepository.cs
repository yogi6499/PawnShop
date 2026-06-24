
using Microsoft.EntityFrameworkCore;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Infrastructure.DBContext;
using PawnShop.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Repositories.QueryRepository;

public class CustomerQueryRepository : ICustomerQueryRepository
{
    private readonly PawnShopDbContext _context;

    public CustomerQueryRepository(PawnShopDbContext context) => _context = context;

    public Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _context.Customers.AsNoTracking().ToListAsync(cancellationToken).ContinueWith(t => (IEnumerable<Customer>)t.Result, cancellationToken);

    public Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == id, cancellationToken);

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default) =>
        _context.Customers.AnyAsync(x => x.CustomerId == id, cancellationToken);
}