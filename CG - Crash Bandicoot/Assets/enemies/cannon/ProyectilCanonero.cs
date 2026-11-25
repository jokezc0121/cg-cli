using UnityEngine;

// Adaptacion de Bala.cs del ejemplo 2D a Rigidbody y Collider 3D.
public class ProyectilCanonero : MonoBehaviour
{
    public float speed = 8f;
    public Rigidbody rb;

    private void Start()
    {
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VidaJugador vida = other.GetComponent<VidaJugador>();

            if (vida != null)
            {
                vida.RecibirGolpe();
            }
            Destroy(gameObject);
        }

        if (other.CompareTag("Piso") || other.CompareTag("Obstaculo"))
        {
            Destroy(gameObject);
        }
    }
}
