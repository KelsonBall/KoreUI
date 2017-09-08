using System.IO;
using TrueTypeSharp;

namespace FontPack
{
    public static class Repository
    {
        public static TrueTypeFont Load(string name)
        {            
            using (var stream = new FileStream(name, FileMode.Open))
                return new TrueTypeFont(stream);
        }
    }
}
