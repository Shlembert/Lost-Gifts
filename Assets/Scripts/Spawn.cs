using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform[] Pos;     // массив позиций объектов служащие координатами и разбросаные по игровому полю.
    public GameObject[] Gifts;  // массив объектов подарки
    public  PlayrMovement PM;   // скрипт управления игроком
    

    private void Start()
    {
        PM.boxUp = false;       //На всякий случай ставим флаг подбора подарка ВЫКЛ. 
    }

    // Корутин призванный делать паузу перед созданием подарка на игровом поле.
    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(2); // пауза 2 секунды
        int m = Random.Range(0, 14);                // переменная получающяя случайный индекс массива координат
        int n = Random.Range(0, 4);                 // переменная получающяя случайный индекс массива подарков

        // Создаем случайный объект в случайных координатах не поворачивая.
        GameObject gameObject1 = Instantiate(Gifts[n], Pos[m].position, Quaternion.identity);
        _ = gameObject1;
    }
    void Update()
    {
        if (PM.boxUp) // Если подарок подобран, то: 
        {
            PM.boxUp = false;  // отключаем флаг подобран кодапрок
            _ = StartCoroutine(Wait()); // запускаем корутин создания нового подарка
        }
    }
}
