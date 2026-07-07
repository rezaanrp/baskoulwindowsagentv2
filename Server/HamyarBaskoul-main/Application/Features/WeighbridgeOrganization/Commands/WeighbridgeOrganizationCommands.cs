using Application.Common.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WeighbridgeOrganization.Commands
{
    public sealed record SaveCompanyCommand(
        int Id,
        string? CompanyName,
        string? CompanyCode,
        string? ConnectionUrl,
        string? ApiUrl,
        bool AutoSync) : IRequest;

    public sealed record SaveWeighbridgeSiteCommand(
        int Id,
        string CompanyCode,
        string SiteName,
        bool IsActive) : IRequest;

    public sealed record SaveWeighbridgeCommand(
        int Id,
        string CompanyCode,
        int SiteId,
        string Name,
        string ScaleCode,
        string UserId,
        int? Type) : IRequest;

    public sealed record AssignWeighbridgeSiteUserCommand(
        string CompanyCode,
        int SiteId,
        string UserId) : IRequest;

    public sealed record RemoveWeighbridgeSiteUserCommand(
        int SiteId,
        string UserId) : IRequest;

    public sealed record DeleteCompanyCommand(int Id) : IRequest;

    public sealed class SaveCompanyCommandHandler : IRequestHandler<SaveCompanyCommand>
    {
        private readonly IWriteDbContext _db;

        public SaveCompanyCommandHandler(IWriteDbContext db)
        {
            _db = db;
        }

        public async Task Handle(SaveCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = request.Id > 0
                ? await _db.Companies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                : null;

            if (company == null)
            {
                company = new Company();
                _db.Companies.Add(company);
            }

            company.CoName = request.CompanyName;
            company.CodMarkaz = request.CompanyCode;
            company.MarkazURL = request.ConnectionUrl;
            company.APIURL = request.ApiUrl;
            company.AutoAsync = request.AutoSync;

            await _db.SaveChangesAsync(cancellationToken);
        }
    }

    public sealed class SaveWeighbridgeSiteCommandHandler : IRequestHandler<SaveWeighbridgeSiteCommand>
    {
        private readonly IWriteDbContext _db;

        public SaveWeighbridgeSiteCommandHandler(IWriteDbContext db)
        {
            _db = db;
        }

        public async Task Handle(SaveWeighbridgeSiteCommand request, CancellationToken cancellationToken)
        {
            var site = request.Id > 0
                ? await _db.WeighbridgeSites.FirstOrDefaultAsync(x => x.ID == request.Id, cancellationToken)
                : null;

            if (site == null)
            {
                site = new WeighbridgeSite();
                _db.WeighbridgeSites.Add(site);
            }

            site.name = request.SiteName;
            site.Company = request.CompanyCode;
            site.isActive = request.IsActive;

            await _db.SaveChangesAsync(cancellationToken);
        }
    }

    public sealed class SaveWeighbridgeCommandHandler : IRequestHandler<SaveWeighbridgeCommand>
    {
        private readonly IWriteDbContext _db;

        public SaveWeighbridgeCommandHandler(IWriteDbContext db)
        {
            _db = db;
        }

        public async Task Handle(SaveWeighbridgeCommand request, CancellationToken cancellationToken)
        {
            var scale = request.Id > 0
                ? await _db.Weighbridges.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                : null;

            if (scale == null)
            {
                scale = new Weighbridge();
                _db.Weighbridges.Add(scale);
            }

            scale.Name = request.Name;
            scale.ScaleCode = request.ScaleCode;
            scale.CodMarkaz = request.CompanyCode;
            scale.WeighbridgeSite = request.SiteId;
            scale.UserID = request.UserId;
            scale.Type = request.Type;

            await WeighbridgeSiteUserLinks.EnsureAsync(_db, request.UserId, request.SiteId, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }

    public sealed class AssignWeighbridgeSiteUserCommandHandler : IRequestHandler<AssignWeighbridgeSiteUserCommand>
    {
        private readonly IWriteDbContext _db;

        public AssignWeighbridgeSiteUserCommandHandler(IWriteDbContext db)
        {
            _db = db;
        }

        public async Task Handle(AssignWeighbridgeSiteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                return;
            }

            user.CodMarkaz = request.CompanyCode;
            user.SelectedSiteId = request.SiteId;

            await WeighbridgeSiteUserLinks.EnsureAsync(_db, user.Id, request.SiteId, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }

    public sealed class RemoveWeighbridgeSiteUserCommandHandler : IRequestHandler<RemoveWeighbridgeSiteUserCommand>
    {
        private readonly IWriteDbContext _db;

        public RemoveWeighbridgeSiteUserCommandHandler(IWriteDbContext db)
        {
            _db = db;
        }

        public async Task Handle(RemoveWeighbridgeSiteUserCommand request, CancellationToken cancellationToken)
        {
            var links = await _db.WeighbridgeSiteUsers
                .Where(x => x.UserId == request.UserId && x.SiteId == request.SiteId)
                .ToListAsync(cancellationToken);

            if (links.Count == 0)
            {
                return;
            }

            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user != null && user.SelectedSiteId == request.SiteId)
            {
                user.SelectedSiteId = await _db.WeighbridgeSiteUsers
                    .Where(x => x.UserId == request.UserId && x.SiteId != request.SiteId)
                    .Select(x => (int?)x.SiteId)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            _db.WeighbridgeSiteUsers.RemoveRange(links);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }

    public sealed class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
    {
        private readonly IWriteDbContext _db;

        public DeleteCompanyCommandHandler(IWriteDbContext db)
        {
            _db = db;
        }

        public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _db.Companies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (company == null)
            {
                return;
            }

            _db.Companies.Remove(company);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }

    internal static class WeighbridgeSiteUserLinks
    {
        public static async Task EnsureAsync(IWriteDbContext db, string userId, int siteId, CancellationToken cancellationToken)
        {
            var exists = await db.WeighbridgeSiteUsers
                .AnyAsync(x => x.UserId == userId && x.SiteId == siteId, cancellationToken);

            if (!exists)
            {
                db.WeighbridgeSiteUsers.Add(new WeighbridgeSiteUser
                {
                    UserId = userId,
                    SiteId = siteId,
                    AssignedAt = DateTime.Now
                });
            }
        }
    }
}
