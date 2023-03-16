using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace ExamManagement.Services
{
    public class ImageService
    {
        // Path to the directory
        private string basePath = Directory.GetCurrentDirectory() + "/Temp"+"/img";

        // Adds an image to the directory specified by the new path.
        public string AddImage(string imagePath, Guid newPath)
        {
            // Combines the base path with the new path to create a directory path for the image.
            var directPath = basePath + "/" + newPath.ToString();

            // Checks if the directory already exists.
            if (Directory.Exists(basePath + "/" + directPath))
            {
                // Copies the image to the directory.
                File.Copy(imagePath, directPath + "/" + Path.GetFileName(imagePath));

                // Returns the full path to the image.
                return directPath + "/" + Path.GetFileName(imagePath);
            }
            else
            {
                // Creates the directory and copies the image to it.
                Directory.CreateDirectory(directPath);
                File.Copy(imagePath, directPath + "/" + Path.GetFileName(imagePath));

                // Returns the full path to the image.
                return directPath + "/" + Path.GetFileName(imagePath);
            }
        }

        // Retrieves an image from the specified path and returns it as a BitmapImage.
        public BitmapImage GetMedia(string path)
        {
            BitmapImage bitmap = new BitmapImage();

            // Initializes the BitmapImage and sets the URI source to the path.
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.EndInit();

            // Returns the BitmapImage.
            return bitmap;
        }
    }
}

