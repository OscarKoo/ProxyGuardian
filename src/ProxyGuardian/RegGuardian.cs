using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProxyGuardian
{
    public class RegGuardian
    {
        public int IntervalSeconds { get; set; }
        public ProxyConfig ProxyConfig { get; set; }

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

                    await Task.Delay(Math.Max(IntervalSeconds * 1000, 1000), token).ConfigureAwait(false);
                }
            }
            catch (Exception ex) { }
        }

        public void Update()
        {
            var script = ProxyConfig?.EnabledScript;
            var server = ProxyConfig?.EnabledServer;
            if (script == null && server == null)
                return;

            try { }
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