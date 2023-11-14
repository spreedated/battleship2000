using System.IO;
using System.Reflection;

namespace EngineLayer
{
    public static class HelperFunctions
    {
        public static string LoadEmbeddedResourceString(Assembly assembly,string resourceName, bool singleLine = false)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                return null;
            }

            string content = null;

            using (Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.{resourceName}"))
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
