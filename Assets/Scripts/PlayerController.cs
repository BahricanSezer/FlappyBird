using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip scoreSound;

    public ParticleSystem deathParticles;
    public float deathDelay = 1.5f;
    public float shakeDuration = 0.4f;
    public float shakeMagnitude = 0.2f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    private float currentJumpForce = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentDifficulty != null)
        {
            rb.gravityScale = GameManager.Instance.currentDifficulty.gravityMultiplier;
            currentJumpForce = GameManager.Instance.currentDifficulty.jumpForce;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isDead)
            {
                rb.linearVelocity = Vector2.up * currentJumpForce;
                PlaySound(jumpSound);
            }
            else if (GameManager.Instance != null && GameManager.Instance.gameOverPanel != null && GameManager.Instance.gameOverPanel.activeInHierarchy)
            {
                // Karakter �l�yse ve Game Over paneli ekrandaysa Space tu�u oyunu yeniden ba�lat�r
                GameManager.Instance.RestartGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreZone") && !isDead)
        {
            GameManager.Instance.AddScore(1);
            PlaySound(scoreSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground")) && !isDead)
        {
            StartCoroutine(DieRoutine());
        }
    }

    private IEnumerator DieRoutine()
    {
        isDead = true;
        PlaySound(deathSound);

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        if (spriteRenderer != null) spriteRenderer.enabled = false;

        if (deathParticles != null)
        {
            deathParticles.transform.position = transform.position;
            deathParticles.Play();
        }

        Vector3 originalCamPos = Camera.main.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, originalCamPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = originalCamPos;

        yield return new WaitForSeconds(deathDelay - shakeDuration);

        GameManager.Instance.GameOver();
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}