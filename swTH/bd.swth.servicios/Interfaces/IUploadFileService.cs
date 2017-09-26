using bd.swth.entidades.ObjectTransfer;
using bd.swth.entidades.Utils;
using Microsoft.AspNetCore.Mvc;
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
        bool DeleteFile(string folder, string fileName, string extension);
        DocumentoInstitucionalTransfer GetFile(string folder, string fileName, string extension);
    }
}
