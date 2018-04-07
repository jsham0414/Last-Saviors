using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomColSpawn : MonoBehaviour {
    [SerializeField] private GameObject m_Original = null;

    private ArrayList m_Collision;

    int i;

    // Use this for initialization
    void Start() {
        m_Collision = new ArrayList();
        print(m_Original);
        Vector3 Pos = new Vector3(100, 82.8f, 0);
        for (int i = 0; i < 10; i++) {
            GameObject obj = Instantiate(m_Original, transform.position + Pos, transform.rotation, transform);
            print(obj);
            print(obj.gameObject);
            m_Collision.Add(obj);
            Pos.y -= 20;
        }
	}

    void Update() {
        i = 0;
        //TODO : 버튼으로 해보기
        foreach (GameObject b in m_Collision) {
            //b.GetComponent<Button>.

            i++;
        }
    }
}
