using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource music;  // Проигрыватель музыки
    public AudioClip[] list;   // Плейлист, массив из мелодий
    private int play;          // переменная хранящая индекс мелодии

    void Start()
    {
        music = GetComponent<AudioSource>(); // получаем компонет проигрывателя
        play = Random.Range(0, list.Length); // случайно выбираем мелодию и сохраняем в переменную индекс
        music.clip = list[play];             // загружаем в проигрыватель выбранную мелодию по индексу
        music.Play();                        // играем мелодию
    }
    
}
