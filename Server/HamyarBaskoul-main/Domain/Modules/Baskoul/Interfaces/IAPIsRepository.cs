using Domain.ViewModels.APIs;
using Domain.ViewModels.Weighbridge;
using Microsoft.AspNetCore.Identity;
using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using Domain.Classes;

namespace Domain.Interfaces
{
    public interface IAPIsRepository
    {
        Task<ReturnType<string>> GetToken(string username, string password, string codmarkaz);
        string CreateWindowsToken(IdentityUser user);
        Task<TakhlieDomainViewModel> Takhlie(string UserId, long idTakhlie);
        Task<BargiriDomainViewModel> Bargiri(string UserId, long idBargiri);
        Task<SendToServerDomainViewModel> SendToServer(string codmarkaz, string userid, string selectedDore);
        Task<SyncServerDomainViewModel> GetDore(string codmarkaz, string token, string userid);
        Task<bool> GetFromServer(long Dore, string ListId, string codmarkaz, string userId);
        Task InsertDefaultTypeJabejaiee(string userid, string codmarkaz);
        Task UpdateSelectedCycle(string codmarkaz, string Dore);
        Task<bool> isAutoAsyncOn(string codmarkaz);
        Task<string?> GetSelectedCycle(string codmarkaz);
    }
}


