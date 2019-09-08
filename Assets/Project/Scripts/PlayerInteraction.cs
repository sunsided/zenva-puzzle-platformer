using JetBrains.Annotations;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameManager _game;

    private void Awake()
    {
        // TODO: Rather directly accessing the game manager here, expose behavior through events.
        // TODO: This code is repeated in Player.
        _game = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D([NotNull] Collider2D other)
    {
        var key = other.gameObject.GetComponent<Key>();
        if (!key) return;
        _game.AddKey(key.Color);
        key.Take();
    }

    private void OnCollisionEnter2D([NotNull] Collision2D other)
    {
        var door = other.gameObject.GetComponent<Door>();
        if (!door || !_game.HasKey(door.Color)) return;
        _game.RemoveKey(door.Color);
        door.Unlock();
    }
}
