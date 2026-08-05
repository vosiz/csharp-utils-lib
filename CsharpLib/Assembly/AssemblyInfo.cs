using System;
using Vosiz.Commons;

namespace Vosiz.Assembly
{

    public class AssemblyInfo
    {

        public Version Version { private set; get; }
        public string OS { private set; get; }
        public bool IsProduction { private set; get; }
        public string Label { private set; get; }
        public string BuildId { private set; get; }


        // Constructor with version and the deferred metadata fields
        public AssemblyInfo(Version version, string os, bool is_production, string label, string build_id)
        {

            Assert.OnNull(version);

            Version = version;
            OS = os;
            IsProduction = is_production;
            Label = label;
            BuildId = build_id;
        }

    }
}
