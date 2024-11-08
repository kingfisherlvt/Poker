﻿using System;
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
	public class ServerManagerEditor: EditorWindow
	{
		private string managerAddress;
		private string account;
		private string password;

		[MenuItem("Tools/Server Management Tools")]
		private static void ShowWindow()
		{
			GetWindow(typeof (ServerManagerEditor));
		}

		private void OnGUI()
		{
			GUILayout.BeginHorizontal();
			
			GUILayout.Label("Manager external network address:");
			managerAddress = EditorGUILayout.TextField(this.managerAddress);
			
			GUILayout.Label("Account number:");
			this.account = GUILayout.TextField(this.account);
			
			GUILayout.Label("Password:");
			this.password = GUILayout.TextField(this.password);
			
			if (GUILayout.Button("Reload"))
			{
				if (!Application.isPlaying)
				{
					Log.Error($"Reload must start the client first!");
					return;
				}

				Reload(this.managerAddress, this.account, this.password);
			}
			
			GUILayout.EndHorizontal();
		}

		private static async void Reload(string address, string account, string password)
		{
			using (Session session = Game.Scene.GetComponent<NetOuterComponent>().Create(address))
			{
				try
				{
					await session.Call(new C2M_Reload() {Account = account, Password = password});	
					Log.Info($"Reload server successfully!");
				}
				catch (Exception e)
				{
					Log.Error(e);
				}
			}
		}
	}
}