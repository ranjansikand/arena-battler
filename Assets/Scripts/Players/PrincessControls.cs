using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrincessControls : MonoBehaviour, IDamageable
{
    PlayerControls _playerInput;
    Rigidbody2D _rigidbody;
    BoxCollider2D _collider;
    Animator _animator;
    SpriteRenderer[] _spriteRenderers;
    StatusBar _healthbar;
    
    // Static variables
    static float _speed = 4.0f;
    static float _maxHealth = 100;

    // Health
    float _currentHealth;
    WaitForSeconds _damageIndicatorDelay = new WaitForSeconds(0.5f);

    // Status
    bool _canMove = true;
    bool _canTakeDamage = true;
    int _facingDirection = 0;

    // Animation hashes
    int _isWalking;
    int _isHurt;
    int _isDead;

    Vector2 _currentMovementInput;
    
    void Awake()
    {
        // Components and Game Objects
        _playerInput = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _healthbar = GameObject.Find("PrincessBar").GetComponent<StatusBar>();

        // Animation hashes
        _isWalking = Animator.StringToHash("isWalking");
        _isHurt = Animator.StringToHash("isHurt");
        _isDead = Animator.StringToHash("isDead");

        // Input Actions
        _playerInput.Princess.Movement.performed += OnMovementInput;
        _playerInput.Princess.Movement.canceled += OnMovementInput;

        // Other initialization
        _currentHealth = _maxHealth;
        _healthbar.InitializeStatusBar(_currentHealth, _maxHealth);
    }

    private void OnEnable() {
        _playerInput.Enable();
    }

    private void OnDisable() {
        _playerInput.Disable();
    }

    void OnMovementInput(InputAction.CallbackContext context) {
        if (_canMove) _currentMovementInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Damage(float damage) {
        if (_canTakeDamage) {
            _currentHealth -= damage;
            _healthbar.Decrease(damage);

            if (_currentHealth <= 0) { Dead(); }
            else { StartCoroutine(SpriteUpdateRoutine()); }
        }
    }

    /***** Functions called by other functions *****/
    void Move() 
    {
        if (_currentMovementInput != Vector2.zero) {
            Vector3 displacement = _currentMovementInput * _speed * Time.deltaTime;
            transform.position += displacement;
            _animator.SetBool(_isWalking, true);

            Flip();
        }
        else {
            if (_animator.GetBool(_isWalking)) { _animator.SetBool(_isWalking, false); }
        }
    }

    void Flip() {
        // Face left if moving left
        if (_currentMovementInput.x < 0 && _facingDirection != -1) {
            transform.rotation = new Quaternion(0,0,0,1);
            _facingDirection = -1;
        }
        // Face right if moving right
        else if (_currentMovementInput.x > 0 && _facingDirection != 1) {
            transform.rotation = new Quaternion(0,1,0,0);
            _facingDirection = 1;
        }
    }

    IEnumerator SpriteUpdateRoutine() {
        // Invulnerable "damage taken" state
        _canTakeDamage = false;
        ChangeToColor(Color.red);
        _animator.SetTrigger(_isHurt);

        yield return _damageIndicatorDelay;

        // Return to normal
        ChangeToColor(Color.white);
        _canTakeDamage = true;
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
