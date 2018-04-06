// Room.cs 
// 방 정보에 관한 클래스들입니다.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    int m_index;
    string m_name;
    int m_blueTeamNum;
    int m_redTeamNum;
    string m_password;
    int m_mode;
    int m_state;
    int m_num;
    string m_master;
    int m_map;
    int m_blueWinRate;
    int m_redWinRate;

    public int Index { get { return m_index; } set { m_index = Index; } }
    public string Name { get { return m_name; } set { m_name = Name; } }
    public int BlueTeamNum { get { return m_blueTeamNum; } set { m_blueTeamNum = BlueTeamNum; } }
    public int RedTeamNum { get { return m_redTeamNum; } set { m_redTeamNum = RedTeamNum; } }
    public string Password { get { return m_password; } set { m_password = Password; } }
    public int Mode { get { return m_mode; } set { m_mode = Mode; } }
    public int State { get { return m_state; } set { m_state = State; } }
    public int Num { get { return m_num; } set { m_num = Num; } }
    public string Master { get { return m_master; } set { m_master = Master; } }
    public int Map { get { return m_map; } set { m_map = Map; } }
    public int BlueWinRate { get { return m_blueWinRate; } set { m_blueWinRate = BlueWinRate; } }
    public int RedWinRate { get { return m_redWinRate; } set { m_redWinRate = RedWinRate; } }
    
    // Use this for initialization
    void Start () {
        m_name = string.Empty;
        m_password = string.Empty;
    }

    public Room(int _index, string _name, int _blueTeamNum,
        int _redTeamNum, int _state, int _mode, int _num, string _master = "", int _map = 0, int _blueWinRate = 0, int _redWinRate = 0) {
        m_index = _index;
        m_name = _name;
        m_blueTeamNum = _blueTeamNum;
        m_redTeamNum = _redTeamNum;
        m_state = _state;
        m_mode = _mode;
        m_num = _num;
        m_master = _master;
        m_map = _map;
        m_blueWinRate = _blueWinRate;
        m_redWinRate = _redWinRate;
    }
}

public class RoomTeam {
    int m_index;
    string m_name;
    string m_image;
    int m_level;

    public int Index { get { return m_index; } set { m_index = Index; } }
    public string Name { get { return m_name; } set { m_name = Name; } }
    public string Image { get { return m_image; } set { m_image = Image; } }
    public int Level { get { return m_level; } set { m_level = Level; } }

    void Start() {
        m_name = string.Empty;
        m_image = string.Empty;
    }

    public RoomTeam(int _index = 0, string _name = "", string _image = "", int _level = 0) {
        m_index = _index;
        m_name = _name;
        m_image = _image;
        m_level = _level;
    }
}
