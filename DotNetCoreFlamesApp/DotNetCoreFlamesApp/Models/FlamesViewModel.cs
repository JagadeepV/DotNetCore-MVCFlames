using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreFlamesApp.Models
{
    public class FlamesViewModel
    {
        public FlamesModel Flames { get; set; }
        public string Result { get; set; }
        public string ImgSrc { get; set; }
        public string AltImgSrc { get; set; }
    }
}
