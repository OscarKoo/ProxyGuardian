using System;
using System.Collections.Generic;
using System.Linq;

namespace ProxyGuardian
{
    public class ProxySetting
    {
        public string AutoConfigURL { get; set; }
        public bool ProxyEnable { get; set; }
        public string ProxyServer { get; set; }
        public string ProxyOverride { get; set; }

        const string local = "<local>";

        public bool Bypass
        {
            set
            {
                var entries = ProxyOverride?.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
                if (value)
                {
                    if (!entries.Contains(local, StringComparer.OrdinalIgnoreCase))
                    {
                        entries.Add(local);
                    }
                }
                else
                {
                    if (entries.Contains(local, StringComparer.OrdinalIgnoreCase))
                    {
                        entries.RemoveAll(w => string.Equals(w, local, StringComparison.OrdinalIgnoreCase));
                    }
                }

                ProxyOverride = string.Join(";", entries);
            }
        }
    }
}