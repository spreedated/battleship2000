using System.IO;
using static Battleship2000.Logic.RuntimeStorage;

namespace Battleship2000.Logic
{
    internal static class HelperFunctions
    {
        public static string LoadEmbeddedResource(string resourceName, bool singleLine = false)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                return null;
            }

            string content = null;

            using (Stream stream = MyAssembly.GetManifestResourceStream($"{MyAssembly.GetName().Name}.Resources.{resourceName}"))
            {
                using (StreamReader reader = new(stream))
                {
                    content = reader.ReadToEnd();
                }
            }

            if (singleLine)
            {
                content = content.Replace("\n", "").Replace("\r", "").Trim();
            }

            return content;
        }
    }
}
