using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Revit_ART_RemplacerPPGFamilles
{
    //for the securite, if the userdomain isn't "ARTELIAGROUP", the command is not availability

    public class CommandEnabler : IExternalCommandAvailability
    {
        #region IExternalCommandAvailability Members

        public bool IsCommandAvailable(UIApplication uiApp, CategorySet catSet)
        {
            string userDomainName = System.Environment.UserDomainName;

            if (userDomainName == "ARTELIAGROUP" && null != uiApp.ActiveUIDocument)
                return true;
            else
                return false;
        }

        #endregion
    }
    [Transaction(TransactionMode.Manual)]
    public class Application : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            // Method to add Tab and Panel 
            RibbonPanel panel = ribbonPanel(a, "Coordination BIM");
            // Reflection to look for this assembly path 
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            // Add button to panel 
            PushButton replaceButton = panel.AddItem(new PushButtonData("replaceButton", "Remplacer PPG\nFamilles", thisAssemblyPath, "Revit_ART_RemplacerPPGFamilles.ReplaceParameter")) as PushButton;
            //Add securite
            replaceButton.AvailabilityClassName = "Revit_ART_RemplacerPPGFamilles.CommandEnabler";
            replaceButton.ToolTip = "Ajouter les paramètre paratagés dans les fichiers familles";
            // Reflection of path to image 
            //replaceButton.LargeImage = ;
            //help
            ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url, "https://connect.arteliagroup.com/community/user-group-bim/revit-user-group");
            replaceButton.SetContextualHelp(contextHelp);


            #region Visible of plugin
            XmlDocument doc = new XmlDocument();
            //加载要读取的XML
            string path = @"%appdata%\Autodesk\Revit\Addins";
            path = Environment.ExpandEnvironmentVariables(path);
            string xmlPath1 = path + @"\Revit_ART_Configurateur.xml";
            doc.Load(xmlPath1);
            XmlNode visible = doc.SelectSingleNode("/Plugins/Revit_ART_RemplacerPPGFamilles");
            IList<RibbonItem> listItem = panel.GetItems();
            if (visible.Attributes["Mode"].Value == "no")
            {
                replaceButton.Visible = false;
                //if all of item of this panel are not visible, this panel is not visible
                int compte = 0;
                foreach (RibbonItem item in listItem)
                {
                    if (item.Visible == false)
                    {
                        compte++;
                    }
                }
                if (compte == listItem.Count)
                {
                    panel.Visible = false;
                }
            }
            else
            {
                panel.Visible = true;
                replaceButton.Visible = true;
                //panel.AddSeparator();//可以不用
            }
            #endregion

            return Result.Succeeded;
        }
        #region ribbonPanel method
        public RibbonPanel ribbonPanel(UIControlledApplication a, string panelName)
        {
            // Tab name 
            string tab = "Artelia";

            // Empty ribbon panel 
            RibbonPanel ribbonPanel = null;
            // Try to create ribbon tab. 
            try
            {
                //a.CreateRibbonPanel("My Test Tools");
                a.CreateRibbonTab(tab);
            }
            catch { }
            // Try to create ribbon panel. 
            try
            {
                RibbonPanel panel = a.CreateRibbonPanel(tab, panelName);
            }
            catch { }
            // Search existing tab for your panel. 
            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels)
            {
                if (p.Name == panelName)
                {
                    ribbonPanel = p;
                }
            }
            //return panel 
            return ribbonPanel;
        }
        #endregion
        #region ConvertToBitmapSource method
        private BitmapSource ConvertToBitmapSource(Bitmap bitmap)
        {
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap
            (
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions()
            );
            return bitmapSource;
        }
        #endregion
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
