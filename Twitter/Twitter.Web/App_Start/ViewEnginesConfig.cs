using System.Web.Mvc;

namespace Twitter.Web.App_Start
{
    public static class ViewEnginesConfig
    {
        public static void RegisterViewEngines (ViewEngineCollection viewEngines)
        {
            viewEngines.Clear();
            viewEngines.Add(new RazorViewEngine());
        }
    }
}