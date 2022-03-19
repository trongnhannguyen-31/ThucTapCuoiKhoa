using Akavache;

namespace Phoenix.Mobile.Core.LocalData
{
    public sealed class LocalStore
    {
        public static IBlobCache SecuredDb => BlobCache.Secure;
        public static IBlobCache LongCache => BlobCache.UserAccount;
        public static IBlobCache ShortCache=> BlobCache.LocalMachine;
        public static IBlobCache UserDataCache => BlobCache.UserAccount;
    }
}
