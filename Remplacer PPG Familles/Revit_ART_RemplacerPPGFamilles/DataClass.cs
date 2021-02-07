using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit;
using Autodesk.Revit.UI.Selection;

namespace Revit_ART_RemplacerPPGFamilles
{
    public class DataClass
    {
        private DefinitionFile definitionFile;
        public DefinitionGroups groups;
        public List<DefinitionGroup> defGroupe;
        public List<String> strsGroupe;
        public List<String> strsFile;
        public List<String> strsFileName;
        public List<String> strsPara;

        public Dictionary<String, ParameterType> vide;
        public Dictionary<String, Dictionary<String, ParameterType>> dicDic;

        public Dictionary<String, ParameterType> strsDic;

        public Dictionary<String, ParameterType> nomETtypePara;//为了获得在参数列表中选中参数以及所对应的type


        //Regrouper 的内容
        public Dictionary<String, BuiltInParameterGroup> builtParaGroupDic;//45个BuiltInParameterGroup
        public BuiltInParameterGroup builtParaGroup;

        //获取当前revit中的打开的txt文件中的共享参数
        public DataClass(Autodesk.Revit.ApplicationServices.Application revitApp)
        {
            try
            {
                definitionFile = revitApp.OpenSharedParameterFile();
                groups = definitionFile.Groups;
                defGroupe = new List<DefinitionGroup>();
                strsGroupe = new List<String>();

                vide = new Dictionary<string, ParameterType>();
                dicDic = new Dictionary<string, Dictionary<string, ParameterType>>();


                builtParaGroupDic = new Dictionary<string, BuiltInParameterGroup>();


                foreach (DefinitionGroup group in groups)
                {
                    DefinitionGroup dg2 = group as DefinitionGroup;
                    defGroupe.Add(dg2);
                    strsGroupe.Add(dg2.Name);

                }

                foreach (DefinitionGroup group in defGroupe)
                {
                    vide = new Dictionary<string, ParameterType>();
                    dicDic.Add(group.Name, vide);

                    foreach (Definition definition in group.Definitions)
                    {
                        dicDic[group.Name].Add(definition.Name, definition.ParameterType);
                    }

                }


                #region list of BuiltInParameterGroup
                builtParaGroupDic.Add("Alignement analytique", BuiltInParameterGroup.PG_ANALYTICAL_ALIGNMENT);
                builtParaGroupDic.Add("Analyse d'énergie", BuiltInParameterGroup.PG_ENERGY_ANALYSIS);
                builtParaGroupDic.Add("Analyse structurelle", BuiltInParameterGroup.PG_STRUCTURAL_ANALYSIS);
                builtParaGroupDic.Add("Autre", BuiltInParameterGroup.INVALID);
                builtParaGroupDic.Add("Calques", BuiltInParameterGroup.PG_REBAR_SYSTEM_LAYERS);
                builtParaGroupDic.Add("Construction", BuiltInParameterGroup.PG_CONSTRUCTION);
                builtParaGroupDic.Add("Contraintes", BuiltInParameterGroup.PG_CONSTRAINTS);
                builtParaGroupDic.Add("Cotes", BuiltInParameterGroup.PG_GEOMETRY);
                builtParaGroupDic.Add("Données", BuiltInParameterGroup.PG_DATA);
                builtParaGroupDic.Add("Données d'identification", BuiltInParameterGroup.PG_IDENTITY_DATA);
                builtParaGroupDic.Add("Electricité", BuiltInParameterGroup.PG_AELECTRICAL);
                builtParaGroupDic.Add("Electricité - Charges", BuiltInParameterGroup.PG_ELECTRICAL_LOADS);
                builtParaGroupDic.Add("Electricité - Circuit", BuiltInParameterGroup.PG_ELECTRICAL_CIRCUITING);
                builtParaGroupDic.Add("Electricité - Eclairage", BuiltInParameterGroup.PG_ELECTRICAL_LIGHTING);
                builtParaGroupDic.Add("Electrotechnique", BuiltInParameterGroup.PG_ELECTRICAL);
                builtParaGroupDic.Add("Extrémité principale", BuiltInParameterGroup.PG_PRIMARY_END);
                builtParaGroupDic.Add("Extrémité secondaire", BuiltInParameterGroup.PG_SECONDARY_END);
                builtParaGroupDic.Add("Forces", BuiltInParameterGroup.PG_FORCES);
                builtParaGroupDic.Add("Général", BuiltInParameterGroup.PG_GENERAL);
                builtParaGroupDic.Add("Génie climatique", BuiltInParameterGroup.PG_MECHANICAL);
                builtParaGroupDic.Add("Génie climatique - Charges", BuiltInParameterGroup.PG_MECHANICAL_LOADS);
                builtParaGroupDic.Add("Génie climatique - Ecoulement", BuiltInParameterGroup.PG_MECHANICAL_AIRFLOW);
                builtParaGroupDic.Add("Géométrie de division", BuiltInParameterGroup.PG_DIVISION_GEOMETRY);
                builtParaGroupDic.Add("Graphismes", BuiltInParameterGroup.PG_GRAPHICS);
                builtParaGroupDic.Add("Jeu", BuiltInParameterGroup.PG_COUPLER_ARRAY);
                builtParaGroupDic.Add("Jeu d'armatures", BuiltInParameterGroup.PG_REBAR_ARRAY);
                builtParaGroupDic.Add("Légende globale", BuiltInParameterGroup.PG_OVERALL_LEGEND);
                builtParaGroupDic.Add("Matériaux et finitions", BuiltInParameterGroup.PG_MATERIALS);
                builtParaGroupDic.Add("Modèle analytique", BuiltInParameterGroup.PG_ANALYTICAL_MODEL);
                builtParaGroupDic.Add("Modification de la forme de dalle", BuiltInParameterGroup.PG_SLAB_SHAPE_EDIT);
                builtParaGroupDic.Add("Moments", BuiltInParameterGroup.PG_MOMENTS);
                builtParaGroupDic.Add("Paramètres IFC", BuiltInParameterGroup.PG_IFC);
                builtParaGroupDic.Add("Phase de construction", BuiltInParameterGroup.PG_PHASING);
                builtParaGroupDic.Add("Photométriques", BuiltInParameterGroup.PG_LIGHT_PHOTOMETRICS);
                builtParaGroupDic.Add("Plomberie", BuiltInParameterGroup.PG_PLUMBING);
                builtParaGroupDic.Add("Propriétés du modèle", BuiltInParameterGroup.PG_ADSK_MODEL_PROPERTIES);
                builtParaGroupDic.Add("Propriétés Green Building", BuiltInParameterGroup.PG_GREEN_BUILDING);
                builtParaGroupDic.Add("Protection contre les incendies", BuiltInParameterGroup.PG_FIRE_PROTECTION);
                builtParaGroupDic.Add("Relâchements / Forces des éléments", BuiltInParameterGroup.PG_RELEASES_MEMBER_FORCES);
                builtParaGroupDic.Add("Résultats de l'analyse", BuiltInParameterGroup.PG_ANALYSIS_RESULTS);
                builtParaGroupDic.Add("Segments et raccords", BuiltInParameterGroup.PG_SEGMENTS_FITTINGS);
                builtParaGroupDic.Add("Structure", BuiltInParameterGroup.PG_STRUCTURAL);
                builtParaGroupDic.Add("Texte", BuiltInParameterGroup.PG_TEXT);
                builtParaGroupDic.Add("Texte du titre", BuiltInParameterGroup.PG_TITLE);
                builtParaGroupDic.Add("Visibilité", BuiltInParameterGroup.PG_VISIBILITY);
                #endregion
            }

            catch (Exception e)
            {
                TaskDialog.Show("Erreur", e.Message);
            }

        }

    }

}

