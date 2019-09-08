using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    public void RemoveLife()
    {
        --lives;
        if (lives <= 0)
        {
            Debug.Log("Game over");
        }
    }
}
