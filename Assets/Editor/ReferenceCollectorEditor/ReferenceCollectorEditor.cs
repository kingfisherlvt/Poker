using System;
using System.Collections.Generic;
using System.Text;
using ETModel;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

[CustomEditor(typeof(ReferenceCollector))]
[CanEditMultipleObjects]
public class ReferenceCollectorEditor : Editor
{
    private Dictionary<string, string> uiComponentType = new Dictionary<string, string>()
    {
            {"Text", "Text" },
            {"Image", "Image" },
            {"RawImage", "RawImage" },
            {"Button", "Button" },
            {"Toggle", "Toggle" },
            {"Slider", "Slider" },
            {"Scrollbar", "Scrollbar" },
            {"Dropdown", "Dropdown" },
            {"InputField", "InputField" },
            {"Canvas", "Canvas" },
            {"Panel", "Transform" },
            {"ScrollView", "ScrollRect" },
            {"RC", "ReferenceCollector" },
            {"Armature", "UnityArmatureComponent" }
    };

    private string searchKey
    {
        get
        {
            return _searchKey;
        }
        set
        {
            if (_searchKey != value)
            {
                _searchKey = value;
                heroPrefab = referenceCollector.Get<Object>(searchKey);
            }
        }
    }

    private ReferenceCollector referenceCollector;

    private Object heroPrefab;

    private string _searchKey = "";

    private void DelNullReference()
    {
        var dataProperty = serializedObject.FindProperty("data");
        for (int i = dataProperty.arraySize - 1; i >= 0; i--)
        {
            var gameObjectProperty = dataProperty.GetArrayElementAtIndex(i).FindPropertyRelative("gameObject");
            if (gameObjectProperty.objectReferenceValue == null)
            {
                dataProperty.DeleteArrayElementAtIndex(i);
            }
        }
    }

    private void OnEnable()
    {
        referenceCollector = (ReferenceCollector)target;
    }

    string mSearchStr;
    List<string> mKeyNames = new List<string>();

