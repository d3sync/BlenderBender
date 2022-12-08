using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderBender.Models
{
    public class FileModel
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string FileExtension { get; set; }
        public string ChangeType { get; set; }
        public string Classification { get; set; }
        public string HasBarcodes { get; set; }
    }
}
