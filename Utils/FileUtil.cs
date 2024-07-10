namespace HeThongDangKyDuHoc.Utils
{
    public class FileUtil
    {
        public static async Task<bool> SaveFile(string directory, string fileName, IFormFile file)
        {
            string path = "./wwwroot/img/" + directory + "/" + fileName;
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
