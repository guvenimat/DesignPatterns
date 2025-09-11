using DesignPatterns.Domain.Interfaces.Creational;

namespace DesignPatterns.Domain.Patterns.Creational.Singleton
{
    // Singleton Pattern - Thread-safe implementation
    public class ConfigurationManager : IConfigurationManager
    {
        private static ConfigurationManager? _instance;
        private static readonly object _lock = new object();
        private readonly Dictionary<string, string> _configurations;

        private ConfigurationManager()
        {
            _configurations = new Dictionary<string, string>
            {
                {"AppName", "Design Patterns Demo"},
                {"Version", "1.0.0"},
                {"Environment", "Development"}
            };
        }

        public static ConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigurationManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public string GetConfiguration(string key)
        {
            return _configurations.TryGetValue(key, out var value) ? value : "Not Found";
        }

        public void SetConfiguration(string key, string value)
        {
            _configurations[key] = value;
        }

        public Dictionary<string, string> GetAllConfigurations()
        {
            return new Dictionary<string, string>(_configurations);
        }
    }
}