using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    private Portal[] _scenePortals;

    private int _portalToSpawnAt;

    private void Awake()
    {
        _scenePortals = FindObjectsOfType<Portal>();
    }

    public void SetSpawnPortal([NotNull] Portal portal)
    {
        _portalToSpawnAt = portal.Id;
        PlayerPrefs.SetInt("PortalToSpawnAt", _portalToSpawnAt);
        SceneManager.LoadScene(portal.Scene, LoadSceneMode.Single);
    }

    public Vector3 GetSpawnPosition()
    {
        _portalToSpawnAt = PlayerPrefs.GetInt("PortalToSpawnAt", -1);

        var spawnPos = Vector3.zero;
        foreach (var portal in _scenePortals)
        {
            if (portal.Id == _portalToSpawnAt)
            {
                spawnPos = portal.SpawnPosition;
                return spawnPos;
            }
        }

        throw new InvalidOperationException($"Missing portal reference for portal {_portalToSpawnAt}");
    }
}
