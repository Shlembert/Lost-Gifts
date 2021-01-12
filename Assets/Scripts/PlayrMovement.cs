using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayrMovement : MonoBehaviour
{
    public Rigidbody2D rb;   // Переменная RigidBody не знаю зачем такое коментировать.
    public float speed;      // Переменная скорость игрока.
    public float coins;      // Переменная хранящая кол-во собранных подарков.
    public int hp;           // Количество жизней игрока.

    public bool boxUp;       // Флаг, собран ли подарок.
    public bool dead;        // Флаг, получил ли урон персонаж
    public bool gameover;    // Флаг, закончилась ли игра?

    public AudioSource win;  // Звук поднятия подарка. Как то криво назвал, но менять уже поздно.
    public GameObject auch;  // ГО, на котором весит звук "АЙ!" игрока

    public GameController GC; // Сылка на скрипт GameController в котором твориться вся магия.
    
    //######################################################################################//

    void Start()
    {
        win = GetComponent<AudioSource>();// получаем контроль над звуком поднятия подарочка
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент на физ. тело игрока, что бы творить с ним всякое.
        dead = false;                     // Флаг урон (ну вот так назвал) ВЫКЛ
        boxUp = false;                    // Флаг подняли подарок ВЫКЛ
        gameover = false;                 // Флаг конец игры ВЫКЛ
        coins = 0;                        // собрано ноль
    }

    // Триггер при прикосновении с подарочком
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("box")) // Если игрок столкнулся с объектом и это подарочек, то:
        {
            Destroy(other.gameObject);   // убераем со сцены этот объект
            coins++;                     // прибавляем к собранным подаркам 1
            boxUp = true;                // Флаг подбор подарочка ВКЛ
            win.Play();                  // Играем звук поднятия подарка
        }
    }

    // Если столнулись с объектами имеющими коллайдер, то:
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (hp < 2) // проверяем сколько здоровья. Не знаю почему работает проверка на 2. Если ставить 3 почемуто появляется доп. жизнь.
        {           // Смысл: если жизней не осталось, выполняем:

            StartCoroutine(Death()); // Запускаем корутин Death (см. ниже)
            Time.timeScale = 0.3f;   // уменьшаем течение времени в три раза. Слоумо все дела.
        }
        auch.SetActive(true);        // Активируем ГО со звуком, что бы проиграть "АУЧ!" Криво конечно, но работает.
        dead = true;                 // Флаг Урон ВКЛ
        hp --;                       // Жизни уменьшаем на 1
    }

    // А это тот самый корутин Смерть:
    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.6f); // ждем 6 секунд
        gameover = true;                       // Включаем флаг Конец игры.
    }

    void FixedUpdate()
    {
        if (GC.gameStart)  // Если игра запущена, то:
        {
            rb.gravityScale = 0.3f;  // добавляем гравитацию

            if (Input.GetKey(KeyCode.W)) // Если нажата клавиша W, то:
            {
                rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse); // Добавляем силу вверх умноженную на скорость типом импульс
            }

            if (Input.GetKey(KeyCode.A)) // Если нажата клавиша A, то:
            {
                rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);     // Добавляем силу вверх умноженную на скорость типом импульс
                rb.AddForce(-Vector2.right * 0.5f, ForceMode2D.Impulse);  // Добавляем силу влево умноженную на скорость типом импульс
                transform.localRotation = Quaternion.Euler(0, 180, 0);    // разворачиваем спрайт в сторону движения
            }

            if (Input.GetKey(KeyCode.D))  // Если нажата клавиша A, то:
            {
                rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);    // Добавляем силу вверх умноженную на скорость типом импульс
                rb.AddForce(Vector2.right * 0.5f, ForceMode2D.Impulse);  // Добавляем силу вправо умноженную на скорость типом импульс
                transform.localRotation = Quaternion.Euler(0, 0, 0);     // разворачиваем спрайт в сторону движения
            }
        }
        else  // Если игра не запущена, то:
        {
            rb.gravityScale = 0f; // убераем гравитацию, что бы игрок не падал. Решение кривое, из-за меню на той же сцене. 
        }
    }
}
