using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main S; // Одиночка

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; // Массив шаблонов Enemy
    public float enemySpawnPerSecond = 0.5f; // Спаун кораблей в секунду
    public float enemyDefaultPadding = 1.5f; // Отступ для позиционирования

    private BoundsCheck bndCheck;
    private void Awake()
    {
        S = this;
        // Записать в bndCheck ссылку на компонент BoundsCheck 
        // этого игрового объекта
        bndCheck = GetComponent<BoundsCheck>();
        // Вызывать SpawnEnemy() один раз (в 2 сек по умолчанию)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }
    public void SpawnEnemy()
    {
        // Выбрать случайный шаблон Enemy для создания
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        //Разместить врагов над экраном в случайной позиции х
        float enemyPadding = enemyDefaultPadding;
        if(go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }
        // Установить начальные координаты врага
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        // Снова вызвать SpawnEnemy()
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }
    public void DelayedRestart(float delay)
    {
        // Вызвать метод Restart() через delay секунд
        Invoke("Restart", delay);
    }
    public void Restart()
    {
        // Перезагрузить _Scene_0
        SceneManager.LoadScene("_Scene_0");
    }
}
