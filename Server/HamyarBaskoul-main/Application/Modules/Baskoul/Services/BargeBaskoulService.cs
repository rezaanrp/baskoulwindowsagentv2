using Application.Interfaces;
using Domain.Interfaces;
using AutoMapper;
using Domain.Models;
using Application.ViewModels.Weighbridge;
using Domain.ViewModels.Weighbridge;
using System.Reflection.Metadata.Ecma335;
using Application.Tools;
using Application.Classes;
using ClosedXML.Excel;

namespace Application.Services
{
    public class BargeBaskoulService : IBargeBaskoul
    {
        private readonly IBargeBaskoulRepository _bargebaskoulrepository;
        private readonly IMapper _mapper;

        public BargeBaskoulService(IMapper mapper, IBargeBaskoulRepository bargeBaskoulRepository)
        {
            _mapper = mapper;
            _bargebaskoulrepository = bargeBaskoulRepository;
        }

        public async Task<BargeBaskoulViewModel> CreateBargeBaskoul(int type)
        {
            var model = new BargeBaskoulDomainViewModel();
            model.TypeBarge = type;
            await _bargebaskoulrepository.AddAsync(model);
            var result = _mapper.Map<BargeBaskoulViewModel>(model);
            return result;
        }
        public async Task AddBargeBaskoulAsync(BargeBaskoulViewModel entity)
        {
            entity.TypeBarge ??= InferBargeType(entity.VaznPor, entity.VanKhali);
            entity = await MapMabanis(entity);
            var result = _mapper.Map<BargeBaskoulDomainViewModel>(entity);
            await _bargebaskoulrepository.AddAsync(result);
        }

