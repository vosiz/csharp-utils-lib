using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vosiz.Commons;

namespace Vosiz.Utils.Archive
{
    public class Zip : IArchiver
    {
        public Retval ArchiveFiles(string[] filepaths, string zipfilepath)
        {

            try {

                using (FileStream file = new FileStream(zipfilepath, FileMode.OpenOrCreate))
                    using (ZipArchive archive = new ZipArchive(file, ZipArchiveMode.Update))
                    {
                        foreach (var path in filepaths)
                        {
                            archive.CreateEntryFromFile(path, Path.GetFileName(path));
                        }
                    }

                return Retval.Np;

            } catch (Exception exc)
            {
                throw new WrapperException(exc);
            }
        }

        public Retval ArchiveFolder(string dirpath, string zipfilepath)
        {
            try
            {
                ZipFile.CreateFromDirectory(dirpath, zipfilepath);
                return Retval.Np;
            }
            catch (Exception exc) {

                throw new WrapperException(exc);
            }
            
        }
    }
}
