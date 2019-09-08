using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string keyColor;

    public string Color => keyColor;

    [NotNull]
    private string PrefName => $"key:{name}-{SceneManager.GetActiveScene().name}";

    private void Awake()
    {
        var collected = PlayerPrefs.GetInt(PrefName, 0) > 0;
        if (collected) Destroy(gameObject);
    }

    public void Take()
    {
        PlayerPrefs.SetInt(PrefName, 1);
        Destroy(gameObject);
    }
}
