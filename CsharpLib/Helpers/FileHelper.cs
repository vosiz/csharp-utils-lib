using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Helpers
{
    public static class FileHelper
    {

        public static async Task CopyDirectoryAsync(string src, string dest, IProgress<int> progress)
        {
            Directory.CreateDirectory(dest);

            string[] files = Directory.GetFiles(src);
            string[] subdirectories = Directory.GetDirectories(src);

            int files_cnt = files.Length;
            int dirs_cnt = subdirectories.Length;

            int fcopied = 0;
            int dcopied = 0;

            foreach (var file in files)
            {
                string destf = Path.Combine(dest, Path.GetFileName(file));

                if (!File.Exists(destf) || File.GetLastWriteTime(file) > File.GetLastWriteTime(destf))
                {
                    File.Copy(file, destf, overwrite: true);
                    fcopied++;
                    int perc = (int)((float)(fcopied + dcopied) / (files_cnt + dirs_cnt) * 100);
                    progress.Report(perc);
                }

                await Task.Delay(1); // To ensure smoothness
            }

            // Copy all subdirectories
            foreach (var subdir in subdirectories)
            {
                string destsubdir = Path.Combine(dest, Path.GetFileName(subdir));

                if (!Directory.Exists(destsubdir))
                {
                    Directory.CreateDirectory(destsubdir);
                    await CopyDirectoryAsync(subdir, destsubdir, progress); // Recursive call
                }

                dcopied++;
                int subdirperc = (int)((float)(fcopied + dcopied) / (files_cnt + dirs_cnt) * 100);
                progress.Report(subdirperc);

                await Task.Delay(1); // To ensure smoothness
            }
        }

    }
}
