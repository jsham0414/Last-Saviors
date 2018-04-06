// NetworkManager.cs
// 서버와의 통신을 위한 클래스입니다.

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using SimpleJSON;
using Header;
using System.Globalization;
using System.Text.RegularExpressions;

public class NetworkManager : MonoBehaviour {
    // 싱글톤
    private static NetworkManager instance = null;
    public static NetworkManager Instance { get { return instance; } }

    private object lockObject = new object();

    private bool isRunning = true;

    private Socket sock;
    private Thread NetworkThread;
    private string receiveBuffer;

    private IEnumerator PasreEnumerator;

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // 서버와 연결하는 함수
    public bool Connect() {
        bool isConnect = true;
        if (sock != null)
            sock.Close();
        sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try {
            IPAddress svrIp = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipep = new IPEndPoint(svrIp, 50000);
            sock.Connect(ipep);
            if (sock.Connected) {
                isConnect = true;
                CoroutineStart();
            }
        } catch (SocketException ex) {
            print(ex.Message);
            isConnect = false;
        }

        isRunning = isConnect;
        return isConnect;
    }

    // 서버가 보낸 데이터를 받는 함수
    void Receive() {
        byte[] data = new byte[5128];
        while (true) {
            if (isRunning) {
                try {
                    lock (lockObject) {
                        int size = sock.Receive(data);

                        if (size > 0) {
                            receiveBuffer += Encoding.UTF8.GetString(data, 0, size);
                        }
                    }
                } catch (SocketException ex) {
                    print(ex.Message);
                    isRunning = false;
                }
            }
        }
    }

    public void CoroutineStart() {
        //StartCoroutine("Receive");
        NetworkThread = new Thread(new ThreadStart(Receive));
        NetworkThread.Start();
        StartCoroutine(PasreEnumerator);
    }

    public void CoroutineAbort() {
        //StopCoroutine("Receive");
        print("서버 접속 종료");
        if (NetworkThread != null) NetworkThread.Abort();
        if (sock != null) {
            if (sock.Connected) {
                sock.Close();
            }
        }
        StopCoroutine(PasreEnumerator);
    }

    void Start() {
        receiveBuffer = string.Empty;
        PasreEnumerator = Parse();
        if (Connect()) {
            print("서버 연결 완료");
        } else {
            print("서버 연결 실패");
        }
    }

    IEnumerator Parse() {
        //lock (receiveBuffer) {
        while (true) {
            if (receiveBuffer.Length > 0) {
                int sIndex = -1, eIndex = -1;
                sIndex = receiveBuffer.IndexOf('{');
                eIndex = receiveBuffer.IndexOf('}');

                if (!((sIndex == -1 || eIndex == -1) ||
                    (sIndex >= eIndex))) {
                    string convData = string.Empty;
                    for (int i = sIndex; i < (sIndex == 0 ? 1 : sIndex) + eIndex; i++) {
                        convData += receiveBuffer[i];
                    }

                    receiveBuffer = receiveBuffer.Replace(convData, "");

                    PacketParsing(convData);
                }
            }
            yield return null;
        }
        //}
    }

    void OnApplicationQuit() {
        CoroutineAbort();
        print("클라이언트를 종료합니다.");
    }

