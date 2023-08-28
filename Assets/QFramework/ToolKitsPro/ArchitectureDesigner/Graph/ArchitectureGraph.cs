#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace QFramework.Pro
{
    // 版本迭代
    [CreateAssetMenu(menuName = "@ArchitectureDesigner/Create Graph", fileName = "Architecture Graph")]
    public class ArchitectureGraph : IMGUIGraph
    {
        public string Namespace = "QFrameworkPro.Example";

        [HideInInspector] public string ProjectName = "Global";

        [HideInInspector] public string GenerateFolder = "Assets";

        [HideInInspector] public bool IsRoot = true;
        // Model
        // System
        //  Command
        //  Event
        // Model
        // Utility

        public List<CommandNode> Commands => nodes.Where(n => n is CommandNode).Cast<CommandNode>().ToList();
        public List<ModelNode> Models => nodes.Where(n => n is ModelNode).Cast<ModelNode>().ToList();


        public string ScriptsFolderPath => IsRoot ? "Assets/Scripts" : $"{GenerateFolder}/{ProjectName}/Scripts";

        // public override void RemoveNode(IMGUIGraphNode node)
        // {
        //
        //     if (node is ModelNode)
        //     {
        //         var modelNode = node as ModelNode;
        //         AssetDatabase.RemoveObjectFromAsset(modelNode.ModelGraph);
        //     }
        //     
        //     base.RemoveNode(node);
        //     
        // }
    }

    [CustomEditor(typeof(ArchitectureGraph), false)]
    public class ArchitectureGraphInspector : IMGUIGlobalGraphInspector
    {
        private SerializedProperty mGenerateFolder;
        private SerializedProperty mProjectName;
        private SerializedProperty mIsRoot;


        private ArchitectureGraph mArchitectureGraph;

        private void OnEnable()
        {
            mGenerateFolder = serializedObject.FindProperty("GenerateFolder");
            mProjectName = serializedObject.FindProperty("ProjectName");
            mIsRoot = serializedObject.FindProperty("IsRoot");
            mArchitectureGraph = target as ArchitectureGraph;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            
            EditorGUILayout.PropertyField(mProjectName);

            EditorGUILayout.PropertyField(mIsRoot);

            if (!mIsRoot.boolValue)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(mGenerateFolder);
                if (GUILayout.Button("..."))
                {
                    var folderPath = EditorUtility.OpenFolderPanel("title", mGenerateFolder.stringValue, "Assets");

                    if (!folderPath.Contains(Application.dataPath))
                    {
                        EditorUtility.DisplayDialog("错误", "请不要选择项目之外的目录", "确定");
                    }
                    else
                    {
                        if (folderPath.EndsWith(mProjectName.stringValue))
                        {
                            folderPath =  folderPath.Replace(Application.dataPath, "Assets");
                            mGenerateFolder.stringValue = folderPath.Remove(folderPath.Length - ("/" + mProjectName.stringValue).Length);
                        }
                        else
                        {
                            mGenerateFolder.stringValue = folderPath.Replace(Application.dataPath, "Assets");
                        }
                    }
                }

                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Generate Code"))
            {
                ArchitectureDesigner.GenerateCode(target as ArchitectureGraph);
            }

            GUILayout.BeginVertical("box");
            GUILayout.Label("Preview");
            GUILayout.Label(mArchitectureGraph.ScriptsFolderPath);
            GUILayout.Label(mArchitectureGraph.ScriptsFolderPath + "/Command");
            GUILayout.Label(mArchitectureGraph.ScriptsFolderPath + "/Model");
            GUILayout.Label(mArchitectureGraph.ScriptsFolderPath + "/" + mProjectName.stringValue + ".cs");
            GUILayout.Label(mArchitectureGraph.ScriptsFolderPath + "/" + mProjectName.stringValue + ".Designer.cs");
            GUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomNodeGraphEditor(typeof(ArchitectureGraph))]
    public class ArchitectureGraphEditor : IMGUIGraphEditor
    {
        public override string GetNodeMenuName(Type type)
        {
            if (typeof(ArchitectureNode).IsAssignableFrom(type)) {
                return base.GetNodeMenuName(type);
            }

            return null;
        }

        public override void OnGUI()
        {
            GUILayout.Toolbar(-1, new string[] { "1", "2" });
            base.OnGUI();
        }
    }
}
#endif