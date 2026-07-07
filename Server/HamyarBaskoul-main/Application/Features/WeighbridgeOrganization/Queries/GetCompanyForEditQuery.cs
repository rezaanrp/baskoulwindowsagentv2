using Application.Common.Interfaces;
using Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WeighbridgeOrganization.Queries
{
    public sealed record GetCompanyForEditQuery(int Id) : IRequest<CompanyViewModel?>;

    public sealed class GetCompanyForEditQueryHandler : IRequestHandler<GetCompanyForEditQuery, CompanyViewModel?>
    {
        private readonly IReadDbContext _db;

        public GetCompanyForEditQueryHandler(IReadDbContext db)
        {
            _db = db;
        }

        public async Task<CompanyViewModel?> Handle(GetCompanyForEditQuery request, CancellationToken cancellationToken)
        {
            return await _db.Companies
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new CompanyViewModel
                {
                    Id = x.Id,
                    CoName = x.CoName,
                    CodMarkaz = x.CodMarkaz,
                    MarkazURL = x.MarkazURL,
                    APIURL = x.APIURL,
                    AutoAsync = x.AutoAsync ?? false
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
