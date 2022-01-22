using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnightControls : MonoBehaviour
{
    PlayerControls _playerInput;
    Rigidbody2D _rigidbody;
    Camera _camera;
    StatusBar _staminaBar;

    // Attacking
    [Header("Attack Variables")]
    [SerializeField] Transform _attackPoint;
    [SerializeField] float _attackRadius = 0.5f;
    [SerializeField] LayerMask _enemyLayer;
    
    
    // Reference variables
    static float walk_speed = 2.5f;
    static float sprint_speed = 5.5f;
    static float max_stamina = 100;
    static float attack_damage = 15;

    // Stamina costs
    const float SPRINT_STAMINA_COST = 30.0f;
    const float ATTACK_STAMINA_COST = 25.0f;
    const float STAMINA_REGEN_PER_SEC = 40f;

    // Variables
    Vector2 _mousePos;
    float _currentStamina;
    float _timeOfLastStaminaDrain;
    bool _sprinting = false;
    int _facingDirection = 0;
    
    void Awake()
    {
        _playerInput = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _staminaBar = GameObject.Find("KnightBar").GetComponent<StatusBar>();

        _playerInput.Knight.Movement.performed += OnMovementInput;
        _playerInput.Knight.Movement.canceled += OnMovementInput;
        _playerInput.Knight.Attack.performed += OnAttack;
        _playerInput.Knight.Attack.canceled -= OnAttack;
        _playerInput.Knight.Sprint.performed += OnSprint;
        _playerInput.Knight.Sprint.canceled += OnSprint;

        _currentStamina = max_stamina;
        _staminaBar.InitializeStatusBar(_currentStamina, max_stamina);
    }

    private void OnEnable() {
        _playerInput.Enable();
    }

    private void OnDisable() {
        _playerInput.Disable();
    }

    void OnMovementInput(InputAction.CallbackContext context) {
        // Convert input mouse position to world position
        _mousePos = _camera.ScreenToWorldPoint(context.action.ReadValue<Vector2>());
    }

    void OnAttack(InputAction.CallbackContext context) {
        // Trigger any animation
        HandleAttack();  // Can call this in an animation event
    }

    void OnSprint(InputAction.CallbackContext context) {
        _sprinting = context.ReadValueAsButton();
    }

    void FixedUpdate()
    {
        HandleMotion();
        HandleStaminaRegen();
    }

    /******* Functions executed each frame *******/
    private void HandleMotion() {
        // Distance between mouse and character to implement a deadzone
        float distanceFromMouse = Vector3.Distance(_mousePos, transform.position);
        if (_sprinting) _sprinting = CanPerformAction(SPRINT_STAMINA_COST * Time.deltaTime);

        // Movement
        if (distanceFromMouse > 0.5f) {
            // Check which speed to apply
            float speed = _sprinting ? sprint_speed : walk_speed;
            // Amount to move this frame
            float step = speed * Time.deltaTime;
            // Position to move to
            Vector3 desiredPosition = Vector3.MoveTowards(transform.position, _mousePos, step);
            transform.position = desiredPosition;
            if (_sprinting) { UpdateStamina(SPRINT_STAMINA_COST * Time.deltaTime); }
        }
        // Rotation
        if (distanceFromMouse > 0.1f) { HandleRotation(); }
    }

    void HandleStaminaRegen() {
        // _currentStamina = Mathf.Clamp(_currentStamina, 0, max_stamina);
        
        if (_currentStamina < max_stamina) {
            if (_timeOfLastStaminaDrain + 2.5f < Time.time) {
                float increaseThisFrame = STAMINA_REGEN_PER_SEC * Time.deltaTime;
                _currentStamina += increaseThisFrame;
                _staminaBar.Increase(increaseThisFrame);
            }
        }
    }

    void HandleRotation() {
        // If mouse to the left
        if (_mousePos.x < transform.position.x && _facingDirection != -1) {
            transform.rotation = new Quaternion(0,0,0,1);
            _facingDirection = -1;
        }
        // if mouse to the right
        else if (_mousePos.x > transform.position.x && _facingDirection != 1) {
            transform.rotation = new Quaternion(0,1,0,0);
            _facingDirection = 1;
        }
    }

    /******* Functions called by Input and Events *******/
    void HandleAttack() {
        // If stamina remaining is insufficient
        if (!CanPerformAction(ATTACK_STAMINA_COST)) return;

        // Generate hit field
        Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _enemyLayer);

        // Check hit objects
        foreach (Collider2D hitbox in hits) {
            var hitReciever = hitbox.gameObject.GetComponent<IDamageable>();
            // Damage any valid damageable objects
            hitReciever?.Damage(attack_damage);
        }

        // Apply stamina cost
        UpdateStamina(ATTACK_STAMINA_COST);
    }

    /******* Functions called by other functions *******/
    private bool CanPerformAction(float actionCost) {
        if (_currentStamina - actionCost >= 0) { return true; }
        return false;
    }

    private void UpdateStamina(float decrease) {
        _currentStamina = Mathf.Clamp(_currentStamina - decrease, 0, max_stamina);
        _staminaBar.Decrease(decrease);
        _timeOfLastStaminaDrain = Time.time;
    }
}
