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
        int? Type) : IRequest;

    public sealed record AssignSiteUserCommand(
        string CompanyCode,
        int SiteId,
        string UserId) : IRequest;

    public sealed record RemoveSiteUserCommand(
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

            var company = await _db.Companies
                .FirstOrDefaultAsync(x => x.CodMarkaz == request.CompanyCode, cancellationToken);
            if (company == null)
            {
                return;
            }

            site.name = request.SiteName;
            site.CompanyId = company.Id;
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
            scale.WeighbridgeSiteId = request.SiteId;
            scale.Type = request.Type;

            await _db.SaveChangesAsync(cancellationToken);
        }
    }

    public sealed class AssignSiteUserCommandHandler : IRequestHandler<AssignSiteUserCommand>
    {
        private readonly IWriteDbContext _db;

        public AssignSiteUserCommandHandler(IWriteDbContext db)
        {
            _db = db;
        }

        public async Task Handle(AssignSiteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if (user == null)
            {
                return;
            }

            user.CodMarkaz = request.CompanyCode;
            if (user.SelectedSiteId == null)
            {
                user.SelectedSiteId = request.SiteId;
            }

            var exists = await _db.UserSiteAccesses
                .AnyAsync(x => x.UserId == user.Id && x.SiteId == request.SiteId, cancellationToken);

            if (!exists)
            {
                _db.UserSiteAccesses.Add(new UserSiteAccess
                {
                    UserId = user.Id,
                    SiteId = request.SiteId,
                    AssignedAt = DateTime.Now
                });
            }

            await _db.SaveChangesAsync(cancellationToken);
        }
    }

    public sealed class RemoveSiteUserCommandHandler : IRequestHandler<RemoveSiteUserCommand>
    {
        private readonly IWriteDbContext _db;

        public RemoveSiteUserCommandHandler(IWriteDbContext db)
        {
            _db = db;
        }

        public async Task Handle(RemoveSiteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            var access = await _db.UserSiteAccesses
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.SiteId == request.SiteId, cancellationToken);
            if (access != null)
            {
                _db.UserSiteAccesses.Remove(access);
            }

            if (user != null && user.SelectedSiteId == request.SiteId)
            {
                user.SelectedSiteId = await _db.UserSiteAccesses
                    .Where(x => x.UserId == request.UserId && x.SiteId != request.SiteId)
                    .Select(x => (int?)x.SiteId)
                    .FirstOrDefaultAsync(cancellationToken);
            }

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

}
