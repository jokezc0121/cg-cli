using UnityEngine;

// El metodo de dano procede de Enemigo.cs del ejemplo 2D.
public class EnemigoSalud : MonoBehaviour
{
    public float salud = 1f;
    public float altura = 1.2f;

    [Header("Manzana")]
    public GameObject manzana;
    public float impulsoManzana = 3f;

    public void getDamage(float dmg)
    {
        // Si ya murio en este cuadro no vuelve a soltar otra manzana.
        if (salud <= 0)
        {
            return;
        }

        salud -= dmg;

        if (salud <= 0)
        {
            SoltarManzana();
            Destroy(gameObject);
        }
    }

    // Las hitboxes de AtaqueGiro y AtaquePunetazo tienen el tag Attack.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            getDamage(1f);
        }
    }

    void SoltarManzana()
    {
        if (manzana != null)
        {
            Vector3 posicion = transform.position + Vector3.up * (altura + 0.3f);
            GameObject nueva = Instantiate(manzana, posicion, Quaternion.identity);

            // Impulso hacia arriba como la variante Impulse de MovoAddforce; luego cae por gravedad.
            nueva.GetComponent<Rigidbody>().AddForce(Vector3.up * impulsoManzana, ForceMode.Impulse);
        }
    }
}
