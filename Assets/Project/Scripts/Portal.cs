using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private int portalId;

    [SerializeField]
    private int scene;

    private Vector3 _spawnPosition;

    public int Id => portalId;

    public int Scene => scene;

    public Vector3 SpawnPosition => _spawnPosition;

    private void Awake()
    {
        // We expect the first (and only) child of a portal to be the spawn position.
        Debug.Assert(transform.childCount == 1, "transform.childCount == 1");
        _spawnPosition = transform.GetChild(0).position;
    }
}
