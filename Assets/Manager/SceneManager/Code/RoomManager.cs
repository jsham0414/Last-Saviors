using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using Header;

public class RoomManager : MonoBehaviour {
    public static RoomManager Instance { get; private set; }

    [SerializeField] private Text[] m_BlueName = null;
    [SerializeField] private Text[] m_RedName = null;

    [SerializeField] private Image[] m_BlueImage = null;
    [SerializeField] private Image[] m_RedImage = null;

    [SerializeField] private GameObject m_PrefabJobChange = null;
    private GameObject m_JobChange = null;

    public Text[] m_consoleText;
    private List<string> m_chatLog = null;

    public InputField m_inputField;

    void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < m_consoleText.Length; i++) {
            m_consoleText[i].text = string.Empty;
        }
        m_chatLog = new List<string>();

        ChangeData();
    }
	
	// Update is called once per frame
	void Update () {
        RenderChatLog();

    }

    public void RenderChatLog() {
        int i = 0;
        m_chatLog.Reverse();
        foreach (string s in m_chatLog) {
            if (i > 7) break;
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
        p["type"] = 2;
        p["message"] = m_inputField.text;


        NetworkManager.Instance.SendPacket(p);
        m_inputField.text = string.Empty;
    }

    public void ChangeTeam() {
        JSONObject p = new JSONObject();
        p["header"] = CTS.TEAM_CHANGE;

        NetworkManager.Instance.SendPacket(p);
    }

    public void GameStart() {
        JSONObject p = new JSONObject();
        p["header"] = CTS.GAME_START;

        NetworkManager.Instance.SendPacket(p);
    }

    public void CreateJobChange() {
        if (m_JobChange == null) {
            m_JobChange = Instantiate(m_PrefabJobChange, GameObject.Find("Canvas").gameObject.transform);
        } else {
            Destroy(m_JobChange);
        }
    }

    public void Chat(string s) {
        m_chatLog.Add(s);
    }

    public void ChangeData() {
        int i = 0;
        for (i = 0; i < 5; i++) {
            m_BlueName[i].text = "";
            m_BlueImage[i].sprite = null;
            m_BlueImage[i].color = new Color(0, 0, 0, 0);
        }
        for (i = 0; i < 5; i++) {
            m_RedName[i].text = "";
            m_RedImage[i].sprite = null;
            m_RedImage[i].color = new Color(0, 0, 0, 0);
        }
        i = 0;
        foreach (RoomTeam rt in DataManager.Instance.RoomTeam(0)) {
            m_BlueName[i].text = "Lv " + rt.Level + " " + rt.Name;
            m_BlueImage[i].sprite = Resources.Load<Sprite>("Characters/" + rt.Image);
            m_BlueImage[i].color = new Color(255, 255, 255, 255);
            i++;
        }
        i = 0;
        foreach (RoomTeam rt in DataManager.Instance.RoomTeam(1)) {
            m_RedName[i].text = "Lv " + rt.Level + " " + rt.Name;
            m_RedImage[i].sprite = Resources.Load<Sprite>("Characters/" + rt.Image);
            m_RedImage[i].color = new Color(255, 255, 255, 255);
            i++;
        }
        
    }

    public void ExitRoom() {
        JSONObject p = new JSONObject();
        p["header"] = CTS.EXIT_ROOM;
        NetworkManager.Instance.SendPacket(p);
    }
}
