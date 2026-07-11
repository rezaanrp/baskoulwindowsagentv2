using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels.Weighbridge;
using Domain.Classes;
using Infra.Data.Context;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Infra.Data.Repository
{
    public class BargeBaskoulRepository : IBargeBaskoulRepository
    {
        private readonly WriteDbContext _context;
        private readonly IMapper _mapper;

        public BargeBaskoulRepository(WriteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //public async Task AddAsync(BargeBaskoulDomainViewModel entity)
        //{
        //    var model = _mapper.Map<BargeBaskoul>(entity);
        //    await _context.AddAsync(model);
        //    await _context.SaveChangesAsync();
        //    await _context.Database.ExecuteSqlRawAsync(
        //        "EXEC UpdateEntityWithSafeGhabzId @p0, @p1",
        //        model.CodMarkaz, model.ID);
        //}

        public async Task AddAsync(BargeBaskoulDomainViewModel entity)
        {
            var model = _mapper.Map<BargeBaskoul>(entity);
            await _context.BargeBaskouls.AddAsync(model);
            await _context.SaveChangesAsync();

            // Begin a transaction to simulate SQL transaction + row lock behavior
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var persianCalendar = new PersianCalendar();
                var now = DateTime.Now;
                var currentYear = persianCalendar.GetYear(now);
                var codMarkaz = model.CodMarkaz;

                // Get last two digits of the year
                var lastTwoDigitsYear = (currentYear % 100).ToString("D2");

                // Try to get tracker row with lock (simulate UPDLOCK, HOLDLOCK)
                var tracker = await _context.GhabzSerialTrackers
                    .Where(t => t.CodMarkaz == codMarkaz && t.Year == currentYear)
                    .FirstOrDefaultAsync();

                int newSerial;

                if (tracker != null)
                {
                    tracker.Serial += 1;
                    newSerial = tracker.Serial;
                }
                else
                {
                    tracker = new GhabzSerialTracker
                    {
                        CodMarkaz = codMarkaz,
                        Year = currentYear,
                        Serial = 1
                    };
                    _context.GhabzSerialTrackers.Add(tracker);
                    newSerial = 1;
                }

                await _context.SaveChangesAsync();

                // Construct GhabzId with last two digits of the year
                var ghabzId = $"{codMarkaz}{lastTwoDigitsYear}{newSerial.ToString().PadLeft(4, '0')}";

                // Update BargeBaskoul row
                model.GhabzBaskolID = long.Parse(ghabzId);
                _context.BargeBaskouls.Update(model);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> AddOrCompleteByPlateAsync(BargeBaskoulDomainViewModel entity)
        {
            var normalizedPlate = (entity.ShomareMashin ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(normalizedPlate) || string.IsNullOrWhiteSpace(entity.CodMarkaz) || !entity.siteId.HasValue)
            {
                throw new InvalidOperationException("اطلاعات پلاک، مرکز یا سایت کامل نیست.");
            }

            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
            try
            {
                var incomplete = await _context.BargeBaskouls
                    .Where(b =>
                        b.CodMarkaz == entity.CodMarkaz &&
                        b.siteId == entity.siteId &&
                        b.ShomareMashin != null &&
                        b.ShomareMashin.Trim() == normalizedPlate &&
                        b.FlgSabt != true &&
                        b.FlgEbtal != true &&
                        ((b.VaznPor.HasValue && b.VaznPor.Value > 0 && (!b.VanKhali.HasValue || b.VanKhali.Value <= 0)) ||
                         (b.VanKhali.HasValue && b.VanKhali.Value > 0 && (!b.VaznPor.HasValue || b.VaznPor.Value <= 0))))
                    .OrderByDescending(b => b.DateTimeBarge ?? DateTime.MinValue)
                    .ThenByDescending(b => b.ID)
                    .FirstOrDefaultAsync();

                if (incomplete != null)
                {
                    var id = incomplete.ID;
                    var receiptId = incomplete.GhabzBaskolID;
                    var dateBarge = incomplete.DateBarge;
                    var timeBarge = incomplete.TimeBarge;
                    var insertedBy = incomplete.Karbar_Ins;
                    var insertedAt = incomplete.DateTimeBarge;
                    var driverId = incomplete.IDRanande;
                    var manualDriver = incomplete.OnvanRanandeh;

                    _mapper.Map(entity, incomplete);
                    incomplete.ID = id;
                    incomplete.GhabzBaskolID = receiptId;
                    incomplete.DateBarge = dateBarge;
                    incomplete.TimeBarge = timeBarge;
                    incomplete.Karbar_Ins = insertedBy;
                    incomplete.DateTimeBarge = insertedAt;
                    incomplete.IDRanande ??= driverId;
                    incomplete.OnvanRanandeh ??= manualDriver;

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }

                var model = _mapper.Map<BargeBaskoul>(entity);
                model.ShomareMashin = normalizedPlate;
                await _context.BargeBaskouls.AddAsync(model);
                await _context.SaveChangesAsync();

                var persianCalendar = new PersianCalendar();
                var currentYear = persianCalendar.GetYear(DateTime.Now);
                var tracker = await _context.GhabzSerialTrackers
                    .FirstOrDefaultAsync(t => t.CodMarkaz == model.CodMarkaz && t.Year == currentYear);

                var newSerial = 1;
                if (tracker == null)
                {
                    tracker = new GhabzSerialTracker
                    {
                        CodMarkaz = model.CodMarkaz,
                        Year = currentYear,
                        Serial = newSerial
                    };
                    _context.GhabzSerialTrackers.Add(tracker);
                }
                else
                {
                    tracker.Serial += 1;
                    newSerial = tracker.Serial;
                }

                var yearSuffix = (currentYear % 100).ToString("D2");
                model.GhabzBaskolID = long.Parse($"{model.CodMarkaz}{yearSuffix}{newSerial.ToString().PadLeft(4, '0')}");
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return false;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Dictionary<int, long?>> MabaniDict(List<long> idsToFetch)
        {
            var mabanidict = await _context.Mabanis
                .Where(m => idsToFetch.Contains(m.ID))
                .ToDictionaryAsync(m => m.ID, m => m.IDLinq);
            return mabanidict;
        }

        public IEnumerable<BargeBaskoulDomainViewModel> GetAll(string codeMarkaz)
        {
            var models = _context.BargeBaskouls.Where(b => b.CodMarkaz == codeMarkaz).ToList();
            var result = _mapper.Map<List<BargeBaskoulDomainViewModel>>(models);
            return result;
        }

        public async Task<PagedResultDomain<BargeBaskoulDomainViewModel>> GetAllAsync(string codeMarkaz, int siteId, int pageNumber, int pageSize)
        {
            var query = _context.BargeBaskouls
                .Where(b => b.CodMarkaz == codeMarkaz && b.siteId == siteId);

            var totalCount = await query.CountAsync();

            var models = await query
                .OrderByDescending(b => b.GhabzBaskolID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<BargeBaskoulDomainViewModel>>(models);

            return new PagedResultDomain<BargeBaskoulDomainViewModel>
            {
                Items = result,
                TotalCount = totalCount
            };
        }

        public async Task<PagedResultDomain<BargeBaskoulDomainViewModel>> GetAllAsyncbyType(int type, string codeMarkaz, int pageNumber, int pageSize)
        {
            var query = _context.BargeBaskouls
                .Where(b => b.TypeBarge == type && b.CodMarkaz == codeMarkaz);

            var totalCount = await query.CountAsync();

            var models = await query
                .OrderByDescending(b => b.GhabzBaskolID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = _mapper.Map<List<BargeBaskoulDomainViewModel>>(models);

            return new PagedResultDomain<BargeBaskoulDomainViewModel>
            {
                Items = result,
                TotalCount = totalCount
            };
        }

        public async Task<IEnumerable<MabaniDomainViewModel>> GetAllMabanisAsync(string codeMarkaz)
        {
            _context.Database.SetCommandTimeout(180); // 180 seconds
            var models = await _context.Mabanis.Where(m => m.CodMarkaz == codeMarkaz).ToListAsync();
            var result = _mapper.Map<List<MabaniDomainViewModel>>(models);
            return result;
        }

        public BargeBaskoulDomainViewModel? GetByGhabzBaskoul(long ghabzId)
        {
            var model = _context.BargeBaskouls.FirstOrDefault(b => b.GhabzBaskolID == ghabzId);
            var result = _mapper.Map<BargeBaskoulDomainViewModel>(model);
            return result;
        }

        public async Task<BargeBaskoulDomainViewModel?> GetLatestByPlateAsync(string codeMarkaz, int siteId, string shomareMashin)
        {
            var normalizedPlate = (shomareMashin ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(normalizedPlate))
            {
                return null;
            }

            var model = await _context.BargeBaskouls
                .AsNoTracking()
                .Where(b =>
                    b.CodMarkaz == codeMarkaz &&
                    b.siteId == siteId &&
                    b.ShomareMashin != null &&
                    b.ShomareMashin.Trim() == normalizedPlate &&
                    b.FlgSabt != true &&
                    b.FlgEbtal != true &&
                    ((b.VaznPor.HasValue && b.VaznPor.Value > 0 && (!b.VanKhali.HasValue || b.VanKhali.Value <= 0)) ||
                     (b.VanKhali.HasValue && b.VanKhali.Value > 0 && (!b.VaznPor.HasValue || b.VaznPor.Value <= 0))))
                .OrderByDescending(b => b.DateTimeBarge ?? DateTime.MinValue)
                .ThenByDescending(b => b.ID)
                .FirstOrDefaultAsync();

            return _mapper.Map<BargeBaskoulDomainViewModel>(model);
        }

        public async Task<BargeBaskoulDomainViewModel?> GetByGhabzBaskoulAsync(long ghabzId)
        {
            var model = await _context.BargeBaskouls.FirstOrDefaultAsync(b => b.GhabzBaskolID == ghabzId);
            var result = _mapper.Map<BargeBaskoulDomainViewModel>(model);
            return result;
        }

        public async void RemoveAsync(BargeBaskoulDomainViewModel entity)
        {
            var result = _mapper.Map<BargeBaskoul>(entity);
            _context.BargeBaskouls.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BargeBaskoulDomainViewModel entity)
        {
            var barg = await _context.BargeBaskouls.FirstOrDefaultAsync(b => b.ID == entity.ID);
            if (barg == null) return;

            _mapper.Map(entity, barg); // map onto the existing tracked entity

            await _context.SaveChangesAsync();
        }

        public async Task<BargeBaskoulDomainViewModel> GetByIDAsync(int id)
        {
            var model = await _context.BargeBaskouls.FirstOrDefaultAsync(b => b.ID == id);
            var result = _mapper.Map<BargeBaskoulDomainViewModel>(model);
            return result;
        }

        public async Task<PagedResultDomain<BargeBaskoulDomainViewModel>> GetFilteredAsyncbyType(
            int type, string codeMarkaz, int siteId, string searchTerm, int page, int pageSize, string sortColumn, string sortDirection)
        {
            _context.Database.SetCommandTimeout(180); // 180 seconds
            IQueryable<BargeBaskoul> query = _context.BargeBaskouls;

            // Filter based on type
            if (type == 1 || type == 2)
            {
                query = query.Where(x => x.TypeBarge == type && x.CodMarkaz == codeMarkaz && x.siteId == siteId);
            }
            else
            {
                query = query.Where(x => x.CodMarkaz == codeMarkaz && x.siteId == siteId);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x =>
                    x.ShomareMashin.Contains(searchTerm) ||
                    x.GhabzBaskolID.ToString().Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();

            // Sorting
            if (!string.IsNullOrEmpty(sortColumn))
            {
                query = ApplySorting(query, sortColumn, sortDirection);
            }
            else
            {
                query = query.OrderByDescending(x => x.ID);
            }

            // Pagination
            var resultList = await query
                .AsNoTracking()   // good practice for read-only
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var model = _mapper.Map<List<BargeBaskoulDomainViewModel>>(resultList);

            return new PagedResultDomain<BargeBaskoulDomainViewModel>
            {
                Items = model,
                TotalCount = totalCount
            };
        }

        private IQueryable<BargeBaskoul> ApplySorting(IQueryable<BargeBaskoul> query, string column, string direction)
        {
            switch (column)
            {
                case "1":
                    return direction == "asc" ? query.OrderBy(x => x.GhabzBaskolID) : query.OrderByDescending(x => x.GhabzBaskolID);
                case "2":
                    return direction == "asc" ? query.OrderBy(x => x.ShomareMashin) : query.OrderByDescending(x => x.ShomareMashin);
                case "10":
                    return direction == "asc" ? query.OrderBy(x => x.ID) : query.OrderByDescending(x => x.ID);
                default:
                    return query.OrderByDescending(x => x.ID);
            }
        }
        public async Task<bool> EbtalBargeAnbar(int bargId, string userId)
        {
            var barge = await _context.BargeBaskouls.FirstOrDefaultAsync(b => b.ID == bargId);
            if (barge == null || barge.FlgSabt == true) return false;
            barge.FlgEbtal = !(barge.FlgEbtal == true);
            barge.Karbar_Ebtal = userId;
            barge.Date_Ebtal = DateTime.Now;
            barge.Karbar_Up = userId;
            barge.Date_Up = DateTime.Now;

            int result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> SabtBargeAnbar(int bargId, string userId)
        {
            var barge = await _context.BargeBaskouls.FirstOrDefaultAsync(b => b.ID == bargId);
            if (barge == null || barge.FlgEbtal == true) return false;

            if (barge.FlgSabt != true)
            {
                barge.FlgSabt = true;
                barge.Karbar_Sabt = userId;
                barge.Date_Sabt = DateTime.Now;
                barge.Date_Up = DateTime.Now;
                barge.Karbar_Up = userId;
            }
            else if (barge.FlgSabt != false)
            {
                barge.FlgSabt = false;
                barge.Date_Up = DateTime.Now;
                barge.Karbar_Up = userId;
            }

            int result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}

