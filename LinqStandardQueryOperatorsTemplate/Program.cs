using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using $safeprojectname$.DataSource;
using $safeprojectname$.Extensions;
using $safeprojectname$.Model;

namespace $safeprojectname$
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRepository.Categories.Dump();
            DataRepository.Products.Dump();
        }
    }
}
