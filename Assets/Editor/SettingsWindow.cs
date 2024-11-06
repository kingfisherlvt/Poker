using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class SettingsWindow : EditorWindow
{
    private List<MacorItem> m_List = new List<MacorItem>();
    private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();
    private string m_Macor = null;
    [MenuItem("Tools/Gadgets/Set Unity Editor macro definition #%&E")]
    public static void Show()
    {
        GetWindow<SettingsWindow>();
    }

    void OnEnable()
    {
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        m_List.Clear();
        //NET45;ASYNC;ILRuntime;ENABLE_LOG_LOOP;ENABLE_LOG
        m_List.Add(new MacorItem() { Name = "NET45", DisplayName = "Script compilation .Net45 mode", IsDev = true, IsDebug = true, IsRelease = true });
        m_List.Add(new MacorItem() { Name = "ASYNC", DisplayName = "ASYNC asynchronous mode", IsDev = false, IsDebug = true, IsRelease = true });
        m_List.Add(new MacorItem() { Name = "ILRuntime", DisplayName = "Enable IL hot-change mode", IsDev = false, IsDebug = true, IsRelease = true });
        m_List.Add(new MacorItem() { Name = "ENABLE_HOTFIX_LOG", DisplayName = "Enable hot-change layer log", IsDev = true, IsDebug = true, IsRelease = true });
        // m_List.Add(new MacorItem() { Name = "ENABLE_LOG_LOOP;ENABLE_LOG", DisplayName = "Enable logging and mobile capabilities for non-hot-change layers", IsDebug = true, IsRelease = true });

        m_List.Add(new MacorItem() { Name = "LOG_ZSX", DisplayName = "ZSX macro (for Sixin only)", IsDev = false, IsDebug = false, IsRelease = true });
        m_List.Add(new MacorItem() { Name = "APPStore", DisplayName = "The trial package is on the hook", IsDev = false, IsDebug = false, IsRelease = true });

        for (int i = 0; i < m_List.Count; i++)
        {
            if (!string.IsNullOrEmpty(m_Macor) && m_Macor.IndexOf(m_List[i].Name) != -1)
            {
                m_Dic[m_List[i].Name] = true;
            }
            else
            {
                m_Dic[m_List[i].Name] = false;
            }
        }
    }

    void OnGUI()
    {

        for (int i = 0; i < m_List.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            m_Dic[m_List[i].Name] = GUILayout.Toggle(m_Dic[m_List[i].Name], m_List[i].DisplayName);
            EditorGUILayout.EndHorizontal();
        }

        //开启一行
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Save", GUILayout.Width(100)))
        {
            SaveMacor();
        }

        if (GUILayout.Button("Dev Mode", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDev;
            }

            SaveMacor();
        }

        if (GUILayout.Button("Debug Mode", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            }

            SaveMacor();
        }

        if (GUILayout.Button("Release Mode", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsRelease;
            }

            SaveMacor();
        }

        EditorGUILayout.EndHorizontal();

    }

    private void SaveMacor()
    {
        m_Macor = string.Empty;
        foreach (var item in m_Dic)
        {
            if (item.Value)
            {
                m_Macor += string.Format("{0};", item.Key);
            }
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macor);

    }

    /// <summary>
    /// 宏项目
    /// </summary>
    public class MacorItem
    {
        //名称
        public string Name;
        //显示名称
        public string DisplayName;
        //是否开发项
        public bool IsDev;
        //是否调试项
        public bool IsDebug;
        //是否发布项
        public bool IsRelease;
    }

}
