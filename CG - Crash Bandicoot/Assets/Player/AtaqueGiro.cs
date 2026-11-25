using System.Collections;
using UnityEngine;

// Basado en PlayerAttack.cs: en vez de OverlapCircleAll activa una hitbox con tag Attack.
public class AtaqueGiro : MonoBehaviour
{
    public GameObject hitboxGiro;
    public float duracion = 0.4f;
    public float tiempoEspera = 0.5f;

    private bool atacando;

    void Update()
    {
        // Tecla P
        if (Input.GetKeyDown(KeyCode.P) && !atacando)
        {
            StartCoroutine(Girar());
        }
    }

    IEnumerator Girar()
    {
        atacando = true;
        hitboxGiro.SetActive(true);
        Debug.Log("Ataque giro realizado");

        yield return new WaitForSeconds(duracion);
        hitboxGiro.SetActive(false);

        yield return new WaitForSeconds(tiempoEspera);
        atacando = false;
    }
}