using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetPlayer : Character {
    private Animator animator;
    private float lastDX = 0, lastDY = -1;
    public Vector3 MoveQueue { get; set; }
    public bool EndMove { get; set; }
    private Rigidbody2D rb;

    void Start() {
        EndMove = false;
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        if (deadX != 0) {
            lastDX = deadX;
            lastDY = 0;
        }
        if (deadY != 0) {
            lastDY = deadY;
            lastDX = 0;
        }

        animator.SetFloat("Horizontal", lastDX);
        animator.SetFloat("Vertical", lastDY);
        bool Walk = false;
        if (deadX != 0 || deadY != 0)
            Walk = true;
        animator.SetBool("Walk", Walk);
    }

    void FixedUpdate() {
        if (MoveQueue == null)
            DeadReconing();
        else {
            Vector3 DirVec = new Vector3();
            DirVec = MoveQueue - transform.position;

            transform.Translate(DirVec.normalized * Info.Speed * Time.deltaTime);
        }

    }

    void Animation() {

    }

    void DeadReconing() {
        Vector3 dr = new Vector3(0, 0, 0);
        float dx = dPosition.x + Info.Speed * ((Time.time - deadTime));
        float dy = dPosition.y + Info.Speed * ((Time.time - deadTime));

        if (dx * deadX == 0)
            dr.x = dPosition.x;
        else if (deadX < 0)
            dr.x = dPosition.x - Info.Speed * ((Time.time - deadTime));
        else
            dr.x = dx * deadX;

        if (dy * deadY == 0)
            dr.y = dPosition.y;
        else if (deadY < 0)
            dr.y = dPosition.y - Info.Speed * ((Time.time - deadTime));
        else
            dr.y = dy * deadY;

        rb.MovePosition(dr);
        //gameObject.transform.SetPositionAndRotation(dr, gameObject.transform.rotation);
    }
}
