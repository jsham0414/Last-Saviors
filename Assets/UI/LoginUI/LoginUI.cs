using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using Header;

public class LoginUI : MonoBehaviour {
    [SerializeField] private GameObject m_RegisterUI;
    private InputField m_Id;
    private InputField m_Pass;

    void Start () {
        foreach (InputField obj in GetComponentsInChildren<InputField>()) {
            switch (obj.name) {
            case "Id":
                m_Id = obj;
                break;
            case "Pass":
                m_Pass = obj;
                break;
            }
        }
    }

    public void Login() {
        if (m_Id.text.ToString() != "" || m_Pass.text.ToString() != "") {
            JSONObject p = new JSONObject();
            p["header"] = STC.LOGIN;
            p["id"] = m_Id.text.ToString();
            p["pass"] = m_Pass.text.ToString();

            NetworkManager.Instance.SendPacket(p);
        } else {
            print("아이디나 비밀번호는 공백일 수 없습니다.");
        }
    }

    public void CreateRegister() {
        Instantiate(m_RegisterUI, GameObject.Find("Canvas").transform);
    }
}
