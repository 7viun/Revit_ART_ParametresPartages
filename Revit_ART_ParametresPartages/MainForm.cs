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
    public partial class MainForm : System.Windows.Forms.Form
    {
        NewPara myCommand = null;
        ExternalEvent hander = null;

        Save saveCommand = null;
        ExternalEvent savehander = null;

        public DataClass m_class;
        
        //for receive all of file which need to be saved
        public List<String> listSave;//为了得到所有已经操作过的文件，归总后进行保存

        //for judging whether have added the parameter
        public bool compte = false;//为了判断是否有进行添加参数的操作
        RadioButton type;

        public ListBox box;

        private string appLang = "English";
        

        public MainForm(DataClass dataClass, Autodesk.Revit.ApplicationServices.Application app)
        {
            switch (app.Language)
            {
                case Autodesk.Revit.ApplicationServices.LanguageType.French:
                    appLang = "French";
                    break;
                default:
                    break;
            }
            InitializeComponent();
            m_class = dataClass;
            m_class.groups = dataClass.groups;
            m_class.strsGroupe = dataClass.strsGroupe;
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

            myCommand = new NewPara(this);
            hander = ExternalEvent.Create(myCommand);

            saveCommand = new Save(this);
            savehander = ExternalEvent.Create(saveCommand);

            #region add the list of BuiltInParameterGroup to the comboBox2
            m_class.builtParaGroupDic = dataClass.builtParaGroupDic;
            foreach (string key in m_class.builtParaGroupDic.Keys)
            {
                comboBox2.Items.Add(key);
            }
            #endregion

            #region add the parameter group to the comboBox1
            for (int i = 0; i < m_class.strsGroupe.Count; i++)
            {

                comboBox1.Items.Add(m_class.strsGroupe[i]);
            }
            #endregion

            box = listBox3;
            
        }
        //for externaldefinition from shared parameter file
        public DefinitionGroups definitionGroups
        {
            get { return m_class.groups; }
        }
        
        //open the file dialog for choosing files
        //打开文件选择对话框，选择添加进listbox的文件
        private void button1_Click(object sender, EventArgs e)
        {
    
            OpenFileDialog op = new OpenFileDialog(); //实例一个文件对话框
            op.Title = Application.displayableText[appLang]["formButton1OPTitle"]; //文件标题
            op.Filter = Application.displayableText[appLang]["formButton1OPFilter"];//文件类型
            op.Multiselect = true; //是否允许选择多项文件
            op.InitialDirectory = "D:\\我的文档"; //初始文件夹

            if (op.ShowDialog() == DialogResult.OK)  
            {
                string[] filesname = op.SafeFileNames;
                string[] adressname = op.FileNames;

                bool flag = false;
                for (int i = 0; i < filesname.Count(); i++)
                {
                    foreach (string s in listBox1.Items)
                    {
                        if (filesname[i] == s)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        listBox1.Items.Add(filesname[i]);
                        m_class.strsFileName.Add(filesname[i]);
                        m_class.strsFile.Add(adressname[i]);
                    }
                    flag = false;
                }

            }
            op.Dispose();
        }

        //button of delete
        //把不需要的从listbox1删除，同时删除strsFile中相对应的
        private void button3_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection indices = this.listBox1.SelectedIndices;
            int selected = indices.Count;
            if (indices.Count > 0)
            {
                for (int n = selected - 1; n >= 0; n--)
                {
                    int index = indices[n];
                    listBox1.Items.RemoveAt(index);
                    m_class.strsFileName.RemoveAt(index);
                    m_class.strsFile.RemoveAt(index);
                }
            }

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

        //button of "ok"
        //最后确认添加参数按钮
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                hander.Raise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.displayableText[appLang]["warning"]);
            }
        }

        //the event of comboBox
        //通过comboBox的选择，listbox显示相应组别的参数
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            m_class.strsDic = new Dictionary<string, ParameterType>();

            string group = comboBox1.Text;
            m_class.strsDic = m_class.dicDic[group];

            foreach (string key in m_class.dicDic[group].Keys)
            {
                listBox2.Items.Add(key);
            }

        }

        //for DefinitionGroup myGroup = myGroups.get_Item("MyGroup");
        public string groupName
        {
            get { return comboBox1.Text; }
        }

        //button of "set"
        //将选中参数传递到设置set
        private void button10_Click(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
            m_class.nomETtypePara.Clear();

            for (int i = 0; i < listBox2.SelectedItems.Count; i++)
            {
                m_class.nomETtypePara.Add(listBox2.SelectedItems[i].ToString(), m_class.strsDic[listBox2.SelectedItems[i].ToString()]);
                string nomETtype = string.Format("{0} ---- {1}", listBox2.SelectedItems[i].ToString(), m_class.strsDic[listBox2.SelectedItems[i].ToString()]);
                listBox4.Items.Add(nomETtype);
            }

            //default: type = type
            //使用者无论选择与否，默认type为Type
            radioButton1.Checked = true;

            //default: regrouper = the first one in the list
            //无论选择与否，默认选择第一个
            comboBox2.SelectedIndex = 0;
            m_class.builtParaGroup = m_class.builtParaGroupDic[comboBox2.SelectedItem.ToString()];

        }

        //return the name of parameter and the type of parameter
        //返回参数名称
        //返回参数类型
        public Dictionary<String, ParameterType> nomTypeParaDic
        {
            get { return m_class.nomETtypePara; }
        }

        //type = type
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                type = radioButton1;
            }
        }

        //type = occurence
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                type = radioButton2;
            }

        }

        //return de type
        public string paraType
        {
            get { return type.Text; }
        }

        //choose the type of "regrouper"
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = comboBox2.SelectedItem.ToString();
            m_class.builtParaGroup = m_class.builtParaGroupDic[key];
        }

        //return the result of "regrouper"
        //返回参数的regrouper，BuiltInParameterGroup
        public BuiltInParameterGroup BuiltInParameterGroup
        {
            get { return m_class.builtParaGroup; }
        }

        //button of "save"
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                savehander.Raise();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.displayableText[appLang]["warning"]);
            }
        }

        //for some items in the listBox4 which can't be showed complete, when the mouse move in the item, il wills show all of text.
        //当鼠标移动到listbox的项目上，动态显示其内容
        private void listBox4_MouseMove(object sender, MouseEventArgs e)
        {
            int index = listBox4.IndexFromPoint(e.Location);
            // Check if the index is valid.
            if (index != -1 && index < listBox4.Items.Count)
            {
                // Check if the ToolTip's text isn't already the one
                // we are now processing.
                if (toolTip1.GetToolTip(listBox4) != listBox4.Items[index].ToString())
                {
                    // If it isn't, then a new item needs to be
                    // displayed on the toolTip. Update it.
                    toolTip1.SetToolTip(listBox4, listBox4.Items[index].ToString());
                }
            }

        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            // Check if the index is valid.
            if (index != -1 && index < listBox1.Items.Count)
            {
                // Check if the ToolTip's text isn't already the one
                // we are now processing.
                if (toolTip1.GetToolTip(listBox1) != listBox1.Items[index].ToString())
                {
                    // If it isn't, then a new item needs to be
                    // displayed on the toolTip. Update it.
                    toolTip1.SetToolTip(listBox1, listBox1.Items[index].ToString());
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://connect.arteliagroup.com/community/user-group-bim/revit-user-group");
        }
    }
}
