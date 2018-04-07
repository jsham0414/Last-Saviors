using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject PlayerPrefeb = null;
    [SerializeField] private GameObject NetPlayerPrefeb = null;
    [SerializeField] private GameObject DialogPrefab = null;

    public CharacterInfo PlayerData { get; set; }
    public Player MainPlayer { get; set; }
    public List<GameObject> NetPlayers { get; set; } 

    public static GameManager Instance { get; private set; }

    void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true;
    }

    // Use this for initialization
    void Start () {
        PlayerData = new CharacterInfo();
        NetPlayers = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartMove(int no, int dx, int dy, float x, float y) {
        if (PlayerData.No == no)
            return;

        Player p;
        NetPlayer np;
        foreach (GameObject obj in NetPlayers) {
            if (obj == null)
                continue;
            switch (obj.name) {
            case "Player(Clone)":
                p = obj.GetComponent<Player>();
                if (p.Info.No == no) {
                    p.transform.position = new Vector3(x, y, 0);
                    p.dPosition = new Vector3(x, y, 0);
                    p.deadX = dx;
                    p.deadY = dy;
                    p.deadTime = Time.time;
                    return;
                }
                break;
            case "NetPlayer(Clone)":
                np = obj.GetComponent<NetPlayer>();
                if (np.Info.No == no) {
                    np.EndMove = false;
                    np.transform.position = new Vector3(x, y, 0);
                    np.dPosition = new Vector3(x, y, 0);
                    np.deadX = dx;
                    np.deadY = dy;
                    np.deadTime = Time.time;
                    return;
                }
                break;
            }
        }
    }

    public void EndMove(int no, float x, float y) {
        Player p;
        NetPlayer np;
        foreach (GameObject obj in NetPlayers) {
            if (obj == null)
                continue;
            switch (obj.name) {
            case "Player(Clone)":
                p = obj.GetComponent<Player>();
                if (p.Info.No == no) {
                    p.transform.position = new Vector3(x, y, 0);
                    return;
                }
                break;
            case "NetPlayer(Clone)":
                np = obj.GetComponent<NetPlayer>();
                if (np.Info.No == no) {
                    np.EndMove = true;
                    np.MoveQueue = new Vector3(x, y, 0);
                    np.transform.position = new Vector3(x, y, 0);
                    return;
                }
                break;
            }
        }
    }

    public void AddPlayer(CharacterInfo p) {
        GameObject obj = Instantiate(PlayerPrefeb, GameObject.Find("Characters").transform);
        Player player = obj.GetComponent<Player>();
        SetCharacterInfo(player, p);
        obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Characters/" + player.Info.Image.Replace("\"", ""));
        MainPlayer = player;
        NetPlayers.Add(obj);
    }

    public void AddNetPlayer(CharacterInfo np) {
        GameObject obj = Instantiate(NetPlayerPrefeb, GameObject.Find("Characters").transform);
        print("x = " + obj.transform.position.x + "y = " + obj.transform.position.y);
        NetPlayer netPlayer = obj.GetComponent<NetPlayer>();
        SetCharacterInfo(netPlayer, np);
        obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Characters/" + netPlayer.Info.Image.Replace("\"", ""));
        NetPlayers.Add(obj);
    }

    public NetPlayer GetNetPlayerInNo(int no) {
        foreach (GameObject np in NetPlayers) {
            if (np.GetComponent<NetPlayer>().Info.No == no) {
                return np.GetComponent<NetPlayer>();
            }
        }

        return null;
    }

    public void CreateDialog(string title, string text) {
        GameObject obj = Instantiate(DialogPrefab, GameObject.Find("Canvas").transform);
        foreach(Text t in obj.GetComponentsInChildren<Text>()) {
            switch (t.name) {
            case "Title":
                t.text = title.ToString();
                break;
            case "Message":
                t.text = text.ToString();
                break;
            }
        }
    }

    public void SetCharacterInfo(Character c1, CharacterInfo c2) {
        c1.Info.No = c2.No;
        c1.Info.Name = c2.Name;
        c1.Info.Image = c2.Image;
        c1.Info.Level = c2.Level;
        c1.Info.Hp = c2.Hp;
        c1.Info.MaxHp = c2.MaxHp;
        c1.Info.Mp = c2.Mp;
        c1.Info.MaxMp = c2.MaxMp;
        c1.Info.Team = c2.Team;
    }
}
