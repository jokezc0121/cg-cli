using System.Collections;
using UnityEngine;

// Basado en PlayerAttack.cs: en vez de OverlapCircleAll activa una hitbox con tag Attack.
public class AtaquePunetazo : MonoBehaviour
{
    public GameObject hitboxPunetazo;
    public float duracion = 0.2f;
    public float tiempoEspera = 0.4f;

    private bool atacando;

    void Update()
    {
        // Tecla O
        if (Input.GetKeyDown(KeyCode.O) && !atacando)
        {
            StartCoroutine(Golpear());
        }
    }

    IEnumerator Golpear()
    {
        atacando = true;
        hitboxPunetazo.SetActive(true);
        Debug.Log("Ataque puñetazo realizado");

        yield return new WaitForSeconds(duracion);
        hitboxPunetazo.SetActive(false);

        yield return new WaitForSeconds(tiempoEspera);
        atacando = false;
    }
}