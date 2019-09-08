using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string doorColor;

    public string Color => doorColor;

    public void Unlock()
    {
        Destroy(gameObject);
    }
}
