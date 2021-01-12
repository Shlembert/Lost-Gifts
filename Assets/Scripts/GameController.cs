using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayrMovement PM; // Скрипт управления игроком

    public Enemy ghost1;     //-------------------------------- //
    public Enemy ghost2;     //  Скрипты управления врагами     //
    public Enemy ghost3;     //                                 //
    public Enemy ghost4;     //---------------------------------//

    public Text cointTxt;    // Текстовое поле выводящее собраные подарки во время игры
    public Text result;      // Текстовое поле выводящее сумму собраных подарков врагами.
    public Text ghostC;      // Текстовое поле выводящее собранные падарки в конце игры.(в Панеле GameOver)

    public GameObject Menu;  // Панель меню, онаже главная страничка начала игры.
    public GameObject Instruction; // Панель с правилами игры
    public GameObject Pause;       // Панель паузы
    public GameObject GameOver;    // Панель окончания игры

    public GameObject Player;     // Объект Игрок

    public GameObject Ghost1;     // Объекты Враги 1, 
    public GameObject Ghost2;     // 2,
    public GameObject Ghost3;     // 3,
    public GameObject Ghost4;     // 4.

    public AudioSource klick;   // Звук нажатия кнопки
    public AudioSource god;     // Звук юбилейного подарка 10,20,30...
    private bool kek;           // Флаг для одиночного срабатывания очков жизни
    public bool gameStart;      // Флаг определяющий начало Игры
    private int ghostResult;    // Переменная суммирующая собранные подарки врагов

    public GameObject hp1;      // Спрайт "сердечко" обознаяающий 1 жизнь.
    public GameObject hp2;      // Спрайт "сердечко" обознаяающий 2 жизни.
    public GameObject hp3;      // Спрайт "сердечко" обознаяающий 3 жизни.

    //#######################################################################################//

    // Метод  задающий начальные параметры игры
    void Start()
    {
        klick = GetComponent<AudioSource>();          // получаем компонет управление звуком "нажатой кнопки"
        Cursor.visible= true;                         // Включаем видимость курсора
        kek = true;                                   // Ставим временный флаг кек в "вкл"
        gameStart = false;                            // Как бы это странно не выглядело, но игра еще не началась. Флаг "выкл"
    }

    //**********************___Методы___***********************************************************//

    // Метод срабатывающий при нажатии кнопки старт:
    public void GameStart()
    {
        Menu.SetActive(false);        // Скрываем панель Меню
        klick.Play();                 // Проигрываем звук "кнопка"
        Instruction.SetActive(true);  // Активируем панель Инструкция с правилами игры
        Time.timeScale = 0;           // Останавливаем время, что бы ни какая падла... Что бы игрок не упал.
    }

    // Метод Продолжить, срабатывает при нажатии на кнопку Continue
    public void Continue()
    {
        Instruction.SetActive(false);  // Скрываем панель Инструкция
        Cursor.visible = false;        // Прячем курсор
        Time.timeScale = 1;            // Запускаем время
        klick.Play();                  // Проигрываем звук "кнопка"
        gameStart = true;              // Ставим флаг Старт игры в "вкл"
    }

    // Метод Пауза. Вызывается нажатием на клавишу Esc
    public void Pauses()
    {
        Time.timeScale = 0;        // Останавливаем время, а мы это можем.
        Pause.SetActive(true);     // Делаем активным/видимым панель Пауза.
        Cursor.visible = true;     // Делаем курсор видимым.
    }

    // Метот вернутся. Вызывается нажанием кнопки Return из панели Пауза.
    public void Return()
    {
        Time.timeScale = 1;        // Запускаем время
        Pause.SetActive(false);    // Прячем панель Пауза
        Cursor.visible = false;    // Прячем курсор
    }

    // Метод закрытия приложения вызывается нажатием кнопки Exit
    public void Quit()
    {
        Application.Quit();
    }

    // Метод Меню. Вызывается нажатием на кнопку Меню
    public void MenuP()
    {
        Pause.SetActive(false);                                             // Прячем панель Пауза
        gameStart = false;                                                  // Переключаем флаг Старт игры в "выкл"
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // Запускаем сцену С начала.
        klick.Play();                                                       // Проигрываем звук кнопки
    }
    
    // Метод Рестарт. Вызывается кнопкой Рестарт в панеле GameOver (Конец игры)
    public void Restart()
    {
        GameOver.SetActive(false);                                          // Cкрываем панель GameOver
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // Запускаем сцену С начала.
        klick.Play();                                                       // Проигрываем звук кнопки
    }

    //#################################################################################################//

    void Update()
    {
        // Выводим число собранных подарков Игроком во время игры
        cointTxt.text = PM.coins + "";

        // Суммируем все собранные подарки всеми врагами
        ghostResult = ghost1.ghostCoins + ghost2.ghostCoins + ghost3.ghostCoins + ghost4.ghostCoins;

        // выводим сумму собраных врагами подарков в поле в панеле GameOver
        ghostC.text = ghostResult + "";

        // Метод Пауза при нажатой клавиши Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pauses();
        }

        // Проверяем, окончена игра? Если верно:
        if (PM.gameover)
        {
            Time.timeScale = 0;             // Останавливаем время
            GameOver.SetActive(true);       // Активируем панель GameOver
            Cursor.visible = true;          // Включаем курсор
            gameStart = false;              // Ставим флаг Старт Игры в "выкл"
            result.text = PM.coins + "";    // Выводим число собранных подарков игроком в панель GameOver
        }


        #region Метод активации врагов  
        // Проверяем сколько собрал игрок подарков, если 10, 20, 30, 40. Активируем врагов.
        if (PM.coins == 10)
        {
            Ghost1.SetActive(true);
        }

        if (PM.coins == 20)
        {
            Ghost2.SetActive(true);
        }

        if (PM.coins == 30)
        {
            Ghost3.SetActive(true);
        }

        if (PM.coins == 40)
        {
            Ghost4.SetActive(true);
        }
        #endregion

        // Метод добавляющий жизни Игроку

        if (PM.coins % 10 == 0 && PM.coins >0) //Если кратно 10, то:
        {
            if (kek) // Проверяем через флаг, выполнялся ли этот код?
            {
                if (PM.hp < 3) // Проверяем, меньше максимума? То:
                {
                    PM.hp++;  // Увеличиваем количество жизни на 1.
                }

                god.Play();   // Проигрываем звук "типа хор" при юбилейном подарочке
                kek = false;  // Ставим флаг в "выкл" сообщяя, что метод уже сработал.
            }
        }

        // Этот метод возвращает флаг в "вкл"
        if(PM.coins % 10 != 0) //Если кол-во собраных коробок не кратно 10, то:
        {
            kek = true; // флаг "вкл" Что позволяет сработать Юбилею на следующем десятке.
        }

        // Проверяем сколько жизней у игрока и включаем/отключаем изображение сердечек.
        if (PM.hp == 3)
        {
            hp1.SetActive(true);
            hp2.SetActive(true);
            hp3.SetActive(true);
        }
        else if(PM.hp == 2)
        {
            hp1.SetActive(true);
            hp2.SetActive(true);
            hp3.SetActive(false);
        }
        else if(PM.hp == 1)
        {
            hp1.SetActive(true);
            hp2.SetActive(false);
            hp3.SetActive(false);
        }
        else if (PM.hp == 0)
        {
            hp1.SetActive(false);
            hp2.SetActive(false);
            hp3.SetActive(false);
        }
    }
}
