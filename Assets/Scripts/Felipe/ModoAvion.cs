using UnityEngine;

public class PuertaModoAvion : MonoBehaviour
{
    [SerializeField] private puerta101 puerta;

    private bool yaActivado = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (yaActivado) return;
        if (!other.CompareTag("Player")) return;

        yaActivado = true;

        bool modoAvion = Application.internetReachability == NetworkReachability.NotReachable;

        if (modoAvion)
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