        public async Task<bool> AddOrCompleteByPlateAsync(BargeBaskoulViewModel entity)
        {
            entity.TypeBarge ??= InferBargeType(entity.VaznPor, entity.VanKhali);
            entity = await MapMabanis(entity);
            var result = _mapper.Map<BargeBaskoulDomainViewModel>(entity);
            return await _bargebaskoulrepository.AddOrCompleteByPlateAsync(result);
        }
        public async Task<PagedResultBarge> GetAllAsync(string codeMarkaz, int siteID, int pageNumber, int pageSize)
        {
            var list = await _bargebaskoulrepository.GetAllAsync(codeMarkaz, siteID, pageNumber, pageSize);
            if (list == null || list.Items == null) return null;
            var result = _mapper.Map<List<BargeBaskoulViewModel>>(list.Items);
            await PopulateDriverNamesAsync(result, codeMarkaz);
            return new PagedResultBarge
            {
                bargeBaskoulViews = result,
                TotalCount = list.TotalCount
            };
        }
        public async Task<PagedResultBarge> GetAllAsyncbyType(int type, string codeMarkaz, int pageNumber, int pageSize)
        {
            var list = await _bargebaskoulrepository.GetAllAsyncbyType(type, codeMarkaz, pageNumber, pageSize);
            var result = _mapper.Map<List<BargeBaskoulViewModel>>(list.Items);
            return new PagedResultBarge
            {
                bargeBaskoulViews = result,
                TotalCount = list.TotalCount
            };
        }
        public async Task<IEnumerable<MabaniViewModel>> GetAllMabanisAsync(string codeMarkaz)
        {
            var list = await _bargebaskoulrepository.GetAllMabanisAsync(codeMarkaz);
            var result = _mapper.Map<List<MabaniViewModel>>(list);
            return result;
        }
        private async Task<BargeBaskoulViewModel> MapMabanis(BargeBaskoulViewModel entity)
        {
            var idsToFetch = new List<long>();
            if (entity.IDKala.HasValue && entity.IDKala.Value != 0) idsToFetch.Add(entity.IDKala.Value);
            if (entity.IDRanande.HasValue && entity.IDRanande.Value != 0) idsToFetch.Add(entity.IDRanande.Value);
            if (entity.IDVasile.HasValue && entity.IDVasile.Value != 0) idsToFetch.Add(entity.IDVasile.Value);
            if (entity.IDTypeMarkazHamz.HasValue && entity.IDTypeMarkazHamz.Value != 0) idsToFetch.Add(entity.IDTypeMarkazHamz.Value);
            if (entity.IDTypeJabejaiee.HasValue && entity.IDTypeJabejaiee.Value != 0) idsToFetch.Add(entity.IDTypeJabejaiee.Value);
            if (entity.IDShakhs.HasValue && entity.IDShakhs.Value != 0) idsToFetch.Add(entity.IDShakhs.Value);
            if (entity.IDTafsili.HasValue && entity.IDTafsili.Value != 0) idsToFetch.Add(entity.IDTafsili.Value);
            if (entity.IDTafsili2.HasValue && entity.IDTafsili2.Value != 0) idsToFetch.Add(entity.IDTafsili2.Value);
            if (entity.IDAnbar.HasValue && entity.IDAnbar.Value != 0) idsToFetch.Add(entity.IDAnbar.Value);

            //var model = new BargeBaskoulViewModel();
            var mabaniDict = await _bargebaskoulrepository.MabaniDict(idsToFetch);

            if (entity.IDKala.HasValue && mabaniDict.TryGetValue((int)entity.IDKala.Value, out var idKala))
                entity.IDKala = idKala;

            if (entity.IDRanande.HasValue && mabaniDict.TryGetValue((int)entity.IDRanande.Value, out var idRanande))
                entity.IDRanande = idRanande;

            if (entity.IDVasile.HasValue && mabaniDict.TryGetValue((int)entity.IDVasile.Value, out var idVasile))
                entity.IDVasile = idVasile;

            if (entity.IDTypeMarkazHamz.HasValue && mabaniDict.TryGetValue((int)entity.IDTypeMarkazHamz.Value, out var idTypeMarkazHamz))
                entity.IDTypeMarkazHamz = idTypeMarkazHamz;

            if (entity.IDTypeJabejaiee.HasValue && mabaniDict.TryGetValue((int)entity.IDTypeJabejaiee.Value, out var idTypeJabejaiee))
                entity.IDTypeJabejaiee = idTypeJabejaiee;

            if (entity.IDShakhs.HasValue && mabaniDict.TryGetValue((int)entity.IDShakhs.Value, out var idShakhs))
                entity.IDShakhs = idShakhs;

            if (entity.IDTafsili.HasValue && mabaniDict.TryGetValue((int)entity.IDTafsili.Value, out var idTafsili))
                entity.IDTafsili = idTafsili;

            if (entity.IDTafsili2.HasValue && mabaniDict.TryGetValue((int)entity.IDTafsili2.Value, out var idTafsili2))
                entity.IDTafsili2 = idTafsili2;

            if (entity.IDAnbar.HasValue && mabaniDict.TryGetValue((int)entity.IDAnbar.Value, out var idAnbar))
                entity.IDAnbar = idAnbar;

            entity.DateBarge = new csShamciToMiladi().MiladiToShamci(DateTime.Now.Date);   // Sets only the date (YYYY-MM-DD)
            entity.DateBaskol = new csShamciToMiladi().MiladiToShamci(DateTime.Now.Date);   // Sets only the date (YYYY-MM-DD)
            entity.TimeBarge = DateTime.Now.ToString("HH:mm:ss");
            entity.TimeBaskol = DateTime.Now.ToString("HH:mm:ss");

            return entity;
        }
        public async Task<BargeBaskoulViewModel> GetBargeBaskoul(int id)
        {
            var barg = await _bargebaskoulrepository.GetByIDAsync(id);
            var model = _mapper.Map<BargeBaskoulViewModel>(barg);
            if (model.IDRanande != null)
                model.isManual = false;
            else model.isManual = true;
                return model;
        }

        public async Task<BargeBaskoulViewModel?> GetLatestByPlateAsync(string codeMarkaz, int siteId, string shomareMashin)
        {
            var barg = await _bargebaskoulrepository.GetLatestByPlateAsync(codeMarkaz, siteId, shomareMashin);
            if (barg == null)
            {
                return null;
            }

            return _mapper.Map<BargeBaskoulViewModel>(barg);
        }

        public async Task UpdateBargeBaskoulAsync(BargeBaskoulViewModel entity)
        {
            entity.TypeBarge ??= InferBargeType(entity.VaznPor, entity.VanKhali);
            entity = await MapMabanis(entity);
            var barg = _mapper.Map<BargeBaskoulDomainViewModel>(entity);
            await _bargebaskoulrepository.UpdateAsync(barg);
        }
        public async Task<PagedResultBarge> GetFilteredAsyncbyType(int type, string codeMarkaz, int siteId, string searchTerm,
            int page, int pageSize, string sortColumn, string sortDirection)
         {
            var model = await _bargebaskoulrepository.GetFilteredAsyncbyType(type, codeMarkaz, siteId, searchTerm, page, pageSize, sortColumn, sortDirection);
            var result = new PagedResultBarge
            {
                TotalCount = model.TotalCount,
                bargeBaskoulViews = _mapper.Map<List<BargeBaskoulViewModel>>(model.Items)
            };
            await PopulateDriverNamesAsync(result.bargeBaskoulViews, codeMarkaz);
            return result;
        }

