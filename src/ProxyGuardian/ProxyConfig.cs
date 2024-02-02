using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ProxyGuardian
{
    public class ProxyConfig
    {
        public int IntervalSeconds { get; set; } = 300;
        public List<ScriptSetting> Scripts { get; set; } = new List<ScriptSetting>();
        public List<ServerSetting> Servers { get; set; } = new List<ServerSetting>();

        [JsonIgnore]
        public ScriptSetting EnabledScript => Scripts?.FirstOrDefault(w => w.Selected);
        [JsonIgnore]
        public ServerSetting EnabledServer => Servers?.FirstOrDefault(w => w.Selected);
    }

    public interface ISelected
    {
        bool Selected { get; set; }
    }

    public interface IName
    {
        string Name { get; }
    }

    public class ScriptSetting : ISelected, IName
    {
        public string Address { get; set; }
        public bool Selected { get; set; }

        public virtual string Name => Address;
    }

    public class ServerSetting : ScriptSetting
    {
        public int? Port { get; set; }
        public string Exceptions { get; set; }
        public bool Bypass { get; set; }

        public override string Name => $"{Address}:{Port}";
    }
}