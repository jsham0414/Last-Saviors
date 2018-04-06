using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    [SerializeField] private GameObject m_CardFrame;

	// Use this for initialization
	void Start () {
		for (int y = 0; y < 7; y++) {
            for (int x = 0; x < y; x++) {
                float f = Mathf.Sqrt(Mathf.Pow(2, 2) - Mathf.Pow(1f, 2));
                Vector3 Pos = new Vector3(x * 2 - (y * 1), -y * f, 0);
                GameObject a;
                a = Instantiate(m_CardFrame, Pos, Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