        private async Task PopulateDriverNamesAsync(IEnumerable<BargeBaskoulViewModel> items, string codeMarkaz)
        {
            var drivers = (await _bargebaskoulrepository.GetAllMabanisAsync(codeMarkaz))
                .Where(m => m.TableName == "Ranande" && m.IDLinq.HasValue)
                .GroupBy(m => m.IDLinq!.Value)
                .ToDictionary(g => g.Key, g => g.First().Onvan ?? "ثبت نشده");

            foreach (var item in items)
            {
                item.DriverDisplayName = !string.IsNullOrWhiteSpace(item.OnvanRanandeh)
                    ? item.OnvanRanandeh
                    : item.IDRanande.HasValue && drivers.TryGetValue(item.IDRanande.Value, out var driverName)
                        ? driverName
                        : "ثبت نشده";
            }
        }

        public async Task<bool> EbtalBargeAnbar(int bargId, string userId)
        {
            if (string.IsNullOrEmpty(userId) || bargId == null) return false;
            return await _bargebaskoulrepository.EbtalBargeAnbar(bargId, userId);
        }

        public async Task<bool> SabtBargeAnbar(int bargId, string userId)
        {
            if (string.IsNullOrEmpty(userId) || bargId == null) return false;
            return await _bargebaskoulrepository.SabtBargeAnbar(bargId, userId);
        }

        public async Task<bool> SaveToDBFromExcelFile(Stream fileStream, string fileName, string codemarkaz)
        {
            try
            {
                using (var workbook = new XLWorkbook(fileStream))
                {
                    var worksheet = workbook.Worksheets.First();
                    var rows = worksheet.RowsUsed().Skip(1); // Skip header row

                    foreach (var row in rows)
                    {
                        var result = new BargeBaskoulDomainViewModel();

                        result.OnvanRanandeh = row.Cell(2).GetValue<string>()?.Trim();

                        result.VaznPor = TryParseFloat(row.Cell(3).GetValue<string>());
                        result.VanKhali = TryParseFloat(row.Cell(4).GetValue<string>());
                        result.ShomareMashin = row.Cell(5).GetValue<string>()?.Trim();
                        result.KerayeHaml = TryParseFloat(row.Cell(6).GetValue<string>());
                        result.VaznBasteBandi = TryParseFloat(row.Cell(7).GetValue<string>());
                        result.MablaghBaskol = TryParseFloat(row.Cell(8).GetValue<string>());
                        result.Tozihat = row.Cell(9).GetValue<string>()?.Trim();
                        result.IDNegahbani = TryParseLong(row.Cell(10).GetValue<string>());
                        result.DateNegahbani = row.Cell(11).GetValue<string>()?.Trim();
                        result.TimeNegahbani = row.Cell(12).GetValue<string>()?.Trim();
                        result.CodMarkaz = codemarkaz;
                        result.TypeBarge = (row.Cell(13).GetValue<string>()?.Trim() == "ورود کالا") ? 1 : 2;

                        if (!string.IsNullOrEmpty(result.OnvanRanandeh) && !string.IsNullOrEmpty(result.ShomareMashin)
                            && result.TypeBarge != null)
                            await _bargebaskoulrepository.AddAsync(result);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error while reading the xml file: {ex.Message}");
                return false;
            }
        }
        private float? TryParseFloat(string input)
        {
            return float.TryParse(input, out var result) ? result : null;
        }

        private long? TryParseLong(string input)
        {
            return long.TryParse(input, out var result) ? result : null;
        }

        private static int? InferBargeType(float? currentWeight, float? previousWeight)
        {
            if (!currentWeight.HasValue || !previousWeight.HasValue)
            {
                return null;
            }

            if (currentWeight.Value <= 0 || previousWeight.Value <= 0 || currentWeight.Value == previousWeight.Value)
            {
                return null;
            }

            return previousWeight.Value > currentWeight.Value ? 1 : 2;
        }

    }
}


