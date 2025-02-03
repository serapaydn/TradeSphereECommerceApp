using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradeSphereECommerceApp.Models
{
    public class FileChangeHistory
    {
        public int ID { get; set; }
        public string FilePath { get; set; }
        public DateTime LastModifiedTime { get; set; }
    }
}