using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Text;
using Header;

public class LobbyManager : MonoBehaviour {
    public static LobbyManager Instance { get; private set; }

    [SerializeField] private InputField m_ifTitle;
    [SerializeField] private InputField m_ifPassword;

    [SerializeField] private GameObject m_PrefabCreateRoom = null;
    private GameObject m_CreateRoom = null;

    private List<string> m_chatLog = null;

    private int m_Page = 1;

    private float m_RoomUpdateTime = 1.0f;
    private float m_LastTime = 0.0f;

    public int m_pageTextSize = 10;
    public Text[] m_indexText;
    public Text[] m_titleText;
    public Text[] m_stateText;
    public Text[] m_userNumText;

    public Text[] m_consoleText;

    public InputField m_inputField;

    private int m_RoomIndex = -1;
    public int RoomIndex { get { return m_RoomIndex; } set { m_RoomIndex = value; } }


    void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start() {
        for (int i = 0; i < m_pageTextSize; i++) {
            m_indexText[i].text = string.Empty;
            m_titleText[i].text = string.Empty;
            m_stateText[i].text = string.Empty;
            m_userNumText[i].text = string.Empty;
        }
        for (int i = 0; i < m_consoleText.Length; i++) {
            m_consoleText[i].text = string.Empty;
        }
        m_chatLog = new List<string>();

        LoadRoomList();
    }

    void Update() {
        WaitLoadRoomList();
        RenderRoomList();
        RenderChatLog();
    }

    public void CreateRoom() {
        InputField[] inputFields = m_CreateRoom.GetComponentsInChildren<InputField>();

        JSONObject p = new JSONObject();
        p["header"] = CTS.CREATE_ROOM;
        p["name"] = inputFields[0].text;
        p["pass"] = inputFields[1].text;
        NetworkManager.Instance.SendPacket(p);
    }

    public void CreateRoomUI() {
        if (m_CreateRoom == null) {
            m_CreateRoom = Instantiate(m_PrefabCreateRoom, GameObject.Find("Canvas").gameObject.transform);
        } else {
            Destroy(m_CreateRoom);
        }
    }

    private void LoadRoomList() {
        JSONObject p = new JSONObject();
        p["header"] = CTS.LOAD_ROOMLIST;
        p["page"] = m_Page;
        NetworkManager.Instance.SendPacket(p);
    }

    private void WaitLoadRoomList() {
        m_LastTime += Time.deltaTime;
        if (m_LastTime > m_RoomUpdateTime) {
            LoadRoomList();
            m_LastTime = 0.0f;
        }
    }

    private void RenderRoomList() {
        int i = 0;
        if (DataManager.Instance.RoomData.Count > 0) {
            foreach (Room o in DataManager.Instance.RoomData) {
                if (m_RoomIndex + (m_Page - 1) * 10 == o.Index) {
                    m_indexText[i].color = new Color(255, 0, 0);
                    m_titleText[i].color = new Color(255, 0, 0);
                    m_stateText[i].color = new Color(255, 0, 0);
                    m_userNumText[i].color = new Color(255, 0, 0);
                } else {
                    m_indexText[i].color = new Color(0, 0, 0);
                    m_titleText[i].color = new Color(0, 0, 0);
                    m_stateText[i].color = new Color(0, 0, 0);
                    m_userNumText[i].color = new Color(0, 0, 0);
                }
                m_indexText[i].text = (o.Index + 1).ToString();
                m_titleText[i].text = o.Name.Replace("\"", "");
                switch (o.State) {
                    case 0:
                        m_stateText[i].text = "대기 중";
                        break;
                    case 1:
                        m_stateText[i].text = "게임 중";
                        break;
                    case 2:
                        m_stateText[i].text = "종료 중";
                        break;
                }
                m_userNumText[i].text = o.Num.ToString() + " / 10";
                i++;
            }
        }
    }

    public void ClickRoomIndex(int i) {

    }

    public void EnterRoom() {
        if (m_RoomIndex == -1)
            return;

        JSONObject p = new JSONObject();
        p["header"] = CTS.ENTER_ROOM;
        p["index"] = m_RoomIndex;
        p["page"] = m_Page;


        NetworkManager.Instance.SendPacket(p);
    }

    public void RenderChatLog() {
        int i = 0;
        m_chatLog.Reverse();
        foreach (string s in m_chatLog) {
            if (i > 8) break;
            m_consoleText[i].text = s;
            i++;
        }
        m_chatLog.Reverse();
    }

    public void SendChat() {
        if (m_inputField.text == string.Empty)
            return;

        JSONObject p = new JSONObject();
        p["header"] = CTS.CHAT;
        p["type"] = 1;
        p["message"] = m_inputField.text;


        NetworkManager.Instance.SendPacket(p);
        m_inputField.text = string.Empty;
    }

    public void Chat(string s) {
        m_chatLog.Add(s);
    }
}
