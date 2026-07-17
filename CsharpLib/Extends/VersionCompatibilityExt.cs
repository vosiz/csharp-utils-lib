using Vosiz.Assembly;

namespace Vosiz.Extends
{
    public static class VersionCompatibilityExt
    {
        public static bool ToBoolean(this VersionCompatibility compatibility, bool strict = true)
        {

            switch (compatibility)
            {

                case VersionCompatibility.Compatible:
                    return true;

                case VersionCompatibility.CompatibleWithReservation:
                    return !strict;

                case VersionCompatibility.NotCompatible:
                default:
                    return false;
            }
        }
    }
}
