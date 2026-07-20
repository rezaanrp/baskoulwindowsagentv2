using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.ViewModels;
using Domain.ViewModels.Reports;
using Infra.Data.Context;
using Infra.Data.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly WriteDbContext _context;
        private readonly IMapper _mapper;

        public ReportsRepository(WriteDbContext cleanArchData, IMapper mapper)
        {
            _context = cleanArchData;
            _mapper = mapper;
        }

        public async Task<ReportSettingDomainViewModel> GetReportSettingByCodMarkaz(string codmarkaz)
        {
            var entity = await _context.ReportSettings.FirstOrDefaultAsync(r => r.Company == codmarkaz);
            var model = _mapper.Map<ReportSettingDomainViewModel>(entity);
            return model;
        }

        public async Task<SimpleReportDomainViewModel> GetSimpleReportViewModel(int id)
        {
            var barge = await _context.BargeBaskouls.FirstOrDefaultAsync(b => b.ID == id);
            var markaz = await _context.Companies.FirstOrDefaultAsync(c => c.CodMarkaz == barge.CodMarkaz);
            var taf = barge.IDTafsili != null ? await _context.Mabanis.FirstOrDefaultAsync(t => t.IDLinq == barge.IDTafsili) : null;
            var shakhs = barge.IDShakhs != null ? await _context.Mabanis.FirstOrDefaultAsync(t => t.IDLinq == barge.IDShakhs) : null;
            var anbar = barge.IDAnbar != null ? await _context.Mabanis.FirstOrDefaultAsync(t => t.IDLinq == barge.IDAnbar) : null;
            var kala = barge.IDKala != null ? await _context.Mabanis.FirstOrDefaultAsync(t => t.IDLinq == barge.IDKala) : null;
            var vasile = barge.IDVasile != null ? await _context.Mabanis.FirstOrDefaultAsync(t => t.IDLinq == barge.IDVasile) : null;
            var TypeMarkazHaml = barge.IDTypeMarkazHamz != null ? await _context.Mabanis.FirstOrDefaultAsync(t => t.IDLinq == barge.IDTypeMarkazHamz) : null;
            var TypeJabejaiee = barge.IDTypeJabejaiee != null ? await _context.Mabanis.FirstOrDefaultAsync(t => t.IDLinq == barge.IDTypeJabejaiee) : null;
            var model = new SimpleReportDomainViewModel
            {
                GhabzBaskoul = barge.GhabzBaskolID,
                TypeBarge = (barge.TypeBarge == 1) ? "برگه باسکول ورود کالا" : "برگه باسکول خروج کالا",
                CoName = markaz.CoName,
                DateBarge = barge.DateTimeBarge,
                TimeBaskoul = barge.TimeBaskol,
                DateBaskoul = barge.DateBaskol,
                IdNegahbani = barge.IDNegahbani,
                TimeNegahbani = barge.TimeNegahbani,
                DateNegahbani = barge.DateNegahbani,
                IdBargiri = barge.IDBargiri,
                IdTakhlie = barge.IDTakhlie,
                TimeBargiri = barge.TimeBargiri,
                TimeTakhlie = barge.TimeTakhlie,
                DateBargiri = barge.DateBargiri,
                DateTakhlie = barge.DateTakhlie,
                Taf = taf==null ? "" : taf.Onvan,
                NameRanande = barge.OnvanRanandeh,
                NameShakhs = shakhs == null ? "" : shakhs.Onvan,
                Anbar = anbar == null ? "" : anbar.Onvan,
                Kala = kala == null ? "" : kala.Onvan,
                Vasile = vasile == null ? "" : vasile.Onvan,
                TypeJabejaei = TypeJabejaiee == null ? "" : TypeJabejaiee.Onvan,
                TypeMarkazHaml = TypeMarkazHaml == null ? "" : TypeMarkazHaml.Onvan,
                ShomareMashin = barge.ShomareMashin,
                VaznPor = barge.VaznPor,
                VaznKhali = barge.VanKhali,
                Tozihat = barge.Tozihat,
                TimeVazneKhali = barge.TimeVaznKhali,
                TimeVaznePor = barge.TimeVaznPor,
                VazneBasteBandi = barge.VaznBasteBandi,
                KerayeHaml = barge.KerayeHaml.ToString(),
                MablaghBaskoul = barge.MablaghBaskol.ToString()
            };
            return model;
        }

        public async Task<TripleReportDomainViewModel> GetTripleReportViewModel(int id, string username)
        {
            var barge = await _context.BargeBaskouls.FirstOrDefaultAsync(b => b.ID == id);
            if (barge == null)
            {
                return null;
            }

            var markaz = await _context.Companies.FirstOrDefaultAsync(c => c.CodMarkaz == barge.CodMarkaz);
            if (markaz == null)
            {
                return null;
            }

            var kala = barge.IDKala != null ? await _context.Mabanis.FirstOrDefaultAsync(t => t.IDLinq == barge.IDKala) : null;
            var vasile = barge.IDVasile != null ? await _context.Mabanis.FirstOrDefaultAsync(t => t.IDLinq == barge.IDVasile) : null;
            var reportSetting = await _context.ReportSettings.FirstOrDefaultAsync(s => s.Company == markaz.CodMarkaz);
            var model = new TripleReportDomainViewModel
            {
                GhabzBaskoul = barge.GhabzBaskolID,
                TypeBarge = (barge.TypeBarge == 1) ? "برگه باسکول ورود کالا" : "برگه باسکول خروج کالا",
                CoName = markaz.CoName,
                DateBarge = barge.DateTimeBarge,
                IdBargiri = barge.IDBargiri,
                IdTakhlie = barge.IDTakhlie,
                NameRanande = barge.OnvanRanandeh,
                Kala = kala == null ? "" : kala.Onvan,
                Vasile = vasile == null ? "" : vasile.Onvan,
                ShomareMashin = barge.ShomareMashin,
                VaznPor = barge.VaznPor,
                VaznKhali = barge.VanKhali,
                VazneBasteBandi = barge.VaznBasteBandi,
                Tozihat = barge.Tozihat,
                TimeVazneKhali = barge.TimeVaznKhali,
                TimeVaznePor = barge.TimeVaznPor,
                OfficeAdress = reportSetting == null
                    ? string.Empty
                    : "دفتر مرکزی: " + reportSetting.OfficeAddress + "-تلفن: "
                    + reportSetting.OfficePhone
                    + "-کد پستی: " + reportSetting.Postcode + "-تلفاکس: " + reportSetting.Telfax,
                FactoryAddress = reportSetting == null
                    ? string.Empty
                    : "آدرس کارگاه: " + reportSetting.FactoryAddress +
                    "-تلفن: " + reportSetting.FactoryPhone + "-فاکس: " + reportSetting.Fax,
                ShomareSanad = reportSetting?.ShomareSanad,
                BaznegariDate = reportSetting?.TarikhBaznegari,
                BaznegariNumber = reportSetting?.ShomareBaznegari,
                AdminName = username,
                LogoPath = reportSetting?.LogoPath ?? string.Empty
            };
            return model;
        }

        public async Task<bool> SaveReportSettings(ReportSettingDomainViewModel model)
        {
            var report = _mapper.Map<ReportSetting>(model);
            var entity = await _context.ReportSettings.FirstOrDefaultAsync(r => r.Company == model.Company);
            if (entity == null)
            {
                await _context.ReportSettings.AddAsync(report);
            }
            else
            {
                var entry = _context.Entry(entity);
                var values = _context.Entry(report).CurrentValues;

                foreach (var property in values.Properties)
                {
                    if (property.Name != nameof(entity.ID)) // Skip key
                    {
                        entry.Property(property.Name).CurrentValue = values[property.Name];
                    }
                }
            }
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}

