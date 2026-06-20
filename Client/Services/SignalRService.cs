using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWinformsApp.Services
{
    public class SignalRService : IDisposable
    {
        private readonly HubConnection _connection;

        public bool IsConnected => _connection?.State == HubConnectionState.Connected;

        public SignalRService(string hubUrl)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task StartAsync()
        {
            if (_connection == null) return;
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception)
            {
                // swallow here; caller may retry or log
                throw;
            }
        }

        public async Task SendWeightAsync(string siteCode, string scaleCode, string weight, string codMarkaz)
        {
            if (_connection == null) return;
            try
            {
                await _connection.InvokeAsync("SendWeightUpdate", siteCode, scaleCode, weight, codMarkaz);
            }
            catch (Exception)
            {
                // log or ignore; do not crash UI
            }
        }

        public void Dispose()
        {
            try
            {
                _connection?.StopAsync().GetAwaiter().GetResult();
                _connection?.DisposeAsync().GetAwaiter().GetResult();
            }
            catch { }
        }
    }
}
