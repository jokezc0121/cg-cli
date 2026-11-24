using UnityEngine;

// Adaptacion de posiciones, Vector3 y LookRotation vistos en 3D.
public class CamaraSeguimiento : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 6, -8);

    private CheckPiso pie;
    private float alturaDeApoyo;

    void Start()
    {
        pie = target.GetComponent<MovimientoQ>().checkPiso;
        alturaDeApoyo = pie.transform.position.y;
    }

    void LateUpdate()
    {
        if (pie.enPiso)
        {
            alturaDeApoyo = pie.transform.position.y;
        }
        Vector3 posicion = target.position + offset;
        // Sigue las lomas y conserva la referencia del ultimo apoyo durante el salto.
        posicion.y = Mathf.MoveTowards(transform.position.y, alturaDeApoyo + offset.y, 4f * Time.deltaTime);
        transform.position = posicion;

        Vector3 direction = new Vector3(0, 1, 0) - offset;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
