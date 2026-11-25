using UnityEngine;

public class MovimientoQ : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float runSpeed = 3.5f;
    public float speedSalto = 8.1f;
    public CheckPiso checkPiso;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Saltar();
        }
    }

    public void Saltar()
    {
        // Adaptacion del salto de Movimiento.cs 2D a Rigidbody y Vector3.
        if (checkPiso.enPiso)
        {
            // El apoyo se recupera al volver a tocar suelo, incluso en pendiente.
            checkPiso.enPiso = false;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, speedSalto, rb.linearVelocity.z);
        }
    }

    void FixedUpdate()
    {
        Mover(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetAxis("Fire3") > 0);
    }

    public void Mover(float horizontalInput, float verticalInput, bool correr)
    {
        // Adaptacion: ambos ejes desplazan al personaje por el camino.
        Vector3 direccion = new Vector3(horizontalInput, 0, verticalInput);
        if (direccion.magnitude > 1f)
        {
            direccion = direccion.normalized;
        }

        // Carrera: el eje Fire3 ya corresponde a Shift en Input Manager.
        float velocidad = moveSpeed;
        if (correr)
        {
            velocidad = runSpeed;
        }

        // Como en Movimiento.cs 2D, conservamos Y para el salto y la gravedad.
        rb.linearVelocity = new Vector3(direccion.x * velocidad, rb.linearVelocity.y, direccion.z * velocidad);

        // LookRotation se vio en clase; aqui mira hacia donde nos movemos.
        if (direccion != Vector3.zero)
        {
            rb.MoveRotation(Quaternion.LookRotation(direccion));
        }
    }
}
