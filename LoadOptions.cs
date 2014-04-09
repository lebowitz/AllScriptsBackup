using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllScriptRipper
{
    public class LoadOptions
    {
        public bool Demographics { get; set; }
        public bool ReleaseOfInformation { get; set; }
        public Uri ElasticSearchUri { get; set; }
        public string Index { get; set; }
        public string Type { get; set; }
        public IEnumerable<string> PatientIds { get; set; }
    }
}
