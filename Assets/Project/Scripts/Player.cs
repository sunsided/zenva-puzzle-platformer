using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager _game;
    private int _hazardLayer;

    private void Awake()
    {
        _hazardLayer = LayerMask.NameToLayer("Hazard");

        // TODO: Rather directly accessing the game manager here, expose behavior through events.
        _game = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D([NotNull] Collision2D other)
    {
        if (other.gameObject.layer == _hazardLayer)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Died!");
        _game.RemoveLife();
        // SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
