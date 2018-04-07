using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {

    void Start() {
        Destroy(gameObject, 5.0f);
    }

    public void AcceptClick() {
        Destroy(gameObject);
    }
}
