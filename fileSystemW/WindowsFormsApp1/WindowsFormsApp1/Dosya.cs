using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Root_
    {
       public List<UserFiles> files { get; set; }

    }
    public class UserFiles
    {
        public int FileID { get; set; }
        public string Owner { get; set; }
        public string FileName { get; set; }
        public int FileSizeByte { get; set; }
        public string FileStatus { get; set; }
        public bool IsMyFile { get; set; }
    }
}