    // 패킷을 JSON형태로 변환하는 함수
    public void PacketParsing(string packet) {
        // JSON을 이용해 패킷을 파싱
        JSONNode p = JSON.Parse(packet) as JSONNode;
        if (p == null) {
            return;
        }

        Room room;
        CharacterInfo player;

        // 헤더값을 이용해 어떤 패킷이 전송되었는지 확인
        switch (p["header"].AsInt) {
        case STC.LOGIN:
            switch (p["type"].AsInt) {
            case 0:
                print("로그인 완료");
                SceneManager.LoadScene("Lobby");
                GameManager.Instance.PlayerData.No = p["no"];
                break;
            case 1:
                print("아이디나 비밀번호가 다릅니다.");
                break;
            case 2:
                print("서버 자체 오류입니다.");
                break;
            case 3:
                print("이미 접속중인 계정입니다.");
                break;
            }
            break;
        case STC.CHAT:
            switch (SceneManager.GetActiveScene().name) {
            case "Lobby":
                LobbyManager.Instance.Chat(p["message"]);
                break;
            case "Room":
                RoomManager.Instance.Chat(p["message"]);
                break;
            }
            break;
        case STC.START_GAME:
            SceneManager.LoadScene("Stage");
            break;
        case STC.CREATE_CHARACTER:
            switch (p["type"].AsInt) {
            case Database.CharacterType.USER:
                if (GameManager.Instance.PlayerData.No == p["no"]) {
                    player = new CharacterInfo();
                    if (p["name"] != null) player.Name = p["name"].ToString();
                    if (p["image"] != null) player.Image = p["image"].ToString();
                    if (p["level"] != null) player.Level = p["level"].AsInt;
                    if (p["hp"] != null) player.Hp = p["hp"].AsInt;
                    if (p["maxhp"] != null) player.MaxHp = p["maxhp"].AsInt;
                    if (p["mp"] != null) player.Mp = p["mp"].AsInt;
                    if (p["maxmp"] != null) player.MaxMp = p["maxmp"].AsInt;
                    if (p["team"] != null) player.Team = p["team"].AsInt;
                    GameManager.Instance.AddPlayer(player);
                } else {
                    player = new CharacterInfo();
                    if (p["no"] != null) player.No = p["no"].AsInt;
                    if (p["name"] != null) player.Name = p["name"].ToString();
                    if (p["image"] != null) player.Image = p["image"].ToString();
                    if (p["level"] != null) player.Level = p["level"].AsInt;
                    if (p["hp"] != null) player.Hp = p["hp"].AsInt;
                    if (p["maxhp"] != null) player.MaxHp = p["maxhp"].AsInt;
                    if (p["mp"] != null) player.Mp = p["mp"].AsInt;
                    if (p["maxmp"] != null) player.MaxMp = p["maxmp"].AsInt;
                    if (p["team"] != null) player.Team = p["team"].AsInt;
                    GameManager.Instance.AddNetPlayer(player);
                }
                break;
            }

            break;
        case STC.UPDATE_USERDATA:
            switch (p["type"].AsInt) {
            case Database.CharacterType.USER:
                if (GameManager.Instance.PlayerData.No == p["no"]) {
                    player = GameManager.Instance.PlayerData;
                    if (p[string.Format("{0}", Database.Status.IMAGE)] != null) player.Image = p[string.Format("{0}", Database.Status.IMAGE)].ToString();
                    if (p[string.Format("{0}", Database.Status.LEVEL)] != null) player.Level = p[string.Format("{0}", Database.Status.LEVEL)].AsInt;
                    if (p[string.Format("{0}", Database.Status.HP)] != null) player.Hp = p[string.Format("{0}", Database.Status.HP)].AsInt;
                    if (p[string.Format("{0}", Database.Status.MAX_HP)] != null) player.MaxHp = p[string.Format("{0}", Database.Status.MAX_HP)].AsInt;
                    if (p[string.Format("{0}", Database.Status.MP)] != null) player.Mp = p[string.Format("{0}", Database.Status.MP)].AsInt;
                    if (p[string.Format("{0}", Database.Status.MAX_MP)] != null) player.MaxMp = p[string.Format("{0}", Database.Status.MAX_MP)].AsInt;
                    if (p[string.Format("{0}", Database.Status.TEAM)] != null) player.Team = p[string.Format("{0}", Database.Status.TEAM)].AsInt;
                } else {
                    player = GameManager.Instance.GetNetPlayerInNo(p["no"]).Info;
                    if (player == null) return;
                    if (p[string.Format("{0}", Database.Status.IMAGE)] != null) player.Image = p[string.Format("{0}", Database.Status.IMAGE)].ToString();
                    if (p[string.Format("{0}", Database.Status.LEVEL)] != null) player.Level = p[string.Format("{0}", Database.Status.LEVEL)].AsInt;
                    if (p[string.Format("{0}", Database.Status.HP)] != null) player.Hp = p[string.Format("{0}", Database.Status.HP)].AsInt;
                    if (p[string.Format("{0}", Database.Status.MAX_HP)] != null) player.MaxHp = p[string.Format("{0}", Database.Status.MAX_HP)].AsInt;
                    if (p[string.Format("{0}", Database.Status.MP)] != null) player.Mp = p[string.Format("{0}", Database.Status.MP)].AsInt;
                    if (p[string.Format("{0}", Database.Status.MAX_MP)] != null) player.MaxMp = p[string.Format("{0}", Database.Status.MAX_MP)].AsInt;
                    if (p[string.Format("{0}", Database.Status.TEAM)] != null) player.Team = p[string.Format("{0}", Database.Status.TEAM)].AsInt;
                }
                break;
            }

            break;
        case STC.LOAD_ROOMLIST:
            DataManager.Instance.RoomData.Clear();
            int length = p["length"].AsInt;
            for (int i = 0; i < length; i++) {
                if (p["state"].AsInt == 0) {
                    DataManager.Instance.RoomData.Add(new Room(
                        p["room" + i + "Index"],
                        p["room" + i + "Name"].ToString(),
                        p["room" + i + "Blue"],
                        p["room" + i + "Red"],
                        p["room" + i + "State"],
                        p["room" + i + "Mode"],
                        p["room" + i + "Num"]
                        ));
                }
            }
            break;
        case STC.ENTER_ROOM:
            room = new Room(
                p["roomIndex"],
                p["roomName"],
                p["roomBlue"],
                p["roomRed"],
                p["roomState"],
                p["roomMode"],
                p["roomNum"],
                p["roomMaster"],
                p["roomMap"],
                p["blueWinRate"],
                p["redWinRate"]
                );
            DataManager.Instance.NowRoom = room;
            DataManager.Instance.InitRoomTeam();
            if (p["blueLength"] > 0) {
                for (int i = 0; i < p["blueLength"]; i++) {
                    DataManager.Instance.RoomTeam(0).Add(new RoomTeam(
                        p["blue" + i + "Index"],
                        p["blue" + i + "Name"],
                        p["blue" + i + "Image"],
                        p["blue" + i + "Level"]
                        ));
                }
            }
            if (p["redLength"] > 0) {
                for (int i = 0; i < p["redLength"]; i++) {
                    DataManager.Instance.RoomTeam(1).Add(new RoomTeam(
                        p["red" + i + "Index"],
                        p["red" + i + "Name"],
                        p["red" + i + "Image"],
                        p["red" + i + "Level"]
                        ));
                }
            }
            if (p["obLength"] > 0) {
                for (int i = 0; i < p["obLength"]; i++) {
                    DataManager.Instance.RoomTeam(2).Add(new RoomTeam(
                        p["ob" + i + "Index"],
                        p["ob" + i + "Name"],
                        p["ob" + i + "Image"],
                        p["ob" + i + "Level"]
                        ));
                }
            }
            switch (p["state"].AsInt) {
            case 0:
                SceneManager.LoadScene("Room");
                break;
            case 1:
                // 
                break;
            }
            break;
        case STC.UPDATE_ROOM:
            room = new Room(
                p["roomIndex"],
                p["roomName"],
                p["roomBlue"],
                p["roomRed"],
                p["roomState"],
                p["roomMode"],
                p["roomNum"],
                p["roomMaster"],
                p["roomMap"],
                p["blueWinRate"],
                p["redWinRate"]
                );
            DataManager.Instance.NowRoom = room;
            DataManager.Instance.InitRoomTeam();
            if (p["blueLength"] > 0) {
                for (int i = 0; i < p["blueLength"]; i++) {
                    DataManager.Instance.RoomTeam(0).Add(new RoomTeam(
                        p["blue" + i + "Index"],
                        p["blue" + i + "Name"],
                        p["blue" + i + "Image"],
                        p["blue" + i + "Level"]
                        ));
                }
            }
            if (p["redLength"] > 0) {
                for (int i = 0; i < p["redLength"]; i++) {
                    DataManager.Instance.RoomTeam(1).Add(new RoomTeam(
                        p["red" + i + "Index"],
                        p["red" + i + "Name"],
                        p["red" + i + "Image"],
                        p["red" + i + "Level"]
                        ));
                }
            }
            if (p["obLength"] > 0) {
                for (int i = 0; i < p["obLength"]; i++) {
                    DataManager.Instance.RoomTeam(2).Add(new RoomTeam(
                        p["ob" + i + "Index"],
                        p["ob" + i + "Name"],
                        p["ob" + i + "Image"],
                        p["ob" + i + "Level"]
                        ));
                }
            }
            if (RoomManager.Instance != null)
                RoomManager.Instance.ChangeData();
            break;
        case STC.START_MOVE:
            if (SceneManager.GetActiveScene().name != "Stage")
                return;
            GameManager.Instance.StartMove(p["no"], p["dx"], p["dy"], p["bx"].AsInt == 0 ? p["x"].AsFloat : p["x"].AsFloat, p["by"].AsInt == 0 ? p["y"].AsFloat : p["y"].AsFloat);
            break;
        case STC.END_MOVE:
            if (SceneManager.GetActiveScene().name != "Stage")
                return;
            GameManager.Instance.EndMove(p["no"], p["bx"].AsInt == 0 ? p["x"].AsFloat : p["x"].AsFloat, p["by"].AsInt == 0 ? p["y"].AsFloat : p["y"].AsFloat);
            break;
        case STC.MOVE_SCENE:
            switch (p["type"].AsInt) {
            case 6:
                SceneManager.LoadScene("Room");
                break;
            }
            break;
        }
    }

