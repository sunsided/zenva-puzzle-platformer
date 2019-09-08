using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private int _hazardLayer;

    private void Awake()
    {
        _hazardLayer = LayerMask.NameToLayer("Hazard");
    }

    private void OnCollisionEnter2D([NotNull] Collision2D other)
    {
        if (other.gameObject.layer == _hazardLayer)
        {
            Die();
        }
    }

    private static void Die()
    {
        Debug.Log("Died!");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
