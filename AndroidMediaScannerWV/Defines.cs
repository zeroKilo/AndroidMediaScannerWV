using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMediaScannerWV
{
    public static class Defines
    {
        public enum FileTypeCategory
        {
            Other,
            Audio,
            Image,
            Video
        };

        public static Dictionary<string, FileTypeCategory> AllowedFileTypes = new Dictionary<string, FileTypeCategory>()
        {
            {".mp3", FileTypeCategory.Audio },
            {".opus", FileTypeCategory.Audio },
            {".jpg", FileTypeCategory.Image },
            {".jpeg", FileTypeCategory.Image },
            {".png", FileTypeCategory.Image },
            {".gif", FileTypeCategory.Image },
            {".mp4", FileTypeCategory.Video }
        };
    }
}
