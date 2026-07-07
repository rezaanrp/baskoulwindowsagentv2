using Application.Common.Interfaces;
using Application.ViewModels;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WeighbridgeOrganization.Queries
{
    public sealed record GetWeighbridgeOrganizationTreeQuery : IRequest<CompanyTreeViewModel>;

    public sealed class GetWeighbridgeOrganizationTreeQueryHandler : IRequestHandler<GetWeighbridgeOrganizationTreeQuery, CompanyTreeViewModel>
    {
        private readonly IReadDbContext _db;

        public GetWeighbridgeOrganizationTreeQueryHandler(IReadDbContext db)
        {
            _db = db;
        }

        public async Task<CompanyTreeViewModel> Handle(GetWeighbridgeOrganizationTreeQuery request, CancellationToken cancellationToken)
        {
            var companies = await _db.Companies
                .AsNoTracking()
                .OrderBy(x => x.CoName)
                .ToListAsync(cancellationToken);

            var sites = await _db.WeighbridgeSites
                .AsNoTracking()
                .Include(x => x.WeighbridgeSiteUsers)
                .ThenInclude(x => x.User)
                .OrderBy(x => x.name)
                .ToListAsync(cancellationToken);

            var scales = await _db.Weighbridges
                .AsNoTracking()
                .Include(x => x.User)
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            var users = await _db.Users
                .AsNoTracking()
                .Where(x => !x.IsDelete)
                .OrderBy(x => x.UserName)
                .ToListAsync(cancellationToken);

            return new CompanyTreeViewModel
            {
                Users = users.Select(ToUserOption).ToList(),
                Companies = companies.Select(company => new CompanyNodeViewModel
                {
                    Id = company.Id,
                    CompanyCode = company.CodMarkaz,
                    CompanyName = company.CoName,
                    ConnectionUrl = company.MarkazURL,
                    ApiUrl = company.APIURL,
                    AutoSync = company.AutoAsync ?? false,
                    WeighbridgeSites = sites
                        .Where(site => site.Company == company.CodMarkaz)
                        .Select(site => new CompanySiteNodeViewModel
                        {
                            Id = site.ID,
                            SiteName = site.name,
                            CompanyCode = site.Company,
                            IsActive = site.isActive,
                            Users = site.WeighbridgeSiteUsers
                                .Where(x => x.User != null)
                                .Select(x => ToUserOption(x.User!))
                                .ToList(),
                            Scales = scales
                                .Where(scale => scale.WeighbridgeSite == site.ID)
                                .Select(scale => new CompanyScaleNodeViewModel
                                {
                                    Id = scale.Id,
                                    Name = scale.Name,
                                    ScaleCode = scale.ScaleCode,
                                    Type = scale.Type,
                                    UserId = scale.UserID,
                                    UserName = scale.User?.UserName
                                })
                                .ToList()
                        })
                        .ToList()
                }).ToList()
            };
        }

        private static CompanyUserOptionViewModel ToUserOption(AppUser user)
        {
            return new CompanyUserOptionViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                DisplayName = string.Join(" ", new[] { user.Name, user.Family }.Where(x => !string.IsNullOrWhiteSpace(x)))
            };
        }
    }
}
