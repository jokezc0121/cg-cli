using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Base de MovimientoEnemigo.cs: patrulla lenta con NavMeshAgent hasta detectar al jugador.
public class MovimientoArmadillo : MonoBehaviour
{
    public Transform player;

    public Transform[] waypoints;
    public float waypointDistance = 1f;
    public float rangoPlayer = 8f;

    [Header("Rodar")]
    public GameObject cuerpoNormal;
    public GameObject cuerpoEnrollado;
    public float tiempoEnrollarse = 0.6f;
    public float velocidadRodar = 9f;
    public float tiempoRodando = 2f;
    public float tiempoDescanso = 1.5f;
    public float distanciaGolpe = 1.3f;

    private NavMeshAgent agent;
    private int currentWaypoint = 0;
    private bool enrollado;
    private bool rodando;
    private bool golpeo;
    private Vector3 direccionRodar;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Sin waypoints se queda quieto esperando al jugador.
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    void Update()
    {
        if (rodando)
        {
            Rodar();
            return;
        }

        // Mientras se enrolla o descansa no patrulla ni vuelve a detectar.
        if (enrollado)
        {
            return;
        }

        float distancia = Vector3.Distance(transform.position, player.position);

        // PLAYER CERCA -> enrollarse y rodar
        if (distancia < rangoPlayer)
        {
            StartCoroutine(Enrollarse());
        }
        // PLAYER LEJOS -> patrullar
        else if (waypoints.Length > 0)
        {
            if (!agent.pathPending && agent.remainingDistance <= waypointDistance)
            {
                GoToNextWaypoint();
            }
        }
    }

    IEnumerator Enrollarse()
    {
        enrollado = true;
        golpeo = false;
        agent.ResetPath();
        cuerpoNormal.SetActive(false);
        cuerpoEnrollado.SetActive(true);

        // La direccion se guarda una sola vez para rodar en linea recta.
        direccionRodar = player.position - transform.position;
        direccionRodar.y = 0;
        direccionRodar.Normalize();

        if (direccionRodar != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direccionRodar);
        }

        yield return new WaitForSeconds(tiempoEnrollarse);
        rodando = true;

        yield return new WaitForSeconds(tiempoRodando);
        rodando = false;
        cuerpoNormal.SetActive(true);
        cuerpoEnrollado.SetActive(false);

        yield return new WaitForSeconds(tiempoDescanso);
        enrollado = false;

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    void Rodar()
    {
        // Move lo mantiene sobre el NavMesh, asi se frena en bordes y paredes.
        agent.Move(direccionRodar * velocidadRodar * Time.deltaTime);
        cuerpoEnrollado.transform.Rotate(velocidadRodar * 90f * Time.deltaTime, 0, 0);

        // Solo golpea una vez por cada vez que rueda.
        if (!golpeo && Vector3.Distance(transform.position, player.position) < distanciaGolpe)
        {
            golpeo = true;
            VidaJugador vida = player.GetComponent<VidaJugador>();

            if (vida != null)
            {
                vida.RecibirGolpe();
            }
        }
    }

    void GoToNextWaypoint()
    {
        currentWaypoint++;

        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }

        agent.SetDestination(waypoints[currentWaypoint].position);
    }
}
