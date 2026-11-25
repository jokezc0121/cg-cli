using UnityEngine;

// Se rompe al chocar con el Player desde arriba o desde abajo, o al recibir una hitbox Attack.
public class Caja : MonoBehaviour
{
    // Diferencia de altura entre el centro del jugador y el de la caja para contar como arriba o abajo.
    public float alturaGolpe = 0.9f;

    private bool rota;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float diferencia = collision.transform.position.y - transform.position.y;

            // Positiva: cae sobre la caja. Negativa: la golpea desde abajo. De lado no se rompe.
            if (Mathf.Abs(diferencia) > alturaGolpe)
            {
                Romper();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            Romper();
        }
    }

    void Romper()
    {
        // Evita contar dos bananas si la tocan dos cosas en el mismo cuadro.
        if (rota)
        {
            return;
        }

        rota = true;

        if (Inventario.Instance != null)
        {
            Inventario.Instance.SumarBanana();
        }

        Destroy(gameObject);
    }
}
