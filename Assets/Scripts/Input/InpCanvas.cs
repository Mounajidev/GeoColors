// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/InpCanvas.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InpCanvas : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InpCanvas()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InpCanvas"",
    ""maps"": [
        {
            ""name"": ""InputCanvas"",
            ""id"": ""97f5cbe0-7a10-4464-b9bd-916a00ed0df1"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""404f1542-7ece-458c-9ca0-55f976c11c8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""39f24bd0-772b-470e-a687-0f10f672c753"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56ee782d-b10a-402b-bea9-9a2138e65692"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InputCanvas
        m_InputCanvas = asset.FindActionMap("InputCanvas", throwIfNotFound: true);
        m_InputCanvas_Pause = m_InputCanvas.FindAction("Pause", throwIfNotFound: true);
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

    // InputCanvas
    private readonly InputActionMap m_InputCanvas;
    private IInputCanvasActions m_InputCanvasActionsCallbackInterface;
    private readonly InputAction m_InputCanvas_Pause;
    public struct InputCanvasActions
    {
        private @InpCanvas m_Wrapper;
        public InputCanvasActions(@InpCanvas wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_InputCanvas_Pause;
        public InputActionMap Get() { return m_Wrapper.m_InputCanvas; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputCanvasActions set) { return set.Get(); }
        public void SetCallbacks(IInputCanvasActions instance)
        {
            if (m_Wrapper.m_InputCanvasActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_InputCanvasActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_InputCanvasActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_InputCanvasActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_InputCanvasActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public InputCanvasActions @InputCanvas => new InputCanvasActions(this);
    public interface IInputCanvasActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}
