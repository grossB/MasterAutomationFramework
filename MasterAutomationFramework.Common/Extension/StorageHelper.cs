﻿using System.IO;

namespace NunitTest.Extensions
{
    public static class StorageHelper
    {
        public static void DeleteFile(string path)
        {
            if (!File.Exists(path))
                return;

            File.Delete(path);
        }

        public static void CreateDirectory(string path)
        {
            if (Path.GetExtension(path).HasValue())
            {
                path = Path.GetDirectoryName(path);
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static bool Exists(string path)
        {
            if (Path.GetExtension(path).HasValue())
            {
                return File.Exists(path);
            }
            else
            {
                return Directory.Exists(path);
            }
        }
    }
}
