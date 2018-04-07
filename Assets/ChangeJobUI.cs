using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using Header;


public class ChangeJobUI : MonoBehaviour {
    private List<GameObject> m_Button;
    [SerializeField] private GameObject m_Original;
    
    void Start() {
        m_Button = new List<GameObject>();

        for (int i = 0; i < 4; i++) {
            Vector3 v3 = new Vector3(0, i * -23.5f, 0);
            GameObject obj = Instantiate(m_Original, transform);
            obj.transform.Translate(v3);
            obj.GetComponentInChildren<Text>().text = DataManager.Instance.JobByIndex(i);
            SetButtonOnClickAnswer(obj.GetComponent<Button>(), i);
            m_Button.Add(obj);
        }
    }

    void SetButtonOnClickAnswer(Button button, int index) {
        button.onClick.AddListener(() => SetJob(index + 1));
    }
    
    void Update() {

    }

    void Destroy() {
        foreach (GameObject obj in m_Button) {
            obj.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    public void SetJob(int i) {
        JSONObject p = new JSONObject();
        p["header"] = CTS.ROOM_SETTING;
        p["type"] = 2;
        p["value"] = i;
        NetworkManager.Instance.SendPacket(p);
    }
}
