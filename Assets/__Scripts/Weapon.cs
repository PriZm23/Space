using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    none, // По умолчанию / нет оружия
    blaster, // Бластер
    spread, // Веерная пушка
    phaser, // Волновой фазер
    missile, // Самонаводящиеся ракеты
    laser, // Наносит повреждения при долговременном воздействии
    shield // Увеличивает щит
}
[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter; // Буква на кубике, изоюражающем бонус
    public Color color = Color.white; // Цвет ствола оружия и кубика бонуса
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
    public float damageOnHit = 0; // Разрушительная мощность
    public float continuousDamage = 0; // Степень разрушения в секунду для Laser
    public float delayBetweenShots = 0;
    public float velocity = 20; // Скорость полёта снарядов
}
public class Weapon : MonoBehaviour
{
    
}
