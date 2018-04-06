using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using Header;

public class CreateAccount : MonoBehaviour {
    private InputField m_Id;
    private InputField m_Pass;
    private InputField m_Name;
    private InputField m_Email;

    void Start() {
        foreach (InputField obj in GetComponentsInChildren<InputField>()) {
            switch (obj.name) {
            case "Id":
                m_Id = obj;
                break;
            case "Pass":
                m_Pass = obj;
                break;
            case "Name":
                m_Name = obj;
                break;
            case "Email":
                m_Email = obj;
                break;
            }
        }
    }

    public void Register() {
        JSONObject p = new JSONObject();
        p["header"] = CTS.REGISTER;
        p["id"] = m_Id.text;
        p["pass"] = m_Pass.text;
        p["name"] = m_Name.text;
        p["email"] = m_Email.text;
        NetworkManager.Instance.SendPacket(p);
    }

    public void Cancle() {
        Destroy(gameObject);
    }
}
