using bd.swth.entidades.Utils;
using bd.swth.servicios.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace bd.swth.servicios.Servicios
{
    public class UploadFileService : IUploadFileService
    {
        private IHostingEnvironment _hostingEnvironment;


        public UploadFileService(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        public async Task<bool> UploadFile(byte[] file, string folder, string fileName, string extension)
        {

            try
            {
                var stream = new MemoryStream(file);
                var a = string.Format("{0}/{1}.{2}", folder, fileName, extension);
                var targetDirectory = Path.Combine(_hostingEnvironment.WebRootPath,a );

                using (var fileStream = new FileStream(targetDirectory, FileMode.Create, FileAccess.Write))
                {
                   await stream.CopyToAsync(fileStream);
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }
}
