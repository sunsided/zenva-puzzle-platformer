using System.Collections;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask climbableLayer;

    [SerializeField]
    private float climbSpeed = 4f;

    private GameManager _game;
    private Rigidbody2D _body;
    private int _hazardLayer;
    private bool _tryingToClimb;
    private bool _isClimbing;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _hazardLayer = LayerMask.NameToLayer("Hazard");

        // TODO: Rather directly accessing the game manager here, expose behavior through events.
        _game = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        _tryingToClimb = Input.GetButton("ClimbUp") || Input.GetButton("ClimbDown");
    }

    private void FixedUpdate()
    {
        // A raycast hit can be cast to bool: true for hit, false for no hit.
        var hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, climbableLayer);
        if (_tryingToClimb) _isClimbing = hit;

        if (_isClimbing)
        {
            _body.gravityScale = 0;
            _body.velocity = new Vector2(_body.velocity.x, Input.GetAxis("Vertical") * climbSpeed);
        }

        if (!hit)
        {
            _body.gravityScale = 3f; // TODO: Course states that the player should fall quicker here. However, the player will always have high gravity here.
            _isClimbing = false;
        }
    }

    private void OnCollisionStay2D([NotNull] Collision2D other)
    {
        var effector = other.gameObject.GetComponent<PlatformEffector2D>();
        if (effector && Input.GetButtonDown("ClimbDown"))
        {
            StartCoroutine(FallThroughPlatform(other.collider));
        }
    }

    private void OnCollisionEnter2D([NotNull] Collision2D other)
    {
        if (other.gameObject.layer == _hazardLayer)
        {
            Die();
        }
    }

    [SuppressMessage("ReSharper", "Unity.InefficientPropertyAccess", Justification = "Collider needs to be toggled.")]
    private IEnumerator FallThroughPlatform([NotNull] Collider2D other)
    {
        other.enabled = false;
        yield return new WaitForSeconds(.5f);
        other.enabled = true;
    }

    private void Die()
    {
        Debug.Log("Died!");
        _game.RemoveLife();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
