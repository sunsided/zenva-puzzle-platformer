using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotateSpeed = 200;

    private int _targetIndex;

    private void Update()
    {
        var tf = transform;
        tf.Rotate(0, 0, -rotateSpeed * Time.deltaTime);

        if (waypoints == null || waypoints.Length == 0) return;

        var pos = tf.position;
        var targetPos = waypoints[_targetIndex].position;

        pos = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
        tf.position = pos;
        if (Vector2.Distance(pos, targetPos) <= 0.1f)
        {
            pos = targetPos;
            _targetIndex = _targetIndex < waypoints.Length - 1 ? _targetIndex + 1 : 0;
        }

        tf.position = pos;
    }
}
