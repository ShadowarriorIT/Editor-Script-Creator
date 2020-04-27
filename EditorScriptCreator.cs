using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class EditorScriptCreator
{
    [MenuItem("CONTEXT/MonoBehaviour/Create Editor Script")]
    public static void CreateEditorScript(MenuCommand command)
    {
        string componentType = command.context.GetType().ToString();
        string folderPath = EditorUtility.OpenFolderPanel("Select the folder", Application.dataPath, "");
        string filename = componentType + "Editor";
        string path = folderPath + "/" + filename + ".cs";

        if (!string.IsNullOrEmpty(folderPath))
        {
            if (File.Exists(path))
            {
                Debug.Log("The file already exists.");
            }
            else
            {
                StreamWriter sw = new StreamWriter(path);

                sw.Write("using System.Collections;\n" +
                    "using System.Collections.Generic;\n" +
                    "using UnityEngine;\n" +
                    "using UnityEditor;\n\n" +
                    "[CustomEditor(typeof(" + componentType + "))]\n" +
                    "public class " + filename + ": Editor\n" +
                    "{\n" +
                    "\tprivate " + componentType + " myTarget;\n\n" +
                    "\tprivate void OnEnable()\n" +
                    "\t{\n" +
                    "\t\tmyTarget = target as " + componentType + ";\n" +
                    "\t}\n\n" +
                    "\tprivate void OnSceneGUI()\n" +
                    "\t{\n\n" +
                    "\t}\n\n" +
                    "\tpublic override void OnInspectorGUI()\n" +
                    "\t{\n" +
                    "\t\tbase.OnInspectorGUI();\n" +
                    "\t}\n" +
                    "}");

                sw.Close();

                AssetDatabase.Refresh();
            }
        }
    }

    private static void AddEmptyLine(StreamWriter sw)
    {
        sw.WriteLine("");
    }
}
