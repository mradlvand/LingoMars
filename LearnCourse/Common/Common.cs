
using Model.General;
using System.Security.Claims;

namespace LearnCourse.Common
{
    public static class Common
    {
        public static string UploadFile(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            using (var fileSrteam = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileSrteam);
            }

            return fileName;
        }

        //public static Category GetUserCategory(ClaimsIdentity identity)
        //{
        //    var categoryClaim = identity.FindFirst("UserCategory").Value;

        //    Category category = (Category)Enum.Parse(typeof(Category), categoryClaim, true);

        //    return category;
        //}

        //public static UserRole GetUserRole(ClaimsIdentity identity)
        //{
        //    var roleClaim = identity.FindFirst("Role").Value;

        //    var userRole = (UserRole)Enum.Parse(typeof(UserRole), roleClaim, true);

        //    return userRole;
        //}
    }


}
