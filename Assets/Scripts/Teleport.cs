using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Player; // Сылка на объект Игрок
    public GameObject pos;    // Сылка на объект позиция (координаты)
    

   
    // Если игрок пересечет коллайдер с триггером то перемещаем в координаты объекта pos
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.transform.position = new Vector2(pos.transform.position.x, pos.transform.position.y);
        }
    }
}
