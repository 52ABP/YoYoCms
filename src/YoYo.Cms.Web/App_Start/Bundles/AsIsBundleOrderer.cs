using System.Collections.Generic;
using System.Web.Optimization;

namespace YoYo.Cms.Web.Bundles
{
    public class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}