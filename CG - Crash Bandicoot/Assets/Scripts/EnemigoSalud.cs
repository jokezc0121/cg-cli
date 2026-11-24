using UnityEngine;

// El metodo de dano procede de Enemigo.cs del ejemplo 2D.
public class EnemigoSalud : MonoBehaviour
{
    public float salud = 1f;
    public float altura = 1.2f;

    public void getDamage(float dmg)
    {
        salud -= dmg;

        if (salud <= 0)
        {
            Destroy(gameObject);
        }
    }
}
