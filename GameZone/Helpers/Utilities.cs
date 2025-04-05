namespace GameZone.Helpers
{
    public class Utilities
    {
        public static void CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                }
                catch (Exception ex)
                {
                    throw new Exception();
                }
            }

        }
        private static string ReplaceFileNameWithGUID(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);
            return Guid.NewGuid() + extension;
        }

        /// <summary> 
        /// Saves the uploaded file asynchronously to the specified destination folder. 
        /// </summary> 
        /// <param name="file">The uploaded file to be saved.</param> 
        /// <param name="destination">The directory path where the file should be saved.</param> 
        /// <returns> 
        /// A tuple containing: 
        /// <list type="bullet"> 
        ///     <item><c>bool Succeeded</c> - Indicates whether the file was saved successfully.</item> 
        ///     <item><c>string FileName</c> - The name of the saved file if successful; otherwise, an empty string.</item> 
        /// </list> 
        /// </returns> 
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="file"/> is null.</exception> 
        /// <exception cref="ArgumentException">Thrown if the <paramref name="destination"/> is null or empty.</exception> 
        /// <exception cref="IOException">Thrown if an I/O error occurs while saving the file.</exception> 
        /// <exception cref="Exception">Thrown if any unexpected error occurs.</exception> 
        public static async Task<(bool Succeeded, string FileName)> SaveFileAsync(IFormFile file, string destination)
        {
            try
            {
                if (file == null)
                    throw new ArgumentNullException(nameof(file), "The file cannot be null.");

                if (string.IsNullOrWhiteSpace(destination))
                    throw new ArgumentException("Destination path cannot be empty.", nameof(destination));

                CreateFolderIfDoesNotExist(destination);
                var newFileName = ReplaceFileNameWithGUID(file);
                var newFilePath = Path.Combine(destination, newFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return (true, newFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return (false, string.Empty);
            }
        }




        public static bool DeleteFile(string file, string destination)
        {
            try
            {
                string filePath = Path.Combine(destination, file);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
                return false;
            }
        }
    }
  }