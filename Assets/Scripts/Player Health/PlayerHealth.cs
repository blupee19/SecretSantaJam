using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private GameObject player;
    [SerializeField] private float startingHealth;
    [SerializeField] private Camera mainCamera;
    private Animator surgeonAnim;
    public bool dead;
    public float currentHealth { get; private set; }

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer surgeon;

    private Respawn respawn;

    private void Awake()
    {
        currentHealth = startingHealth;
        surgeonAnim = GetComponent<Animator>();
        surgeon = GetComponent<SpriteRenderer>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();
    }

    public void Update()
    {
        //...
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(Invulnerability());
            StartCoroutine(Shake(0.2f, 0.2f));

        }
        else
        {
            if (!dead)
            { 
                
                dead = true;
                RespawnPlayer();
                currentHealth = startingHealth;
                
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        TakeDamage(1f);
    //        StartCoroutine(Shake(0.2f,0.2f));
    //        StartCoroutine(Invulnerability());

    //        Debug.Log("Hit!!!");
    //    }
    //}


    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = mainCamera.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalPosition;
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10,11, true);
        for (int i = 0; i < numOfFlashes; i++) 
        {
            surgeon.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
            surgeon.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10,11, false);


    }

    public void RespawnPlayer()
    {
        dead = false;

        if (respawn.respawnPoint != null)
        {
            Debug.Log($"Respawning player at checkpoint: {respawn.respawnPoint.name}");
            player.transform.position = respawn.respawnPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("No respawn point set! Ensure the player has triggered a checkpoint.");
        }
    }
}
