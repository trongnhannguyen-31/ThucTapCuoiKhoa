using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Akavache.Sqlite3;
using Foundation;
using UIKit;

namespace CongDongBau.iOS.Linker
{
    public static class LinkerPreserve
    {
        static LinkerPreserve()
        {
            var persistentName = typeof(SQLitePersistentBlobCache).FullName;
            var encryptedName = typeof(SQLiteEncryptedBlobCache).FullName;
        }
    }
}