using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateLibrary
{
    public class HomeImage
    {

        private string caption;
        private string link;
        public string Caption { get { return caption; } set { caption = value; } }
        public string Link { get { return link; } set { link = value; } }
        public HomeImage(string link, string caption)
        {
            this.link = link;
            this.caption = caption;
        }
    }
}
