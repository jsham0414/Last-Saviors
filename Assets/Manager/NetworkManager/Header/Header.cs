// Header.cs
// 서버와의 통신을 위한 헤더를 정의해놓은 클래스입니다.

namespace Header {
   public class CTS {
        public const int LOGIN = 0;
        public const int REGISTER = 1;
        public const int MOVE_CHARACTER = 2;
        public const int TURN_CHARACTER = 3;
        public const int REMOVE_EQUIP_ITEM = 4;
        public const int USE_STAT_POINT = 5;
        public const int ACTION = 6;
        public const int USE_ITEM = 7;
        public const int USE_SKILL = 8;
        public const int DROP_ITEM = 9;
        public const int DROP_GOLD = 10;
        public const int PICK_ITEM = 11;
        public const int CHAT = 12;

        public const int OPEN_REGISTER_WINDOW = 100;
        public const int CHANGE_ITEM_INDEX = 101;
        public const int REQUEST_TRADE = 102;
        public const int RESPONSE_TRADE = 103;
        public const int LOAD_TRADE_ITEM = 104;
        public const int DROP_TRADE_ITEM = 105;
        public const int CHANGE_TRADE_GOLD = 106;
        public const int FINISH_TRADE = 107;
        public const int CANCEL_TRADE = 108;
        public const int SELECT_MESSAGE = 109;
        public const int CREATE_PARTY = 110;
        public const int INVITE_PARTY = 111;
        public const int RESPONSE_PARTY = 112;
        public const int QUIT_PARTY = 113;
        public const int KICK_PARTY = 114;
        public const int BREAK_UP_PARTY = 115;
        public const int CREATE_GUILD = 116;
        public const int INVITE_GUILD = 117;
        public const int RESPONSE_GUILD = 118;
        public const int QUIT_GUILD = 119;
        public const int KICK_GUILD = 120;
        public const int BREAK_UP_GUILD = 121;
        public const int BUY_SHOP_ITEM = 122;

        public const int ENTER_ROOM = 123;
        public const int CREATE_ROOM = 124;
        public const int OPEN_SHOP = 125;
        public const int OPEN_INVENTORY = 126;
        public const int LOAD_ROOMLIST = 127;
        public const int EXIT_ROOM = 128;
        public const int TEAM_CHANGE = 129;
        public const int ROOM_SETTING = 130;
        public const int GAME_START = 131;
        public const int BASIC_ATTACK = 132;
        public const int SET_TARGET = 133;
        public const int CHARGE = 134;
        public const int DODGE = 135;
        public const int USE_MOUSE_SKILL = 136;
        public const int USE_MAP_SKILL = 137;
        public const int NORMAL_ELECT = 138;
        public const int EQUIP_ITEM_SLOT = 139;
        public const int UNEQUIP_ITEM_SLOT = 140;
        public const int SET_ITEM_SLOT = 141;
        public const int GET_INFO = 142;
        public const int CHECK_MOVING = 143;
        public const int MASTER_CHANGE = 144;
        public const int USE_MOUSE_SKILL2 = 145;

        public const int GAME_EXIT = 200;
        public const int START_MOVE = 201;
        public const int END_MOVE = 202;

    }

    public class STC {
        public const int LOGIN = 0;
        public const int REGISTER = 1;
        public const int MOVE_CHARACTER = 2;
        public const int TURN_CHARACTER = 3;
        public const int CREATE_CHARACTER = 4;
        public const int REMOVE_CHARACTER = 5;
        public const int REFRESH_CHARACTER = 6;
        public const int JUMP_CHARACTER = 7;
        public const int ANIMATION_CHARACTER = 8;
        public const int MOTION_CHARACTER = 9;
        public const int UPDATE_CHARACTER = 10;
        public const int DAMAGE_CHARACTER = 11;
        public const int LOAD_DROP_ITEM = 12;
        public const int LOAD_DROP_GOLD = 13;
        public const int REMOVE_DROP_ITEM = 14;
        public const int REMOVE_DROP_GOLD = 15;
        public const int NOTIFY = 16;
        public const int MOVE_MAP = 17;
        public const int CHAT = 18;
        public const int SET_ITEM_SLOT = 19;

        public const int OPEN_REGISTER_WINDOW = 100;
        public const int UPDATE_STATUS = 101;
        public const int SET_ITEM = 102;
        public const int UPDATE_ITEM = 103;
        public const int SET_SKILL = 104;
        public const int UPDATE_SKILL = 105;
        public const int REQUEST_TRADE = 106;
        public const int OPEN_TRADE_WINDOW = 107;
        public const int LOAD_TRADE_ITEM = 108;
        public const int DROP_TRADE_ITEM = 109;
        public const int CHANGE_TRADE_GOLD = 110;
        public const int FINISH_TRADE = 111;
        public const int CANCEL_TRADE = 112;
        public const int OPEN_MESSAGE_WINDOW = 113;
        public const int CLOSE_MESSAGE_WINDOW = 114;
        public const int SET_SHOP_ITEM = 115;
        public const int SET_PARTY = 116;
        public const int INVITE_PARTY = 117;
        public const int SET_PARTY_MEMBER = 118;
        public const int REMOVE_PARTY_MEMBER = 119;
        public const int CREATE_GUILD = 120;
        public const int SET_GUILD = 121;
        public const int INVITE_GUILD = 122;
        public const int SET_GUILD_MEMBER = 123;
        public const int REMOVE_GUILD_MEMBER = 124;
        public const int OPEN_SHOP_WINDOW = 125;
        public const int OPEN_DIALOG = 126;

        public const int LOAD_ROOMLIST = 127;
        public const int UPDATE_ROOM = 128;
        public const int ENTER_ROOM = 129;
        public const int END_GAME = 130;
        public const int UPDATE_USERDATA = 131;
        public const int START_GAME = 132;
        public const int SET_MOVE_SPEED = 133;
        public const int SET_TARGET = 134;
        public const int MOTION = 135;
        public const int SET_COOLTIME = 136;
        public const int SET_SLOT = 137;
        public const int SET_MOTION_STOP = 138;
        public const int SET_BUFF = 139;
        public const int SET_TIMER = 140;
        public const int SET_HIDE = 141;
        public const int COUNTING = 142;
        public const int SET_JOB = 143;
        public const int CREATE_OBJECT = 144;
        public const int ANIMATION = 145;
        public const int ANIMATION2 = 146;
        public const int SET_DIE = 147;
        public const int SET_IMAGE = 148;
        public const int GET_INFO = 149;
        public const int RESET_INDEX = 150;

        public const int MOVE_SCENE = 151;
        public const int UPDATE_OBJECT = 152;
        public const int BUSY_CHECK = 153;

        public const int START_MOVE = 200;
        public const int END_MOVE = 201;

        public const int SET_SEED = 301;

    }
}