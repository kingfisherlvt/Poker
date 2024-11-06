using System;
using System.Collections.Generic;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIClub_ScoreComponentSystem : AwakeSystem<UIClub_ScoreComponent>
    {
        public override void Awake(UIClub_ScoreComponent self)
        {
            self.Awake();
        }
    }

    /// <summary> 页面名: </summary>
    public class UIClub_ScoreComponent : UIBaseComponent
    {
        private ReferenceCollector rc;

        int clubId = 0;
        int integral = 0;

        int operCount = 0;
        int type = 1;
        int lastReqType = 1;
        public void Awake()
        {
            InitUI();
        }

        public override void OnShow(object obj)
        {         
            SetUpNav(GameCache.Instance.nick, UIType.UIClub_Score);
 
            int[] param = obj as int[];
            if (param != null)
            {
                clubId = (int)param[0];
                integral = (int)param[1];
            }
            else
            {
                clubId = 0;
            }
            rc.Get<GameObject>(UIClubAssitent.cubinfo_btn_distribute).SetActive(type == 1);
            rc.Get<GameObject>(UIClubAssitent.cubinfo_btn_recovery).SetActive(type == 2);

            UIEventListener.Get(rc.Get<GameObject>(UIClubAssitent.cubinfo_btn_distributedisable)).onClick = async(go) => {
                type = 1;
                rc.Get<GameObject>(UIClubAssitent.cubinfo_btn_distribute).SetActive(type == 1);
                rc.Get<GameObject>(UIClubAssitent.cubinfo_btn_recovery).SetActive(type == 2);
            };
            UIEventListener.Get(rc.Get<GameObject>(UIClubAssitent.cubinfo_btn_recoverydisable)).onClick = async(go) => {
                type = 2;
                rc.Get<GameObject>(UIClubAssitent.cubinfo_btn_distribute).SetActive(type == 1);
                rc.Get<GameObject>(UIClubAssitent.cubinfo_btn_recovery).SetActive(type == 2);
            };
            rc.Get<GameObject>(UIClubAssitent.cubinfo_txt_score).GetComponent<Text>().text = "";

            UIEventListener.Get(rc.Get<GameObject>(UIClubAssitent.cubinfo_btn_confirm)).onClick = async(go) => {
                var count = rc.Get<GameObject>(UIClubAssitent.clubinfo_inputfield).GetComponent<InputField>().text;
                int.TryParse(count, out operCount);
                if (operCount > 0)
                    ReqScoreOper(type);
            };
            ReqSelfScore();
            
        }
        void ReqScoreOper(int type)
        {
            Log.Debug($"ReqSelfScore:clubId={clubId},integral={operCount},type={type}" );
            WEB2_club_score.RequestData dataperson_req = new WEB2_club_score.RequestData()
                {
                    clubId = clubId,
                    integral = operCount,
                    type = type
                };
                WEB2_club_score.RequestData clubscore_req = new WEB2_club_score.RequestData();
                HttpRequestComponent.Instance.Send(
                                WEB2_club_score.API,
                                WEB2_club_score.Request(dataperson_req),
                                (data)=>{
                                    ClubScoreApplyCall(data, type);
                                });
        }
        void ReqSelfScore()
        {
            Log.Debug($"ReqSelfScore:{clubId}" );
            WEB2_club_scoreQuery.RequestData dataperson_req = new WEB2_club_scoreQuery.RequestData()
                {
                    clubId = clubId
                };
                WEB2_club_scoreQuery.RequestData clubscoreQuery_req = new WEB2_club_scoreQuery.RequestData();
                HttpRequestComponent.Instance.Send(
                                WEB2_club_scoreQuery.API,
                                WEB2_club_scoreQuery.Request(dataperson_req),
                                this.ClubScoreQueryQueryCall);
        }
        public override void OnHide()
        {
        }

        public override void Dispose()
        {
            base.Dispose();
        }       
        protected virtual void InitUI()
        {
            rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>(); 
            
                      
        }  

        /// <summary>
        /// 俱乐部列表信息回调
        /// </summary>
        /// <param name="resData"></param>
        void ClubScoreApplyCall(string resData, int type) {
            Log.Debug("ClubScoreApplyCall:"+resData);

            var tDto = WEB2_club_score.Response(resData);
            if (tDto.status == 0)
            {
                UIComponent.Instance.Toast("成功");
                ReqSelfScore();
            }else{
                UIComponent.Instance.Toast("失败");
            }
        }

                /// <summary>
        /// 俱乐部积分查询返回
        /// </summary>
        /// <param name="resData"></param>
        void ClubScoreQueryQueryCall(string resData) {
            Log.Debug("ClubScoreQueryQueryCall:"+resData);
            var tDto = WEB2_club_scoreQuery.Response(resData);
            if (tDto.status == 0)
            {
                integral= tDto.data;
                var value = integral == -1 ? "Infinity" : $"{integral}";
                rc.Get<GameObject>(UIClubAssitent.cubinfo_txt_score).GetComponent<Text>().text = value;
            }
        }
    }
}