using System.IO;
using System.Linq;

namespace GC.Core.Entities
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }

        public bool IsSupported(string filename)
        {
            return this.AcceptedFileTypes.Any(s => s == Path.GetExtension(filename.ToLower()));
        }
    }
}
