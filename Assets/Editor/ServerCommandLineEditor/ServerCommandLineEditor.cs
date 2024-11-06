using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ETModel;
using MongoDB.Bson;
using UnityEditor;
using UnityEngine;

namespace ETEditor
{
	public class ServerCommandLineEditor: EditorWindow
	{
		private const string ConfigDir = @"../Config/StartConfig/";

		private List<string> files;

		private int selectedIndex;

		private string fileName;

		private string newFileName = "";

		private int copyNum = 1;

		private AppType AppType = AppType.None;

		private readonly List<StartConfig> startConfigs = new List<StartConfig>();

		[MenuItem("Tools/Command line configuration")]
		private static void ShowWindow()
		{
			GetWindow(typeof (ServerCommandLineEditor));
		}

		private void OnEnable()
		{
			this.files = this.GetConfigFiles();
			if (this.files.Count > 0)
			{
				this.fileName = this.files[this.selectedIndex];
				this.LoadConfig();
			}
		}

		public void ClearConfig()
		{
			foreach (StartConfig startConfig in this.startConfigs)
			{
				startConfig.Dispose();
			}
			this.startConfigs.Clear();
		}

		private List<string> GetConfigFiles()
		{
			List<string> fs = Directory.GetFiles(ConfigDir).ToList();
			DirectoryInfo directoryInfo = new DirectoryInfo(ConfigDir);
			FileInfo[] fileInfo = directoryInfo.GetFiles();
			fs = fileInfo.Select(x => x.Name).ToList();
			return fs;
		}

		private void LoadConfig()
		{
			string filePath = this.GetFilePath();
			if (!File.Exists(filePath))
			{
				return;
			}

			string s2 = "";
			try
			{
				this.ClearConfig();
				string[] ss = File.ReadAllText(filePath).Split('\n');
				foreach (string s in ss)
				{
					s2 = s.Trim();
					if (s2 == "")
					{
						continue;
					}

					StartConfig startConfig = MongoHelper.FromJson<StartConfig>(s2);
					this.startConfigs.Add(startConfig);
				}
			}
			catch (Exception e)
			{
				Log.Error($"Failed to load configuration! {s2} \n {e}");
			}
		}

		private string GetFilePath()
		{
			return Path.Combine(ConfigDir, this.fileName);
		}

