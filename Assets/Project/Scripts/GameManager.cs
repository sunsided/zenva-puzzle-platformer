using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    private readonly HashSet<string> _collectedKeys = new HashSet<string>();

    public void RemoveLife()
    {
        --lives;
        if (lives <= 0)
        {
            Debug.Log("Game over");
        }
    }

    public void AddKey([NotNull] string key) => _collectedKeys.Add(key);

    public void RemoveKey([NotNull] string key) => _collectedKeys.Remove(key);

    public bool HasKey([NotNull] string key) => _collectedKeys.Contains(key);
}
