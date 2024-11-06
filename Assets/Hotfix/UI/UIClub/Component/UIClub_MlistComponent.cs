using System;
using System.Collections.Generic;
using System.Net;
using BestHTTP;
using DG.Tweening;
using ETModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIClub_MlistComponentSystem : AwakeSystem<UIClub_MlistComponent>
    {
        public override void Awake(UIClub_MlistComponent self)
        {
            self.Awake();
        }
    }

    public class UIClub_MlistComponent : UIBaseComponent
    {
        ReferenceCollector rc;
        int iClubId;
        CZScrollRect scrollcomponent;
        List<WEB2_club_mlist.DataElement> clubmlist_list;
        List<WEB2_club_mlist.DataElement> tempclubmlist_list;

        private int myUserType;
        private Vector2 downPoint;
        private RectTransform moveTrans;
        private float cacheX = 0;
        private float cacheY = 0;

        public void Awake()
        {
            rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            scrollcomponent = new CZScrollRect();
            scrollcomponent.prefab = rc.Get<GameObject>(UIClubAssitent.mlist_info);
            scrollcomponent.scrollRect = rc.Get<GameObject>(UIClubAssitent.mlist_scrollview).GetComponent<ScrollRect>();
            scrollcomponent.onScrollObj = OnScrollObj;
            scrollcomponent.interval = 230;
            scrollcomponent.limitNum = 12;
            scrollcomponent.Init();
        }

        public override void OnShow(object obj)
        {
            SetUpNav(UIType.UIClub_Mlist);

            object[] arr = obj as object[];
            RefreshView((int)arr[0], arr[1] as WEB2_club_mlist.ResponseData);

            var inputfield = rc.Get<GameObject>(UIClubAssitent.mlist_inputfield_query).GetComponent<InputField>();
            inputfield.onValueChanged.RemoveAllListeners();
            inputfield.onValueChanged.AddListener((value) => {

                Search(value);
            });

            UITrManager.Instance.Append(UIComponent.Instance.Get(UIType.UIClub_Info).GameObject, rc.gameObject);
        }

        public override void OnHide()
        {
            rc.Get<GameObject>(UIClubAssitent.mlist_inputfield_query).GetComponent<InputField>().onValueChanged.RemoveAllListeners();
            UITrManager.Instance.Remove(rc.gameObject);
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            base.Dispose();
            UITrManager.Instance.Remove(rc.gameObject);
            rc = null;
        }

        public void RefreshView(int clubid, WEB2_club_mlist.ResponseData res)
        {
            iClubId = clubid;
            rc.Get<GameObject>(UIClubAssitent.mlist_inputfield_query).GetComponent<InputField>().text = string.Empty;
            clubmlist_list = res.data;
            //clubmlist_list.Sort((a, b) =>
            //{
            //    if (a.userType == b.userType) return 0;
            //    if (a.userType == 1)
            //        return -1;
            //    if (a.userType == 4 && (b.userType == 2 || b.userType == 5))
            //        return -1;
            //    if (a.userType == 2 && b.userType == 5)
            //        return -1;
            //    return 1;
            //});
            tempclubmlist_list = clubmlist_list;

            myUserType = 5;
            for (int i = 0; i < clubmlist_list.Count; i++)
            {
                if (GameCache.Instance.nUserId == clubmlist_list[i].userId)
                {
                    myUserType = clubmlist_list[i].userType;
                }
            }
            scrollcomponent.Refresh(res.data.Count);
        }

        void Search(string sear)
        {
            tempclubmlist_list = new List<WEB2_club_mlist.DataElement>();
            if (sear == "" || sear == string.Empty)
            {

                for (int i = 0; i < clubmlist_list.Count; ++i)
                {

                    tempclubmlist_list.Add(clubmlist_list[i]);
                }
            }
            else
            {
                for (int i = 0; i < clubmlist_list.Count; ++i)
                {

                    if (clubmlist_list[i].nickName.IndexOf(sear) >= 0)
                    {

                        tempclubmlist_list.Add(clubmlist_list[i]);
                    }
                }
            }
            scrollcomponent.Refresh(tempclubmlist_list.Count);
        }

        void OnScrollObj(GameObject obj, int index)
        {
            string user_id = tempclubmlist_list[index].uid;
            string headerId = tempclubmlist_list[index].userHead;

            //Log.Debug($"index = {index} headerId = {headerId}");
            Head h = HeadCache.GetHead(eHeadType.USER, user_id);
            if (h.headId == string.Empty || h.headId != headerId)
            {
                obj.transform.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = WebImageHelper.GetDefaultHead();//未加载图片时先显示默认图片
                WebImageHelper.GetHeadImage(headerId, (t2d) => {

                    //Log.Debug($"index = {index} 加载成功 headerId = {headerId} objindex = {scrollcomponent.GetObjIndex(obj)}");
                    if (obj != null && scrollcomponent.GetObjIndex(obj) == index) {

                        obj.transform.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = t2d;
                    }
                    //缓存头像
                    h.headId = headerId;
                    h.t2d = t2d;
                });
            }
            else
            {
                //已存在图片
                obj.transform.GetChild(0).GetChild(0).GetComponent<RawImage>().texture = h.t2d;
            }

            //Debug.Log($"OnScrollObj -- {obj.name} index = {index}");
            string nickName = tempclubmlist_list[index].nickName;
            int type = tempclubmlist_list[index].userType;
            string userid = tempclubmlist_list[index].uid;
            string userHead = tempclubmlist_list[index].userHead;

            obj.transform.GetChild(1).GetComponent<Text>().text = $"{nickName}";
            Text languageText = obj.transform.GetChild(2).Find("Text").GetComponent<Text>();
            obj.transform.GetChild(2).gameObject.SetActive(true);
            string key = "";
            switch (type)
            {
                case 1: //创建者
                    key = "club_tribe_6";
                    break;
                case 2: //普通成员
                    key = "club_tribe_22";
                    break;
                case 3: //待加入
                    key = "club_tribe_21";
                    break;
                case 4: //管理者
                    key = "club_tribe_23";
                    break;
                case 5: //待加入
                    key = "club_tribe_21";
                    break;
                default:
                    break;
            }
            languageText.text = LanguageManager.Get(key);

            RectTransform rectTransform = obj.transform.GetChild(6).GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            if (myUserType == 4 || myUserType == 1)
            {
                if ((myUserType == 4 && type != 5) || type == 1)
                {
                    UIEventListener.Get(obj.transform.GetChild(5).gameObject).onClick = (go) =>
                    {
                        Game.EventSystem.Run(UIClubEventIdType.CLUB_MLISTINFO, userid);
                    };
                    return;
                }
                UIEventListener.Get(obj.transform.GetChild(5).gameObject).onClick = null;

                UIScrollEventListener listener = UIScrollEventListener.Get(obj.transform.GetChild(5).gameObject);

                rectTransform.GetChild(0).gameObject.SetActive(type == 5);
                rectTransform.GetChild(1).gameObject.SetActive(type == 2);
                rectTransform.GetChild(2).gameObject.SetActive(type == 4);

                UIEventListener.Get(rectTransform.GetChild(0).GetChild(0).gameObject).onClick = (go) =>
                {
                    // 同意按钮
                    var web = new WEB_club_check.RequestData()
                    {
                        clubId = iClubId.ToString(),
                        userId = tempclubmlist_list[index].userId.ToString(),
                        checkStatus = 2.ToString(),
                    };
                    HttpRequestComponent.Instance.Send(WEB_club_check.API, WEB_club_check.Request(web), json =>
                    {
                        Refresh();
                    });
                };

                UIEventListener.Get(rectTransform.GetChild(0).GetChild(1).gameObject).onClick = (go) =>
                {
                    // 拒绝按钮
                    var web = new WEB_club_check.RequestData()
                    {
                        clubId = iClubId.ToString(),
                        userId = tempclubmlist_list[index].userId.ToString(),
                        checkStatus = 1.ToString(),
                    };
                    HttpRequestComponent.Instance.Send(WEB_club_check.API, WEB_club_check.Request(web), json =>
                    {
                        Refresh();
                    });
                };

                UIEventListener.Get(rectTransform.GetChild(1).gameObject).onClick = (go) =>
                {
                    // 设置管理员
                    var web = new WEB_club_setAdmin.RequestData()
                    {
                        clubId = iClubId.ToString(),
                        userId = tempclubmlist_list[index].userId.ToString(),
                        type = 4.ToString(),
                    };
                    HttpRequestComponent.Instance.Send(WEB_club_setAdmin.API, WEB_club_setAdmin.Request(web), json =>
                    {
                        Refresh();
                    });
                };

                UIEventListener.Get(rectTransform.GetChild(2).gameObject).onClick = (go) =>
                {
                    // 取消为管理员
                    var web = new WEB_club_setAdmin.RequestData()
                    {
                        clubId = iClubId.ToString(),
                        userId = tempclubmlist_list[index].userId.ToString(),
                        type = 2.ToString(),
                    };
                    HttpRequestComponent.Instance.Send(WEB_club_setAdmin.API, WEB_club_setAdmin.Request(web), json =>
                    {
                        Refresh();
                    });
                };

                listener.onBeginDrag = (eventData) =>
                {
                    downPoint = eventData.position;

                    if (moveTrans == rectTransform)
                    {
                        moveTrans = null;
                    }
                };

                listener.onEndDrag = (eventData) =>
                {
                    moveTrans = rectTransform;
                    float xMove = 0;
                    if (downPoint.x > eventData.position.x)
                    {
                        xMove = -rectTransform.rect.width;
                    }
                    rectTransform.DOAnchorPosX(xMove, 0.3f).onComplete = () =>
                    {
                        moveTrans = null;
                    };
                };


                UIEventListener.Get(obj.transform.GetChild(5).gameObject).onDown = (go) =>
                {
                    cacheX = rectTransform.anchoredPosition.x;
                    cacheY = scrollcomponent.scrollRect.content.anchoredPosition.y;
                };
                UIEventListener.Get(obj.transform.GetChild(5).gameObject).onUp = (go) =>
                {
                    if (Mathf.Abs(cacheX - rectTransform.anchoredPosition.x) <= 10
                        && Mathf.Abs(cacheY - scrollcomponent.scrollRect.content.anchoredPosition.y) <= 10
                        && moveTrans == null)
                    {
                        Game.EventSystem.Run(UIClubEventIdType.CLUB_MLISTINFO, userid);
                    }
                };

                listener.onDrag = (eventData) =>
                {
                    rectTransform.anchoredPosition += eventData.delta;
                    float x = Mathf.Clamp(rectTransform.anchoredPosition.x, -rectTransform.rect.width, 0);
                    float y = 0;
                    rectTransform.anchoredPosition = new Vector2(x, y);

                    scrollcomponent.scrollRect.content.anchoredPosition += new Vector2(0, eventData.delta.y);
                };
            }
            else
            {
                UIEventListener.Get(obj.transform.GetChild(5).gameObject).onClick = (go) =>
                {
                    Game.EventSystem.Run(UIClubEventIdType.CLUB_MLISTINFO, userid);
                };
                UIScrollEventListener listener = UIScrollEventListener.Get(obj.transform.GetChild(5).gameObject);
                listener.onBeginDrag = null;
                listener.onEndDrag = null;
                listener.onDrag = null;
            }

            //UIEventListener.Get(obj.transform.GetChild(5).gameObject).onClick = (go) =>
            //{
            //    Game.EventSystem.Run(UIClubEventIdType.CLUB_MLISTINFO, userid);
            //};
        }

        private void Refresh()
        {
            moveTrans = null;
            HttpRequestComponent.Instance.Send(
                                WEB2_club_mlist.API,
                                WEB2_club_mlist.Request(new WEB2_club_mlist.RequestData { clubId = iClubId }),
                                json2 => {
                                    WEB2_club_mlist.ResponseData clublist_res = WEB2_club_mlist.Response(json2);
                                    RefreshView(iClubId, clublist_res);
                                });
        }
    }
}



