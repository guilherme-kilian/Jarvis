using Jarvis.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jarvis.Utils
{
    public static class JsonUtils
    {
        private static JsonSerializerOptions? _options;

        public static JsonSerializerOptions GetOptions()
        {
            if(_options is null)
            {
                _options = new()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,                    
                };

                _options.Converters.Add(new EnumStringConverter());
            }

            return _options;
        }
    }
}
