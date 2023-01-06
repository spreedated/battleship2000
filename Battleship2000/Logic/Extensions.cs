using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship2000.Logic
{
    internal static class Extensions
    {
        public static string ToNiceString(this Version x, bool versionSign = false) => string.Format("{0}{1}.{2}{3}{4}", versionSign ? "v" : "", x.Major, x.Minor, x.Revision == 0 && x.Build != 0 || x.Revision != 0 && x.Build != 0 ? "." + x.Build.ToString() : "", x.Revision != 0 && x.Build != 0 ? "." + x.Revision.ToString() : "");

        public static T GetRandomElement<T>(this IEnumerable<T> l)
        {
            Random rnd = new(BitConverter.ToInt32(Guid.NewGuid().ToByteArray()));
            return l.ToArray()[rnd.Next(0,l.Count())];
        }
    }
}
