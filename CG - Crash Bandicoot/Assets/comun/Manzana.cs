using UnityEngine;

// Como la moneda de Movimiento.cs 2D: al tocarla el jugador se suma al inventario.
public class Manzana : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Inventario.Instance != null)
            {
                Inventario.Instance.SumarManzana();
            }

            Destroy(gameObject);
        }
    }
}
