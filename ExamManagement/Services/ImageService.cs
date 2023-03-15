using System;
using System.IO;


namespace ExamManagement.Services
{
    public class ImageService
    {
        private string basePath = Directory.GetCurrentDirectory() + "/Temp"+"/img";

        public string AddImage(string imagePath, Guid newPath)
        {
            var directPath = basePath + "/" + newPath.ToString();
            if (Directory.Exists(basePath+ "/" + directPath))
            {
                
                File.Copy(imagePath, directPath + "/" + Path.GetFileName(imagePath));
                return directPath + "/" + Path.GetFileName(imagePath);
            }
            else
            {
                Directory.CreateDirectory(directPath);
                File.Copy(imagePath, directPath + "/" + Path.GetFileName(imagePath));
                return directPath + "/" + Path.GetFileName(imagePath);
               
            }
        }


        public void GetMedia(string path)
        {

        }
    }
}
