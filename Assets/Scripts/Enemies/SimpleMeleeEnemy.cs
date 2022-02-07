using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

// For proper setup
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SortingGroup))]

public class SimpleMeleeEnemy : MonoBehaviour, IDamageable
{
    Rigidbody2D _rigidbody;
    Collider2D[] _colliders;
    Animator _animator;
    SpriteRenderer[] _spriteRenderers;
    Transform _target;

    // Health and Damage
    public float _maxHealth = 15;
    float _currentHealth;
    WaitForSeconds _damageIndicatorDelay = new WaitForSeconds(0.15f);

    // Animation hashes
    int _isWalking;
    int _isAttacking;
    int _isDead;

    // Movement
    [Header("Movement Variables")]
    [SerializeField] float _speed = 2.5f;
    [SerializeField] float _stoppingDistance = 1f;
    float _facingDirection = 0;

    // Attacking
    [Header("Attack Variables")]
    [SerializeField] float _attackDamage = 15f;
    [SerializeField] float _attackRadius = 0.5f;
    [SerializeField] Transform _attackPoint;
    [SerializeField] LayerMask _enemyLayer;

    // Death
    [Header("Death and Effects")]
    [SerializeField] GameObject _deathParticle;
    [SerializeField] GameObject _lootDrop;
    [SerializeField, Range(0, 1)] float _lootDropChance;

    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _attackSounds;
    [SerializeField] AudioClip[] _hurtSounds;  // for armored enemies only
    [SerializeField] AudioClip[] _deathSounds;

    bool _canMove = true;
    bool _canAttack = true;
    
    void Awake()
    {
        // Set components and references
        _rigidbody = GetComponent<Rigidbody2D>();
        _colliders = GetComponents<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _target = GameObject.FindGameObjectWithTag("Princess").transform;

        // Set animation hashes
        _isWalking = Animator.StringToHash("isWalking");
        _isAttacking = Animator.StringToHash("isAttacking");
        _isDead = Animator.StringToHash("isDead");

        // Set variables
        _currentHealth = _maxHealth;
    }

    
    void Update()
    {
        if (_canMove) Move();
    }

    /******* Functions via Update *******/
    void Move()
    {
        float distanceFromTarget = Vector3.Distance(_target.position, transform.position);

        if (distanceFromTarget > _stoppingDistance) {
            // Start animation
            if (!_animator.GetBool(_isWalking)) _animator.SetBool(_isWalking, true);
            // Amount to move this frame
            float step = _speed * Time.deltaTime;
            // Position to move to
            Vector3 desiredPosition = Vector3.MoveTowards(transform.position, _target.position, step);
            transform.position = desiredPosition;
        } 
        else if (_animator.GetBool(_isWalking)) {
            _animator.SetBool(_isWalking, false);
        } else {
            Attack();
        }

        Rotate();
    }

    void Rotate() {
        // If target is to the left
        if (_target.position.x < transform.position.x && _facingDirection != -1) {
            transform.rotation = new Quaternion(0,0,0,1);
            _facingDirection = -1;
        }
        // if target is to the right
        else if (_target.position.x> transform.position.x && _facingDirection != 1) {
            transform.rotation = new Quaternion(0,1,0,0);
            _facingDirection = 1;
        }
    }

    void Attack()
    {
        if (_canAttack) {
            _animator.SetTrigger(_isAttacking);
            _canAttack = false;
        }
    }

    /******* Functions called by other scripts *******/
    public void Damage(float damage) {
        _currentHealth -= damage;

        if (_currentHealth <= 0) { Dead(); }
        else { 
            StartCoroutine(SpriteUpdateRoutine()); 
            _audioSource.clip = _hurtSounds[Random.Range(0, _hurtSounds.Length)];
            _audioSource.Play();
        }
    }

    public void PerformAttack()
    {
        // Called by attack animation
        Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _enemyLayer);

        // Check hit objects
        foreach (Collider2D hitbox in hits) {
            var hitReciever = hitbox.gameObject.GetComponent<IDamageable>();
            // Damage any valid damageable objects
            hitReciever?.Damage(_attackDamage);
        }
    }

    public void ResetAttack()
    {
        // Called by attack animation
        _canAttack = true;
    }

    public void EndDeathSequence()
    {
        // Called by death animation
        
        // Play any associated particle effect
        if (_deathParticle != null) {
            Instantiate(_deathParticle, _attackPoint.position, Quaternion.identity);
        }
        // Chance to drop loot (lower being less likely)
        if (_lootDrop != null && Random.Range(0f, 1f) < _lootDropChance) {
            Instantiate(_lootDrop, _attackPoint.position, Quaternion.identity);
        }

        // Add score
        GameManager.instance.AddScore();

        Invoke(nameof(DestroyThisObject), 0.05f);
    }

    /******* Functions called by other functions *******/
    IEnumerator SpriteUpdateRoutine() {
        ChangeToColor(Color.red);

        yield return _damageIndicatorDelay;

        // Return to normal
        ChangeToColor(Color.white);
    }

    void Dead() {
        _canMove = false;
        foreach (Collider2D collider in _colliders) {
            collider.enabled = false;
        }
        // Visual Effect
        _animator.SetTrigger(_isDead);
        ChangeToColor(Color.grey);
        // Audio Effect
        _audioSource.clip = _deathSounds[Random.Range(0, _deathSounds.Length)];
        _audioSource.Play();
    }

    void ChangeToColor(Color newColor) {
        foreach (SpriteRenderer bodyPart in _spriteRenderers) {
            bodyPart.color = newColor;
        }
    }

    void DestroyThisObject()
    {
        Destroy(gameObject);
    }

    void PlayAttackSound()
    {
        // Reset Volume (better this way than via update)
        _audioSource.volume = GameManager.instance.CurrentMasterVolume();

        // Play Sound
        _audioSource.clip = _attackSounds[Random.Range(0, _attackSounds.Length)];
        _audioSource.Play();
    }
}
