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

namespace Revit_ART_RemplacerPPGFamilles
{
    public partial class ReplacePPGForm : System.Windows.Forms.Form
    {
        public ExternalCommandData command;

        ReplaceCommand myCommand = null;
        ExternalEvent hander = null;

        Save saveCommand = null;
        ExternalEvent savehander = null;

        public DataClass m_class;
        //for receive all of file which need to be saved
        public List<String> listSave;//为了得到所有已经操作过的文件，归总后进行保存
        int selectI;
        public Dictionary<string, FamilyParameter> pUnique;
        public Dictionary<string, FamilyParameter> pInCommon;

        public List<string> paramToReplace;
        public List<string> paramWhichReplace;

        //for judging whether have added the parameter
        public bool compte = false;//为了判断是否有进行添加参数的操作

        public ListBox box;
        public ReplacePPGForm(DataClass dataClass, ExternalCommandData commandData)
        {
            InitializeComponent();
            command = commandData;
            m_class = dataClass;
            m_class.groups = dataClass.groups;
            m_class.strsGroupe = dataClass.strsGroupe;
            m_class.defGroupe = dataClass.defGroupe;
            //for the name of group and name of parameter ande type of parameter
            m_class.dicDic = dataClass.dicDic;//为了整合组别名和参数名以及参数类型
            //for the adresse of file
            m_class.strsFile = new List<string>();//为了返回文件路径，后台打开文件
            //fot the name of file
            m_class.strsFileName = new List<string>();//为了返回文件名，提示作用
            m_class.strsPara = new List<string>();
            //for the name and the type of parameter which is selected
            m_class.nomETtypePara = new Dictionary<string, ParameterType>();//为了获得在参数列表中选中参数以及所对应的type
            //for the file which need to be saved
            listSave = new List<string>();//为了获取需要保存的文件路径
            //for the type of "regrouper"
            m_class.builtParaGroup = dataClass.builtParaGroup;//为了regrouper

            myCommand = new ReplaceCommand(this, dataClass, command);
            hander = ExternalEvent.Create(myCommand);

            saveCommand = new Save(this);
            savehander = ExternalEvent.Create(saveCommand);

            pUnique = new Dictionary<string, FamilyParameter>();
            pInCommon = new Dictionary<string, FamilyParameter>();

            paramToReplace = new List<string>();
            paramWhichReplace = new List<string>();

            selectI = -1;
        }

