using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vosiz.Commons;

namespace Vosiz.Utils.Archive
{
    interface IArchiver
    {

        Retval ArchiveFiles(string[] filepaths, string zipfilepath);

        Retval ArchiveFolder(string dirpath, string zipfilepath);
    }
}
