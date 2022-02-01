// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Princess"",
            ""id"": ""ab16d326-e609-49e4-ba1e-e135820fe5b0"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""47f459e0-8265-4f14-bd02-4ff036359b76"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""10cf2b04-66b1-4379-89db-0c313da2d14f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4fdcf24f-5674-4d9e-8206-4ae3edbb685c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2c45299a-e8cc-46c8-9f80-b5e711c6091d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3d300550-0c92-4e23-97ff-b926d848f500"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ac92a8cc-270f-4e65-8a44-ce8ef6dfd6e7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Knight"",
            ""id"": ""b7c34eee-a78d-450c-96bf-3f1462f151e6"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""6588297f-335e-4977-819e-d67907d60141"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""2983c02b-6879-4bcb-9b4b-e4ab9b005cf6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""432632a4-e95c-405c-a03a-94e823c288b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""58d12c51-1635-432a-9ba4-32b3da60cb08"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c5130be-db5b-4808-8f05-15fd11621170"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5890fe35-5642-479b-8f0f-87f556eafebe"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""General"",
            ""id"": ""9239abf9-11a4-4855-aaee-48f937e3393a"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""2860f913-312e-4dda-a1e2-a60180f6ffba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""591e910d-722c-4d26-9603-1faba6bfcfbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c0fb6e74-f537-4841-8474-17c9f4bed2ac"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""120ab4ed-1218-409d-b78b-afdcc8eee425"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Princess
        m_Princess = asset.FindActionMap("Princess", throwIfNotFound: true);
        m_Princess_Movement = m_Princess.FindAction("Movement", throwIfNotFound: true);
        // Knight
        m_Knight = asset.FindActionMap("Knight", throwIfNotFound: true);
        m_Knight_Movement = m_Knight.FindAction("Movement", throwIfNotFound: true);
        m_Knight_Attack = m_Knight.FindAction("Attack", throwIfNotFound: true);
        m_Knight_Sprint = m_Knight.FindAction("Sprint", throwIfNotFound: true);
        // General
        m_General = asset.FindActionMap("General", throwIfNotFound: true);
        m_General_Pause = m_General.FindAction("Pause", throwIfNotFound: true);
        m_General_Quit = m_General.FindAction("Quit", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Princess
    private readonly InputActionMap m_Princess;
    private IPrincessActions m_PrincessActionsCallbackInterface;
    private readonly InputAction m_Princess_Movement;
    public struct PrincessActions
    {
        private @PlayerControls m_Wrapper;
        public PrincessActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Princess_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Princess; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PrincessActions set) { return set.Get(); }
        public void SetCallbacks(IPrincessActions instance)
        {
            if (m_Wrapper.m_PrincessActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PrincessActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PrincessActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PrincessActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_PrincessActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public PrincessActions @Princess => new PrincessActions(this);

    // Knight
    private readonly InputActionMap m_Knight;
    private IKnightActions m_KnightActionsCallbackInterface;
    private readonly InputAction m_Knight_Movement;
    private readonly InputAction m_Knight_Attack;
    private readonly InputAction m_Knight_Sprint;
    public struct KnightActions
    {
        private @PlayerControls m_Wrapper;
        public KnightActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Knight_Movement;
        public InputAction @Attack => m_Wrapper.m_Knight_Attack;
        public InputAction @Sprint => m_Wrapper.m_Knight_Sprint;
        public InputActionMap Get() { return m_Wrapper.m_Knight; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KnightActions set) { return set.Get(); }
        public void SetCallbacks(IKnightActions instance)
        {
            if (m_Wrapper.m_KnightActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnMovement;
                @Attack.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnAttack;
                @Sprint.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnSprint;
            }
            m_Wrapper.m_KnightActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
            }
        }
    }
    public KnightActions @Knight => new KnightActions(this);

    // General
    private readonly InputActionMap m_General;
    private IGeneralActions m_GeneralActionsCallbackInterface;
    private readonly InputAction m_General_Pause;
    private readonly InputAction m_General_Quit;
    public struct GeneralActions
    {
        private @PlayerControls m_Wrapper;
        public GeneralActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_General_Pause;
        public InputAction @Quit => m_Wrapper.m_General_Quit;
        public InputActionMap Get() { return m_Wrapper.m_General; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralActions instance)
        {
            if (m_Wrapper.m_GeneralActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPause;
                @Quit.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnQuit;
            }
            m_Wrapper.m_GeneralActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
            }
        }
    }
    public GeneralActions @General => new GeneralActions(this);
    public interface IPrincessActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
    public interface IKnightActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
    }
    public interface IGeneralActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
    }
}
