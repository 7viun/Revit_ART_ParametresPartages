using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WinForm = System.Windows.Forms;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI.Selection;
using RevitApp = Autodesk.Revit.ApplicationServices;
using System.Windows.Forms;

namespace Revit_ART_ParametresPartages
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class ShareParameter : IExternalCommand
    {
        private string appLang = "English";
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Autodesk.Revit.ApplicationServices.Application revitApp = commandData.Application.ActiveUIDocument.Application.Application;
            Autodesk.Revit.DB.Document revirDoc = commandData.Application.ActiveUIDocument.Document;

            switch (revitApp.Language)
            {
                case RevitApp.LanguageType.French:
                    appLang = "French";
                    break;
                default:
                    break;
            }

            try
            {
                //prepare data
                DataClass DataClass = new DataClass(revitApp);
                MainForm displayForm = new MainForm(DataClass, revitApp);
                displayForm.Show();

                return Autodesk.Revit.UI.Result.Succeeded;
            }
            catch (Exception e)
            {
                //message = e.Message;
                string errerMsg = string.Format(Application.displayableText[appLang]["commandExceptionDesc"], e.Message);
                MessageBox.Show(errerMsg, Application.displayableText[appLang]["commandExceptionTitle"]);
                return Autodesk.Revit.UI.Result.Failed;
            }
        }
    }
}
