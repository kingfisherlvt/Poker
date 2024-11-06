//im sdk 插件
using System;
using UnityEngine;
using UnityEngine.UI;
using ETModel;
using BestHTTP;
using System.Threading;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Text;
using System.IO;
using com.tencent.imsdk.unity;
using com.tencent.imsdk.unity.types;
using com.tencent.imsdk.unity.enums;

namespace ETHotfix
{
    public class IMTextInfo
    {
        public int type;//0-Emoji 1-魔法表情
        public string sender;//发送者
        public string receiver;//接受者，魔法表情用
        public string groupID;
        public string content;//表情代号等
    }

    public enum IMOpearteType
    {
        joinGroup = 0,
    }

    public class IMOpearteMes
    {
        public int opearteType;
    }

    public class IMMes{

        public string sender;//发送者
        public int mesType;//0-文本信息 1-音效
        public string mesContent;//{mesType=0 mesContent=文本} {mesType=1 mesContent=soundPath}
        public long duration;
    }

    [ObjectSystem]
    public class IMSdkComponentAwakeSystem : AwakeSystem<IMSdkComponent>
    {
        public override void Awake(IMSdkComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class IMSdkComponentUpdateSystem : UpdateSystem<IMSdkComponent>
    {
        public override void Update(IMSdkComponent self)
        {
            self.Update();
        }
    }
    [ObjectSystem]
    public class IMSdkComponentDestroySystem : DestroySystem<IMSdkComponent>
    {
        public override void Destroy(IMSdkComponent self)
        {
            self.Destroy();
        }

    }

    public class IMSdkComponent : Component 
    {
        public static int SUCCESS = 1000;
        public static int E_NOSDCARD = 1001;
        public static int E_STATE_RECODING = 1002;
        public static int E_UNKOWN = 1003;

        public static IMSdkComponent Instance;
        public bool isRecored;

        private int cacheUid;
        private string cacheToken;
        private int cacheAppid;
        private string cacheGid;//若存在数据，则发送给对应接口
        private string cacheVoicePath = "";

        const float pt = 5;//5秒检测一次
        private float protectTimer;
        private bool isKeepAlive = false;


        public bool micConnected = false;
        private int minFreq, maxFreq;//最小和最大频率
        public AudioClip RecordedClip;//录音
        public bool isRecording = false;

        public void RegisiterCallback()
        {
            try
            {
                TencentIMSDK.AddRecvNewMsgCallback((List<Message> message, string user_data) =>
                {
                    foreach (var item in message)
                    {
                        Log.Debug($"TencentIMSDK.RecvNewMsgCallbackStore:{JsonHelper.ToJson(message)}");
                        foreach (var ele in item.message_elem_array)
                        {
                            if (ele.face_elem_index > 0)
                            {
                                GameCache.Instance.CurGame.onReceiveEmoji(ele.face_elem_index.ToString(), item.message_sender);
                            }
                            else if (!string.IsNullOrEmpty(ele.custom_elem_data))
                            {
                                string[] infos = ele.custom_elem_data.Split(new char[] { '|' });
                                if (infos.Length == 1)
                                {
                                    GameCache.Instance.CurGame.onReceiveEmoji(ele.custom_elem_data, item.message_sender);
                                }
                                if (infos.Length == 2)
                                {
                                    GameCache.Instance.CurGame.onReceiveAnimoji(infos[0], item.message_sender, infos[1]);
                                }

                            }
                            else if (!string.IsNullOrEmpty(ele.sound_elem_url))
                            {
                                Log.Error($"onReceiveVoice:{ele.sound_elem_url}");
                                GameCache.Instance.CurGame.onReceiveVoice(ele.sound_elem_url, item.message_sender, ele.sound_elem_file_time);
                            }
                        }

                    }
                });

            }
            catch (System.Exception e)
            {
                Log.Debug($"TencentIMSDK.SetRecvNewMsgCallbackStore error:{e.ToString()}");
            }
        }

        public void Awake() {

            if (Instance == null)
            {
                Log.Debug("im sdk init");
                Instance = this;
                isRecored = false;
                
               
            }
            //Login();
        }

        public void Destroy()
        {
            LoginOut();
        }

        public void Update() {

            //监听IM的状态
            if (isKeepAlive && GameCache.Instance.imstate > GameCache.ImState.enNULL)
            {
                protectTimer += Time.deltaTime;
                if (protectTimer > pt)
                {
                    protectTimer = 0;
                    var state = GetImState();
                    if (state < GameCache.ImState.enLogin)
                    {
                        SdkLogin();
                    }
                }
            }
        }

        /// <summary>
        /// 和服务器请求token后登录IM
        /// </summary>
        public void Login() {
            if (GameCache.Instance.imstate >= GameCache.ImState.enInit && GameCache.Instance.imstate < GameCache.ImState.enLogin)
            {
                SdkLogin();
                return;
            }
            if (GameCache.Instance.imstate >= GameCache.ImState.enLogin)
            {
                return;
            }
            Log.Error("IMSdkComponent.Login");
            TalkingDataSdkComponent.Instance.UploadSdkAnalysis("im - token req");
            WEB2_im_gentoken.RequestData im_req = new WEB2_im_gentoken.RequestData()
            {
                //userId = GameCache.Instance.nUserId
            };
            HttpRequestComponent.Instance.Send(WEB2_im_gentoken.API, WEB2_im_gentoken.Request(im_req),
                (resData)=> {
                    Log.Error($"IMSdkComponent.api/im/gentoken res:{resData}");
                    WEB2_im_gentoken.ResponseData im_res = WEB2_im_gentoken.Response(resData);
                    if (im_res.status == 0)
                    {
                        TalkingDataSdkComponent.Instance.UploadSdkAnalysis("im - token res");
                        //IM登录
                        cacheGid = !string.IsNullOrEmpty(im_res.data.gid) ? im_res.data.gid : GameCache.Instance.room_id.ToString();
                        cacheToken = im_res.data.token;
                        cacheUid = GameCache.Instance.nUserId; //im_res.data.userId;
                        cacheAppid = GlobalData.IM_APPID_0;
                        Log.Error("TencentIMSDK.Init");
                        var result = IMHelper.Init(cacheAppid);

                        if (result != TIMResult.TIM_SUCC)
                        {
                            GameCache.Instance.imstate = GameCache.ImState.enNULL;
                            Log.Error("im sdk 初始化失败，{result}");
                            //UIComponent.Instance.Toast("im sdk 初始化失败");
                            return;
                        }
                        Log.Error("TencentIMSDK.Init suc");
                        RegisiterCallback();
                        GameCache.Instance.imstate = GameCache.ImState.enInit;
                        SdkLogin();
                        
                    }
                    else
                    {
                        Log.Debug($"im_res.msg = {im_res.msg}");
                        //UIComponent.Instance.Toast(im_res.status);
                    }
                });
        }

        /// <summary>
        /// 新账号加入唯一对话组后需告知服务器
        /// </summary>
        public void UpImInfo()
        {

            WEB2_im_upim_info.RequestData upim_req = new WEB2_im_upim_info.RequestData()
            {
                userId = GameCache.Instance.nUserId,
                groupId = cacheGid
            };
            HttpRequestComponent.Instance.Send(WEB2_im_upim_info.API, WEB2_im_upim_info.Request(upim_req),
                (resData) => {

                    WEB2_im_upim_info.ResponseData upim_res = WEB2_im_upim_info.Response(resData);
                    if (upim_res.status == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        Log.Debug($"im_res.msg = {upim_res.msg}");
                        //UIComponent.Instance.Toast(upim_res.status);
                    }
                });
        }

        void SdkLogin()
        {
            if (GameCache.Instance.imstate == GameCache.ImState.enNULL ||GameCache.Instance.imstate >= GameCache.ImState.enLogin)
                return;
            TencentIMSDK.Login(cacheUid.ToString(), cacheToken, (int code, string desc, string data, string user_data) =>
            {
                Log.Error("cache UID " + cacheUid.ToString());
                if (code == 0)
                {
                    GameCache.Instance.imstate = GameCache.ImState.enLogin;

                    //ApplyJoinGroup(cacheGid);
                    isKeepAlive = true;
                }
                else {
                    GameCache.Instance.imstate = GameCache.ImState.enInit;
                    Log.Error($"im login fail:{code},{desc}");
                }
            });
        }
        /// <summary>
        /// 调用原生登录退出
        /// </summary>
        public void LoginOut() {

            if (GameCache.Instance.imstate <= GameCache.ImState.enLogout)
                return;
            TencentIMSDK.Logout((int code, string desc, string data, string user_data) =>
            {
                if (code == 0)
                {
                    GameCache.Instance.imstate = GameCache.ImState.enLogout;
                }
                else
                {
                    //UIComponent.Instance.Toast($"im loginout fail:{desc}");
                }
            });
            isKeepAlive = false;
        }
        public void GroupQuit(Action cb)
        {
            if (GameCache.Instance.imstate <= GameCache.ImState.enLogout)
                return;
            TencentIMSDK.GroupQuit(cacheGid, (int code, string desc, string data, string user_data) =>
            {
                if (code == 0)
                {
                    GameCache.Instance.imstate = GameCache.ImState.enLogout;
                }
                else
                {
                    //UIComponent.Instance.Toast($"im group({cacheGid}) quit fail:{desc}");
                }
                cb?.Invoke();
            });
        }
        
        public void SendGroupMsg(string content, Action<int, string> callback)
        {
            var message = new Message
            {
                message_conv_type = TIMConvType.kTIMConv_Group,
                message_cloud_custom_str = "unity local custom data",
                message_elem_array = new List<Elem> { new Elem
                {
                    elem_type = TIMElemType.kTIMElem_Custom,
                    custom_elem_data = content,
                    custom_elem_desc = "",
                    custom_elem_ext = "",
                } },
                message_priority = TIMMsgPriority.kTIMMsgPriority_Normal,
                message_is_excluded_from_unread_count = false,
                message_is_online_msg = true,
            };
            StringBuilder messageId = new StringBuilder(128);
            message.message_conv_id = cacheGid;
            message.message_conv_type = TIMConvType.kTIMConv_Group;
            TIMResult res = TencentIMSDK.MsgSendMessage(cacheGid, TIMConvType.kTIMConv_Group, message, messageId, (int code, string desc, string data, string user_data)=> { 
                if (code == 0)
                {
                    // 发送成功
                }
                callback?.Invoke(code, desc);
            });

        }

        void SendGroupFace(int faceIndex, Action<int, string> callback)
        {
            var message = new Message
            {
                message_conv_type = TIMConvType.kTIMConv_Group,
                message_cloud_custom_str = "unity local face data",
                message_elem_array = new List<Elem>{new Elem
                  {
                    elem_type = TIMElemType.kTIMElem_Face,
                    face_elem_index = faceIndex,
                    face_elem_buf = "",
                  }},
                message_priority = TIMMsgPriority.kTIMMsgPriority_Normal,
                message_is_online_msg = true
            };
            StringBuilder messageId = new StringBuilder(128);
            message.message_conv_id = this.cacheGid;
            message.message_conv_type = TIMConvType.kTIMConv_Group;
            TIMResult res = TencentIMSDK.MsgSendMessage(this.cacheGid, TIMConvType.kTIMConv_Group, message, messageId, (int code, string desc, string data, string user_data) => {
                callback?.Invoke(code, desc);
            });
        }

        void SendGroupVoice(Action<int, string> callback)
        {
            var message = new Message
            {
                message_cloud_custom_str = "unity local sound data",
                message_elem_array = new List<Elem>{new Elem
                  {
                    elem_type = TIMElemType.kTIMElem_Sound,
                    sound_elem_file_path = cacheVoicePath,
                    sound_elem_file_time = (int)RecordedClip.length*1000
                    }
                },
                message_priority = TIMMsgPriority.kTIMMsgPriority_High,
                message_is_excluded_from_unread_count = false,
                message_is_online_msg = true
            };
            StringBuilder messageId = new StringBuilder(128);
            message.message_conv_id = cacheGid;
            message.message_conv_type = TIMConvType.kTIMConv_Group;
            TIMResult res = TencentIMSDK.MsgSendMessage(cacheGid, TIMConvType.kTIMConv_Group, message, messageId, (int code, string desc, string data, string user_data) => {
                callback?.Invoke(code, desc);
                if (code != 0)
                {
                    Log.Debug($"发送语音失败,{code},{desc}");
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GameCache.ImState GetImState() {

            return GameCache.Instance.imstate;//TencentIMSDK.GetLoginStatus();
        }

        /// <summary>
        /// 开始录音
        /// </summary>
        public bool StartRecord() {

            if (micConnected == false)
            {
                Application.RequestUserAuthorization(UserAuthorization.Microphone);
                if (Microphone.devices.Length <= 0)
                {
                    Log.Error("缺少麦克风设备！");
                }
                else
                {
                    Log.Debug("设备名称为：" + Microphone.devices[0].ToString() + "请点击Start开始录音！");
                    micConnected = true;
                    Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);
                    if (minFreq == 0 && maxFreq == 0)
                    {
                        maxFreq = 44100;
                    }
                    if (minFreq > 22050)
                        maxFreq = 22050;
                }
            }
            
            if (micConnected)
            {
                if (!Microphone.IsRecording(null))
                {
                    RecordedClip = Microphone.Start(null, false, 60, maxFreq);
                    isRecording = true;
                    cacheVoicePath = "";
                }
                return true;
            }
           
            return false;
        }

        /// <summary>
        /// 停止录音  
        /// </summary>
        /// <param name="isSend">是否发送到聊天组</param>
        /// <returns>音频的毫秒数，如果返回0， 表示录制失败, 或者时间不够</returns>
        public long StopRecord(bool isSend, Action<int, string> callback) {

            if (isRecording)
            {
                Microphone.End(null);
                isRecording = false;
                //SaveMp3(ref RecordedClip);
                try {
                    cacheVoicePath = AudioUtil.SaveRecoderAudio(ref RecordedClip);
                    if (isSend && !string.IsNullOrEmpty(cacheVoicePath))
                    {
                        SendGroupVoice(callback);
                    }
                } catch (System.Exception e) {
                    Log.Error("StopRecord:" + e.ToString());
                }
                
                
                //Thread thread = new Thread(() =>
                //{
                //    Save(data);
                //    //SaveMp3(ref RecordedClip);
                //    if (isSend)
                //    {
                //        SendGroupVoice(callback);
                //    }

                //});
                //thread.Start();
            }
            return 0;
        }
        public void PauseRecord() {

            //if (Application.platform == RuntimePlatform.Android)
            //{

            //    NativeManager.OnFuncCall("PauseRecord");
            //}
            //else if (Application.platform == RuntimePlatform.IPhonePlayer)
            //{

            //}
        }

        public void ContinueRecord() {

            //if (Application.platform == RuntimePlatform.Android)
            //{

            //    NativeManager.OnFuncCall("ContinueRecord");
            //}
            //else if (Application.platform == RuntimePlatform.IPhonePlayer)
            //{

            //}
        }

        /// <summary>
        /// 播放录音音效
        /// </summary>
        public void PlayVoice(string path) {

            if (Application.platform == RuntimePlatform.Android)
            {

                NativeManager.OnFuncCall("PlayVoice", path);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {

                NativeManager.PlayVoice(path);
            }
        }

        /// <summary>
        /// 停止播放录音音效
        /// </summary>
        public void StopVoice()
        {

            if (Application.platform == RuntimePlatform.Android)
            {

                NativeManager.OnFuncCall("StopVoice");
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {

                NativeManager.StopVoice();
            }
        }

        /// <summary>
        /// 停止播放录音音效
        /// </summary>
        public bool IsPlayingVoice()
        {

            if (Application.platform == RuntimePlatform.Android)
            {

                return NativeManager.OnFuncCall<bool>("IsPlayingVoice");
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {

                return NativeManager.IsPlayingVoice();
            }
            return false;
        }

        /// <summary>
        /// 设置播放音量  同步牌局音量
        /// </summary>
        /// <param name="volume"></param>
        public void SetVolume(float volume)
        {

            if (Application.platform == RuntimePlatform.Android)
            {

                NativeManager.OnFuncCall("setVolume", volume);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {

                NativeManager.SetVolume(volume);
            }
        }

        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="str"></param>
        public void SendMessage(string content) {

            IMTextInfo info = new IMTextInfo()
            {
                sender = GameCache.Instance.nUserId.ToString(),
                receiver = GameCache.Instance.nUserId.ToString(),
                groupID = cacheGid,
                type = 0,
                content = content
            };
            string infoJsonStr = JsonHelper.ToJson(info);
            Log.Debug("表情发送：" + content);
            if (Application.platform == RuntimePlatform.Android)
            {
                //IMNativeSDK.
                //NativeManager.OnFuncCall("IMSendMessage", infoJsonStr);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                NativeManager.IMSendMessage(infoJsonStr);
            }
        }

        /// <summary>
        /// 加入对话组 不带发送操作
        /// </summary>
        /// <param name="groupId"></param>
        public void ApplyJoinGroup(string groupId) {
        
            //gid 不为空时 加入对话组
            if (!string.IsNullOrEmpty(groupId))
            {
                TencentIMSDK.GroupJoin(groupId, "", (int code, string desc, string data, string user_data) =>
                {
                    if (code == 0)
                    {
                        Log.Debug($"im join group {groupId} success");
                    }
                    else {
                        //UIComponent.Instance.Toast($"im join group {groupId} fail");
                    }
                });
            }
        }

        /// <summary>
        /// 设置对话组
        /// </summary>
        /// <param name="groupId"></param>
        public void SetConversation(string groupId) {
            //ApplyJoinGroup(cacheGid);
            Login();
        }

        /// <summary>
        /// 离开对话组
        /// </summary>
        public void ClearConversation() {
            // GroupQuit(null);
            LoginOut();
        }
    }
    
}
