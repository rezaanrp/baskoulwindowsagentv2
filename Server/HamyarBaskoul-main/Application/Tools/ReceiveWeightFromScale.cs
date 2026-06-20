using Application.Classes;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Application.Tools
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class ReceiveWeightFromScale : Hub
    {
        private readonly ICodeMarkaz codeMarkaz;
        private readonly ISite siteservice;
        private readonly WeightService weightService; // inject here

        public ReceiveWeightFromScale(ICodeMarkaz codeMarkaz, ISite siteservice, WeightService weightService)
        {
            this.codeMarkaz = codeMarkaz;
            this.siteservice = siteservice;
            this.weightService = weightService;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var appName = httpContext?.Items["AppName"]?.ToString();
            var codmarkaz = await codeMarkaz.GetCodMarkazByUrl(appName);

            if (!string.IsNullOrEmpty(codmarkaz))
                await Groups.AddToGroupAsync(Context.ConnectionId, codmarkaz);

            await base.OnConnectedAsync();
        }

        public async Task SendWeightUpdate(string siteCode, string scaleCode, string weight, string codmarkaz)
        {
            FileLogger.Log($"weight: {weight}, siteCode: {siteCode}, scalecode: {scaleCode}, codmarkaz: {codmarkaz}");

            // Optionally trigger local event
            WeightReceived?.Invoke(this, new WeightReceivedEventArgs(scaleCode, weight));

            // Server-side broadcast ignoring token
            await weightService.BroadcastWeight(siteCode, scaleCode, weight, codmarkaz);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var codmarkaz = await getCodmarkaz();
            if (!string.IsNullOrEmpty(codmarkaz))
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, codmarkaz);

            await base.OnDisconnectedAsync(exception);
        }

        private async Task<string> getCodmarkaz()
        {
            var httpContext = Context.GetHttpContext();
            var appname = httpContext?.Items["AppName"]?.ToString();
            return await codeMarkaz.GetCodMarkazByUrl(appname);
        }

        public event EventHandler<WeightReceivedEventArgs> WeightReceived;
    }


    // Custom event arguments class to store weight info
    public class WeightReceivedEventArgs : EventArgs
    {
        public string ScaleCode { get; }
        public string Weight { get; }

        public WeightReceivedEventArgs(string scaleCode, string weight)
        {
            ScaleCode = scaleCode;
            Weight = weight;
        }
    }

    public class WeightService
    {
        private readonly IHubContext<ReceiveWeightFromScale> _hubContext;

        public WeightService(IHubContext<ReceiveWeightFromScale> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastWeight(string siteCode, string scaleCode, string weight, string codmarkaz)
        {
            // No token check; directly broadcast
            await _hubContext.Clients.All.SendAsync("ReceiveWeightUpdate", siteCode, codmarkaz, scaleCode, weight);
        }
    }

}
