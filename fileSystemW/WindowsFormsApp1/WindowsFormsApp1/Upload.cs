using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace FileUploadDownloadAPI
{
    public class Upload
    {
        public string UserInformation { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
    }
}
