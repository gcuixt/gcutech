using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace gcutech.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/metro-all.min.css",
                "~/Content/metro-animation.min.css",
                "~/Content/metro-colors.min.css",
                "~/Content/metro-icons.min.css",
                "~/Content/metro-rtl.min.css",
                "~/Content/metro-third.min.css",
                "~/Content/metro.min.css",
                "~/Content/muse.css,")
                .IncludeDirectory("~/Content", ".css"));

            bundles.Add(new ScriptBundle("~/Scripts").Include(
                "~/Scripts/metro.min.js"));
        }
    }
}