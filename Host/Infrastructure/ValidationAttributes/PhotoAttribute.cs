using System.ComponentModel.DataAnnotations;
using static Host.Infrastructure.Consts.CoffeeModelsHelpersAndConsts;


namespace Host.Infrastructure.ValidationAttributes
{
    public class PhotoAttribute : ValidationAttribute
    {
        public PhotoAttribute() 
        {
            ErrorMessage = $"Проверьте фотографию. Размер фотографии не может быть больше {MAX_LENGTH_PHOTO_MB}мб, разрешение должно быть: {HEIGHT_IMAGE_SIZE}x{WIDTH_IMAGE_SIZE} пикселей";
        }
        readonly string[] FILE_EXTS = new string[] { ".jpg", "jpeg" };
        public override bool IsValid(object? value)
        {
            if(value is null)
                return false;

            var photos = value as IEnumerable<IFormFile>;

            if(photos is null || !photos.Any())
                return false;

            foreach( var photo in photos )
            {
               var validRes = ValidatePhoto(photo);

               if(!validRes)
                    return false;

            }
            return true;

        }

        bool ValidatePhoto(IFormFile photo)
        {
            var validExt = FILE_EXTS.Any(photo.FileName.EndsWith);

            
            if(!validExt) 
                return false;


            var photoBytes = GetBytesFromFile(photo);


            if (!CheckImageSize(photoBytes))
                return false;


            if(!CheckImageRezolution(photoBytes))
                return false;


            return true;
        }

        bool CheckImageSize(byte[] bytes) => bytes.Length <= MAX_LENGTH_PHOTO;
        

        bool CheckImageRezolution(byte[] bytes)
        {
            try
            {
                var image = Image.Load(bytes);
                return image.Height == HEIGHT_IMAGE_SIZE && image.Width == WIDTH_IMAGE_SIZE;
            }
            catch { return false; }
            

            
        }

        byte[] GetBytesFromFile(IFormFile photo)
        {
            using var readStream = photo.OpenReadStream();

            using MemoryStream m = new MemoryStream();

            readStream.CopyTo(m);

            var bytes = m.ToArray();


            readStream.Flush();
            readStream.Position = 0;
            return bytes;
        }
    }
}
