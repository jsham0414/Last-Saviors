using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using Header;

public class Player : Character {
    private Animator animator;
    private float dX = 0, dY = 0;
    private int pX = 0, pY = 0;
    private float pLastX = 0, pLastY = 0;
    private bool Walk = false;
    private bool Move = false;
    private bool LastMove = false;
    private Rigidbody2D rb;
    private float m_MoveUpdate;

    void Start() {
        GameObject.Find("CM vcam1").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = transform;
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        m_MoveUpdate = Time.time + 0.5f;
    }

    void Update() {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        KeyCheck();
    }

    void FixedUpdate() {
        AnimateAndMove();
    }

    void KeyCheck() {
        if ((pLastX != pX || pLastY != pY)) {
            m_MoveUpdate = Time.time + 0.5f;
            Move = true;
            JSONObject p = new JSONObject();
            p["header"] = CTS.START_MOVE;
            p["dx"] = pX;
            p["dy"] = pY;
            p["x"] = gameObject.transform.position.x;
            p["y"] = gameObject.transform.position.y;
            pLastX = pX;
            pLastY = pY;
            NetworkManager.Instance.SendPacket(p);
        } else if (pX == 0 && pY == 0) {
            Move = false;
        }

        if (Move == false && LastMove == true) {
            JSONObject p = new JSONObject();
            p["header"] = CTS.END_MOVE;
            p["x"] = gameObject.transform.position.x;
            p["y"] = gameObject.transform.position.y;
            NetworkManager.Instance.SendPacket(p);
        }
        LastMove = Move;
    }

    float Round(float f) {
        return (float)Math.Round(f, 5);
    }

    void AnimateAndMove() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        pX = 0;
        pY = 0;

        Walk = true;
        if (h > 0) {
            dX = 1;
            dY = 0;
        } else if (h < 0) {
            dX = -1;
            dY = 0;
        } else if (v > 0) {
            dX = 0;
            dY = 1;
        } else if (v < 0) {
            dX = 0;
            dY = -1;
        } else {
            Walk = false;
        }

        if (h > 0) {
            pX = 1;
        } else if (h < 0) {
            pX = -1;
        }
        if (v > 0) {
            pY = 1;
        } else if (v < 0) {
            pY = -1;
        }

        if (Walk) {
            rb.MovePosition(transform.position + new Vector3(pX, pY, 0) * Time.deltaTime * Info.Speed);
            //gameObject.transform.Translate(new Vector3(pX, pY, 0) * Time.deltaTime * 4);
        }

        animator.SetFloat("Horizontal", dX);
        animator.SetFloat("Vertical", dY);
        animator.SetBool("Walk", Walk);
    }
}
