using JetBrains.Annotations;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameManager _game;
    private PortalController _portalController;

    private void Awake()
    {
        // TODO: Rather directly accessing the game manager here, expose behavior through events.
        // TODO: This code is repeated in Player.
        _game = FindObjectOfType<GameManager>();
        _portalController = FindObjectOfType<PortalController>();
    }

    private void OnTriggerEnter2D([NotNull] Collider2D other)
    {
        var key = other.gameObject.GetComponent<Key>();
        if (key)
        {
            _game.AddKey(key.Color);
            key.Take();
        }

        var portal = other.gameObject.GetComponent<Portal>();
        if (portal)
        {
            _portalController.TeleportTo(portal);
        }
    }

    private void OnCollisionEnter2D([NotNull] Collision2D other)
    {
        var door = other.gameObject.GetComponent<Door>();
        if (!door || !_game.HasKey(door.Color)) return;
        _game.RemoveKey(door.Color);
        door.Unlock();
    }
}
