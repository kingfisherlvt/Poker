﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    public class CPErrorCode
    {
        private static StringBuilder sBuilder;
        public static string ErrorDescription(int errorCode)
        {
            string errDes = LanguageManager.Get($"error{errorCode}");
            // string errDes = $"{LanguageManager.Get($"error{errorCode}")}[{errorCode}]";
            if (string.IsNullOrEmpty(errDes))
            {
                errDes = $"{LanguageManager.Get("errorDefault")}({errorCode})";
            }
            return errDes;
        }

        public static string RoomErrorDescription(int tcpCode, int errorCode)
        {
            string errDes = LanguageManager.Get($"roomError{tcpCode}_{errorCode}");
            // string errDes = $"{LanguageManager.Get($"roomError{tcpCode}_{errorCode}")}[{tcpCode}][{errorCode}]";
            if (string.IsNullOrEmpty(errDes))
            {
                errDes = $"{LanguageManager.Get("errorDefault")}({errorCode})";
                // errDes = $"{LanguageManager.Get("errorDefault")}[{tcpCode}][{errorCode}]";
            }
            return errDes;
        }

        public static string LanguageDescription(int LanguageCode, List<object> strParams = null)
        {
            string errDes = LanguageManager.Get($"adaptation{LanguageCode}");
            if (string.IsNullOrEmpty(errDes))
            {
                errDes = $"{LanguageManager.Get("LanguageCodeIsNull")}({LanguageCode})";
            }

            if (null == strParams)
            {
                return errDes;
            }

            if (strParams.Count == 1)
            {
                errDes = errDes.Replace("##", strParams[0].ToString());
                return errDes;
            }

            if (strParams.Count > 1)
            {

                string[] sl = errDes.Split(new string[] { "##" }, StringSplitOptions.None);//把多语言配置的长字符分割
                if (null == sBuilder)
                    sBuilder = new StringBuilder();
                if (sBuilder.Length > 0)
                    sBuilder.Clear();
                // string newStr = "";
                for (int i = 0; i < sl.Length; i++)
                {
                    sBuilder.Append(sl[i]);
                    if (i < strParams.Count)
                        sBuilder.Append(strParams[i]);

                }

                return sBuilder.ToString();
            }

            return errDes;

        }
    }
}
