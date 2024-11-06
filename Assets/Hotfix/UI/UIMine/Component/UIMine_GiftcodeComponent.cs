using System;
using System.Collections;
using System.Collections.Generic;
using ETModel;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIMine_GiftcodeComponentSystem : AwakeSystem<UIMine_GiftcodeComponent>
    {
        public override void Awake(UIMine_GiftcodeComponent self)
        {
            self.Awake();
        }
    }

    public class UIMine_GiftcodeComponent : UIBaseComponent
    {
        private ReferenceCollector rc;
        private Button btnConfirm;
        private InputField giftcodeInput;

        public void Awake()
        {
            InitUI();
            //InitSuperView();
        }

        public override void OnShow(object obj)
        {

            SetUpNav(UIType.UIMine_Giftcode);

        }

        public override void OnHide()
        {

        }



        public override void Dispose()
        {
            if (this.IsDisposed)
                return;

            base.Dispose();
        }

        #region InitUI
        protected virtual void InitUI()
        {

            rc = GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            btnConfirm = rc.Get<GameObject>("creat_btn_creat").GetComponent<Button>();
            giftcodeInput = rc.Get<GameObject>("creat_inputfield_place").GetComponent<InputField>();

            UIEventListener.Get(btnConfirm.gameObject).onClick = onClickConfirm;

            WEB2_store_pay_list.RequestData requestData = new WEB2_store_pay_list.RequestData {
            };

            HttpRequestComponent.Instance.Send(WEB2_store_pay_list.API, WEB2_store_pay_list.Request(requestData), json =>
            {
                var res = WEB2_store_pay_list.Response(json);
                Debug.LogError("store list " + json);
            });
            
        }

        private void onClickConfirm(GameObject go)
        {
            if(giftcodeInput.text.Length == 0)
            {
                UIComponent.Instance.Toast("giftcode is required");
                return;
            }


            WEB2_user_giftcode.RequestData requestData = new WEB2_user_giftcode.RequestData() {
                code = giftcodeInput.text.Trim()
            };
            HttpRequestComponent.Instance.Send(WEB2_user_giftcode.API, WEB2_user_giftcode.Request(requestData), json =>
            {
                var res = WEB2_user_giftcode.Response(json);
                

                if (res.status == 0)
                {
                    UIComponent.Instance.Toast($"nhập code thành công, bạn được cộng {res.data.value}");
                    UIMineModel.mInstance.ObtainUserInfo(pDto =>
                    {
                        GameCache.Instance.gold = pDto.chip;
                        UI uI = UIComponent.Instance.Get(UIType.UIMine);
                        if (null != uI) {
                            UIMineComponent mineComponent = uI.UiBaseComponent as UIMineComponent;
                            mineComponent.ObtainUserInfo();
                        }
                    });

                    giftcodeInput.text = "";
                } else
                {
                    UIComponent.Instance.Toast($"giftcode khong hop le");
                }
            });
        }
        #endregion


    }


}

