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

namespace Revit_ART_ParametresPartages
{
    ////for the securite, if the userdomain isn't "ARTELIAGROUP", the command is not availability
    //public class CommandEnabler : IExternalCommandAvailability
    //{
    //    #region IExternalCommandAvailability Members

    //    public bool IsCommandAvailable(UIApplication uiApp, CategorySet catSet)
    //    {
    //        string userDomainName = System.Environment.UserDomainName;

    //        if (userDomainName == "ARTELIAGROUP" && null != uiApp.ActiveUIDocument)
    //            return true;
    //        else
    //            return false;
    //    }

    //    #endregion
    //}


    [Transaction(TransactionMode.Manual)]
    public class Application : IExternalApplication
    {
        #region Dictionary for all strings in the plugin (english/french...)
        public static Dictionary<string, Dictionary<string, string>> displayableText = new Dictionary<string, Dictionary<string, string>>
        {
            {"English", new Dictionary<string, string> {
                //B
                { "buttonDesc1", "Add shared parameters in the Revit families files" },
                { "buttonDesc2", "Replace shared parameters in the Revit families files" },
                { "buttonDesc3", "Remove shared parameters in the Revit families files" },
                { "buttonName1", "Add Shared Paramters" },
                { "buttonName2", "Replace Shared Parameters" },
                { "buttonName3", "Remove Shared Parameters" },
                { "buttonName4", "Replace Shared Parameters Group" },
                //C
                { "commandExceptionDesc", "Please load a shared parameters file.\n\n{0}" },
                { "commandExceptionTitle", "Error" },
                //D
                { "dataClassDesc", "Please load a shared parameters file in Revit\n{0}" },
                { "dataClassTitle", "Error" },
                //Form   - Buttons
                { "formButton10Text", "Add to the selection" },
                { "formButton1OPFilter", "Family file|*.rfa|All files|*.*" },
                { "formButton1OPTitle", "Choose the family file" },
                { "formButton1Text", "Browse" },
                { "formButton2Text", "Finish" },
                { "formButton3Text", "Remove" },
                { "formButton6Text", "Add and Proceed" },
                ///   - Groups
                { "formGroup1Text", "Configuration" },
                { "formGroup2Text", "Families" },
                { "formGroup3Text", "Shared Parameters" },
                { "formGroup4Text", "Operations Log" },
                ///   - Labels
                { "formLabel1Text", "Select files :" },
                { "formLabel2Text", "Select shared parameters in :" },
                { "formLabel3Text", "The following shared paramters will be configured :" },
                { "formLabel5Text", "Group parameter under :" },
                { "formLinkLabel1Text", "Connect Community Revit User Group" },
                ///   - RadioButton
                { "formRadioButton1Text", "Type" },
                { "formRadioButton2Text", "Instance" },
                ///   - T
                { "formText", "Configurator for adding shared parameters" },
                //L
                { "logWriterEndInfo", "Your performed replacements are saved in a text file : LogPPGChangements.txt. " +
                    "The file is saved in C:\\Users\\{your.login}\\AppData\\Roaming\\Autodesk\\Revit\\Addins" },
                { "logWriterEndInfoRemove", "Your performed removals are saved in a text file : LogPPGChangements.txt. " +
                    "The file is saved in C:\\Users\\{your.login}\\AppData\\Roaming\\Autodesk\\Revit\\Addins" },
                { "logWriterEndTitle", "Information" },
                { "logWriterFileName", "In the file {0} :" },
                { "logWriterHead", "Changes of {0} of the following shared parameters :" },
                { "logWriterHeadRemove", "Removals of {0} of the following shared parameters :" },
                //N
                { "newParaAlExist", "The parameter : {0} \r\nalready exists in the file : \r\n{1} \r\nIt has not been added." },
                { "newParaFormError", "----------------------------------------Error-----------------------------------------" },
                { "newParaFormErrorMsg",  "{0} \n\nTransaction not validated, please contact Atixis VN." },
                { "newParaFormErrorTitle",  "Error" },
                { "newParaFormNoSavedMsg",  "No modification has been brought, files not saved." },
                { "newParaFormSaved",  "---------------------------------------Saved---------------------------------------" },
                { "newParaFormSavedMsg",  "All files have been saved." },
                { "newParaFormSavedTitle", "Error" },
                { "newParaNoFileSelection", "There is no files selected. \r\nSelect a file." },
                { "newParaNoParamSelection", "There is no parameters selected. \r\nSelect a parameter." },
                //P
                { "pullButtonDesc", "Enable the management of shared parameters" },
                { "pullButtonName", "Shared Parameters" },
                //R   - C
                { "ribbonCategoryName", "Parameters" },
                ///   - (replace) Form
                { "rFormAddFamilyOPFilter", "Family file|*.rfa|All files|*.*" },
                { "rFormAddFamilyOPTitle", "Choose the family file" },
                ///   - Form Buttons
                { "rFormButtonAddFamilyText", "Browse" },
                { "rFormButtonEraseChoiceText", "Erase the\nselected parameters" },
                { "rFormButtonLogText", "Operations Log" },
                { "rFormButtonRemoveAllText", "Remove All" },
                { "rFormButtonRemoveText", "Remove the selection" },
                { "rFormButtonTransactionText", "Replace" },
                { "rmFormButtonTransactionText", "Remove" },
                ///   - Form Group
                { "rFormGroupFamiliesText", "Families" },
                { "rFormGroupPPGChoiceText", "Selection of the parameters to be replaced in the families." },
                { "rFormGroupPPGroupeChoiceText", "Selection of the parameters which group will be replaced." },
                { "rmFormGroupPPGChoiceText", "Selection of the parameters to be removed in the families." },
                { "rFormGroupReplaceChoiceText", "Selection of the substitute parameter (\"Replaced Parameter\" <=>\"Substitute Parameter\")" },
                { "rFormGroupReplaceGroupChoiceText", "Selection of the substitute parameter (\"Replaced Parameter\"\t|\"Existing Groupe\"<=>\"Substitute Group\")" },
                { "rmFormGroupReplaceChoiceText", "List of parameters which will be removed" },
                ///   - Form Labels
                { "rFormLabelFamilyText", "Select files :" },
                { "rFormLinkConnectText", "Connect Community Revit User Group" },
                ///   - Form Radio Buttons
                { "rFormRadioButtonAllText", "All Parameters" },
                { "rFormRadioButtonSharedText", "Shared Parameters" },
                ///   - Form Text
                { "rFormText", "Configurator for the replacement of the shared parameters of families" },
                { "rGroupFormText", "Configurator for the replacement of the group of shared parameters" },
                { "rmFormText", "Configurator for the removal of the shared parameters of families" },
                ///   - Form Update
                { "rFormUpdateCBLErrorTitle", "Error" },
                //W
                { "warning", "Warning" },
            } },
            {"French", new Dictionary<string, string> {
                //B
                { "buttonDesc1", "Ajouter les paramètres paratagés dans les fichiers familles Revit" },
                { "buttonDesc2", "Remplacer les paramètres partagés dans les fichiers familles Revit" },
                { "buttonDesc3", "Supprimer les paramètres partagés dans les fichiers familles Revit" },
                { "buttonName1", "Ajouter Paramètres Partagés" },
                { "buttonName2", "Remplacer Paramètres Partagés" },
                { "buttonName3", "Supprimer Paramètres Partagés" },
                { "buttonName4", "Remplacer Groupes Des Paramètres Partagés" },
                //C
                { "commandExceptionDesc", "Veuillez charger un fichier de paramètres partagés.\n\n{0}" },
                { "commandExceptionTitle", "Erreur" },
                //D
                { "dataClassDesc", "Veuillez charger un fichier de paramètres partagés dans Revit\n{0}" },
                { "dataClassTitle", "Erreur" },
                //Form   - Buttons
                { "formButton10Text", "Ajouter à la sélection" },
                { "formButton1OPFilter", "Fichier de famille|*.rfa|Tous les fichiers|*.*" },
                { "formButton1OPTitle", "Rechercher le fichier de famille" },
                { "formButton1Text", "Parcourir" },
                { "formButton2Text", "Terminer" },
                { "formButton3Text", "Supprimer" },
                { "formButton6Text", "Ajouter et Continuer" },
                ///   - Groups
                { "formGroup1Text", "Configuration" },
                { "formGroup2Text", "Familles" },
                { "formGroup3Text", "Paramètres partagés" },
                { "formGroup4Text", "Journal des opérations" },
                ///   - Labels
                { "formLabel1Text", "Sélectionner le(s) fichier(s) :" },
                { "formLabel2Text", "Sélectionner les paramètres partagés dans :" },
                { "formLabel3Text", "Les paramètres partagés suivants vont être configurés :" },
                { "formLabel5Text", "Regrouper le paramètre sous :" },
                { "formLinkLabel1Text", "Communauté Connect Revit User Group" },
                ///   - RadioButtons
                { "formRadioButton1Text", "Type" },
                { "formRadioButton2Text", "Occurence" },
                ///   - T
                { "formText", "Configurateur d\'ajout de paramètres partagés" },
                //L
                { "logWriterEndInfo", "Vos remplacements effectués sont enregistrés dans un fichier texte : LogPPGChangements.txt. " +
                    "Le fichier est enregistré dans C:\\Users\\{votre.login}\\AppData\\Roaming\\Autodesk\\Revit\\Addins" },
                { "logWriterEndInfoRemove", "Vos suppressions effectués sont enregistrées dans un fichier texte : LogPPGChangements.txt. " +
                    "Le fichier est enregistré dans C:\\Users\\{votre.login}\\AppData\\Roaming\\Autodesk\\Revit\\Addins" },
                { "logWriterEndTitle", "Information" },
                { "logWriterFileName", "Dans le fichier {0} :" },
                { "logWriterHead", "Modifications du {0} des paramètres partagés suivants :" },
                { "logWriterHeadRemove", "Suppressions du {0} des paramètres partagés suivants :" },
                //N
                { "newParaAlExist", "Le paramètre : {0} \r\nexiste déjà dans le fichier : \r\n{1} \r\nIl n'a pas été ajouté." },
                { "newParaFormError", "----------------------------------------Erreur-----------------------------------------" },
                { "newParaFormErrorMsg", "{0} \n\nOpération non validée, merci de contacter Atixis VN." },
                { "newParaFormErrorTitle", "Erreur" },
                { "newParaFormNoSavedMsg", "Aucune modification n'a été apportée, fichiers non enregistrés." },
                { "newParaFormNoSavedTitle", "Erreur" },
                { "newParaFormSaved",  "---------------------------------------Enregistré---------------------------------------" },
                { "newParaFormSavedMsg",  "Tous les fichiers ont été enregistrés" },
                { "newParaNoFileSelection", "Il n'y a pas de fichiers sélectionnés. \r\nSélectionnez un fichier." },
                { "newParaNoParamSelection", "Il n'y a pas de paramètres sélectionnés. \r\nSélectionnez un paramètre." },
                //P
                { "pullButtonDesc", "Permet de gérer les paramètres partagés" },
                { "pullButtonName", "Paramètres\nPartagés" },
                //R   - C
                { "ribbonCategoryName", "Paramètres" },
                ///   - (r : replace / rm : remove) Form
                { "rFormAddFamilyOPFilter", "Fichier de famille|*.rfa|Tous les fichiers|*.*" },
                { "rFormAddFamilyOPTitle", "Rechercher le fichier de famille" },
                ///   - Form Buttons
                { "rFormButtonAddFamilyText", "Parcourir" },
                { "rFormButtonEraseChoiceText", "Effacer les\nparamètres choisis" },
                { "rFormButtonLogText", "Journal des modifications" },
                { "rFormButtonRemoveAllText", "Tout Supprimer" },
                { "rFormButtonRemoveText", "Supprimer la sélection" },
                { "rFormButtonTransactionText", "Remplacer" },
                { "rmFormButtonTransactionText", "Supprimer" },
                ///   - Form Group
                { "rFormGroupFamiliesText", "Familles" },
                { "rFormGroupPPGChoiceText", "Choix des paramètres à remplacer dans les familles" },
                { "rFormGroupPPGroupeChoiceText", "Choix des paramètres qui groupe va être remplacé" },
                { "rmFormGroupPPGChoiceText", "Choix des paramètres à supprimer dans les familles" },
                { "rFormGroupReplaceChoiceText", "Choix du paramètre remplaçant (\"Paramètre Remplacé\" <=> \"Paramètre Remplaçant\")" },
                { "rFormGroupReplaceGroupChoiceText", "Choix du paramètre remplaçant (\"Paramètre Remplacé\"\t|\"Groupe Existant\"<=> \"Groupe Remplaçant\")" },
                { "rmFormGroupReplaceChoiceText", "Liste des paramètres qui vont être supprimés" },
                ///   - Form Labels
                { "rFormLabelFamilyText", "Sélectionner le(s) fichier(s) :" },
                { "rFormLinkConnectText", "Communauté Connect Revit User Group" },
                ///   - Form Radio Buttons
                { "rFormRadioButtonAllText", "Tous les paramètres" },
                { "rFormRadioButtonSharedText", "Paramètres communs" },
                ///   - Form Text
                { "rFormText", "Configurateur de remplacement des paramètres partagés de familles" },
                { "rGroupFormText", "Configurateur de remplacement des groupes des paramètres partagés" },
                { "rmFormText", "Configurateur de suppression des paramètres partagés de familles" },
                ///   - Form Update
                { "rFormUpdateCBLErrorTitle", "Erreur" },
                //W
                { "warning", "Attention" },
            } }
        };
        #endregion
        private string appLang = "English";

