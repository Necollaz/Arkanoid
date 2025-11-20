// GENERATED AUTOMATICALLY FROM 'Assets/_Project/Scripts/Gameplay/PlayerInput/PlayerControls.inputactions'

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
            ""name"": ""PlayerInput"",
            ""id"": ""755f3b83-60e7-4fa8-9a1a-e0c626ab79f7"",
            ""actions"": [
                {
                    ""name"": ""LaunchBall"",
                    ""type"": ""Button"",
                    ""id"": ""be552001-d88d-4086-8cd4-3e5f5a8a4695"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8ba51914-a487-4cc8-9130-4268d70a30d5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LaunchBall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63e1681e-6b24-46c6-85f3-8b86c449a46d"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LaunchBall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_LaunchBall = m_PlayerInput.FindAction("LaunchBall", throwIfNotFound: true);
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

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_LaunchBall;
    public struct PlayerInputActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerInputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @LaunchBall => m_Wrapper.m_PlayerInput_LaunchBall;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @LaunchBall.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLaunchBall;
                @LaunchBall.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLaunchBall;
                @LaunchBall.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLaunchBall;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LaunchBall.started += instance.OnLaunchBall;
                @LaunchBall.performed += instance.OnLaunchBall;
                @LaunchBall.canceled += instance.OnLaunchBall;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
    public interface IPlayerInputActions
    {
        void OnLaunchBall(InputAction.CallbackContext context);
    }
}
