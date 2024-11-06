using ETModel;
namespace ETHotfix
{
    [Message(HotfixOpcode.REQ_LOGIN)]
    public partial class REQ_LOGIN : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_LOGIN_RESOURCES)]
    public partial class REQ_LOGIN_RESOURCES : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_ENTER_ROOM)]
    public partial class REQ_GAME_ENTER_ROOM : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_SEND_SEAT_ACTION)]
    public partial class REQ_GAME_SEND_SEAT_ACTION : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_SEND_ACTION)]
    public partial class REQ_GAME_SEND_ACTION : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_ACTION)]
    public partial class REQ_GAME_RECV_ACTION : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_LEAVE)]
    public partial class REQ_GAME_RECV_LEAVE : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_SEAT_DOWN)]
    public partial class REQ_GAME_RECV_SEAT_DOWN : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_CARDS)]
    public partial class REQ_GAME_RECV_CARDS : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_WINNER)]
    public partial class REQ_GAME_RECV_WINNER : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_READYTIME)]
    public partial class REQ_GAME_RECV_READYTIME : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_START_INFOR)]
    public partial class REQ_GAME_RECV_START_INFOR : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_ADD_CHIPS)]
    public partial class REQ_GAME_ADD_CHIPS : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_ENTER_ROOM)]
    public partial class REQ_ENTER_ROOM : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_START_GAME)]
    public partial class REQ_GAME_START_GAME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_ENDING)]
    public partial class REQ_GAME_ENDING : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_TAKE_CONTROL)]
    public partial class REQ_GAME_TAKE_CONTROL : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_CONTROL_RANGE)]
    public partial class REQ_GAME_CONTROL_RANGE : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_PLAYER_CARDS)]
    public partial class REQ_GAME_PLAYER_CARDS : ICPResponse { }

    [Message(HotfixOpcode.REQ_SHOWDOWN)]
    public partial class REQ_SHOWDOWN : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_JACKPOT_SHOW_CARDS)]
    public partial class REQ_GAME_JACKPOT_SHOW_CARDS : ICPResponse { }

    [Message(HotfixOpcode.REQ_SYNC_SIGNAL_RESOURCE)]
    public partial class REQ_SYNC_SIGNAL_RESOURCE : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_SYNC_SIGNAL_GAME)]
    public partial class REQ_SYNC_SIGNAL_GAME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_OFFLINE)]
    public partial class REQ_OFFLINE : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_PRE_START_GAME)]
    public partial class REQ_GAME_RECV_PRE_START_GAME : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_TRUST_ACTION)]
    public partial class REQ_GAME_TRUST_ACTION : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_FINISHGAME)]
    public partial class REQ_FINISHGAME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_CHECK_CAN_CICK)]
    public partial class REQ_GAME_CHECK_CAN_CICK : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_JACKPOT_CHANGE)]
    public partial class REQ_GAME_JACKPOT_CHANGE : ICPResponse { }

    [Message(HotfixOpcode.REQ_START_ROOM)]
    public partial class REQ_START_ROOM : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_REAL_TIME)]
    public partial class REQ_GAME_REAL_TIME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_ADD_TIME)]
    public partial class REQ_ADD_TIME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_PAUSE)]
    public partial class REQ_GAME_PAUSE : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_PREV_GAME)]
    public partial class REQ_GAME_PREV_GAME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_COLLECT_CARD)]
    public partial class REQ_GAME_COLLECT_CARD : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_SHOW_SIDE_POTS)]
    public partial class REQ_SHOW_SIDE_POTS : ICPResponse { }

    [Message(HotfixOpcode.REQ_WAIT_BLIND)]
    public partial class REQ_WAIT_BLIND : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_INSURANCE_TRIGGED)]
    public partial class REQ_INSURANCE_TRIGGED : ICPResponse { }

    [Message(HotfixOpcode.REQ_BUY_INSURANCE)]
    public partial class REQ_BUY_INSURANCE : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_CLAIM_INSURANCE)]
    public partial class REQ_CLAIM_INSURANCE : ICPResponse { }

    [Message(HotfixOpcode.REQ_INSURANCE_ADD_TIME)]
    public partial class REQ_INSURANCE_ADD_TIME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_INSURANCE_NOT_TRIGGED)]
    public partial class REQ_INSURANCE_NOT_TRIGGED : ICPResponse { }

    [Message(HotfixOpcode.REQ_SEE_MORE_PUBLIC_ACTION)]
    public partial class REQ_SEE_MORE_PUBLIC_ACTION : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_CURRENT_ROUND_FINISH)]
    public partial class REQ_CURRENT_ROUND_FINISH : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_KEEP_SEAT)]
    public partial class REQ_GAME_KEEP_SEAT : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_WAIT_DAIRU)]
    public partial class REQ_GAME_WAIT_DAIRU : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_ADD_ROOM_TIME)]
    public partial class REQ_ADD_ROOM_TIME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_PRE_START_GAME_STRADDLE)]
    public partial class REQ_GAME_RECV_PRE_START_GAME_STRADDLE : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_PRE_START_GAME_MUCK)]
    public partial class REQ_GAME_RECV_PRE_START_GAME_MUCK : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_RECV_SPECTATORS_VOICE)]
    public partial class REQ_GAME_RECV_SPECTATORS_VOICE : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_MTT_REMIND)]
    public partial class REQ_GAME_MTT_REMIND : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_MTT_Self_Ranking)]
    public partial class REQ_GAME_MTT_Self_Ranking : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_MTT_REAL_TIME)]
    public partial class REQ_GAME_MTT_REAL_TIME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_MTT_SYSTEM_NOTIFY)]
    public partial class REQ_GAME_MTT_SYSTEM_NOTIFY : ICPResponse { }

    [Message(HotfixOpcode.REQ_MTT_GAME_REBUY_REQUEST)]
    public partial class REQ_MTT_GAME_REBUY_REQUEST : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_MTT_FINAL_RANK)]
    public partial class REQ_GAME_MTT_FINAL_RANK : ICPResponse { }

    [Message(HotfixOpcode.REQ_MTT_GAME_REBUY_CANCEL)]
    public partial class REQ_MTT_GAME_REBUY_CANCEL : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_MTT_APPLY_JOIN)]
    public partial class REQ_MTT_APPLY_JOIN : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_MTT_REBUY_REQUEST_OUTSIDE)]
    public partial class REQ_MTT_REBUY_REQUEST_OUTSIDE : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_MTT_PLAYER_LIST)]
    public partial class REQ_MTT_PLAYER_LIST : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_MTT_ENTER_ROOM)]
    public partial class REQ_GAME_MTT_ENTER_ROOM : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_MTT_GAME_DISMISS)]
    public partial class REQ_MTT_GAME_DISMISS : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_MTT_RECV_START_INFOR)]
    public partial class REQ_GAME_MTT_RECV_START_INFOR : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_MTT_AUTO_OP)]
    public partial class REQ_GAME_MTT_AUTO_OP : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_MTT_START_GAME)]
    public partial class REQ_GAME_MTT_START_GAME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_MTT_ADD_TIME)]
    public partial class REQ_MTT_ADD_TIME : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_SYSTEM_BROADCAST)]
    public partial class REQ_SYSTEM_BROADCAST : ICPResponse { }

    [Message(HotfixOpcode.REQ_Guild_BROADCAST)]
    public partial class REQ_Guild_BROADCAST : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_JACKPOT_HIT_BROADCAST)]
    public partial class REQ_GAME_JACKPOT_HIT_BROADCAST : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_OMAHA_ENTER_ROOM)]
    public partial class REQ_GAME_OMAHA_ENTER_ROOM : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_OMAHA_RECV_WINNER)]
    public partial class REQ_GAME_OMAHA_RECV_WINNER : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_OMAHA_RECV_START_INFOR)]
    public partial class REQ_GAME_OMAHA_RECV_START_INFOR : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_OMAHA_PLAYER_CARDS)]
    public partial class REQ_GAME_OMAHA_PLAYER_CARDS : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_OMAHA_SHOWDOWN)]
    public partial class REQ_GAME_OMAHA_SHOWDOWN : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_OMAHA_SHOW_CARDS)]
    public partial class REQ_GAME_OMAHA_SHOW_CARDS : ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_ASK_TEST)]
    public partial class REQ_GAME_ASK_TEST : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_GAME_SEND_TEST)]
    public partial class REQ_GAME_SEND_TEST : ICPRequest, ICPResponse { }

    [Message(HotfixOpcode.REQ_SHOW_CARDS_BY_WIN_USER)]
    public partial class REQ_SHOW_CARDS_BY_WIN_USER : ICPRequest, ICPResponse { }

}
namespace ETHotfix
{
    public static partial class HotfixOpcode
    {
        public const ushort REQ_LOGIN = 6;  // �˺ŵ�¼
        public const ushort REQ_LOGIN_RESOURCES = 8;  // res��¼
        public const ushort REQ_GAME_ENTER_ROOM = 12;  // ��ͨ�ֽ��뷿��
        public const ushort REQ_GAME_SEND_SEAT_ACTION = 18;  // ��ǰ���վ������»��뿪
        public const ushort REQ_GAME_SEND_ACTION = 19;  // �Լ���������
        public const ushort REQ_GAME_RECV_ACTION = 20;  // �յ���������
        public const ushort REQ_GAME_RECV_LEAVE = 22;  // �����뿪��λ
        public const ushort REQ_GAME_RECV_SEAT_DOWN = 23;  // ��������
        public const ushort REQ_GAME_RECV_CARDS = 24;  // �յ�������
        public const ushort REQ_GAME_RECV_WINNER = 25;  // �յ�ʤ����
        public const ushort REQ_GAME_RECV_READYTIME = 26;  // �յ�׼��ʱ��
        public const ushort REQ_GAME_RECV_START_INFOR = 27;  // ��һ�ֿ�ʼ����ͨ�֡�SNG��
        public const ushort REQ_GAME_ADD_CHIPS = 28;  // ����
        public const ushort REQ_ENTER_ROOM = 30;  // �����ƾֵȴ�ҳ
        public const ushort REQ_GAME_START_GAME = 32;  // ���������ʼ��Ϸ
        public const ushort REQ_GAME_ENDING = 33;  // �ƾֽ���
        public const ushort REQ_GAME_TAKE_CONTROL = 34;  // �������ÿ��ƴ��뿪��
        public const ushort REQ_GAME_CONTROL_RANGE = 40;  // �������ô��뱶��
        public const ushort REQ_GAME_PLAYER_CARDS = 42;  // Allin�·��������
        public const ushort REQ_SHOWDOWN = 43;  // ���ý���ʱ��������
        public const ushort REQ_GAME_JACKPOT_SHOW_CARDS = 44;   // �������Э�飨��Э���ڱ��ƺ�������ҽ���������ƻ���jackpot�Żᴥ����
        public const ushort REQ_SYNC_SIGNAL_RESOURCE = 52;  // Res����
        public const ushort REQ_SYNC_SIGNAL_GAME = 53;  // Game����
        public const ushort REQ_OFFLINE = 55;  // �˳���¼
        public const ushort REQ_GAME_RECV_PRE_START_GAME = 66;  // ÿ����Ϸ��ʼǰ����
        public const ushort REQ_GAME_TRUST_ACTION = 68;  // �й�
        public const ushort REQ_FINISHGAME = 69;  // ��ɢ����
        public const ushort REQ_GAME_CHECK_CAN_CICK = 72;  // ��ѯ��ĳ���Ƿ����߳�Ȩ��
        public const ushort REQ_GAME_JACKPOT_CHANGE = 78;  // JackPot�仯֪ͨ
        public const ushort REQ_START_ROOM = 80;  // �ȴ�ҳ��ʼ���˳����ɢ
        public const ushort REQ_GAME_REAL_TIME = 82;  // ʵʱս��
        public const ushort REQ_ADD_TIME = 86;  // ������ʱ
        public const ushort REQ_GAME_PAUSE = 87;  // ��ͣ��Ϸ
        public const ushort REQ_GAME_PREV_GAME = 90;  // �Ͼֻع�
        public const ushort REQ_GAME_COLLECT_CARD = 118;  // �ղ�����
        public const ushort REQ_SHOW_SIDE_POTS = 120;  // ��ʾ�ֳس���
        public const ushort REQ_WAIT_BLIND = 122;  // ��ׯ��ä
        public const ushort REQ_INSURANCE_TRIGGED = 140;  // ���մ���
        public const ushort REQ_BUY_INSURANCE = 141;  // ������
        public const ushort REQ_CLAIM_INSURANCE = 142;  // �����⸶��Ϣ
        public const ushort REQ_INSURANCE_ADD_TIME = 143;  // ���ռ�ʱ
        public const ushort REQ_INSURANCE_NOT_TRIGGED = 144;  // ������������  (�ͻ��˶�δ�������ݣ��յ���Э������ʾ����OUTS>16��OUTS=0�����ܹ����ա�)
        public const ushort REQ_SEE_MORE_PUBLIC_ACTION = 148;  // �鿴δ��������  (�鿴�ɹ������ڡ�24���յ�������Э���·��鿴�Ĺ�����)
        public const ushort REQ_CURRENT_ROUND_FINISH = 151;  // ��ǰ�ֽ���  (�յ���Э�飬��յ�ǰ�ֵĽ��������)
        public const ushort REQ_GAME_KEEP_SEAT = 152;  // ��������
        public const ushort REQ_ADD_ROOM_TIME = 153;  // �����ʱ  (���������˼�ʱ��������������Ҷ����յ�֪ͨ)
        public const ushort REQ_GAME_RECV_PRE_START_GAME_STRADDLE = 154;  // �����ر�ǿ��Straddle  (���������˿����ر�Straddle��������������Ҷ����յ�֪ͨ)
        public const ushort REQ_GAME_RECV_PRE_START_GAME_MUCK = 155;  // �����ر�Muck  (���������˿����ر�Muck��������������Ҷ����յ�֪ͨ)
        public const ushort REQ_GAME_WAIT_DAIRU = 156;  // �ȴ���˴���
        public const ushort REQ_GAME_RECV_SPECTATORS_VOICE = 160;  // �����رչ�������  (���������˿����ر�Muck��������������Ҷ����յ�֪ͨ)
        public const ushort REQ_GAME_MTT_REMIND = 166;  // MTT������ʼ��Ϣ  (�յ�����Ϣ������ȫ�ֶ�����������������ʼ��֪ͨ:"������xxx������ʼ���Ƿ���������")
        public const ushort REQ_GAME_MTT_Self_Ranking = 167;  // �����������·���mtt����
        public const ushort REQ_GAME_MTT_REAL_TIME = 168;  // MTTʵʱս��
        public const ushort REQ_GAME_MTT_SYSTEM_NOTIFY = 170;  // MTT�ƾ��ڸ���ϵͳ��Ϣ
        public const ushort REQ_MTT_GAME_REBUY_REQUEST = 171;  // MTT�ع�����
        public const ushort REQ_GAME_MTT_FINAL_RANK = 175;  // MTT������Ϸ֪ͨ����  (�յ�����Ϣ�����û���ع����ᣬ�򵯳���Ϸ��������ҳ��������ع����ᣬ�򵯳��ع���)
        public const ushort REQ_MTT_GAME_REBUY_CANCEL = 176;  // MTTȡ���ع�  (��175Э�鴥�����ع���ʾ�򣬵�ȡ��ʱ������Э��)
        public const ushort REQ_MTT_APPLY_JOIN = 180;  // MTT����
        public const ushort REQ_MTT_REBUY_REQUEST_OUTSIDE = 186;  // MTT�ƾ����ع�����
        public const ushort REQ_MTT_PLAYER_LIST = 188;  // MTT�ƾ�������б�
        public const ushort REQ_GAME_MTT_ENTER_ROOM = 212;  // MTT���뷿��
        public const ushort REQ_MTT_GAME_DISMISS = 222;  // MTT��ɢ
        public const ushort REQ_GAME_MTT_RECV_START_INFOR = 226;  // MTTÿ�ֿ�����Ϣ  (��Э�����ͨ�ֵ�27Э�鷵�������Ƶģ�ֻ��Э��Ų�ͬ������ͨ����һ���Ĵ�����С�MTT�Ĳ�ͬ�����յ���Э��Ҫ�����⼸��������)
        public const ushort REQ_GAME_MTT_AUTO_OP = 228;  // MTT�й�
        public const ushort REQ_GAME_MTT_START_GAME = 230;  // mtt������ʼ����Ϣ
        public const ushort REQ_MTT_ADD_TIME = 236;  // MTT������ʱ
        public const ushort REQ_SYSTEM_BROADCAST = 266;  // Resϵͳ�㲥
        public const ushort REQ_Guild_BROADCAST = 268;  // �ƾֹ㲥  (�ƾ����յ��㲥����266Э��һ����չʾ)
        public const ushort REQ_GAME_JACKPOT_HIT_BROADCAST = 269;  // JackPot���й㲥
        public const ushort REQ_GAME_OMAHA_ENTER_ROOM = 412;  // ��������뷿��  (����ͨ�ֲ�ͬ�ģ�158����ʱ����Ϊ����4��,178��179�ֶΣ����������ƣ�187Ѫսģʽ����)
        public const ushort REQ_GAME_OMAHA_RECV_WINNER = 420;  // ������յ�ʤ����  (����ͨ��25Э��һ���ķ��أ������145��146�ֶΣ������������)
        public const ushort REQ_GAME_OMAHA_RECV_START_INFOR = 426;  // �������һ�ֿ�ʼ  (����ͨ�ֲ�ͬ��150��151�����������)
        public const ushort REQ_GAME_OMAHA_PLAYER_CARDS = 432;  // �����Allin�·��������  (����ͨ�ֲ�ͬ��133��134�����������)
        public const ushort REQ_GAME_OMAHA_SHOWDOWN = 433;  // ��������ý���ʱ��������  (����ͨ�ֲ�ͬ��133��Ϊ����)
        public const ushort REQ_GAME_OMAHA_SHOW_CARDS = 448;  // ������������Э��

        public const ushort REQ_GAME_ASK_TEST = 29;  //���Բ�ѯ
        public const ushort REQ_GAME_SEND_TEST = 35;  //���Է���

        public const ushort REQ_SHOW_CARDS_BY_WIN_USER = 149;
    }
}