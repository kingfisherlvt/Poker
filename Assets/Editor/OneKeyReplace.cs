using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
    public class OneKeyReplaceWindow : EditorWindow
    {

        [MenuItem("Tools/Gadgets/Open - Release directory %q")]
        public static void OpenReleaseDirectory()
        {
            System.Diagnostics.Process.Start("explorer.exe", Path.Combine(System.Environment.CurrentDirectory, @"..\Release"));

        }
        [MenuItem("Tools/Gadgets/Clear - Release Directory")]
        public static void ClearReleaseFile()
        {
#if UNITY_IOS
            string path = System.Environment.CurrentDirectory.Replace("Unity", "Release/IOS");
#elif UNITY_ANDROID
          // string path = System.Environment.CurrentDirectory.Replace("Unity", "Release/Android");
          string path = $"{System.Environment.CurrentDirectory}/Release/Android";
#endif
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Debug.Log("The release folder has been deleted");
            }
        }
        [MenuItem("Tools/Gadgets/Open - Local Hot Disk Directory")]
        public static void OpenSDHotfixDirectory()
        {
            if (Directory.Exists(Application.persistentDataPath))
            {
              
                System.Diagnostics.Process.Start("explorer.exe",$@"C:\Users\{System.Environment.UserName}\AppData\LocalLow\{Application.companyName}\{Application.productName}");
            }
            
        }
        [MenuItem("Tools/Gadgets/Clear - Local Hot Disk Directory")]
        public static void DeleteSDHotfixDirectory()
        {
            string path = Application.persistentDataPath;
            if (Directory.Exists(path))
            {
                PlayerPrefs.DeleteAll();
                Directory.Delete(path, true);
                Debug.Log("The hot change sandbox path has been deleted");
            }
        }
       
        [MenuItem("Tools/Gadgets/Clear the StreamingAssets manifest file")]
        public static void ClearManifist()
        {
            string[] files = Directory.GetFiles(Application.streamingAssetsPath, "*.manifest");
            foreach (string file in files)
            {
                File.Delete(file);
            }
            Debug.Log("The list file has been processed");
        }

        //[MenuItem("Tools/Gadgets/删除 Missing Scripts")]
        //public static void RemoveMissingScript()
        //{
           

        //}
    }
}