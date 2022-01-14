using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Blend_MVC.Areas.Manage.Helper
{
    public class Helpers
    {
        public static void DeleteFile(string path, string fileName)
        {
            string filePath = Path.Combine(path, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
       

    }
}
