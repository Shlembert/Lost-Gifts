using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    public float speed;        //Скорость врага
    private GameObject box;    // подпрочки
    public Transform ghost;    // компонент трансформаций
    public PlayrMovement PM;   // Сылка на скрипт управления игроком
    public int ghostCoins;     // собраные подарки
    public AudioSource ha;     // звук смеха приведения
    private Vector2 target;    // позиция врага

    //#################################################################//

    void Start()
    {
        ghost = GetComponent<Transform>(); // получаем компонет Трансформы
        ha = GetComponent<AudioSource>();  // получаем компонент звука
        target = transform.position;       // запоминаем начальную позицию врага
    }

    // Если враг столкнулся триггером:
    public void OnTriggerEnter2D(Collider2D other)
    {
        // а триггер этот оказался подарком, то:
        if (other.gameObject.CompareTag("box"))
        {
            Destroy(other.gameObject); // удаляем подарок со сцены
            ghostCoins++;              // прибавляем к счетчику подарков врага 1
            ha.Play();                 // мерзко хихикаем
            speed += 0.02f;             // увеличиваем скорость врага. Аркада же, сложность растет.
            PM.boxUp = true;           // Флаг подарок подобран ВКЛ
            box = null;                // подарков на сцене нет.
        }
    }

    void Update()
    {
        // Если подарков на сцене нет, то:
        if (box == null)
        {
            // ищем подарок пока не появиться
            box = GameObject.FindWithTag("box");
            // отползаем к начальной точке на половину скорости. Ну что бы не сбивались в кучу.
            ghost.position = Vector2.MoveTowards(ghost.position, target, 0.5f * speed * Time.deltaTime);
        }
        else // если подарок найден, то:
        {
            // меняем позицию на новую в сторону подарка по текущему времени умноженному на скорость
            ghost.position = Vector2.MoveTowards(ghost.position, box.transform.position, speed * Time.deltaTime);
        }
    }
}
