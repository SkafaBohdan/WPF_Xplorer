using pdftron.Filters;
using pdftron.SDF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Services
{
    public class StreamService : IStreamService
    {
        public string Path { get; }

        public StreamService(string path)
        {
            Path = path;
        }

        public Filter CreateStream(Obj streamObj, int streamNumber, out string fullPath)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            var stream = streamObj.GetDecodedStream();
            fullPath = System.IO.Path.Combine(Path, $"stream{streamNumber++}.txt");

            stream.WriteToFile(fullPath, true);

            return stream;
        }
    }
}
