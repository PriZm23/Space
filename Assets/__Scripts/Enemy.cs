using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 10f; // Скорость в м/с
    public float fireRate = 0.3f; // Секунд между выстрелами
    public float health = 10f;
    public int score = 100; // Очки за уничтожение
    private BoundsCheck bndCheck;
    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Это свойство: метод, действующий как поле
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

        if(bndCheck != null && bndCheck.offDown)
        {
            // Корабль за нижней границей, поэтому уничтожить
            Destroy(gameObject);
        }
    }
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if(otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO); // Уничтожить снаряд
            Destroy(gameObject); // Уничтожить врага
        }
    }
}
