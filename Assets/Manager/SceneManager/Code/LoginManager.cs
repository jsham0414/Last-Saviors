// LoginManager.cs
// 로그인 화면, 로그인을 담당할 클래스입니다.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using Header;

public class LoginManager : MonoBehaviour {
    [SerializeField] private InputField m_ifId;
    [SerializeField] private InputField m_ifPassword;
    [SerializeField] private Button m_btLogin;
    private bool m_bLoginClicked;

	// Use this for initialization
	void Start () {
        m_bLoginClicked = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return) || m_bLoginClicked == true) {
            m_bLoginClicked = false;
        }
	}

    void Register() {
    }
    
    public void ClickLoginButton() {
        m_bLoginClicked = true;
    }
}
