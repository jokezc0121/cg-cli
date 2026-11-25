using UnityEngine;

// Objeto que se recoge: al tocarlo el jugador activa la proteccion de VidaJugador.
public class AkuAku : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VidaJugador vida = other.GetComponent<VidaJugador>();

            if (vida != null)
            {
                vida.ActivarAkuAku();
                Destroy(gameObject);
            }
        }
    }
}
