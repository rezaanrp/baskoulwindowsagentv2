using Application.Interfaces;
using Application.ViewModels.BaseData;
using AutoMapper;
using Domain.Interfaces;
using Domain.ViewModels.BaseData;
using Domain.ViewModels.Weighbridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BaseDataService : IBaseService
    {
        private readonly IBaseRepository _baserepo;
        private readonly IMapper _mapper;

        public BaseDataService(IBaseRepository baseDataRepository, IMapper mapper)
        {
            _baserepo = baseDataRepository;
            _mapper = mapper;
        }

        private enum List_Enum : int
        {
            Tafsili = 0,

            Tafsili2 = 1,

            Anbar = 2,

            Kala = 3,

            Vasile = 4,

            Ranande = 5,

            MarkazHaml = 6,

            Shakhs = 7,

            JabeJaei = 8,
        }

        public async Task AddAsync(ListPageViewModel model, string codmarkaz, string userId)
        {
            string tableName = Enum.GetName(typeof(List_Enum), model.type);
            var data = new BaseDataDmainViewModel
            {
                Onvan = model?.name,
                Tozihat = model?.des,
                TableName = tableName
            };
            await _baserepo.AddAsync(data, codmarkaz, userId);
        }

        public async Task<List<BaseDataViewModel>> GetDataAsync(int type, string codmarkaz, string userId)
        {
            // Convert int → enum name (e.g., 2 → "Anbar")
            string tableName = Enum.GetName(typeof(List_Enum), type);

            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentException($"Invalid type value: {type}");

            var list = await _baserepo.GetDataAsync(tableName, codmarkaz);

            var model = _mapper.Map<List<BaseDataViewModel>>(list);
            return model;
        }

        public async Task<BaseDataViewModel> GetByIdAsync(int Id)
        {
            if (Id == null)
                return null;
            var data = await _baserepo.GetByIdAsync(Id);
            var model = _mapper.Map<BaseDataViewModel>(data);
            return model;
        }

        public async Task DeleteAsync(BaseDataViewModel entity)
        {
            if (entity != null)
            {
                var model = _mapper.Map<BaseDataDmainViewModel>(entity);
                await _baserepo.RemoveAsync(model);
            }
        }

        public async Task UpdateAsync(BaseDataViewModel entity, string codmarkaz)
        {
            var model = _mapper.Map<BaseDataDmainViewModel>(entity);
            model.CodMarkaz = codmarkaz;
            await _baserepo.UpdateAsync(model);
        }
    }
}

