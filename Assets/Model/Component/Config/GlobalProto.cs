﻿namespace ETModel
{
	public class GlobalProto
    {
        public string NetLineSwitchUrl;
        public string AssetBundleServerUrl;
		public string Address;

		public string GetUrl()
		{
			string url = NetLineSwitchComponent.Instance.CurrentServerResDomain();
			//url += "2019/";
#if UNITY_ANDROID
			url += "Android/"; 
#elif UNITY_IOS
            url += "IOS/";
#elif UNITY_WEBGL
			url += "WebGL/";
#elif UNITY_STANDALONE_OSX
			url += "MacOS/";
#else
			url += "PC/";
#endif
			//Log.Debug(url);
			return url;
		}
	}
}
