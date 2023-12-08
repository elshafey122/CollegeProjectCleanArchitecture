using Microsoft.AspNetCore.Http;
using SchoolProject.Service.Abstractions;
namespace SchoolProject.Service.Implemintations
{
    public class FileService : IFileService
    {
        public async Task<string> UploadImage(string Location, IFormFile file)
        {
            //upload image to wwwroot folder and return path of image uploaded 

            var path = Location;   // location of wwwroot and folder of images inside it   
            var extension = Path.GetExtension(file.FileName);// get extension of file
            var filename = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;  // make string with a name from guid + extension of image
            if (file.Length > 0)   // is image is exists
            {
                try
                {
                    if (!Directory.Exists(path))  // check folder for wwwrootimage is added
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestream = File.Create(path + filename))  // make file from this root 
                    {
                        await file.CopyToAsync(filestream); // add image to this file
                        await filestream.FlushAsync();
                        return $"/Instructors/{filename}";
                    }
                }
                catch (Exception)
                {
                    return "FailedToUploadImage";
                }
            }
            else
            {
                return "NoImage";
            }
        }
    }
}
