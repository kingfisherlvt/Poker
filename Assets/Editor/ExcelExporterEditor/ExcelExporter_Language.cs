﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ETModel;
using MongoDB.Bson;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;
using UnityEngine;

public class ExcelExporter_Language
{
	[MenuItem("Tools/Export Configuration/Multi-language Configuration")]
	private static void ExportLanguage()
	{
        ExcelExporter_Language exporte = new ExcelExporter_Language();
        exporte.ExportAll();
    }

    private const string exportDir = "./Assets/Res/Config";
    private const string ExcelPath = "../Excel";

	private ExcelMD5Info md5Info;

	

	private void ExportAll()
	{
		string md5File = Path.Combine(ExcelPath, "md5.txt");
        md5File = md5File.Replace('\\', '/');
        if (!File.Exists(md5File))
		{
			this.md5Info = new ExcelMD5Info();
		}
		else
		{
			this.md5Info = MongoHelper.FromJson<ExcelMD5Info>(File.ReadAllText(md5File));
		}

        string filePath = Path.Combine(ExcelPath, "Language.xlsx");
        filePath = filePath.Replace('\\', '/');
        string fileName = Path.GetFileName(filePath);
        string oldMD5 = this.md5Info.Get(fileName);
        string md5 = MD5Helper.FileMD5(filePath);
        this.md5Info.Add(fileName, md5);
        if (md5 == oldMD5)
        {
            Log.Info("Md5 ko thay đổi, file ko bị chỉnh sửa, ngừng export");
            return;
        }

        Export(filePath);

        File.WriteAllText(md5File, this.md5Info.ToJson());

		Log.Info("Multi-language guide completed");
		AssetDatabase.Refresh();
	}

	private void Export(string fileName)
	{
		XSSFWorkbook xssfWorkbook;
		using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			xssfWorkbook = new XSSFWorkbook(file);
		}

        ISheet sheet = xssfWorkbook.GetSheetAt(0);
        string[] names = new string[4] { "ZH", "TW", "EN", "VI" };
        for (int i = 0; i < 4; ++i)
        {
            string protoName = Path.GetFileNameWithoutExtension($"USER_{names[i]}");
            Log.Info($"{protoName}Start of table introduction");
            string exportPath = Path.Combine(exportDir, $"{protoName}.txt");
            exportPath = exportPath.Replace('\\', '/');
            using (FileStream txt = new FileStream(exportPath, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(txt))
            {
                ExportSheet(sheet, sw, i);
            }
            Log.Info($"{protoName}Import table completed");
        }

    }

	private void ExportSheet(ISheet sheet, StreamWriter sw, int index)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i <= sheet.LastRowNum; ++i)
        {
            string keyString = GetCellString(sheet, i, 0);
            string valueString = GetCellString(sheet, i, index+1);
            if (string.IsNullOrEmpty(keyString) || string.IsNullOrWhiteSpace(keyString))
            {
                sb.Append("\n");
            }
            else
            {
                sb.Append(keyString);
                sb.Append("=");
                sb.Append(valueString);
                sb.Append("\n");
            }
        }
        sw.WriteLine(sb.ToString());
    }


	private static string GetCellString(ISheet sheet, int i, int j)
	{
		return sheet.GetRow(i)?.GetCell(j)?.ToString() ?? "";
	}

}
