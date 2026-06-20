using Application.Classes;
using Application.Interfaces;
using Application.ViewModels.APIs;
using AutoMapper;
using Domain.Interfaces;
using Domain.Classes;
using Microsoft.AspNetCore.Identity;
using Infra.Data.Classes;

namespace Application.Services
{
    public class APIService : IAPIService
    {    
        private readonly IAPIsRepository _apirepo;
        private readonly IMapper _mapper;
        
        public APIService(IAPIsRepository aPIsRepository, IMapper mapper)
        {
            _apirepo = aPIsRepository;
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
        }
        public async Task<BargiriViweModel> Bargiri(string UserId, long idBargiri)
        {
            var entity = await _apirepo.Bargiri(UserId, idBargiri);
            var model = _mapper.Map<BargiriViweModel>(entity);
            return model;
        }

        public async Task<SyncServerViewModel> GetSyncServerView(string codmarkaz, string token, string userid)
        {
            var entity = await _apirepo.GetDore(codmarkaz, token, userid);
            var model = _mapper.Map<SyncServerViewModel>(entity);
            return model;
        }

        public async Task<ReturnType<string>> GetToken(string username, string password, string codmarkaz)
        {
            return await _apirepo.GetToken(username, password, codmarkaz);
        }

        public async Task<SendToserverViewModel> SendToServer(string codmarkaz, string userid, long selectedDore)
        {
            var model = await _apirepo.SendToServer(codmarkaz, userid, selectedDore.ToString());
            return new SendToserverViewModel
            {
                State = model.State,
                Message = model.Message,
                Data = model.Data
            };
        }

        public async Task<TakhlieViewModel> Takhlie(string UserId, long idTakhlie)
        {
            var entity = await _apirepo.Takhlie(UserId, idTakhlie);
            var model = _mapper.Map<TakhlieViewModel>(entity);
            return model;
        }

        public async Task<bool> GetFromServer(GetFromServerViewModel model, string codmarkaz, string userId)
        {
            var dore = model.Dore;
            var selectedEnums = model.SelectedEnums; 

            var parsedEnums = selectedEnums
                .Select(x => Enum.Parse<List_Enum>(x))
                .ToList();

            foreach (var item in parsedEnums)
            {
                var response = await _apirepo.GetFromServer(long.Parse(model.Dore), item.ToString(), codmarkaz, userId);
                if (!response) return response;
            }
            await _apirepo.InsertDefaultTypeJabejaiee(userId, codmarkaz);
            await _apirepo.UpdateSelectedCycle(codmarkaz, dore);
            return true;
        }
  
        public async Task<SendToserverViewModel> AutoUpdate(string codmarkaz, string userid)
        {
            if (codmarkaz == null) return new SendToserverViewModel
            {
                State = false,
                Message = "به‌روز رسانی برگه‌ها با خطا مواجه شد"
            };
            var res = await _apirepo.isAutoAsyncOn(codmarkaz);
            if (!res) return new SendToserverViewModel
            {
                State = false,
                Message = "به‌روز رسانی برگه‌ها با خطا مواجه شد"
            };
            var dore = await _apirepo.GetSelectedCycle(codmarkaz);
            if (string.IsNullOrEmpty(dore)) return new SendToserverViewModel
            {
                State = false,
                Message = "به‌روز رسانی برگه‌ها با خطا مواجه شد"
            };
            var result = await _apirepo.SendToServer(codmarkaz, userid, dore);
            return new SendToserverViewModel
            {
                State = result.State,
                Message = result.Message,
                Data = result.Data
            };
        }

        public string CreateWindowsToken(IdentityUser user)
        {
            return _apirepo.CreateWindowsToken(user);
        }
    }
}
