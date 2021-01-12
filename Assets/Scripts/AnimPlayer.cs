using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    private Animator anim;       // Переменная аниматора Игрока
    public PlayrMovement PM;     // Сылка на скрипт управления Игроком

    void Start()
    {
        anim = GetComponent<Animator>(); //Получаем компонет аниматора
    }

    // Корутин для паузы, проигрывания получения урона
    IEnumerator Bobo() 
    {
        anim.SetBool("death", true);                   // проигрывание анимации получения урока
        yield return new WaitForSeconds(0.9f);         // ждем 9 секунд что бы проиграть анимацию
        anim.SetBool("death", false);                  // завершаем анимацию урона
        PM.auch.SetActive(false);                      // Отключаем ГО со звуком ай игрока
    }

    void FixedUpdate()
    {
        // Если нажата клавиша W, A, D, то:
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("go", true); // проигрываем анимацию бега
        }
        else                         // в противном случае:
        {
            anim.SetBool("go", false); // отключаем анимацию бега
        }

        // Если сработал флаг получения урона (см. скрипт PlaayerMovement)
        if (PM.dead)
        {
            StartCoroutine(Bobo()); // Проигрываем корутин Бобо!
            PM.dead = false;        // отключаем влаг урон.
        }
    }
}