        public Result OnStartup(UIControlledApplication a)
        {
            switch (a.ControlledApplication.Language)
            {
                case Autodesk.Revit.ApplicationServices.LanguageType.French:
                    appLang = "French";
                    break;
                default:
                    break;
            }

            // Method to add Tab and Panel 
            RibbonPanel panel = ribbonPanel(a, displayableText[appLang]["ribbonCategoryName"]);
            // Reflection to look for this assembly path 
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            // Add button to panel 
            PushButtonData button = new PushButtonData("ButtonParametre", displayableText[appLang]["buttonName1"], thisAssemblyPath, "Revit_ART_ParametresPartages.ShareParameter");
            PushButtonData replaceButton = new PushButtonData("replaceButton", displayableText[appLang]["buttonName2"], thisAssemblyPath, "Revit_ART_ParametresPartages.ReplaceParameter");
            PushButtonData removeButton = new PushButtonData("removeButton", displayableText[appLang]["buttonName3"], thisAssemblyPath, "Revit_ART_ParametresPartages.RemoveParameter");
            PushButtonData replaceGroupButton = new PushButtonData("replaceGroupButton", displayableText[appLang]["buttonName4"], thisAssemblyPath, "Revit_ART_ParametresPartages.ReplaceGroupParameter");

            ////Add securite
            //button.AvailabilityClassName = "Revit_ART_ParametresPartages.CommandEnabler";
            //replaceButton.AvailabilityClassName = "Revit_ART_ParametresPartages.CommandEnabler";
            //removeButton.AvailabilityClassName = "Revit_ART_ParametresPartages.CommandEnabler";
            //replaceGroupButton.AvailabilityClassName = "Revit_ART_ParametresPartages.CommandEnabler";


            //Add an Image to the buttons ( in the list )
            button.LargeImage = ConvertToBitmapSource(Properties.Resources.Add);
            replaceButton.LargeImage = ConvertToBitmapSource(Properties.Resources.Replace);
            removeButton.LargeImage = ConvertToBitmapSource(Properties.Resources.Remove);
            replaceGroupButton.LargeImage = ConvertToBitmapSource(Properties.Resources.Regroup);

            //Add a tooltip to the buttons ( in the list )
            button.ToolTip = displayableText[appLang]["buttonDesc1"];
            replaceButton.ToolTip = displayableText[appLang]["buttonDesc2"];
            removeButton.ToolTip = displayableText[appLang]["buttonDesc3"];

            //Creation of the pullDownButton and add the buttons to the PulldownButton List
            PulldownButtonData pdButtonData = new PulldownButtonData(displayableText[appLang]["pullButtonName"], displayableText[appLang]["pullButtonName"]);
            PulldownButton pdButton = panel.AddItem(pdButtonData) as PulldownButton;
            pdButton.AddPushButton(button);
            pdButton.AddPushButton(replaceButton);
            pdButton.AddPushButton(removeButton);
            pdButton.AddPushButton(replaceGroupButton);
            pdButton.ToolTip = displayableText[appLang]["pullButtonDesc"];
            //help (when you press F1 on it)
            ContextualHelp contextHelp = new ContextualHelp(ContextualHelpType.Url, "https://connect.arteliagroup.com/community/user-group-bim/revit-user-group");
            button.SetContextualHelp(contextHelp);
            replaceButton.SetContextualHelp(contextHelp);
            removeButton.SetContextualHelp(contextHelp);
            pdButtonData.SetContextualHelp(contextHelp);
            //// Reflection of path to image 
            //string imagePath = Path.GetDirectoryName(thisAssemblyPath) + @"\ParametresPartages.ico";
            //Uri uriImage = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            //BitmapImage largeimage = new BitmapImage(uriImage);
            pdButton.LargeImage = ResourceImage.GetIcon("ParametresPartages.ico");

            //#region Visible of plugin
            //XmlDocument doc = new XmlDocument();
            ////加载要读取的XML
            //string path = @"%appdata%\Autodesk\Revit\Addins";
            //path = Environment.ExpandEnvironmentVariables(path);
            //string xmlPath1 = path + @"\Revit_ART_Configurateur_v2.xml";
            //doc.Load(xmlPath1);
            //XmlNode visible = doc.SelectSingleNode("/Plugins/Revit_ART_ParametresPartages");
            //IList<RibbonItem> listItem = panel.GetItems();
            //if (visible.Attributes["Mode"].Value == "no")
            //{
            //    pdButton.Visible = false;
            //    //if all of item of this panel are not visible, this panel is not visible
            //    int compte = 0;
            //    foreach (RibbonItem item in listItem)
            //    {
            //        if (item.Visible == false)
            //        {
            //            compte++;
            //        }
            //    }
            //    if (compte == listItem.Count)
            //    {
            //        panel.Visible = false;
            //    }
            //}
            //else
            //{
            //    panel.Visible = true;
            //    pdButton.Visible = true;
            //    //panel.AddSeparator();//可以不用
            //}
            //#endregion
            return Result.Succeeded;
        }

        public RibbonPanel ribbonPanel(UIControlledApplication a, string panelName)
        {
            // Tab name 
            string tab = "Atixis VN";

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
