using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;


namespace Revit_ART_ParametresPartages
{
    public class NewPara : IExternalEventHandler
    {
        public MainForm disForm;
        private string appLang = "English";

        public NewPara(MainForm mainForm)
        {
            disForm = mainForm;
        }
        public void Execute(UIApplication app)
        {
            switch (app.Application.Language)
            {
                case Autodesk.Revit.ApplicationServices.LanguageType.French:
                    appLang = "French";
                    break;
                default:
                    break;
            }
            try
            {
                Autodesk.Revit.ApplicationServices.Application revitApp = app.Application;

                if (disForm.listFile.Count != 0)
                {
                    if (disForm.nomTypeParaDic.Count != 0)
                    {
                        
                        for (int i = 0; i < disForm.listFile.Count; i++)
                        {
                            string inPath = disForm.listFile[i].ToString();

                            //get the file and open the file in the disc
                            Document familyDoc = revitApp.OpenDocumentFile(inPath);//获取族文档

                            Transaction ts = new Transaction(familyDoc, "ajoutePara");
                            ts.Start();

                            //get the manager of family in the file
                            FamilyManager m_familyMgr = familyDoc.FamilyManager;//获取族管理器

                            //get all of parameters in the file
                            IList<FamilyParameter> faparms = m_familyMgr.GetParameters();

                            //get the type of "regrouper"
                            BuiltInParameterGroup builtIn = disForm.BuiltInParameterGroup;

                            //type or occurence
                            bool isInstance = false;

                            if (disForm.paraType == Application.displayableText[appLang]["formRadioButton1Text"])
                            {
                                isInstance = false;
                            }

                            if (disForm.paraType == Application.displayableText[appLang]["formRadioButton2Text"])
                            {
                                isInstance = true;
                            }

                            //for the information of operating, add the name of file in the list of result
                            disForm.box.Items.Add(disForm.listFileName[i]);

                            //for all of parameters who have been selected
                            foreach (string key in disForm.nomTypeParaDic.Keys)
                            {
                                bool flag = false;

                                //judge whether the name of parameter has been existed
                                foreach (FamilyParameter parameter in faparms)
                                {
                                    string paraName = parameter.Definition.Name.ToString();

                                    if (paraName == key)
                                    {
                                        flag = true;
                                        string errerMsg1 = string.Format(Application.displayableText[appLang]["newParaAlExist"], key, disForm.listFileName[i]);
                                        MessageBox.Show(errerMsg1); 
                                    }

                                }

                                //if not, add this parameter
                                if (!flag)
                                {
                                    DefinitionGroup myGroup = disForm.definitionGroups.get_Item(disForm.groupName);
                                    ExternalDefinition myExtDef = myGroup.Definitions.get_Item(key) as ExternalDefinition;
                                    FamilyParameter para = m_familyMgr.AddParameter(myExtDef, builtIn, isInstance);
                                    //FamilyParameter param = m_familyMgr.AddParameter(key, builtIn, disForm.nomTypeParaDic[key], isInstance);

                                    //for judging whether have added the parameter
                                    disForm.compte = true;//判断是否有添加参数

                                    disForm.box.Items.Add(key);

                                    //a list of all of files who have been added the parameter
                                    disForm.listSave.Add(disForm.listFile[i]);//归总成功加入参数的文件的文件路径
                                }
                                flag = false;

                            }
                            disForm.box.Items.Add("---------------------------------------------------------------------------------------------");

                            ts.Commit();
                        }

                        //distinct the same name in the list, and delete it
                        //优化：去重 需要保存的文件，即所有成功加入参数的文件
                        disForm.listSave = disForm.listSave.Distinct().ToList();
                    }

                    //if there is not parameter be selected
                    //如果没有选择需要添加的参数
                    else
                    {
                        string errerMsg2 = string.Format(Application.displayableText[appLang]["newParaNoParamSelection"]);
                        MessageBox.Show(errerMsg2);
                    }

                }

                //if there is not file in the list
                //如果文件列表中没有待添加的文件
                else
                {
                    string errerMsg3 = string.Format(Application.displayableText[appLang]["newParaNoFileSelection"]);
                    MessageBox.Show(errerMsg3);
                }
            }

            //for catching the errers
            //for example, when the type of parameter is "falilyType", it will maybe can't be added
            //捕捉错误，比如当类型为familyType的时候报错
            catch (Exception e)
            {
                disForm.box.Items.Add(Application.displayableText[appLang]["newParaFormError"]);
                string errerMsg4 = string.Format(Application.displayableText[appLang]["newParaFormErrorMsg"], e.Message);
                MessageBox.Show(errerMsg4, Application.displayableText[appLang]["newParaFormErrorTitle"]);
            }

        }

        public string GetName()
        {
            return "NewPara";
        }
    }


    //for saving files
    public class Save : IExternalEventHandler
    {
        public MainForm disForm;
        public List<String> listInform;
        private string appLang = "English";

        public Save(MainForm mainForm)
        {
            disForm = mainForm;
        }
        public void Execute(UIApplication app)
        {
            switch (app.Application.Language)
            {
                case Autodesk.Revit.ApplicationServices.LanguageType.French:
                    appLang = "French";
                    break;
                default:
                    break;
            }
            try
            {
                //judge whether there is change in the file
                if (disForm.compte == true)//首先判断是否有进行参数添加
                {
                    Autodesk.Revit.ApplicationServices.Application revitApp = app.Application;

                    for (int i = 0; i < disForm.listSave.Count; i++)
                    {
                        string inPath = disForm.listSave[i].ToString();

                        Document familyDoc1 = revitApp.OpenDocumentFile(inPath);

                        //close and save files
                        familyDoc1.Close();

                    }

                    disForm.compte = false;//减少程序运行。避免在重复按保存按钮时，进行重复保存

                    disForm.listSave.Clear();//把已经保存过的文件清零，避免重复保存占用进程

                    disForm.box.Items.Add(Application.displayableText[appLang]["newParaFormSaved"]);
                    MessageBox.Show(Application.displayableText[appLang]["newParaFormSavedMsg"]);
                }

                else
                {
                    MessageBox.Show(Application.displayableText[appLang]["newParaFormNoSavedMsg"]);
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.displayableText[appLang]["newParaFormSavedTitle"]);
            }
        }

        public string GetName()
        {
            return "test";
        }
    }
}
