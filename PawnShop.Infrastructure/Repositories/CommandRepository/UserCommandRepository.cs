
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Infrastructure.DBContext;
using PawnShop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PawnShop.Infrastructure.Repositories.CommandRepository;

public class UserCommandRepository : IUserCommandRepository
{
    private readonly PawnShopDbContext _context;

    public UserCommandRepository(PawnShopDbContext context) => _context = context;

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Add(user);
        _context.SaveChangesAsync(cancellationToken);
        return Task.CompletedTask;
    }

    public async Task UpdatePasswordAsync(int userId, string newPasswordHash, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        if (user == null) return; // keep behavior consistent; alternatively throw
        user.PasswordHash = newPasswordHash;
        await _context.SaveChangesAsync(cancellationToken);
    }

}