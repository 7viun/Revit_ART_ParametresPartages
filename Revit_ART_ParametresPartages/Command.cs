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

namespace Revit_ART_ParametresPartages
{
    [Transaction(TransactionMode.Manual)]
    public class ReplaceParameter : IExternalCommand
    {
        private string appLang = "English";

        #region ReplaceParameter Button ( when you press it )
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Autodesk.Revit.ApplicationServices.Application revitApp = commandData.Application.ActiveUIDocument.Application.Application;
            Document doc = commandData.Application.ActiveUIDocument.Document;

            switch (revitApp.Language)
            {
                case LanguageType.French:
                    appLang = "French";
                    break;
                default:
                    break;
            }

            try
            {
                //prepare data
                DataClass DataClass = new DataClass(revitApp);
                //Prepare and show the form
                ReplacePPGForm form = new ReplacePPGForm(DataClass, commandData);
                form.Show();
                
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                string error = string.Format(Application.displayableText[appLang]["commandExceptionDesc"], e.Message);
                MessageBox.Show(error, Application.displayableText[appLang]["commandExceptionTitle"]);
                return Result.Failed;
            }
        }
        #endregion
    }
    #region Transaction Button : External Event
    [Transaction(TransactionMode.Manual)]
    public class ReplaceCommand : IExternalEventHandler
    {
        private string appLang = "English";
        ReplacePPGForm disReplace;
        DataClass dataC;
        ExternalCommandData command;
        public ReplaceCommand(ReplacePPGForm replace, DataClass dataClass, ExternalCommandData commandData)
        {
            //When creating the External Event, get the different resources needed by the Exteral Event Method, so that we can use their informations
            disReplace = replace;
            dataC = dataClass;
            command = commandData;
        }
        //Execute the event when the hander.Raise() is hit;
        public void Execute(UIApplication app)
        {
            try
            {
                //Create or open the log file in order to write in
                StreamWriter log = new StreamWriter(@"C:\Users\" + System.Environment.UserName + @"\AppData\Roaming\Autodesk\Revit\Addins\LogPPGChangements.txt", true);
                Autodesk.Revit.ApplicationServices.Application revitApp = app.Application;
                bool locked = false;
                switch (app.Application.Language)
                {
                    case LanguageType.French:
                        appLang = "French";
                        break;
                    default:
                        break;
                }
                if (log != null)
                {
                    log.WriteLine(string.Format(Application.displayableText[appLang]["logWriterHead"], DateTime.Now.ToString())); // the first line in each transaction in log file
                }
                foreach (string filename in disReplace.listFile)
                {
                    Document familyDoc = revitApp.OpenDocumentFile(filename);
                    FamilyManager m_familyMgr = familyDoc.FamilyManager;
                    IList<FamilyParameter> paramList = m_familyMgr.GetParameters();

                    log.WriteLine(string.Format(Application.displayableText[appLang]["logWriterFileName"], filename));

                    for (int i = 0; i < disReplace.paramToReplace.Count; i++) //For each paramToReplace and his paramWhichReplace, do the transaction
                    {
                        if (disReplace.paramWhichReplace[i] != "/") // if there is a param that replace the one to replace
                        {
                            foreach (FamilyParameter fparam in paramList) // We start our seek. We're looking, here, for the right family parameter to replace
                            {
                                if (fparam.Definition.Name == disReplace.paramToReplace[i])
                                {
                                    foreach (DefinitionGroup group in disReplace.m_class.defGroupe) //if its the right one found, look for the right definition parameter
                                    {
                                        foreach (Definition definition in group.Definitions)
                                        {
                                            if (definition.Name == disReplace.paramWhichReplace[i].Remove(0, disReplace.paramWhichReplace[i].IndexOf('|') + 2)) // retrieve from paramWhichReplace, the parameter group in order to have the good parameter name
                                            {
                                                Transaction ts = new Transaction(familyDoc, "Remplacement de Paramètres"); // our transaction is to the family document, so familyDoc is used at each instance
                                                ts.Start();
                                                if (m_familyMgr.IsParameterLockable(fparam) && m_familyMgr.IsParameterLocked(fparam))
                                                {
                                                    m_familyMgr.SetParameterLocked(fparam, false);
                                                    locked = true;
                                                }
                                                FamilyParameter replace = m_familyMgr.ReplaceParameter(fparam, definition as ExternalDefinition, fparam.Definition.ParameterGroup, fparam.IsInstance);
                                                if (m_familyMgr.IsParameterLockable(replace) && locked)
                                                {
                                                    m_familyMgr.SetParameterLocked(replace, true);
                                                }
                                                ts.Commit();

                                                if (log != null)
                                                {
                                                    log.WriteLine(disReplace.paramToReplace[i] + " <=> " + disReplace.paramWhichReplace[i]); // write the Transaction
                                                }
                                                locked = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    log.WriteLine("");
                    familyDoc.Save(); // save and close the document when the transaction is finished
                    familyDoc.Close();
                }
                if (log != null)
                {
                    log.WriteLine("====================");
                    log.WriteLine("");
                    log.Close();
                }
                MessageBox.Show(Application.displayableText[appLang]["logWriterEndInfo"], Application.displayableText[appLang]["logWriterEndTitle"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                disReplace.Focus();
                disReplace.Close();
                //recreate the form in order to not close it and let the user doing more changes
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
    #endregion
    [Transaction(TransactionMode.Manual)]
    public class RemoveParameter : IExternalCommand
    {
        private string appLang = "English";

        #region RemoveParameter Button ( when you press it )
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Autodesk.Revit.ApplicationServices.Application revitApp = commandData.Application.ActiveUIDocument.Application.Application;
            Document doc = commandData.Application.ActiveUIDocument.Document;

            switch (revitApp.Language)
            {
                case LanguageType.French:
                    appLang = "French";
                    break;
                default:
                    break;
            }

            try
            {
                //prepare data
                DataClass DataClass = new DataClass(revitApp);
                //Prepare and show the form
                RemovePPGForm form = new RemovePPGForm(DataClass, commandData);
                form.Show();

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                string error = string.Format(Application.displayableText[appLang]["commandExceptionDesc"], e.Message);
                MessageBox.Show(error, Application.displayableText[appLang]["commandExceptionTitle"]);
                return Result.Failed;
            }
        }
        #endregion
    }
    #region Transaction Button : External Event
    [Transaction(TransactionMode.Manual)]
    public class RemoveCommand : IExternalEventHandler
    {
        private string appLang = "English";
        RemovePPGForm disRemove;
        DataClass dataC;
        ExternalCommandData command;
        public RemoveCommand(RemovePPGForm remove, DataClass dataClass, ExternalCommandData commandData)
        {
            //When creating the External Event, get the different resources needed by the Exteral Event Method, so that we can use their informations
            disRemove = remove;
            dataC = dataClass;
            command = commandData;
        }
        //Execute the event when the hander.Raise() is hit;
        public void Execute(UIApplication app)
        {
            try
            {
                //Create or open the log file in order to write in
                StreamWriter log = new StreamWriter(@"C:\Users\" + System.Environment.UserName + @"\AppData\Roaming\Autodesk\Revit\Addins\LogPPGChangements.txt", true);
                Autodesk.Revit.ApplicationServices.Application revitApp = app.Application;
                switch (app.Application.Language)
                {
                    case LanguageType.French:
                        appLang = "French";
                        break;
                    default:
                        break;
                }
                if (log != null)
                {
                    log.WriteLine(string.Format(Application.displayableText[appLang]["logWriterHeadRemove"], DateTime.Now.ToString())); // the first line in each transaction in log file
                }
                foreach (string filename in disRemove.listFile)
                {
                    Document familyDoc = revitApp.OpenDocumentFile(filename);
                    FamilyManager m_familyMgr = familyDoc.FamilyManager;
                    IList<FamilyParameter> paramList = m_familyMgr.GetParameters();

                    log.WriteLine(string.Format(Application.displayableText[appLang]["logWriterFileName"], filename));

                    for (int i = 0; i < disRemove.paramToRemove.Count; i++) //For each paramToReplace and his paramWhichReplace, do the transaction
                    {
                        paramList = m_familyMgr.GetParameters();
                        foreach (FamilyParameter fparam in paramList) // We start our seek. We're looking, here, for the right family parameter to replace
                        {
                            if (fparam.Definition.Name == disRemove.paramToRemove[i])
                            {
                                Transaction ts = new Transaction(familyDoc, "Suppression de Paramètres"); // our transaction is to the family document, so familyDoc is used at each instance
                                ts.Start();
                                if (m_familyMgr.IsParameterLockable(fparam) && m_familyMgr.IsParameterLocked(fparam))
                                {
                                    m_familyMgr.SetParameterLocked(fparam, false);
                                }
                                m_familyMgr.RemoveParameter(fparam);
                                ts.Commit();

                                if (log != null)
                                {
                                    log.WriteLine(disRemove.paramToRemove[i]); // write the Transaction
                                }
                                break;
                            }
                        }
                    }
                    log.WriteLine("");
                    familyDoc.Save(); // save and close the document when the transaction is finished
                    familyDoc.Close();
                }
                if (log != null)
                {
                    log.WriteLine("====================");
                    log.WriteLine("");
                    log.Close();
                }
                MessageBox.Show(Application.displayableText[appLang]["logWriterEndInfoRemove"], Application.displayableText[appLang]["logWriterEndTitle"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                disRemove.Focus();
                disRemove.Close();
                //recreate the form in order to not close it and let the user doing more changes
                RemovePPGForm form = new RemovePPGForm(dataC, command);
                form.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public string GetName()
        {
            return "RemoveCommand";
        }
    }
    #endregion

    #region Transaction Button : External Event
    [Transaction(TransactionMode.Manual)]
    public class ReplaceGroupCommand : IExternalEventHandler
    {
        private string appLang = "English";
        ReplacePPGroupeForm disReplace;
        DataClass dataC;
        ExternalCommandData command;
        public ReplaceGroupCommand(ReplacePPGroupeForm replace, DataClass dataClass, ExternalCommandData commandData)
        {
            //When creating the External Event, get the different resources needed by the Exteral Event Method, so that we can use their informations
            disReplace = replace;
            dataC = dataClass;
            command = commandData;
        }
        //Execute the event when the hander.Raise() is hit;
        public void Execute(UIApplication app)
        {
            try
            {
                //Create or open the log file in order to write in
                StreamWriter log = new StreamWriter(@"C:\Users\" + System.Environment.UserName + @"\AppData\Roaming\Autodesk\Revit\Addins\LogPPGChangements.txt", true);
                Autodesk.Revit.ApplicationServices.Application revitApp = app.Application;
                bool locked = false;
                string dummyname = "ArteliaDummyParameter";
                switch (app.Application.Language)
                {
                    case LanguageType.French:
                        appLang = "French";
                        break;
                    default:
                        break;
                }
                if (log != null)
                {
                    log.WriteLine(string.Format(Application.displayableText[appLang]["logWriterHead"], DateTime.Now.ToString())); // the first line in each transaction in log file
                }
                foreach (string filename in disReplace.listFile)
                {
                    Document familyDoc = revitApp.OpenDocumentFile(filename);
                    UIDocument uidoc = new UIDocument(familyDoc);
                    DefinitionFile defFile = uidoc.Application.Application.OpenSharedParameterFile();
                    var myGroups = defFile.Groups;
                    FamilyManager m_familyMgr = familyDoc.FamilyManager;
                    IList<FamilyParameter> paramList = m_familyMgr.GetParameters();
                    var paramNameList = paramList.Cast<FamilyParameter>().Select(x => x.Definition.Name).ToList();
                    log.WriteLine(string.Format(Application.displayableText[appLang]["logWriterFileName"], filename));
                    int i = 0;
                    foreach (string parameterName in disReplace.paramToReplace)
                    {
                        ExternalDefinition myExtDef = (ExternalDefinition)GetSharedParameterGroup(defFile, parameterName).Definitions.get_Item(parameterName);
                        if (paramNameList.Contains(dummyname)) { dummyname = dummyname + i.ToString(); }
                        if (paramNameList.Contains(parameterName))
                        {
                            var parameter = paramList[paramNameList.IndexOf(parameterName)];
                            Transaction ts = new Transaction(familyDoc, "Replace Shared Parameter width Dummy Parameter"); // our transaction is to the family document, so familyDoc is used at each instance
                            ts.Start();
                            if (m_familyMgr.IsParameterLockable(parameter) && m_familyMgr.IsParameterLocked(parameter))
                            {
                                m_familyMgr.SetParameterLocked(parameter, false);
                                locked = true;
                            }
                            FamilyParameter replace = m_familyMgr.ReplaceParameter(parameter, dummyname, BuiltInParameterGroup.INVALID, parameter.IsInstance);
                            if (m_familyMgr.IsParameterLockable(replace) && locked)
                            {
                                m_familyMgr.SetParameterLocked(replace, true);
                            }
                            ts.Commit();
                            familyDoc.Save();
                            locked = false;
                        }
                        ///Refresh List of Parameters
                        var m_familyMgrRefreshed = familyDoc.FamilyManager;
                        IList<FamilyParameter> paramListRefreshed = m_familyMgrRefreshed.GetParameters();
                        var paramNameListRefreshed = paramListRefreshed.Cast<FamilyParameter>().Select(x => x.Definition.Name).ToList();
                        ///Now we switch to the old parameter
                        if(paramNameListRefreshed.Contains(dummyname))
                        {
                            var parameterReal = paramListRefreshed[paramNameListRefreshed.IndexOf(dummyname)];
                            ///Replace Dummy Parameter with The Real One
                            Transaction ts = new Transaction(familyDoc, "Replace Dummy Parameter with The Real One"); // our transaction is to the family document, so familyDoc is used at each instance
                            ts.Start();
                            if (m_familyMgrRefreshed.IsParameterLockable(parameterReal) && m_familyMgr.IsParameterLocked(parameterReal))
                            {
                                m_familyMgrRefreshed.SetParameterLocked(parameterReal, false);
                                locked = true;
                            }
                            FamilyParameter replace = m_familyMgr.ReplaceParameter(parameterReal, myExtDef, disReplace.builtParaGroupDic[disReplace.paramWhichReplace[i]], parameterReal.IsInstance);
                            if (m_familyMgrRefreshed.IsParameterLockable(replace) && locked)
                            {
                                m_familyMgrRefreshed.SetParameterLocked(replace, true);
                            }
                            ts.Commit();
                            familyDoc.Save();
                            if (log != null)
                            {
                                log.WriteLine(disReplace.paramToReplace[i] + " <=> " + disReplace.paramWhichReplace[i]); // write the Transaction
                            }
                            locked = false;
                        }
                        i++;
                    }
                    log.WriteLine("");
                    familyDoc.Close();
                }
                if (log != null)
                {
                    log.WriteLine("====================");
                    log.WriteLine("");
                    log.Close();
                }
                MessageBox.Show(Application.displayableText[appLang]["logWriterEndInfo"], Application.displayableText[appLang]["logWriterEndTitle"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                disReplace.Focus();
                disReplace.Close();
                //recreate the form in order to not close it and let the user doing more changes
                ReplacePPGroupeForm form = new ReplacePPGroupeForm(dataC, command);
                form.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public string GetName()
        {
            return "ReplaceGroupCommand";
        }
        private static DefinitionGroup GetSharedParameterGroup(DefinitionFile definitionFile,string parameterName)
        {
            bool _executed = false;
            DefinitionGroup FParamGroup = null;
            using (IEnumerator<DefinitionGroup> enumerator = definitionFile.Groups.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    DefinitionGroup current = enumerator.Current;
                    IEnumerator<Definition> definitionEnumerator = current.Definitions.GetEnumerator();
                    while (definitionEnumerator.MoveNext())
                    {
                        Definition definition = definitionEnumerator.Current;
                        if (definition.Name==parameterName)
                        {
                            FParamGroup = current;
                            _executed = true;
                            break;
                        }

                    }
                    if (_executed == true) { break; }
                }
            }
            return FParamGroup;
        }
    }
    #endregion
    [Transaction(TransactionMode.Manual)]
    public class ReplaceGroupParameter : IExternalCommand
    {
        private string appLang = "English";

        #region RemoveParameter Button ( when you press it )
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Autodesk.Revit.ApplicationServices.Application revitApp = commandData.Application.ActiveUIDocument.Application.Application;
            Document doc = commandData.Application.ActiveUIDocument.Document;

            switch (revitApp.Language)
            {
                case LanguageType.French:
                    appLang = "French";
                    break;
                default:
                    break;
            }

            try
            {
                //prepare data
                DataClass DataClass = new DataClass(revitApp);
                //Prepare and show the form
                ReplacePPGroupeForm form = new ReplacePPGroupeForm(DataClass, commandData);
                form.Show();

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                string error = string.Format(Application.displayableText[appLang]["commandExceptionDesc"], e.Message);
                MessageBox.Show(error, Application.displayableText[appLang]["commandExceptionTitle"]);
                return Result.Failed;
            }
        }
        #endregion
    }
}
