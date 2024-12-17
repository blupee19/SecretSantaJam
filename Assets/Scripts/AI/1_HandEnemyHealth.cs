//using UnityEngine;
//using UnityEngine.Events;

//public class HandEnemyHealth : MonoBehaviour
//{
//    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
//    public GameObject HandEnemy;  // Reference to the hand enemy
//    public Collider2D handCollider;  // Collider of the hand enemy

//    [SerializeField] private int currentHealth = 20, maxHealth = 20;
//    [SerializeField] public bool isDead = false;
//    [SerializeField] private float destroyDelay = 2f;
//    private bool isHit = false;// The recoil force when hit
//    private Rigidbody2D rb;  // Rigidbody for applying forces    
//    private HandEnemyAI handEnemyAI;
//    private Animator animator;// Reference to the HandEnemyAI script

//    void Start()
//    {
//        rb = HandEnemy.GetComponent<Rigidbody2D>();
//        handEnemyAI = GetComponent<HandEnemyAI>();
//        animator = GetComponent<Animator>();
//    }

//    public void InitializeHealth(int healthValue)
//    {
//        currentHealth = healthValue;
//        maxHealth = healthValue;
//        isDead = false;
//    }

//    public void GetHit(int amount, GameObject sender)
//    {
//        if (isDead)
//        {
//            return;
//        }

//        // Only allow damage if the enemy is not underground (use emerging state check or position check)
//        if (handEnemyAI != null && !handEnemyAI.isEmerging) // Check if the enemy is emerging
//        {
//            return;
//        }

//        if (sender.layer == gameObject.layer)
//        {
//            return;
//        }

//        if (handEnemyAI.isEmerging)
//        {
//            currentHealth -= amount;
//            isHit = true;

//            if (currentHealth > 0)
//            {
//                OnHitWithReference?.Invoke(sender);
//            }
//            else
//            {
//                OnDeathWithReference?.Invoke(sender);
//                isDead = true;
//                handEnemyAI.enabled = false;
//                handEnemyAI.AnimationCalls();


//                handCollider.isTrigger = true;
//                Invoke(nameof(DestroyHand), destroyDelay);


//            }
//        }
//    }


//    private void DestroyHand()
//    {
//        Destroy(HandEnemy);
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Projectile"))
//        {
//            currentHealth -= 5;

//            if (currentHealth <= 0)
//            {
//                isDead = true;

//                //BloodSplash(); // Trigger blood splash on death
//                Physics2D.IgnoreLayerCollision(10, 11, true);
//            }
//        }

//    }
//}