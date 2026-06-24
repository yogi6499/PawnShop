
using Microsoft.EntityFrameworkCore;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Infrastructure.DBContext;
using PawnShop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Repositories.CommandRepository;

public class CustomerCommandRepository : ICustomerCommandRepository
{
    private readonly PawnShopDbContext _context;

    public CustomerCommandRepository(PawnShopDbContext context) => _context = context;

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        // Load tracked entity and remove
        var entity = await _context.Customers.FindAsync(id, cancellationToken);
        if (entity == null) throw new InvalidOperationException("Customer not found");

        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync(cancellationToken);
    }

}