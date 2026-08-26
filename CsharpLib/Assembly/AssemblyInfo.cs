using System;
using System.Text;
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

        // Simple descriptor: just the version, e.g. "1.2.3"
        public override string ToString()
        {

            return Version.ToString();
        }

        // Renders the full descriptor from stored fields
        // - override to append custom parts beyond what's stored here
        public virtual string ToFullString()
        {

            StringBuilder sb = new StringBuilder(ToString());

            if (!string.IsNullOrEmpty(Label))
                sb.Append('-').Append(Label);

            if (!string.IsNullOrEmpty(BuildId))
                sb.Append('+').Append(BuildId);

            sb.Append(" (");

            if (!string.IsNullOrEmpty(OS))
                sb.Append(OS).Append(", ");

            sb.Append(IsProduction ? "Release" : "Debug");
            sb.Append(')');

            return sb.ToString();
        }

    }
}
