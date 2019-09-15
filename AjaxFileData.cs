using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCBC_NextGen.model
{
    public class AjaxFileData
    {
        public int ID { get; set; }
        public string FileID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string CreatedDate { get; set; }
    }
}