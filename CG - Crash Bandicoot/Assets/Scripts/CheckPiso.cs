using UnityEngine;

// Detector con triggers 3D; combina las bases de suelo y colisiones de clase.
public class CheckPiso : MonoBehaviour
{
    public bool enPiso;
    public Rigidbody cuerpo;
    private int contactosPiso;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Piso"))
        {
            if (contactosPiso == 0)
            {
                enPiso = true;
            }
            contactosPiso++;
        }

        if (other.CompareTag("Enemigo") && !enPiso && cuerpo.linearVelocity.y < 0)
        {
            EnemigoSalud enemigo = other.GetComponent<EnemigoSalud>();

            // Solo el pie que desciende por encima del techo cuenta como pisoton.
            if (transform.position.y > other.transform.position.y + enemigo.altura)
            {
                enemigo.getDamage(1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Piso"))
        {
            contactosPiso--;
            if (contactosPiso == 0)
            {
                enPiso = false;
            }
        }
    }
}
