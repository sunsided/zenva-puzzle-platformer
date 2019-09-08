using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private int lives = 3;

    [Header("HUD")]
    [SerializeField]
    private TextMeshProUGUI[] keyCountTexts;

    [SerializeField]
    private TextMeshProUGUI livesText;

    private readonly Collection<string> _collectedKeys = new Collection<string>();

    private void Awake()
    {
        Load();
    }

    private void OnDestroy()
    {
        Save();
    }

    [MenuItem("Dev Tools/Delete Saves")]
    public static void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    public void RemoveLife()
    {
        --lives;
        if (lives <= 0)
        {
            Debug.Log("Game over");
        }

        UpdateUI();
    }

    public void AddKey([NotNull] string key)
    {
        _collectedKeys.Add(key);
        UpdateUI();
    }

    public void RemoveKey([NotNull] string key)
    {
        _collectedKeys.Remove(key);
        UpdateUI();
    }

    public bool HasKey([NotNull] string key) => _collectedKeys.Contains(key);

    private void Save()
    {
        // Save the player stats.
        PlayerPrefs.SetInt("Lives", lives);

        // Save the keys.
        var keyIndex = 0;
        PlayerPrefs.SetInt("KeyCount", _collectedKeys.Count);
        foreach (var key in _collectedKeys)
        {
            PlayerPrefs.SetString($"key{keyIndex++}", key);
        }
    }

    private void Load()
    {
        // Load the player stats.
        lives = PlayerPrefs.GetInt("Lives", 3);

        // Load the keys.
        var keyCount = PlayerPrefs.GetInt("KeyCount", 0);
        for (var i = 0; i < keyCount; ++i)
        {
            var key = PlayerPrefs.GetString($"key{i}", null);
            if (key == null) continue;
            _collectedKeys.Add(key);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        keyCountTexts[0].text = _collectedKeys.Count(x => x == "blue").ToString();
        keyCountTexts[1].text = _collectedKeys.Count(x => x == "red").ToString();
        keyCountTexts[2].text = _collectedKeys.Count(x => x == "green").ToString();
        livesText.text = lives.ToString();
    }
}
