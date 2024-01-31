using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Models.ImageManager
{
    public class ImageStream
    {
        public Stream Image { get; set; }
        public string Name { get; set; }
    }
}