        //open the file dialog for choosing files
        //打开文件选择对话框，选择添加进listbox的文件
        private void ButtonAjoutFamilles_Click(object sender, EventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog(); //实例一个文件对话框
            op.Title = "Rechercher le fichier de famille "; //文件标题
            op.Filter = "fichier de famille|*.rfa|Tous les fichiers|*.*";//文件类型
            op.Multiselect = true; //是否允许选择多项文件
            op.InitialDirectory = "D:\\我的文档"; //初始文件夹

            if (op.ShowDialog() == DialogResult.OK)
            {
                string[] filesname = op.SafeFileNames;
                string[] adressname = op.FileNames;
                

                bool flag = false;
                for (int i = 0; i < filesname.Count(); i++)
                {
                    m_class.strsFileName.Capacity = filesname.Count() + 1;
                    m_class.strsFile.Capacity = adressname.Count() + 1;
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
            try
            {
                Autodesk.Revit.ApplicationServices.Application revitApp = command.Application.Application;
                if (listFile.Count != 0)
                {
                    foreach (string filename in listFile)
                    {
                        //get the file and open the file in the disc
                        Document familyDoc = revitApp.OpenDocumentFile(filename);
                        //get the manager of family in the file
                        FamilyManager m_familyMgr = familyDoc.FamilyManager;//获取族管理器

                        //get all of parameters in the file
                        IList<FamilyParameter> faparms = m_familyMgr.GetParameters();
                        foreach (FamilyParameter param in faparms)
                        {
                            if (param.IsShared)
                            {
                                if (pUnique == null || pUnique.ContainsKey(param.Definition.Name) == false)
                                {
                                    pUnique.Add(param.Definition.Name, param);
                                }
                                else if (pInCommon == null || pInCommon.ContainsKey(param.Definition.Name) == false)
                                {
                                    pInCommon.Add(param.Definition.Name, param);
                                }
                            }
                            
                        }
                    }
                }
                checkedListBoxPPGChoice.Items.Clear();
                if (radioButtonAll.Checked == true || pInCommon.Count == 0)
                {
                    foreach (FamilyParameter value in pUnique.Values)
                    {
                        checkedListBoxPPGChoice.Items.Add(value.Definition.Name);
                    }
                }
                else
                {
                    foreach (FamilyParameter value in pInCommon.Values)
                    {
                        checkedListBoxPPGChoice.Items.Add(value.Definition.Name);
                    }
                }
                this.Focus();
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Problem", ex.Message);
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

        }

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

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBoxPPGChoice.Items.Clear();
            if (radioButtonAll.Checked == true || pInCommon.Count == 0)
            {
                foreach (FamilyParameter value in pUnique.Values)
                {
                    checkedListBoxPPGChoice.Items.Add(value.Definition.Name);
                    /*if (paramToReplace.Contains(value.Definition.Name) && checkedListBoxPPGChoice.Items.Contains(value.Definition.Name))
                    {
                        checkedListBoxPPGChoice.SetItemChecked(checkedListBoxPPGChoice.Items.IndexOf(value.Definition.Name), true);
                    }*/
                }
                paramToReplace.Capacity = pUnique.Count + 1;
                paramWhichReplace.Capacity = pUnique.Count + 1;
            }
            else
            {
                foreach (FamilyParameter value in pInCommon.Values)
                {
                    checkedListBoxPPGChoice.Items.Add(value.Definition.Name);
                    /*if (paramToReplace.Contains(value.Definition.Name) && checkedListBoxPPGChoice.Items.Contains(value.Definition.Name))
                    {
                        checkedListBoxPPGChoice.SetItemChecked(checkedListBoxPPGChoice.Items.IndexOf(value.Definition.Name), true);
                    }*/
                }
            }
        }
        
        private void ItemCheckChanged_UpdateParamToChangeList(object sender, EventArgs e)
        {
            if (checkedListBoxPPGChoice.Items.Count != 0)
            {
                if (checkedListBoxPPGChoice.GetItemChecked(checkedListBoxPPGChoice.SelectedIndex) == false)
                {
                    if (paramToReplace.Contains(checkedListBoxPPGChoice.SelectedItem.ToString()) == false)
                    {
                        paramToReplace.Add(checkedListBoxPPGChoice.SelectedItem.ToString());
                        paramWhichReplace.Add("/");
                        listBoxReplace.Items.Add(checkedListBoxPPGChoice.SelectedItem.ToString() + " <=> ");
                    }
                }
                else
                {
                    int index = paramToReplace.IndexOf(checkedListBoxPPGChoice.SelectedItem.ToString());
                    if (index != -1)
                    {
                        listBoxReplace.Items.RemoveAt(index);
                        paramToReplace.RemoveAt(index);
                        paramWhichReplace.RemoveAt(index);
                    }
                }
            }
        }

        private void ListBoxReplace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxReplace.SelectedIndex != -1)
            {
                comboBoxPPGReplaceChoice.Items.Clear();
                string name = paramToReplace[listBoxReplace.SelectedIndex];
                FamilyParameter fp = null;

                foreach (FamilyParameter param in pUnique.Values)
                {
                    if (param.Definition.Name == name)
                    {
                        fp = param;
                        break;
                    }
                }
                foreach (string group in m_class.strsGroupe)
                {
                    foreach (KeyValuePair<string, ParameterType> pair in m_class.dicDic[group])
                    {
                        if (pair.Value == fp.Definition.ParameterType)
                        {
                            comboBoxPPGReplaceChoice.Items.Add(group + " | " + pair.Key);
                        }
                    }
                }

                if (comboBoxPPGReplaceChoice.Items.Contains(paramWhichReplace[listBoxReplace.SelectedIndex]) && paramWhichReplace[listBoxReplace.SelectedIndex].ToString() != "/" && comboBoxPPGReplaceChoice.Text != paramWhichReplace[listBoxReplace.SelectedIndex])
                {
                    comboBoxPPGReplaceChoice.Text = paramWhichReplace[listBoxReplace.SelectedIndex];
                }
            }
        }

        private void ComboBoxPPGReplaceChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (paramWhichReplace[listBoxReplace.SelectedIndex] != comboBoxPPGReplaceChoice.Text)
            {
                int countB = listBoxReplace.Items.Count;
                selectI = listBoxReplace.SelectedIndex;
                listBoxReplace.SelectedIndex = -1;
                paramWhichReplace[selectI] = comboBoxPPGReplaceChoice.SelectedItem.ToString();
                listBoxReplace.Items.RemoveAt(selectI);
                if (selectI >= countB - 1)
                {
                    listBoxReplace.Items.Add(paramToReplace[selectI] + " <=> " + paramWhichReplace[selectI]);
                }
                else
                {
                    listBoxReplace.Items.Insert(selectI, paramToReplace[selectI] + " <=> " + paramWhichReplace[selectI]);
                }
                listBoxReplace.SelectedIndex = selectI;
            }
        }

        private void ButtonEraseChoice_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < paramWhichReplace.Count && i < listBoxReplace.Items.Count; i++)
            {
                paramWhichReplace[i] = "/";
                listBoxReplace.Items[i] = paramToReplace[i] + " <=> ";
            }
        }

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

        private void ButtonLog_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\Users\" + System.Environment.UserName + @"\AppData\Roaming\Autodesk\Revit\Addins\LogPPGChangements.txt");
        }

        private void LinkConnect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://connect.arteliagroup.com/community/user-group-bim/revit-user-group");
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

        //for externaldefinition from shared parameter file
        public DefinitionGroups definitionGroups
        {
            get { return m_class.groups; }
        }

        //return the name of parameter and the type of parameter
        //返回参数名称
        //返回参数类型
        public Dictionary<String, ParameterType> nomTypeParaDic
        {
            get { return m_class.nomETtypePara; }
        }

        
        //return the result of "regrouper"
        //返回参数的regrouper，BuiltInParameterGroup
        public BuiltInParameterGroup BuiltInParameterGroup
        {
            get { return m_class.builtParaGroup; }
        }
        //for DefinitionGroup myGroup = myGroups.get_Item("MyGroup");
        public string groupName
        {
            get { return ""; }
        }
    }
}
