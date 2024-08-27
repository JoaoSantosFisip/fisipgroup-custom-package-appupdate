using UnityEditor;
//using UnityEngine;
//using FisipGroup.CustomPackage.Tools.EditorTool;
using FisipGroup.CustomPackage.Tools.Helpers;
//using FisipGroup.CustomPackage.Tools.Extensions;

namespace FisipGroup.CustomPackage.AppUpdate.Editor
{
    /// <summary>
    /// Editor window where the developer can insert the required data for the app updater to work.
    /// </summary>
    public class AppUpdaterEditorWindow : EditorWindow
    {
        private static AppUpdaterInfoScriptableObject Info;
        private static readonly string PackageName = "AppUpdate";

        [MenuItem("FisipGroup/App Update")]
        public static void ShowWindow()
        {
            HelperCustomPackage.CreateResourcesFolders(PackageName);

            Info = HelperCustomPackage.GetInfoFile<AppUpdaterInfoScriptableObject>(PackageName) as AppUpdaterInfoScriptableObject;

            Selection.activeObject = Info;

            //GetWindow<AppUpdaterEditorWindow>("FisipGroup AppUpdate");
        }

        //private void OnGUI()
        //{
        //    var textAreaHeight = EditorWindowStyles.InputTextStyle.lineHeight * 1;
        //
        //    GUILayout.Label("Info", EditorWindowStyles.TitleStyle);
        //    GUILayout.BeginHorizontal();
        //    GUILayout.Space(10);
        //    GUILayout.Label("Apple app ID", EditorWindowStyles.SectionStyle);
        //    Info.appleAppID = EditorGUILayout.TextArea(Info.appleAppID, EditorWindowStyles.InputTextStyle, GUILayout.Height(textAreaHeight));
        //    GUILayout.Space(10);
        //    GUILayout.EndHorizontal();
        //    GUILayout.BeginHorizontal();
        //    GUILayout.Space(10);
        //    GUILayout.Label("Can be found on https://appstoreconnect.apple.com, on the General->App Information section .", EditorWindowStyles.SmallTextStyle);
        //    GUILayout.Space(10);
        //    GUILayout.EndHorizontal();
        //
        //    GUILayout.Space(10);
        //    GUILayout.Label("Tools", EditorWindowStyles.TitleStyle);
        //    if (GUILayout.Button("Save"))
        //    {
        //        var newInfoFile = (AppUpdaterInfoScriptableObject)CreateInstance(typeof(AppUpdaterInfoScriptableObject));
        //        newInfoFile.appleAppID = Info.appleAppID.RemoveWhitespaceLinesAndTabs();
        //
        //        Info = HelperCustomPackage.SaveFileChanges(newInfoFile, PackageName) as AppUpdaterInfoScriptableObject;
        //    }
        //}
    }
}
