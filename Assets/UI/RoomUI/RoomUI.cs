using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour {
	void Start () {
		foreach (Text t in GetComponentsInChildren<Text>()) {
            switch (t.name) {
            case "RoomTitle":
                t.text = DataManager.Instance.NowRoom.Name;
                break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
