using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace Demo_session_3_MVC.Helpers
{
    public class DocumentSetting
    {
        public static string UploadFile (IFormFile file,string folderName)
        {
            
            string folderPath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot\\files" , folderName);

         
            string fileName =$"{Guid.NewGuid()}{ file.FileName}";

       
            string filePath = Path.Combine(folderPath, fileName);

       
           using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;

        }
        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot\\files" , folderName , fileName);
            if (System.IO.File.Exists(filePath) )
                System.IO.File.Delete(filePath);
        }
    }
}
