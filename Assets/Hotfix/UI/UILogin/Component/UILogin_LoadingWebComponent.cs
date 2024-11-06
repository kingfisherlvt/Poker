using UnityEngine;
using UnityEngine.UI;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    [ObjectSystem]
    public class UILogin_LoadingWebComponentSystem : AwakeSystem<UILogin_LoadingWebComponent>
    {
        public override void Awake(UILogin_LoadingWebComponent self)
        {
            self.Awake();
        }
    }

    public class UILogin_LoadingWebComponent : UIBaseComponent
    {
        public sealed class LoadingWebData
        {
            public string mWebUrl { get; set; }
            public string mTitleTxt { get; set; }
        }


        private ReferenceCollector rc;
        //private UniWebView webView;
        private GameObject UIGongPing;
        private GameObject UIErWeiMa;
        private GameObject UIAboutUs;
        private GameObject UIUserLicense;

        public void Awake()
        {
            rc = GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            //webView = rc.Get<GameObject>("UniWebView").GetComponent<UniWebView>();
            UIGongPing = rc.Get<GameObject>("UIGongPing");
            UIErWeiMa = rc.Get<GameObject>("UIErWeiMa");
            UIAboutUs = rc.Get<GameObject>("UIAboutUs");
            UIUserLicense = rc.Get<GameObject>("UIUserLicense");
        }

        public override void OnShow(object obj)
        {
            NativeManager.SafeArea safeArea = NativeManager.Instance.safeArea;
            int realTop = (int)(safeArea.top * 1242 / safeArea.width);
            RectTransform rectTransform = rc.gameObject.GetComponent<RectTransform>();
            int navTop = (int)((146 + realTop) * Screen.height / rectTransform.rect.height) + (int)(safeArea.top);
            if (Application.platform == RuntimePlatform.IPhonePlayer)
                navTop = (int)(safeArea.width * 146 / 1242 + safeArea.top);

            //webView.insets = new UniWebViewEdgeInsets(navTop, 0,0,0);

            //webView.toolBarShow = false;

            UIErWeiMa.SetActive(false);
            UIAboutUs.SetActive(false);
            UIUserLicense.SetActive(false);
            UIGongPing.SetActive(false);
            //webView.gameObject.SetActive(false);

            if (obj != null && obj is LoadingWebData)
            {
                var tDto = obj as LoadingWebData;
                if (tDto.mWebUrl.Contains("chat"))
                {
                    // 客服
                    UIErWeiMa.SetActive(true);
                }
                else if (tDto.mWebUrl.Contains("gameFair"))
                {
                    // 游戏公平
                    UIGongPing.SetActive(true);
                }
                else if (tDto.mWebUrl.Contains("aboutUs"))
                {
                    // 关于我们
                    UIAboutUs.SetActive(true);
                }
                else if (tDto.mWebUrl.Contains("userLicense"))
                {
                    // 用户协议
                    UIUserLicense.SetActive(true);
                }
                else
                {
                    //webView.gameObject.SetActive(true);
                }

                SetUpNav(tDto.mTitleTxt, UIType.UILogin_LoadingWeb);
                //webView.Load(tDto.mWebUrl);
                Log.Debug("url=" + tDto.mWebUrl);
            }
            else
            {//空时
                //webView.Load(GlobalData.Instance.UserAgentURL+UIMineModel.mInstance.GetUrlSuffix());
                SetUpNav(LanguageManager.mInstance.GetLanguageForKey("UIMatchModel_102"), UIType.UILogin_LoadingWeb);
            }

        }

        //获取VIP特权信息
        void FinishTask()
        {
            WEB2_user_task_finish_task_app.RequestData requestData = new WEB2_user_task_finish_task_app.RequestData()
            {
                action = 3
            };
            HttpRequestComponent.Instance.Send(
                WEB2_user_task_finish_task_app.API,
                WEB2_user_task_finish_task_app.Request(requestData),
                this.OpenFinishTask);
        }

        void OpenFinishTask(string resData)
        {
            Log.Debug(resData);
        }

        public override void OnHide()
        {

        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            rc = null;
            //webView.CleanCache();
            //webView = null;
            base.Dispose();
        }
    }
}