		private void Save()
		{
			string path = this.GetFilePath();
			using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create)))
			{
				foreach (StartConfig startConfig in this.startConfigs)
				{
					sw.Write(MongoHelper.ToJson(startConfig));
					sw.Write('\n');
				}
			}
		}

		private void OnGUI()
		{
			GUILayout.BeginHorizontal();
			string[] filesArray = this.files.ToArray();
			this.selectedIndex = EditorGUILayout.Popup(this.selectedIndex, filesArray);

			string lastFile = this.fileName;
			this.fileName = this.files[this.selectedIndex];

			if (this.fileName != lastFile)
			{
				this.LoadConfig();
			}

			this.newFileName = EditorGUILayout.TextField("File name", this.newFileName);

			if (GUILayout.Button("Add to"))
			{
				this.fileName = this.newFileName;
				this.newFileName = "";
				File.WriteAllText(this.GetFilePath(), "");
				this.files = this.GetConfigFiles();
				this.selectedIndex = this.files.IndexOf(this.fileName);
				this.LoadConfig();
			}

			if (GUILayout.Button("Copy"))
			{
				this.fileName = $"{this.fileName}-copy";
				this.Save();
				this.files = this.GetConfigFiles();
				this.selectedIndex = this.files.IndexOf(this.fileName);
				this.newFileName = "";
			}

			if (GUILayout.Button("Duplicate naming"))
			{
				if (this.newFileName == "")
				{
					Log.Debug("Please enter a new name!");
				}
				else
				{
					File.Delete(this.GetFilePath());
					this.fileName = this.newFileName;
					this.Save();
					this.files = this.GetConfigFiles();
					this.selectedIndex = this.files.IndexOf(this.fileName);
					this.newFileName = "";
				}
			}

			if (GUILayout.Button("Delete"))
			{
				File.Delete(this.GetFilePath());
				this.files = this.GetConfigFiles();
				this.selectedIndex = 0;
				this.newFileName = "";
			}

			GUILayout.EndHorizontal();

			GUILayout.Label("Placement details:");
			for (int i = 0; i < this.startConfigs.Count; ++i)
			{
				StartConfig startConfig = this.startConfigs[i];
				GUILayout.BeginHorizontal();
				GUILayout.Label($"AppId:");
				startConfig.AppId = EditorGUILayout.IntField(startConfig.AppId, GUILayout.Width(30));
				GUILayout.Label($"Server IP:");
				startConfig.ServerIP = EditorGUILayout.TextField(startConfig.ServerIP, GUILayout.Width(100));
				GUILayout.Label($"AppType:");
				startConfig.AppType = (AppType) EditorGUILayout.EnumPopup(startConfig.AppType);

				InnerConfig innerConfig = startConfig.GetComponent<InnerConfig>();
				if (innerConfig != null)
				{
					GUILayout.Label($"Intranet address:");
					innerConfig.Address = EditorGUILayout.TextField(innerConfig.Address, GUILayout.Width(120));
				}

				OuterConfig outerConfig = startConfig.GetComponent<OuterConfig>();
				if (outerConfig != null)
				{
					GUILayout.Label($"External network address:");
					outerConfig.Address = EditorGUILayout.TextField(outerConfig.Address, GUILayout.Width(120));
					GUILayout.Label($"External network address 2:");
					outerConfig.Address2 = EditorGUILayout.TextField(outerConfig.Address2, GUILayout.Width(120));
				}

				ClientConfig clientConfig = startConfig.GetComponent<ClientConfig>();
				if (clientConfig != null)
				{
					GUILayout.Label($"Connection Address:");
					clientConfig.Address = EditorGUILayout.TextField(clientConfig.Address, GUILayout.Width(120));
				}

				HttpConfig httpConfig = startConfig.GetComponent<HttpConfig>();
				if (httpConfig != null)
				{
					GUILayout.Label($"AppId:");
					httpConfig.AppId = EditorGUILayout.IntField(httpConfig.AppId, GUILayout.Width(20));
					GUILayout.Label($"AppKey:");
					httpConfig.AppKey = EditorGUILayout.TextField(httpConfig.AppKey);
					GUILayout.Label($"Url:");
					httpConfig.Url = EditorGUILayout.TextField(httpConfig.Url);
					GUILayout.Label($"ManagerSystemUrl:");
					httpConfig.ManagerSystemUrl = EditorGUILayout.TextField(httpConfig.ManagerSystemUrl);
				}

				DBConfig dbConfig = startConfig.GetComponent<DBConfig>();
				if (dbConfig != null)
				{
					GUILayout.Label($"Connection:");
					dbConfig.ConnectionString = EditorGUILayout.TextField(dbConfig.ConnectionString);

					GUILayout.Label($"DBName:");
					dbConfig.DBName = EditorGUILayout.TextField(dbConfig.DBName);
				}

				if (GUILayout.Button("Delete"))
				{
					this.startConfigs.Remove(startConfig);
					break;
				}
				if (GUILayout.Button("Copy"))
				{
					for (int j = 1; j < this.copyNum + 1; ++j)
					{
						StartConfig newStartConfig = MongoHelper.FromBson<StartConfig>(startConfig.ToBson());
						newStartConfig.AppId += j;
						this.startConfigs.Add(newStartConfig);
					}
					break;
				}

				if (i > 0)
				{
					if (GUILayout.Button("Move Up"))
					{
						StartConfig s = this.startConfigs[i];
						this.startConfigs.RemoveAt(i);
						this.startConfigs.Insert(i - 1, s);
						for (int j = 0; j < startConfigs.Count; ++j)
						{
							this.startConfigs[j].AppId = j + 1;
						}
						break;
					}
				}

				if (i < this.startConfigs.Count - 1)
				{
					if (GUILayout.Button("Move Down"))
					{
						StartConfig s = this.startConfigs[i];
						this.startConfigs.RemoveAt(i);
						this.startConfigs.Insert(i + 1, s);
						for (int j = 0; j < startConfigs.Count; ++j)
						{
							this.startConfigs[j].AppId = j + 1;
						}
						break;
					}
				}
				GUILayout.EndHorizontal();
			}

			GUILayout.Label("");

			GUILayout.BeginHorizontal();
			this.copyNum = EditorGUILayout.IntField("Number of copies: ", this.copyNum);

			GUILayout.Label($"Added AppType:");
			this.AppType = (AppType) EditorGUILayout.EnumPopup(this.AppType);

			if (GUILayout.Button("Add a line of configuration"))
			{
				StartConfig newStartConfig = new StartConfig();

				newStartConfig.AppType = this.AppType;

				if (this.AppType.Is(AppType.Gate | AppType.Realm | AppType.Manager))
				{
					newStartConfig.AddComponent<OuterConfig>();
				}

				if (this.AppType.Is(AppType.Gate | AppType.Realm | AppType.Manager | AppType.Http | AppType.DB | AppType.Map | AppType.Location))
				{
					newStartConfig.AddComponent<InnerConfig>();
				}

				if (this.AppType.Is(AppType.Http))
				{
					newStartConfig.AddComponent<HttpConfig>();
				}

				if (this.AppType.Is(AppType.DB))
				{
					newStartConfig.AddComponent<DBConfig>();
				}
				
				if (this.AppType.Is(AppType.Benchmark))
				{
					newStartConfig.AddComponent<ClientConfig>();
				}
				
				if (this.AppType.Is(AppType.BenchmarkWebsocketServer))
				{
					newStartConfig.AddComponent<OuterConfig>();
				}
				
				if (this.AppType.Is(AppType.BenchmarkWebsocketClient))
				{
					newStartConfig.AddComponent<ClientConfig>();
				}

				this.startConfigs.Add(newStartConfig);
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Save"))
			{
				this.Save();
			}
			
			if (GUILayout.Button("Start up"))
			{
				StartConfig startConfig = null;
				foreach (StartConfig config in this.startConfigs)
				{
					if (config.AppType.Is(AppType.Manager))
					{
						startConfig = config;
					}
				}

				if (startConfig == null)
				{
					Log.Error("No Manager is configured!");
					return;
				}

				string arguments = $"App.dll --appId={startConfig.AppId} --appType={startConfig.AppType} --config=../Config/StartConfig/{this.fileName}";

				ProcessStartInfo info = new ProcessStartInfo("dotnet", arguments) { UseShellExecute = true, WorkingDirectory = @"../Bin/" };
				Process.Start(info);
			}
			GUILayout.EndHorizontal();
		}
		
		private void OnDestroy()
		{
			this.ClearConfig();
		}
	}
}