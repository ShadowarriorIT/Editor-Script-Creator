using UnityEngine;
using UnityEditor;
using System.IO;

public static class EditorScriptCreator
{
    private static string fileText = "using UnityEngine;\n" +
                        "using UnityEditor;\n\n" +
                        "[CustomEditor(typeof({0}))]\n" +
                        "public class {0}Editor: Editor\n" +
                        "{{\n\tprivate {0} myTarget;\n\n" +
                        "\tprivate void OnEnable()\n" +
                        "\t{{\n\t\tmyTarget = target as {0};\n\t}}\n\n" +
                        "\tprivate void OnSceneGUI()\n\t{{\n\n\t}}\n\n" +
                        "\tpublic override void OnInspectorGUI()\n" +
                        "\t{{\n\t\tbase.OnInspectorGUI();\n\t}}\n}}";

    [MenuItem("CONTEXT/MonoBehaviour/Create Editor Script")]
    public static void CreateEditorScript(MenuCommand command)
    {
        string folderPath = EditorUtility.OpenFolderPanel("Select the folder", Application.dataPath, string.Empty);

        if (!string.IsNullOrEmpty(folderPath))
        {
            string componentType = command.context.GetType().ToString();
            string path = folderPath + "/" + componentType + "Editor.cs";

            if (File.Exists(path))
            {
                Debug.LogWarning("The file already exists.");
            }
            else
            {
                StreamWriter sw = new StreamWriter(path);
                sw.Write(string.Format(fileText, componentType));
                sw.Close();

                AssetDatabase.Refresh();
            }
        }
    }
}
