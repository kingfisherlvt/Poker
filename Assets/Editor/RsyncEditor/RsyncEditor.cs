using System.Diagnostics;
using System.IO;
using ETModel;
using UnityEditor;
using UnityEngine;

namespace ETEditor
{
	public class RsyncEditor: EditorWindow
	{
		private const string ConfigFile = @"..\Tools\cwRsync\Config\rsyncConfig.txt";
		private RsyncConfig rsyncConfig;
		private bool isFold = true;

		[MenuItem("Tools/Rsync")]
		private static void ShowWindow()
		{
			GetWindow(typeof (RsyncEditor));
		}

		private void OnEnable()
		{
			if (!File.Exists(ConfigFile))
			{
				this.rsyncConfig = new RsyncConfig();
				return;
			}
			string s = File.ReadAllText(ConfigFile);
			this.rsyncConfig = MongoHelper.FromJson<RsyncConfig>(s);
		}

		private void OnGUI()
		{
			rsyncConfig.Host = EditorGUILayout.TextField("Server address", rsyncConfig.Host);
			rsyncConfig.Account = EditorGUILayout.TextField("Account (must be an existing Linux account)", rsyncConfig.Account);
			rsyncConfig.Password = EditorGUILayout.TextField("password", rsyncConfig.Password);
			rsyncConfig.RelativePath = EditorGUILayout.TextField("Relative Path", rsyncConfig.RelativePath);

			this.isFold = EditorGUILayout.Foldout(isFold, $"Exclusion List:");

			if (!this.isFold)
			{
				for (int i = 0; i < this.rsyncConfig.Exclude.Count; ++i)
				{
					GUILayout.BeginHorizontal();
					this.rsyncConfig.Exclude[i] = EditorGUILayout.TextField(this.rsyncConfig.Exclude[i]);
					if (GUILayout.Button("Delete"))
					{
						this.rsyncConfig.Exclude.RemoveAt(i);
						break;
					}
					GUILayout.EndHorizontal();
				}
			}

			if (GUILayout.Button("Add Exclusions"))
			{
				this.rsyncConfig.Exclude.Add("");
			}

			if (GUILayout.Button("Save"))
			{
				File.WriteAllText(ConfigFile, MongoHelper.ToJson(this.rsyncConfig));
				using (StreamWriter sw = new StreamWriter(new FileStream(@"..\Tools\cwRsync\Config\exclude.txt", FileMode.Create)))
				{
					foreach (string s in this.rsyncConfig.Exclude)
					{
						sw.Write(s + "\n");
					}
				}

				File.WriteAllText($@"..\Tools\cwRsync\Config\rsync.secrets", this.rsyncConfig.Password);
				File.WriteAllText($@"..\Tools\cwRsync\Config\rsyncd.secrets", $"{this.rsyncConfig.Account}:{this.rsyncConfig.Password}");

				string rsyncdConf = "uid = 0\n" + "gid = 0\n" + "use chroot = no\n" + "max connections = 100\n" + "read only = no\n" + "write only = no\n" +
				                    "log file =/var/log/rsyncd.log\n" + "fake super = yes\n" + "[Upload]\n" + $"path = /home/{this.rsyncConfig.Account}/\n" + 
									$"auth users = {this.rsyncConfig.Account}\n" + "secrets file = /etc/rsyncd.secrets\n" + "list = yes";
				File.WriteAllText($@"..\Tools\cwRsync\Config\rsyncd.conf", rsyncdConf);
			}

			if (GUILayout.Button("同步"))
			{
				string arguments =
						$"-vzrtopg --password-file=./Tools/cwRsync/Config/rsync.secrets --exclude-from=./Tools/cwRsync/Config/exclude.txt --delete ./ {this.rsyncConfig.Account}@{this.rsyncConfig.Host}::Upload/{this.rsyncConfig.RelativePath} --chmod=ugo=rwX";
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = @".\Tools\cwRsync\rsync.exe";
				startInfo.Arguments = arguments;
				startInfo.UseShellExecute = true;
				startInfo.WorkingDirectory = @"..\";
				Process p = Process.Start(startInfo);
				p.WaitForExit();
				Log.Info($"{startInfo.FileName} {startInfo.Arguments}");
				Log.Info("Synchronization completed!");
			}
		}
	}
}