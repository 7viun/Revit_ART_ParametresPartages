using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace Revit_ART_ParametresPartages
{
    public partial class RemovePPGForm : System.Windows.Forms.Form
    {
        public ExternalCommandData command;

        RemoveCommand myCommand = null;
        ExternalEvent hander = null;

        public DataClass m_class;
        //selectI = int for comboBox Selection and in order to avoid some infinte rounds
        public Dictionary<string, FamilyParameter> pUnique;
        public Dictionary<string, FamilyParameter> pInCommon;
        public Dictionary<string, FamilyParameter> pToKeep;

        public List<string> paramToRemove;

        private string appLang = "English";

        public RemovePPGForm(DataClass dataClass, ExternalCommandData commandData)
        {
            switch (commandData.Application.Application.Language)
            {
                case Autodesk.Revit.ApplicationServices.LanguageType.French:
                    appLang = "French";
                    break;
                default:
                    break;
            }
            InitializeComponent();
            command = commandData;
            m_class = dataClass;
            m_class.strsGroupe = dataClass.strsGroupe;
            m_class.defGroupe = dataClass.defGroupe;
            //for the name of group and name of parameter ande type of parameter
            m_class.dicDic = dataClass.dicDic;//为了整合组别名和参数名以及参数类型
            //for the adresse of file
            m_class.strsFile = new List<string>();//为了返回文件路径，后台打开文件
            //fot the name of file
            m_class.strsFileName = new List<string>();//为了返回文件名，提示作用
            //transaction event initialization
            myCommand = new RemoveCommand(this, dataClass, command);
            hander = ExternalEvent.Create(myCommand);
            //list of unique and InCommon parameters in the given documents
            pUnique = new Dictionary<string, FamilyParameter>();
            pInCommon = new Dictionary<string, FamilyParameter>();
            pToKeep = new Dictionary<string, FamilyParameter>();
            // parameter lists ( to Replace, and the one who replace )
            paramToRemove = new List<string>();

        }

        //open the file dialog for choosing files and save it in a list
        //打开文件选择对话框，选择添加进listbox的文件
        private void ButtonAjoutFamilles_Click(object sender, EventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog(); //实例一个文件对话框
            op.Title = Application.displayableText[appLang]["rFormAddFamilyOPTitle"]; //文件标题
            op.Filter = Application.displayableText[appLang]["rFormAddFamilyOPFilter"];//文件类型
            op.Multiselect = true; //是否允许选择多项文件
            op.InitialDirectory = "D:\\我的文档"; //初始文件夹

            if (op.ShowDialog() == DialogResult.OK)
            {
                string[] filesname = op.SafeFileNames;
                string[] adressname = op.FileNames;

                m_class.strsFileName.Capacity = filesname.Count() + m_class.strsFileName.Count() + 1;
                m_class.strsFile.Capacity = adressname.Count() + m_class.strsFile.Count() + 1;

                bool flag = false;
                for (int i = 0; i < filesname.Count(); i++)
                {
                    foreach (string s in listBoxFamilles.Items)
                    {
                        if (filesname[i] == s)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        listBoxFamilles.Items.Add(filesname[i]);
                        m_class.strsFileName.Add(filesname[i]);
                        m_class.strsFile.Add(adressname[i]);
                    }
                    flag = false;
                }
                op.Dispose();
            }
            UpdateCBL();                
        }

        void UpdateCBL()
        {
            try // fill unique and in common parameters list
            {
                Autodesk.Revit.ApplicationServices.Application revitApp = command.Application.Application;
                if (listFile.Count != 0)
                {
                    pUnique.Clear();
                    pInCommon.Clear();
                    Document familyDoc = revitApp.OpenDocumentFile(listFile[0]);
                    //get the manager of family in the file
                    FamilyManager m_familyMgr = familyDoc.FamilyManager;//获取族管理器

                    //get all of parameters in the file
                    IList<FamilyParameter> faparms = m_familyMgr.GetParameters();

                    foreach (FamilyParameter param in faparms)
                    {
                        if (param.IsShared)
                        {
                            pUnique.Add(param.Definition.Name, param);
                            pInCommon.Add(param.Definition.Name, param);
                        }
                    }

                    for (int i = 1; i < listFile.Count; i++)
                    {
                        //get the file and open the file in the disc
                        familyDoc = revitApp.OpenDocumentFile(listFile[i]);
                        //get the manager of family in the file
                        m_familyMgr = familyDoc.FamilyManager;//获取族管理器

                        //get all of parameters in the file
                        faparms = m_familyMgr.GetParameters();

                        foreach (FamilyParameter param in faparms)
                        {
                            if (param.IsShared)
                            {
                                if (pUnique == null || pUnique.ContainsKey(param.Definition.Name) == false)
                                {
                                    pUnique.Add(param.Definition.Name, param);
                                }
                                if (pInCommon.ContainsKey(param.Definition.Name))
                                {
                                    pToKeep.Add(param.Definition.Name, param);
                                }
                            }
                        }
                        pInCommon.Clear();
                        foreach (KeyValuePair<string, FamilyParameter> kvp in pToKeep)
                        {
                            pInCommon.Add(kvp.Key, kvp.Value);
                        }
                        pToKeep.Clear();
                    }
                } // and add it in the checkedListBox in the second group
                checkedListBoxPPGChoice.Items.Clear();
                if (radioButtonAll.Checked == true || listBoxFamilles.Items.Count == 1)
                {
                    foreach (string key in pUnique.Keys)
                    {
                        checkedListBoxPPGChoice.Items.Add(key);
                    }
                }
                else
                {
                    foreach (string key in pInCommon.Keys)
                    {
                        checkedListBoxPPGChoice.Items.Add(key);
                    }
                }
                this.Focus();
            }
            catch (Exception ex)
            {
                TaskDialog.Show(Application.displayableText[appLang]["rFormUpdateCBLErrorTitle"], ex.Message);
            }
        }

        //button of delete
        //把不需要的从listBoxFamilles删除，同时删除strsFile中相对应的
        private void ButtonSupprimer_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection indices = this.listBoxFamilles.SelectedIndices;
            int selected = indices.Count;
            if (indices.Count > 0)
            {
                for (int n = selected - 1; n >= 0; n--)
                {
                    int index = indices[n];
                    listBoxFamilles.Items.RemoveAt(index);
                    m_class.strsFileName.RemoveAt(index);
                    m_class.strsFile.RemoveAt(index);
                }
            }
            Clear_Everything();
            UpdateCBL();
        }

        private void ButtonToutSupprimer_Click(object sender, EventArgs e)
        {
            listBoxFamilles.Items.Clear();
            m_class.strsFileName.Clear();
            m_class.strsFile.Clear();
            Clear_Everything();
        }

        void Clear_Everything()
        {
            if (listBoxFamilles.Items.Count == 0)
            {
                checkedListBoxPPGChoice.Items.Clear();
                listBoxReplace.Items.Clear();
                paramToRemove.Clear();
                pUnique.Clear();
                pInCommon.Clear();
            }
        }

        // Displays the name when hovering the filenames
        private void ListBoxFamilles_MouseMove(object sender, MouseEventArgs e)
        {
            int index = listBoxFamilles.IndexFromPoint(e.Location);
            // Check if the index is valid.
            if (index != -1 && index < listBoxFamilles.Items.Count)
            {
                // Check if the ToolTip's text isn't already the one
                // we are now processing.
                if (toolTipFamilles.GetToolTip(listBoxFamilles) != listBoxFamilles.Items[index].ToString())
                {
                    // If it isn't, then a new item needs to be
                    // displayed on the toolTip. Update it.
                    toolTipFamilles.SetToolTip(listBoxFamilles, listBoxFamilles.Items[index].ToString());
                }
            }

        }

        // when the radioButtons had their state changed
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBoxPPGChoice.Items.Clear();
            if (pUnique.Count != 0)
            {
                if (radioButtonAll.Checked == true || listBoxFamilles.Items.Count == 1)
                {
                    foreach (string key in pUnique.Keys)
                    {
                        checkedListBoxPPGChoice.Items.Add(key);
                    }
                }
                else
                {
                    foreach (string key in pInCommon.Keys)
                    {
                        checkedListBoxPPGChoice.Items.Add(key);
                    }
                }
                paramToRemove.Capacity = pUnique.Count + 1;
            }
        }
        
        // when an object in the checkedListBox has his state changed
        private void ItemCheckChanged_UpdateParamToChangeList(object sender, EventArgs e)
        {
            if (checkedListBoxPPGChoice.Items.Count != 0)
            {
                if (checkedListBoxPPGChoice.GetItemChecked(checkedListBoxPPGChoice.SelectedIndex) == false)
                {
                    if (paramToRemove.Contains(checkedListBoxPPGChoice.SelectedItem.ToString()) == false)
                    {
                        paramToRemove.Add(checkedListBoxPPGChoice.SelectedItem.ToString());
                        listBoxReplace.Items.Add(checkedListBoxPPGChoice.SelectedItem.ToString());
                    }
                }
                else
                {
                    int index = paramToRemove.IndexOf(checkedListBoxPPGChoice.SelectedItem.ToString());
                    if (index != -1)
                    {
                        listBoxReplace.Items.RemoveAt(index);
                        paramToRemove.RemoveAt(index);
                    }
                }
            }
        }

        // when the "Replacer" button is clicked, start the transaction : --> Command.cs
        private void ButtonTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                hander.Raise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Open and show the LogFile
        private void ButtonLog_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\" + System.Environment.UserName + @"\AppData\Roaming\Autodesk\Revit\Addins\LogPPGChangements.txt");
        }

        private void LinkConnect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://struct-wanderer.github.io/");
        }

        //return the list of files'adresses which need to be added parameter
        //返回需要添加参数的带文件路径的文件列表
        public List<String> listFile
        {
            get { return m_class.strsFile; }
        }

        //return the list of files'name which need to be added parameter
        //返回需要添加参数的文件列表，不带文件路径只是文件名称
        public List<String> listFileName
        {
            get { return m_class.strsFileName; }
        }

    }
}
