using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private int portalId;

    [SerializeField]
    private int scene;

    [SerializeField]
    private Vector3 spawnPosition;

    public int Id => portalId;

    public int Scene => scene;

    public Vector3 SpawnPosition => spawnPosition;

    private void Awake()
    {
        // We expect the first (and only) child of a portal to be the spawn position.
        Debug.Assert(transform.childCount == 1, "transform.childCount == 1");
        spawnPosition = transform.GetChild(0).position;
    }
}
