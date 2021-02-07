using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using System.IO.Compression;
using System.IO;

namespace Revit_ART_RemplacerPPGFamilles
{
    [Transaction(TransactionMode.Manual)]
    public class ReplaceParameter : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Autodesk.Revit.ApplicationServices.Application revitApp = commandData.Application.ActiveUIDocument.Application.Application;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            try
            {
                //prepare data
                
                DataClass DataClass = new DataClass(revitApp);
                ReplacePPGForm form = new ReplacePPGForm(DataClass, commandData);
                form.Show();
                
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                string error = string.Format("Veuillez charger un fichier de paramètres partagés.\n\n{0}", e.Message);
                MessageBox.Show(error, "Erreur");
                return Result.Failed;
            }
        }
    }
    [Transaction(TransactionMode.Manual)]
    public class ReplaceCommand : IExternalEventHandler
    {
        ReplacePPGForm disReplace;
        DataClass dataC;
        ExternalCommandData command;
        public ReplaceCommand(ReplacePPGForm replace, DataClass dataClass, ExternalCommandData commandData)
        {
            disReplace = replace;
            dataC = dataClass;
            command = commandData;
        }
        public void Execute(UIApplication app)
        {
            try
            {
                StreamWriter log = new StreamWriter(@"C:\Users\" + System.Environment.UserName + @"\AppData\Roaming\Autodesk\Revit\Addins\LogPPGChangements.txt", true);
                Autodesk.Revit.ApplicationServices.Application revitApp = app.Application;
                if (log != null)
                {
                    log.WriteLine("Modifications du " + DateTime.Now.ToString() + " des paramètres partagés suivants :");
                }
                foreach (string filename in disReplace.listFile)
                {
                    Document familyDoc = revitApp.OpenDocumentFile(filename);
                    FamilyManager m_familyMgr = familyDoc.FamilyManager;
                    IList<FamilyParameter> paramList = m_familyMgr.GetParameters();

                    log.WriteLine("Dans le fichier " + filename + " :");

                    for (int i = 0; i < disReplace.paramToReplace.Count; i++)
                    {
                        if (disReplace.paramWhichReplace[i] != "/")
                        {
                            foreach (FamilyParameter fparam in paramList)
                            {
                                if (fparam.Definition.Name == disReplace.paramToReplace[i])
                                {
                                    foreach (DefinitionGroup group in disReplace.m_class.defGroupe)
                                    {
                                        foreach (Definition definition in group.Definitions)
                                        {
                                            if (definition.Name == disReplace.paramWhichReplace[i].Remove(0, disReplace.paramWhichReplace[i].IndexOf('|') + 2))
                                            {
                                                Transaction ts = new Transaction(familyDoc, "Remplacement de Paramètres");
                                                ts.Start();
                                                FamilyParameter replace = m_familyMgr.ReplaceParameter(fparam, definition as ExternalDefinition, fparam.Definition.ParameterGroup, fparam.IsInstance);
                                                ts.Commit();
                                                
                                                if (log != null)
                                                {
                                                    log.WriteLine(disReplace.paramToReplace[i] + " <=> " + disReplace.paramWhichReplace[i]);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    log.WriteLine("");
                    familyDoc.Save();
                    familyDoc.Close();
                }
                if (log != null)
                {
                    log.WriteLine("====================");
                    log.WriteLine("");
                    log.Close();
                }
                MessageBox.Show("Vos remplacements effectués seront enregistrés dans un fichier texte : LogPPGChangements.txt. Le fichier est enregistré dans C:\\Users\\{votre.login}\\AppData\\Roaming\\Autodesk\\Revit\\Addins", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                disReplace.Focus();
                disReplace.Close();
                ReplacePPGForm form = new ReplacePPGForm(dataC, command);
                form.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public string GetName()
        {
            return "ReplaceCommand";
        }
    }
}
