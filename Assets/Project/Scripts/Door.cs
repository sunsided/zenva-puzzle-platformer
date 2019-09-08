using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string doorColor;

    public string Color => doorColor;

    [NotNull]
    private string PrefName => $"door:{name}-{SceneManager.GetActiveScene().name}";

    private void Awake()
    {
        var collected = PlayerPrefs.GetInt(PrefName, 0) > 0;
        if (collected) Destroy(gameObject);
    }

    public void Unlock()
    {
        PlayerPrefs.SetInt(PrefName, 1);
        Destroy(gameObject);
    }
}
