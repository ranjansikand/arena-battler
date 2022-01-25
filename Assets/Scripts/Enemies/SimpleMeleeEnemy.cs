using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMeleeEnemy : MonoBehaviour, IDamageable
{
    Rigidbody2D _rigidbody;
    BoxCollider2D _collider;
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
    int _isHurt;
    int _isDead;

    // Movement
    public float _speed = 2.5f;
    float _facingDirection = 0;

    // Attacking
    [Header("Attack Variables")]
    [SerializeField] float _attackDamage = 15;
    [SerializeField] float _attackRadius;
    [SerializeField] Transform _attackPoint;
    [SerializeField] LayerMask _enemyLayer;

    bool _canMove = true;
    bool _canAttack = true;
    
    void Awake()
    {
        // Set components and references
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _target = GameObject.FindGameObjectWithTag("Princess").transform;

        // Set animation hashes
        _isWalking = Animator.StringToHash("isWalking");
        _isAttacking = Animator.StringToHash("isAttacking");
        _isHurt = Animator.StringToHash("isHurt");
        _isDead = Animator.StringToHash("isDead");

        // Set variables
        _currentHealth = _maxHealth;
    }

    
    void Update()
    {
        if (_canMove) Move();
    }

    void Move()
    {
        float distanceFromTarget = Vector3.Distance(_target.position, transform.position);

        if (distanceFromTarget > 1.25f) {
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

    // Functions called by other scripts
    public void Damage(float damage) {
        _currentHealth -= damage;

        if (_currentHealth <= 0) { Dead(); }
        else { StartCoroutine(SpriteUpdateRoutine()); }
    }

    public void PerformAttack()
    {
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
        _canAttack = true;
    }

    // Functions called by other functions
    IEnumerator SpriteUpdateRoutine() {
        ChangeToColor(Color.red);
        _animator.SetTrigger(_isHurt);

        yield return _damageIndicatorDelay;

        // Return to normal
        ChangeToColor(Color.white);
    }

    void Dead() {
        _canMove = false;
        _collider.enabled = false;
        _animator.SetTrigger(_isDead);
        ChangeToColor(Color.grey);
    }

    void ChangeToColor(Color newColor) {
        foreach (SpriteRenderer bodyPart in _spriteRenderers) {
            bodyPart.color = newColor;
        }
    }
}