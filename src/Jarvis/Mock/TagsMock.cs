using Jarvis.Models.Tags;

namespace Jarvis.Mock
{
    public class TagsMock
    {
        public static List<TagModel> Tags = [UnisinosLabel, UrgentLabel, HomeLabel];

        public static readonly TagModel UnisinosLabel = new() { Title = "Unisinos", Color = "#FF0000" };
        public static readonly TagModel UrgentLabel = new() { Title = "Urgente", Color = "#FF0000" };
        public static readonly TagModel HomeLabel = new() { Title = "Casa", Color = "#FF0000" };
    }
}
