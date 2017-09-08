using bd.swth.entidades.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace bd.swth.servicios.Interfaces
{
   public interface IUploadFileService
    {
        Task<bool> UploadFile(byte[] file, string folder, string fileName, string extension);
    }
}
