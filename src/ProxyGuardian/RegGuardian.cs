using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ProxyGuardian
{
    public class RegGuardian
    {
        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        static bool settingsReturn, refreshReturn;

        public ProxyConfig ProxyConfig { get; set; }

        readonly ProxySetting current = new ProxySetting();

        CancellationTokenSource cancellation;

        public void Start()
        {
            if (this.cancellation == null)
                this.cancellation = new CancellationTokenSource();

            Task.Run(() => Guard(this.cancellation.Token));
        }

        async Task Guard(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    Update();

                    await Task.Delay(Math.Max(ProxyConfig.IntervalSeconds * 1000, 1000), token).ConfigureAwait(false);
                }
            }
            catch (Exception ex) { }
        }

        public void Update()
        {
            var script = ProxyConfig?.EnabledScript;
            var server = ProxyConfig?.EnabledServer;

            try
            {
                this.current.AutoConfigURL = script?.Address;

                this.current.ProxyEnable = server != null;
                if (server != null)
                {
                    this.current.ProxyServer = server.Name;
                    this.current.ProxyOverride = server.Exceptions;
                    this.current.Bypass = server.Bypass;
                }

                const string registryPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
                using (var key = Registry.CurrentUser.OpenSubKey(registryPath, true))
                {
                    if (key != null)
                    {
                        if (string.IsNullOrWhiteSpace(this.current.AutoConfigURL))
                        {
                            if (key.GetValueNames().Contains(nameof(ProxySetting.AutoConfigURL), StringComparer.OrdinalIgnoreCase))
                                key.DeleteValue(nameof(ProxySetting.AutoConfigURL));
                        }
                        else
                            key.SetValue(nameof(ProxySetting.AutoConfigURL), this.current.AutoConfigURL);


                        key.SetValue(nameof(ProxySetting.ProxyEnable), this.current.ProxyEnable ? 1 : 0);
                        if (!string.IsNullOrWhiteSpace(this.current.ProxyServer))
                            key.SetValue(nameof(ProxySetting.ProxyServer), this.current.ProxyServer);
                        if (!string.IsNullOrWhiteSpace(this.current.ProxyOverride))
                            key.SetValue(nameof(ProxySetting.ProxyOverride), this.current.ProxyOverride);
                    }
                    else
                    {
                        Console.WriteLine("Unable to open reg.");
                    }
                }

                settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
                refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
            }
            catch (Exception ex) { }
        }

        public void Stop()
        {
            if (this.cancellation == null)
                return;

            this.cancellation.Cancel(false);
            this.cancellation = null;
        }
    }
}