    public override void OnInspectorGUI()
    {
        Undo.RecordObject(referenceCollector, "Changed Settings");
        var dataProperty = serializedObject.FindProperty("data");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Reference"))
        {
            AddReference(dataProperty, Guid.NewGuid().GetHashCode().ToString(), null);
        }
        if (GUILayout.Button("Delete All"))
        {
            dataProperty.ClearArray();
        }
        if (GUILayout.Button("Remove Null"))
        {
            DelNullReference();
        }
        if (GUILayout.Button("Sort"))
        {
            referenceCollector.Sort();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Quick Naming"))
        {
            foreach (var item in referenceCollector.data)
            {
                item.key = item.gameObject.name;
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Gen Declaration"))
        {
            int mSuccess = 0;
            int mError = 0;
            int mErrorName = 0;
            StringBuilder build = new StringBuilder();

            build.AppendLine($"private ReferenceCollector rc;");

            string[] mCache = null;
            foreach (var item in referenceCollector.data)
            {
                if (item.gameObject == null)
                    continue;
                mCache = item.gameObject.name.Split('_');
                if (mCache.Length == 2)
                {
                    mCache[0] = mCache[0].Replace(" ", "");
                    string mType = null;
                    foreach (var keyval in this.uiComponentType)
                    {
                        if (mCache[0] == keyval.Key)
                        {
                            mType = keyval.Value;
                            break;
                        }
                    }

                    if (mType == null)
                    {
                        mError++;
                        build.AppendLine($"private Transform trans{mCache[1]};");
                        Debug.Log($"Notice：<color=red>{item.gameObject.name}</color> Failed to generate UI variable declaration");
                        // break;
                        continue;
                    }

                    mSuccess++;
                    build.AppendLine($"private {mType} {mCache[0].ToLower()}{mCache[1]};");
                }
                else if (mCache.Length == 1)
                {
                    build.AppendLine($"private Transform trans{mCache[0]};");
                }
                else
                {
                    mErrorName++;
                    Debug.Log($"{item.gameObject.name} The naming is not standardized. Please name it as follows：Type_Name，Button_Login");
                }
            }
            ClipboardHelper.ClipBoard = build.ToString();
            Debug.Log($"{referenceCollector.gameObject.name} Generate variable declarations -> success：{mSuccess}，fail：{mError}，Naming Error：{mErrorName}    |    The correct part of the code has been copied!");
        }

        if (GUILayout.Button("Gen fetch code"))
        {
            StringBuilder build = new StringBuilder();

            build.AppendLine($"rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();");

            string[] mCache = null;
            foreach (var item in referenceCollector.data)
            {
                mCache = item.gameObject.name.Split('_');
                if (mCache.Length == 2)
                {
                    mCache[0] = mCache[0].Replace(" ", "");
                    string mType = null;
                    foreach (var keyval in this.uiComponentType)
                    {
                        if (mCache[0] == keyval.Key)
                        {
                            mType = keyval.Value;
                            break;
                        }
                    }

                    if (mType == null)
                    {
                        // Debug.Log($"Notice：{item.gameObject.name} Failed to generate acquisition code");
                        build.AppendLine($"trans{mCache[1]} = rc.Get<GameObject>(\"{item.key}\").transform;");
                        // break;
                        continue;
                    }

                    build.AppendLine($"{mCache[0].ToLower()}{mCache[1]} = rc.Get<GameObject>(\"{item.key}\").GetComponent<{mType}>();");
                }
                else if (mCache.Length == 1)
                {
                    build.AppendLine($"trans{mCache[0]} = rc.Get<GameObject>(\"{item.key}\").transform;");
                }
                else
                {
                    Debug.Log($"{item.gameObject.name} The naming is not standardized. Please name it as follows：Type_Name，Button_Login");
                }
            }

            ClipboardHelper.ClipBoard = build.ToString();

            Debug.Log("Copy the code successfully!");
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        searchKey = EditorGUILayout.TextField(searchKey);
        EditorGUILayout.ObjectField(heroPrefab, typeof(Object), false);
        if (GUILayout.Button("Delete"))
        {
            referenceCollector.Remove(searchKey);
            heroPrefab = null;
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        mSearchStr = EditorGUILayout.TextField("Search Name", mSearchStr);
       
        if (GUILayout.Button("Search, left list"))
        {
            if (string.IsNullOrEmpty(mSearchStr)) return;
            mKeyNames.Clear();
            for (int i = 0; i < referenceCollector.data.Count; i++)
            {
                var tRef = referenceCollector.data[i];
                if (tRef.key.Contains(mSearchStr))
                {
                    Debug.Log("No." + i.ToString() + "indivual," + tRef.key + ", count from the bottom, start from 0");
                    mKeyNames.Add(tRef.key);
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        var delList = new List<int>();
        for (int i = referenceCollector.data.Count - 1; i >= 0; i--)
        {
            GUILayout.BeginHorizontal();
            if (mKeyNames.Contains(referenceCollector.data[i].key))
            {
                EditorGUILayout.LabelField("search",GUILayout.Width(30));
            }
            referenceCollector.data[i].key = EditorGUILayout.TextField(referenceCollector.data[i].key, GUILayout.Width(150));
            referenceCollector.data[i].gameObject = EditorGUILayout.ObjectField(referenceCollector.data[i].gameObject, typeof(Object), true);

            if (GUILayout.Button("B"))
            {
                var tStrKey = referenceCollector.data[i].key;
                string tCopy = string.Format("UIEventListener.Get(rc.Get<GameObject>(\"{0}\")).onClick = Click{0};", tStrKey);
                ClipboardHelper.ClipBoard = tCopy;
            }
            if (GUILayout.Button("C"))//Add a simple and rough single item copy.....Prevent handwritten strings from being written incorrectly
            {
                var tStrKey = referenceCollector.data[i].key;
                if (tStrKey.Contains("text_") || tStrKey.Contains("Text_"))
                {
                    var tName = tStrKey.Split('_')[1];
                    string tCopy = string.Format("private Text text{0};\ntext{0} = rc.Get<GameObject>(\"{1}\").GetComponent<Text>();", tName, tStrKey);
                    ClipboardHelper.ClipBoard = tCopy;
                }
                else if (tStrKey.ToLower().Contains("inputfield_"))
                {
                    var tName = tStrKey.Split('_')[1];
                    string tCopy = string.Format("private InputField inputfield{0};\ninputfield{0} = rc.Get<GameObject>(\"{1}\").GetComponent<InputField>();", tName, tStrKey);
                    ClipboardHelper.ClipBoard = tCopy;
                }
                else
                {
                    string tGoName = string.Format("private GameObject {0};\n{0} = rc.Get<GameObject>(\"{0}\");", tStrKey);
                    ClipboardHelper.ClipBoard = tGoName;
                }
            }

            if (GUILayout.Button("X"))
            {
                delList.Add(i);
            }
            GUILayout.EndHorizontal();
        }
        var eventType = Event.current.type;
        if (eventType == EventType.DragUpdated || eventType == EventType.DragPerform)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            if (eventType == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();
                foreach (var o in DragAndDrop.objectReferences)
                {
                    AddReference(dataProperty, o.name, o);
                }
            }

            Event.current.Use();
        }
        foreach (var i in delList)
        {
            dataProperty.DeleteArrayElementAtIndex(i);
        }
        serializedObject.ApplyModifiedProperties();
        serializedObject.UpdateIfRequiredOrScript();
    }

    private void AddReference(SerializedProperty dataProperty, string key, Object obj)
    {
        int index = dataProperty.arraySize;
        dataProperty.InsertArrayElementAtIndex(index);
        var element = dataProperty.GetArrayElementAtIndex(index);
        element.FindPropertyRelative("key").stringValue = key;
        element.FindPropertyRelative("gameObject").objectReferenceValue = obj;
    }
}