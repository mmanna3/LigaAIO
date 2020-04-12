using System.Web.Optimization;

namespace LigaSoft
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			#region Generales

			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/corejs").Include(
						"~/Scripts/select2.full.js",                //Para combos de selección
						"~/Scripts/bootstrap.js",					
						"~/Scripts/bootstrap3-typeahead.min.js",    //Para autocomplete
						"~/Scripts/jquery.blockUI.js",
						"~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
						"~/Content/bootstrap.css",
						"~/Content/select2/select2.min.css",
						"~/Content/select2/select2-bootstrap.min.css",
						"~/Content/layout.css"));

			bundles.Add(new StyleBundle("~/Content/public_css").Include(
				"~/Content/bootstrap.css",
				"~/Content/public_layout.css"));
			#endregion

			#region FontAwesome

			bundles.Add(new ScriptBundle("~/bundles/fontAwesome").Include(
				"~/Scripts/fontAwesome.js"));

			#endregion

			#region Gijgo
			bundles.Add(new ScriptBundle("~/gijgojs").Include(
				"~/Scripts/gijgo/modular/core.min.js",
				"~/Scripts/gijgo/modular/checkbox.min.js",
				"~/Scripts/gijgo/modular/grid.min.js",
				"~/Scripts/gijgo/modular/datepicker.min.js"));

			bundles.Add(new StyleBundle("~/gijgocss").Include(
				"~/Content/gijgo/modular/core.min.css",
				"~/Content/gijgo/modular/checkbox.min.css",
				"~/Content/gijgo/modular/grid.min.css",
				"~/Content/gijgo/modular/datepicker.min.css"));

			#endregion

			#region jpeg_camera

			bundles.Add(new ScriptBundle("~/jpgcamerajs").Include(
				"~/Scripts/jpeg_camera/canvas-to-blob.min.js",
				"~/Scripts/jpeg_camera/jpeg_camera_no_flash.min.js"));

			#endregion

			#region knockout

			bundles.Add(new ScriptBundle("~/knockoutjs").Include(
				"~/Scripts/knockout/knockout-min.js",
				"~/Scripts/knockout/knockout.validation.min.js"));

			#endregion
		}
	}
}
