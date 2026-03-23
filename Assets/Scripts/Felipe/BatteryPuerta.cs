using UnityEngine;

public class PuertaBateria : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Marca esto en la puerta que debe abrirse con batería ALTA (50% o más). Deja desmarcado para la puerta de batería BAJA.")]
    public bool abreConBateriaAlta = true;

    [SerializeField] private puerta101 puerta;

    private bool yaActivado = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (yaActivado) return;
        if (!other.CompareTag("Player")) return;

        yaActivado = true;

        float bateria = SystemInfo.batteryLevel; // 0.0 a 1.0, -1 si no se puede leer
        bool bateriaAlta = bateria >= 0.5f;

        // Si no se puede leer la batería (editor o dispositivo sin batería), asumir alta
        if (bateria < 0f) bateriaAlta = true;

        bool debeAbrir = abreConBateriaAlta ? bateriaAlta : !bateriaAlta;

        if (debeAbrir)
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