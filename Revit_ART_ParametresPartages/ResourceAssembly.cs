using System.Reflection;


namespace Revit_ART_ParametresPartages
{
    public static class ResourceAssembly
    {

        #region public methods

        /// <summary>
        /// Gets the current resource assembly.
        /// </summary>
        /// <returns></returns>
        public static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        /// <summary>
        /// Gets the namespace of the currently running resource assembly.
        /// </summary>
        /// <returns></returns>
        public static string GetNamespace()
        {
            return typeof(ResourceAssembly).Namespace + ".";
        }

        #endregion
    }
}