    // 서버로 패킷을 보내는 함수
    public void SendPacket(JSONObject packet) {
        JSONObject p = packet;
        string convertString = string.Empty;

        byte[] bytes = Encoding.Unicode.GetBytes(p.ToString());

        int j = 0;
        foreach (char c in p.ToString()) {
            if (IsContainHangul(c)) {
                // Unicode에서 한글을 2칸씩 먹는다.
                convertString += string.Format("\\u{0:X2}{1:X2}", bytes[j + 1], bytes[j]);
                j += 2;
            } else {
                convertString += c;
                j += 2;
            }
        }

        string sendData = string.Empty;
        int nData = convertString.Length;

        // 리틀 엔디안
        sendData += (char)((nData >> 24) & 0xff);
        sendData += (char)((nData >> 16) & 0xff);
        sendData += (char)((nData >> 8) & 0xff);
        sendData += (char)(nData & 0xff);
        sendData += convertString.ToString() + '\n';
        
        // UTF-8로 데이터를 인코딩한다.
        byte[] byteData = Encoding.UTF8.GetBytes(sendData);

        if (!sock.Connected) {
            Connect();
        }

        if (sock.Connected) {
            sock.Send(byteData, byteData.Length, SocketFlags.None);
        }
    }

    // 한글이 포함되어 있나?
    public static bool IsContainHangul(char c) {
        string str = string.Empty + c;
        if (char.GetUnicodeCategory(str[0]) == UnicodeCategory.OtherLetter) {
            return true;
        }
        return false;
    }
}
