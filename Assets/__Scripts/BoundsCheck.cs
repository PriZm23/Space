using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    /// <summary>
    /// I ������������� ����� �������� ������� �� ������� ������.
    /// �����: �������� ������ � ��������������� ������� Main Camera �[0, 0, 0].
    /// </summary>
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Set Dynamically")]
    public float camWidth;
    public float camHeight;
    public bool isOnScreen = true;

    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;

        if(pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            isOnScreen = false;
        }
        if(pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            isOnScreen = false;
        }
        if(pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            isOnScreen = false;
        }
        if(pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            isOnScreen = false;
        }
        if(keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
        }
    }
    // ������ ������� � ������ Scene � ������� OnDrawGizmos()
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
