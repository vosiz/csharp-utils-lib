using System;
using System.IO;
using Vosiz.Helpers;

namespace Tests.Helpers
{

    public static class PathHelperTests
    {

        // UserProfile returns the user profile folder when nothing is appended
        public static void UserProfileReturnsBaseFolder() {

            string expected = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            Check.Equal(expected, PathHelper.UserProfile());
        }

        // UserProfile appends the given path
        public static void UserProfileAppendsPath() {

            string expected = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "sub");

            Check.Equal(expected, PathHelper.UserProfile("sub"));
        }

        // Appdata returns the application data folder when nothing is appended
        public static void AppdataReturnsBaseFolder() {

            string expected = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            Check.Equal(expected, PathHelper.Appdata());
        }

        // Appdata appends the given path
        public static void AppdataAppendsPath() {

            string expected = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "sub");

            Check.Equal(expected, PathHelper.Appdata("sub"));
        }

        // Combine joins path segments the same way Path.Combine does
        public static void CombineJoinsSegments() {

            string expected = Path.Combine("a", "b", "c");

            Check.Equal(expected, PathHelper.Combine("a", "b", "c"));
        }

    }
}
