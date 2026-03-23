using UnityEngine;

public class BotonNivel : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Si está marcado, este botón abre la puerta. Si no, mata al jugador.")]
    public bool esCorecto = false;

    [Tooltip("La puerta que se abre al tocar el botón correcto.")]
    [SerializeField] private puerta101 puerta;

    private bool yaActivado = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (yaActivado) return;
        if (!other.CompareTag("Player")) return;

        yaActivado = true;

        if (esCorecto)
        {
            puerta.AbrirPuerta();
        }
        else
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
                player.killPlayer();
        }
    }
}