using System;
using UnityEngine;

public class Character : MonoBehaviour {
    public Vector3 dPosition;
    public int deadX = -1, deadY = -1;
    public float deadTime;
    public CharacterInfo Info { get; set; }

    void Awake() {
        Info = new CharacterInfo();
    }
}

public class CharacterInfo {
    private int m_No;
    private string m_Name;
    private string m_Image;
    private int m_Level;
    private float m_Hp;
    private float m_MaxHp;
    private float m_Mp;
    private float m_MaxMp;
    private int m_Team;
    private float m_Speed;

    public CharacterInfo() {
        m_Speed = 2.0f;
    }

    public int No { get { return m_No; } set { m_No = value; } }
    public string Name { get { return m_Name; } set { m_Name = value; } }
    public string Image { get { return m_Image; } set { m_Image = value; } }
    public int Level { get { return m_Level; } set { m_Level = value; } }
    public float Hp { get { return m_Hp; } set { m_Hp = value; } }
    public float MaxHp { get { return m_MaxHp; } set { m_MaxHp = value; } }
    public float Mp { get { return m_Mp; } set { m_Mp = value; } }
    public float MaxMp { get { return m_MaxMp; } set { m_MaxMp = value; } }
    public int Team { get { return m_Team; } set { m_Team = value; } }
    public float Speed { get { return m_Speed; } set { m_Speed = value; } }
}