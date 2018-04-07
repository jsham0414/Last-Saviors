using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFrame : MonoBehaviour {
    [SerializeField] private Texture m_Texture = null;
    [SerializeField] private int m_Type = 0;

    // Use this for initialization
    void Start () {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        // 높이, 밑면\
        float f = Mathf.Sqrt(Mathf.Pow(2, 2) - Mathf.Pow(1f, 2)) - 1;

        switch (m_Type) {
            case 0:
                mesh.vertices = new Vector3[] { new Vector3(0, f, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0) };
                mesh.uv = new Vector2[] { new Vector2(0.5f, 1), new Vector2(1, 0), new Vector2(0, 0) };
                mesh.triangles = new int[] { 0, 1, 2 };
                break;
            case 1:

                break;
        }

        Material material = new Material(Shader.Find("Standard"));
        material.SetTexture("_MainTex", m_Texture);
        GetComponent<MeshRenderer>().material = material;


    }

    // Update is called once per frame
    void Update () {
		
	}
}
