using System;
using System.Windows.Media.Imaging;
using System.Reflection;


namespace Revit_ART_ParametresPartages
{
    public static class ResourceImage
    {
        #region public methods

        /// <summary>
        /// Gets the icon image from reource assembly.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static BitmapImage GetIcon(string name)
        {
            // Create the resource reader stream.
            var stream = ResourceAssembly.GetAssembly().GetManifestResourceStream(ResourceAssembly.GetNamespace() + "Images." + name);

            var image = new BitmapImage();

            // Construct and return image.
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();

            // Return constructed BitmapImage.
            return image;
        }
        public static BitmapImage CreateBitmapImage(string uri)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(uri, UriKind.RelativeOrAbsolute);
            image.EndInit();
            return image;
        }

        #endregion
    }
}
