using ETModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ZSXMenu : EditorWindow
{
    [MenuItem("Helps/Refresh and run _F1")]
    public static void ShowZHENTW_copyfff()
    {
        AssetDatabase.Refresh();
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    [MenuItem("Helps/Swipe to login")]
    public static void ShowRefresh()
    {
        GetWindow<LoginEditPlay>(false);
    }


    [MenuItem("Helps/Multilingual copy (abridged version)")]
    public static void ShowZHENTW_Language()
    {
        GetWindow<GenerateStringLanguage>(false);
    }


    [MenuItem("Helps/Detect how many sprite tags a prefab uses, and replace the 2 large atlases with fragments")]
    public static void ShowZHENTW_copy()
    {
        GetWindow<CheckSpriteTag>(false);
    }

    [MenuItem("Helps/Incorrect use of resource name")]
    public static void ShowErrorUseImage()
    {
        GetWindow<CheckErrorUsingImage>(false);
    }

    [MenuItem("Helps/Scale multiplier of prefab root  %&v")]
    public static void ShowWindow_Scale()
    {
        GetWindow<CheckRootScale>(false);
    }

    [MenuItem("Helps/Change whether RayCast is checked %#&r")]
    public static void ShowWindow_RayCast()
    {
        GetWindow<CheckModifyRayCast>(false);
    }

    [MenuItem("Helps/(Check before deleting) Check if fragments are used ", false, 3999)]
    public static void ShowWindowDC()
    {
        GetWindow<CheckDeleteBeforeImage>(false);
    }

    [MenuItem("Assets/Mark AB Pack %#&B", false, 80)]
    public static void ShowRightMouse()
    {
        var tSelect = Selection.activeObject;
        if (tSelect is GameObject)
        {
            var tSed = tSelect as GameObject;
            var tPath = AssetDatabase.GetAssetPath(tSed);
            var tAsset = AssetImporter.GetAtPath(tPath);
            tAsset.assetBundleName = tSed.name + ".unity3d"; //Set the name of the Bundle file
            tAsset.SaveAndReimport();
            AssetDatabase.Refresh();
            Selection.activeObject = null;
        }
        else
        {
            Debug.Log("No GameObject");
        }
    }

    [MenuItem("Helps/Multi-language copy  %&L")]
    public static void ShowWindow_LanguageMore()
    {
        GetWindow<LanguageCopy>(false);
    }

    [MenuItem("Helps/Simple Generate Script")]
    public static void ShowWindow_AddGenerate()
    {
        GetWindow<GenerateScriptCode1>(false);
        //  GetWindow<LanguageCopy>(false);
    }
    [MenuItem("Helps/Clear playerPrefs %&c")]
    public static void ClearPrefab()
    {
        Debug.Log("Cleared playerPrefs");
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Helps/Display Timestamp %T")]
    public static void ShowTime()
    {
        EditorWindow.GetWindow(typeof(TimeLookEditor)); //>(false,"",true);
    }
}


public class GetAssetGOWindow : EditorWindow
{
    /// <summary> That is, click the Apply button in the Inspector panel and mark the AB package.</summary>
    public void SaveNewPrefabs(List<GameObject> pWillChanges)
    {
        if (pWillChanges == null || pWillChanges.Count == 0) return;
        for (int i = 0; i < pWillChanges.Count; i++)
        {
            var tPrefab = pWillChanges[i];
            var tOldHierarchyPrefab = PrefabUtility.InstantiatePrefab(tPrefab) as GameObject;
            var tPath = AssetDatabase.GetAssetPath(tPrefab);
            var tNewEmpty = PrefabUtility.CreateEmptyPrefab(tPath);
            var gameNew = PrefabUtility.ReplacePrefab(tOldHierarchyPrefab, tNewEmpty, ReplacePrefabOptions.ConnectToPrefab);
            GameObject.DestroyImmediate(tOldHierarchyPrefab);

            var tPathAB = AssetDatabase.GetAssetPath(gameNew);
            var tAssetAB = AssetImporter.GetAtPath(tPathAB);
            tAssetAB.assetBundleName = gameNew.name + ".unity3d";
            tAssetAB.SaveAndReimport();
        }
    }


    /// <summary>Get GO pFilter:Pefab or Script pGoStr:prefabName according to the string</summary>
    public GameObject FindOneGo(string pFilter, string pGoStr)
    {
        var tGuids = AssetDatabase.FindAssets("t:" + pFilter + " " + pGoStr, new string[] { "Assets/Bundles/UI" });
        GameObject tGo = null;
        int i = 0;
        foreach (var guid in tGuids)
        {
            tGo = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid)) as GameObject;
            i++;
        }
        if (i > 1) Debug.Log("Search in the provided string, there are 2 + (the one you get is the last one) It is recommended to add '_1' to the prefab name and change it back after getting the string");
        return tGo;
    }
    public List<GameObject> GetPrefabs(string pPath)
    {
        if (string.IsNullOrEmpty(pPath)) pPath = "Assets/Bundles/UI";
        var guids = AssetDatabase.FindAssets("t:Prefab", new string[] { pPath });
        List<GameObject> tAllPrefab = new List<GameObject>();
        foreach (var item in guids)
        {
            var go = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(item)) as GameObject;
            tAllPrefab.Add(go);
        }
        return tAllPrefab;
    }
}