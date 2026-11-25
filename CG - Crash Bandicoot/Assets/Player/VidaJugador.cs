using UnityEngine;

// Vida del jugador; el numero de vidas se guarda en el Inventario.
public class VidaJugador : MonoBehaviour
{
    public Transform spawn;
    public GameObject akuAkuVisual;
    public float tiempoInvulnerable = 1.5f;

    private bool tieneAkuAku;
    private float tiempoProteccion;
    private Vector3 posicionSpawn;
    private Rigidbody rb;
    private CheckPiso pie;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pie = GetComponent<MovimientoQ>().checkPiso;

        posicionSpawn = transform.position;
        if (spawn != null)
        {
            posicionSpawn = spawn.position;
        }

        if (akuAkuVisual != null)
        {
            akuAkuVisual.SetActive(false);
        }
    }

    void Update()
    {
        if (tiempoProteccion > 0)
        {
            tiempoProteccion -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemigoSalud enemigo = collision.collider.GetComponentInParent<EnemigoSalud>();

        // Si el pie esta por encima del enemigo es un pisoton de CheckPiso, no un golpe.
        if (enemigo != null && pie.transform.position.y <= enemigo.transform.position.y + enemigo.altura)
        {
            RecibirGolpe();
        }
    }

    public void ActivarAkuAku()
    {
        tieneAkuAku = true;

        if (akuAkuVisual != null)
        {
            akuAkuVisual.SetActive(true);
        }
    }

    public void RecibirGolpe()
    {
        // Evita perder dos vidas con el mismo contacto.
        if (tiempoProteccion > 0)
        {
            return;
        }

        tiempoProteccion = tiempoInvulnerable;

        // Aku Aku absorbe el golpe y el jugador no pierde vida.
        if (tieneAkuAku)
        {
            tieneAkuAku = false;

            if (akuAkuVisual != null)
            {
                akuAkuVisual.SetActive(false);
            }
            return;
        }

        Morir();
    }

    void Morir()
    {
        if (Inventario.Instance != null)
        {
            Inventario.Instance.PerderVida();
        }

        rb.linearVelocity = Vector3.zero;
        rb.position = posicionSpawn;
        transform.position = posicionSpawn;
    }
}
