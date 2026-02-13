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

        //Task ArchiveFiles(string[] filepaths, string zipfilepath, IProgress<int> progress);

        Retval ArchiveFolder(string dirpath, string zipfilepath);

        //Task ArchiveFolder(string dirpath, string zipfilepath, IProgress<int> progress);


    }
}
