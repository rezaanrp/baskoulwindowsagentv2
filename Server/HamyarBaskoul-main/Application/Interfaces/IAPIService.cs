using Application.Classes;
using Application.ViewModels.APIs;
using Domain.ViewModels.APIs;
using Microsoft.AspNetCore.Identity;
using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.Data.Classes;

namespace Application.Interfaces
{
    public interface IAPIService
    {
        Task<ReturnType<string>> GetToken(string username, string password, string codmarkaz);
        string CreateWindowsToken(IdentityUser user);
        Task<BargiriViweModel> Bargiri(string UserId, long idBargiri);
        Task<TakhlieViewModel> Takhlie(string UserId, long idTakhlie);
        Task<SyncServerViewModel> GetSyncServerView(string codmarkaz, string token, string userid);
        Task<SendToserverViewModel> SendToServer(string codmarkaz, string userid, long selectedDore);
        Task<bool> GetFromServer(GetFromServerViewModel model, string codmarkaz, string userId);
        public Task<SendToserverViewModel> AutoUpdate(string codmarkaz, string userid);
    }
}
