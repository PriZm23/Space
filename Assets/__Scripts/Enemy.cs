using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 10f; // �������� � �/�
    public float fireRate = 0.3f; // ������ ����� ����������
    public float health = 10f;
    public int score = 100; // ���� �� �����������

    // ��� ��������: �����, ����������� ��� ����
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }
    private void Update()
    {
        Move();
    }
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
}