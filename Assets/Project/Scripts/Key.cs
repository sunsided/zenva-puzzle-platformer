using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string keyColor;

    public string Color => keyColor;

    public void Take()
    {
        Destroy(gameObject);
    }
}
