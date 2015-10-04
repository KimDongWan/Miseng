using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miseng.Model
{
    public class FileSrc
    {
        public String Src { get; set; }
        public String Path { get; set; }

        public FileSrc()
        {
            Src = "";
            Path = "";
        }
        public FileSrc(String FilePath)
        {
            Path = FilePath;
            Src = System.IO.File.ReadAllText(Path);
        }

        public string getFileSrc()
        {
            if (String.IsNullOrEmpty(Src) && String.IsNullOrEmpty(Path))
                return null;

            if (String.IsNullOrEmpty(Src) && !String.IsNullOrEmpty(Path))
                return System.IO.File.ReadAllText(Path);

            return Src;
        }

    }
}
