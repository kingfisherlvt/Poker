using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.tencent.imsdk.unity.types;
using com.tencent.imsdk.unity.callback;
using com.tencent.imsdk.unity;
using com.tencent.imsdk.unity.enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JPush;

public class IMHelper
{
    public delegate void OnReceiveEmojiCb(string emojiId, string sender);
    public delegate void OnReceiveTxtCb(string emojiId, string sender);
    public delegate void OnReceiveAnimojiCb(string emojiId, string sender, string receiver);
    public delegate void OnReceiveVoiceCb(string emojiId, string sender, int time);

    public static OnReceiveEmojiCb onReceiveEmoji;
    public static OnReceiveTxtCb onReceiveTxt;
    public static OnReceiveAnimojiCb onReceiveAnimoji;
    public static OnReceiveVoiceCb onReceiveVoice;

    public static void AddRecvNewMsgCallback()
    {
        try
        {
            TencentIMSDK.AddRecvNewMsgCallback((List<Message> message, string user_data) =>
            {
                foreach (var item in message)
                {
                    Debug.LogError($"TencentIMSDK.RecvNewMsgCallbackStore:{JsonHelper.ToJson(message)}");
                    foreach (var ele in item.message_elem_array)
                    {
                        if (ele.face_elem_index > 0)
                        {
                            onReceiveEmoji?.Invoke(ele.face_elem_index.ToString(), item.message_sender);
                        }
                        else if (!string.IsNullOrEmpty(ele.custom_elem_data))
                        {
                            string[] infos = ele.custom_elem_data.Split(new char[] { '|' });
                            if (infos.Length == 1)
                            {
                                onReceiveTxt?.Invoke(ele.custom_elem_data, item.message_sender);
                            }
                            if (infos.Length == 2)
                            {
                                onReceiveAnimoji?.Invoke(infos[0], item.message_sender, infos[1]);
                            }

                        }
                        else if (!string.IsNullOrEmpty(ele.sound_elem_url))
                        {
                            onReceiveVoice?.Invoke(ele.sound_elem_url, item.message_sender, ele.sound_elem_file_time);
                        }
                    }

                }
            });

        }
        catch (System.Exception e)
        {
            Debug.LogError($"TencentIMSDK.SetRecvNewMsgCallbackStore error:{e.ToString()}");
        }

    }

    public static TIMResult Init(long appid)
    {
        var result = TencentIMSDK.Init(appid, new SdkConfig());
        return result;
    }

 
}
