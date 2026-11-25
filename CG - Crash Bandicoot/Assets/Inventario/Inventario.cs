using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Singleton como el GameManager de clase, pero se conserva al cambiar de escena.
public class Inventario : MonoBehaviour
{
    public static Inventario Instance;

    [Header("Objetos")]
    public int manzanas;
    public int bananas;

    [Header("Vidas")]
    public int vidasIniciales = 3;
    public int vidas;
    public string escenaAnterior;

    [Header("Textos del Canvas (TextMeshPro)")]
    public TMP_Text txtManzanas;
    public TMP_Text txtBananas;
    public TMP_Text txtVidas;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        vidas = vidasIniciales;
    }

    void Start()
    {
        ActualizarTextos();
    }

    public void SumarManzana()
    {
        manzanas++;
        ActualizarTextos();
    }

    public void SumarBanana()
    {
        bananas++;
        ActualizarTextos();
    }

    public void PerderVida()
    {
        vidas--;

        if (vidas <= 0)
        {
            vidas = vidasIniciales;

            // Sin nombre asignado se recarga la escena actual.
            if (escenaAnterior == "")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                SceneManager.LoadScene(escenaAnterior);
            }
        }

        ActualizarTextos();
    }

    void ActualizarTextos()
    {
        if (txtManzanas != null)
        {
            txtManzanas.text = "Manzanas: " + manzanas;
        }

        if (txtBananas != null)
        {
            txtBananas.text = "Bananas: " + bananas;
        }

        if (txtVidas != null)
        {
            txtVidas.text = "Vidas: " + vidas;
        }
    }
}