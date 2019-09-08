using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    private readonly HashSet<string> _collectedKeys = new HashSet<string>();

    private void Awake()
    {
        Load();
    }

    private void OnDestroy()
    {
        Save();
    }

    public void RemoveLife()
    {
        --lives;
        if (lives <= 0)
        {
            Debug.Log("Game over");
        }
    }

    [MenuItem("Dev Tools/Delete Saves")]
    public static void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }

    public void AddKey([NotNull] string key) => _collectedKeys.Add(key);

    public void RemoveKey([NotNull] string key) => _collectedKeys.Remove(key);

    public bool HasKey([NotNull] string key) => _collectedKeys.Contains(key);

    private void Save()
    {
        var keyIndex = 0;
        PlayerPrefs.SetInt("KeyCount", _collectedKeys.Count);
        foreach (var key in _collectedKeys)
        {
            PlayerPrefs.SetString($"key{keyIndex++}", key);
        }
    }

    private void Load()
    {
        var keyCount = PlayerPrefs.GetInt("KeyCount", 0);
        for (var i = 0; i < keyCount; ++i)
        {
            var key = PlayerPrefs.GetString($"key{i}", null);
            if (key == null) continue;
            _collectedKeys.Add(key);
        }
    }
}
