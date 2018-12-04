using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solutions
{
    public static class Md5Stringifier
    {
        public static IEnumerable<char> GetHexCharacters(string seed)
        {
            using (var md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(seed));
                foreach (var d in data)
                {
                    var chars = d.ToString("x2");
                    yield return chars[0];
                    yield return chars[1];
                }
            }
        }

        public static string GetMd5String(string seed)
        {
            return new string(GetHexCharacters(seed).ToArray());
        }
    }
    
}
