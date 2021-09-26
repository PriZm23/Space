using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public static Hero S; // Одиночка

    [Header("Set in Inspector")]
    // Поля управляющие движением корабля
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;

    // Эта переменная хранит ссылку на последнийстолкнувшийся игровой объект
    private GameObject lastTriggerGo = null;

    // Объявление нового делегата типа WeaponFireDelegate
    public delegate void WeaponFireDelegate();

    // Создать поле типа WeaponFireDelegate с именем fireDelegate
    public WeaponFireDelegate fireDelegate;

    private void Awake()
    {
        if (S == null)
        {
            S = this; // Сохранить ссылку на одиночку
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
        //fireDelegate += TempFire;
    }
    private void Update()
    {
        // Извлечь инфо из класса Input
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Изменить transform.position, опираясь на информацию по осям
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Повернуть корабль, чтобы придать ощущение динамики
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        // Позволить кораблю выстрелить
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TempFire();
        //}

        // Произвести выстрел из всех видов оружия вызовом fireDelegate
        // Сначала проверить нажатие клавиши: Axis("Jump")
        // Затем убедиться, что значение fireDelegate не равно null,
        // чтобы избежать ошибки
        if(Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }
    }
    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        //rigidB.velocity = Vector3.up * projectileSpeed;

        Projectile proj = projGO.GetComponent<Projectile>();
        proj.type = WeaponType.blaster;
        float tSpeed = Main.GetWeaponDefinition(proj.type).velocity;
        rigidB.velocity = Vector3.up * tSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered: " + go.name);

        //Гарантировать невозможность повторного столкновения с тем же объектом
        if(go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;

        if(go.tag == "Enemy") // Если защитное поле столкнулось с вражеским кораблём
        {
            shieldLevel--; // Уменьшить уровень защиты на 1
            Destroy(go); // и уничтожить врага
        }
        else if(go.tag == "PowerUp")
        {
            // Если защитное поле столкнулось с бонусом
            AbsorbPowerUp(go);
        }
        else
        {
            print("Triggered by non Enemy:" + go.name);
        }
    }
    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        //switch (pu.type)
        {
            // Пока пустой
        }
        pu.AbsorbedBy(this.gameObject);
    }
    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            // Если уровень поля упал до нуля или ниже
            if(value < 0)
            {
                Destroy(gameObject);
                // Сообщить объекту Main.S о необходимости перезапустить игру
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }
}
