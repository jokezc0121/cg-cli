using UnityEngine;

// Recorrido de PlataformaMovimiento.cs y persecucion de MovimientoEnemigo.cs; no usa NavMesh porque vuela.
public class MovimientoAbeja : MonoBehaviour
{
    public Transform player;

    public Transform[] waypoints;
    public float waypointDistance = 0.2f;
    public float rangoPlayer = 6f;

    public float velocidad = 2f;
    public float velocidadPersecucion = 3.5f;
    public float rotationSpeed = 5f;
    public float alturaVuelo = 1.3f;

    [Header("Picadura")]
    public float distanciaAtaque = 1.5f;
    public float fireRate = 1f;

    private int currentWaypoint = 0;

    [SerializeField] private float tiempoPicadura;

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

        // PLAYER CERCA -> perseguir
        if (distancia < rangoPlayer)
        {
            // Vuela un poco por encima del jugador para que se le pueda pisar al saltar.
            Volar(player.position + Vector3.up * alturaVuelo, velocidadPersecucion);

            tiempoPicadura += Time.deltaTime;

            if (distancia < distanciaAtaque && tiempoPicadura > fireRate)
            {
                tiempoPicadura = 0;
                Picar();
            }
        }
        // PLAYER LEJOS -> patrullar
        else if (waypoints.Length > 0)
        {
            Volar(waypoints[currentWaypoint].position, velocidad);

            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= waypointDistance)
            {
                GoToNextWaypoint();
            }
        }
    }

    void Volar(Vector3 destino, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, destino, speed * Time.deltaTime);

        // LookRotation y Slerp de clase, solo en horizontal para que no se incline.
        Vector3 direction = destino - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Picar()
    {
        VidaJugador vida = player.GetComponent<VidaJugador>();

        if (vida != null)
        {
            vida.RecibirGolpe();
        }
    }

    void GoToNextWaypoint()
    {
        currentWaypoint++;

        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }
    }
}
