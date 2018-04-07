// DataManager.cs
// 게임의 여러 데이터를 담는 클래스입니다.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    enum TEAM {
        TEAM_BLUE = 0,
        TEAM_RED,
        TEAM_OBSERVER,
        TEAM_END,
    };

    private Room m_NowRoom = null;
    private List<Room> m_RoomData = null;
    private List<RoomTeam>[] m_RoomTeam;
    //public List m_roomTeam = null;

    public List<Room> RoomData { get { return m_RoomData; } }
    public List<RoomTeam> RoomTeam(int i) { return m_RoomTeam[i]; }
    public Room NowRoom { get { return m_NowRoom; } set { m_NowRoom = NowRoom; } }

    public static DataManager Instance { get; private set; }

    void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        m_RoomData = new List<Room>();
        m_RoomTeam = new List<RoomTeam>[3] {
            new List<RoomTeam>(),
            new List<RoomTeam>(),
            new List<RoomTeam>()
        };
        InitRoomTeam();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InitRoomTeam() {
        for (int i = 0; i < m_RoomTeam.Length; i++) {
            m_RoomTeam[i].Clear();
        }
    }

    public string JobByIndex(int i) {
        switch (i) {
        case 0:
            return "릴딘";
        case 1:
            return "네아";
        case 2:
            return "엘리스";
        case 3:
            return "워록";
        default:
            return "";
        }
    }
}
