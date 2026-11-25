using UnityEngine;

// Adaptacion de Enemigo.cs del ejemplo 2D: no se desplaza, solo apunta y dispara.
public class Canonero : MonoBehaviour
{
    public Transform player;

    [Header("Disparo")]
    public Transform canon;
    public Transform firePoint;
    public GameObject proyectil;
    public float distanciaPlayer = 12f;
    public float fireRate = 2f;
    public float rotationSpeed = 4f;

    [SerializeField] private float tiempoDisparo;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia < distanciaPlayer)
        {
            tiempoDisparo += Time.deltaTime;

            if (tiempoDisparo > fireRate)
            {
                tiempoDisparo = 0;
                Disparar();
            }

            MirarJugador();
        }
    }

    private void Disparar()
    {
        // El proyectil toma la rotacion del firePoint, que ya apunta al jugador.
        Instantiate(proyectil, firePoint.position, firePoint.rotation);
    }

    void MirarJugador()
    {
        // Solo gira el canon con LookRotation y Slerp; la base no se mueve.
        Vector3 direction = player.position - canon.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        canon.rotation = Quaternion.Slerp(canon.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
