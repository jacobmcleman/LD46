// GENERATED AUTOMATICALLY FROM 'Assets/Controls/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player Controls"",
            ""id"": ""aa9cec4d-0136-4964-84c5-be2dc493925c"",
            ""actions"": [
                {
                    ""name"": ""Throttle"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8ae65d6f-6830-488f-8ce2-c4794106faf1"",
                    ""expectedControlType"": ""Double"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pitch"",
                    ""type"": ""PassThrough"",
                    ""id"": ""984ee56d-ac89-4a47-944b-80d54fd06fc5"",
                    ""expectedControlType"": ""Double"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Yaw"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bc828117-8141-4fc5-9ba9-44eab4db60bf"",
                    ""expectedControlType"": ""Double"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""95583cdf-b30c-41ab-8ac6-64709caa19c1"",
                    ""expectedControlType"": ""Double"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""00e3d55c-0761-472c-a5d0-8908bbcacfd8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""b5c0f43f-5880-4b69-a0ed-f3e7eabf68b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GetBestTarget"",
                    ""type"": ""Button"",
                    ""id"": ""2f124f3c-36a1-43bc-85c7-7ceef03012a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GetNextTarget"",
                    ""type"": ""Button"",
                    ""id"": ""54d857a7-707a-493c-bbac-a7e25892ef86"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Regurgitate"",
                    ""type"": ""Button"",
                    ""id"": ""c324796e-f723-4260-b4a6-7a634e043a93"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Continue"",
                    ""type"": ""Button"",
                    ""id"": ""b6e6b4fb-ec30-4bb8-b721-cf240bab870c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""456313b0-753c-4180-b62e-e19274540c46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""KBM"",
                    ""id"": ""1ebb9fa8-1c50-4e5a-a441-f5311670fbc6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""cd607cf8-40be-4e6b-92f2-f540ed02acc1"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e96f1e38-a6c1-43a7-adbc-2304a92095d8"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""a046cbf9-0e11-4cd9-b9ca-df5582d5bd88"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""84edc2aa-1d82-474c-b882-b0f04047d561"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a1f9a486-85a8-4905-92bb-17609f271114"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""0e4f5f9b-a456-4184-8713-84edeabe4b84"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pitch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""afae00ec-6059-42f3-a805-f0f08c082650"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Pitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""538b6898-cd5d-4ecc-95cd-95655a2f173c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Pitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""9a71354b-297a-4516-b3f0-e9293d141005"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pitch"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bab8f0a9-54cd-41a6-87b2-71fd38494e00"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5f21243e-0bca-4b42-880c-655fd573a786"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""e763d94d-bf39-4655-b65a-2a531ea5091f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""cdcb3687-3b1d-4b09-a40a-41ef1edcb6da"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""19cdf9a0-c0dd-4bae-b4ec-d233080a223a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Xbox"",
                    ""id"": ""ef181f37-2f49-47b4-a19f-4a261d8f8059"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6ce99c8f-b79e-4da8-a467-6e0cf6508d42"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d71ee563-1d75-415b-80ae-710cfa7242a1"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""PS4"",
                    ""id"": ""88ef86a0-10e7-4519-8d64-4eb860aacc96"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""cef6d1ee-4d21-4013-a98e-cacca0e7c80e"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d4c3d609-530d-4b1e-9108-1d9f9534e3b8"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad Stick"",
                    ""id"": ""0bec96c4-7d98-421f-af27-1105d40840ba"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Yaw"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8c7c5a6a-a8ac-4797-9ef5-abe799da8e37"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""55f42534-b3ad-44a7-9eda-fc288441c0a9"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""a361585c-f553-44da-bbbe-741120f42757"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""76743a31-a3cd-4b64-b67d-2ef0afe681c0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""07020d10-9f2b-4bd9-905c-032ecf90f587"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""be1eb056-afd3-4e81-b5df-e88d1ca6c442"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0861ff7b-a260-4f76-8592-15e9deb0ad16"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f2b21b18-95f8-4d37-a7e7-953b398266e2"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ffc9b8af-e273-4e27-a3ea-184acd3201d8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""PrimaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa9a0444-9051-4c74-a9d6-125effddc8f5"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PrimaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80d337cf-4157-4bdf-935c-145969d6583e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""SecondaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41acd7e4-16b4-4e84-aba2-70e9ee93900c"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SecondaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f2f7c43-b590-4640-8d09-d971b33c86ac"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""GetBestTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acc39e5e-295a-4f70-8380-0ab31c30a40d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GetBestTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""205fc02d-fa55-4032-b3d3-fb3d6514a4ff"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""GetNextTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a160e6ab-d0b5-4db0-bea3-4d7b115e8796"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GetNextTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c628243-4368-4134-879d-be22c152a89d"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Regurgitate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e3a3b3b-69dd-4be5-b079-cfc33c2b6ccd"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Regurgitate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0651f236-37aa-4b23-a91d-4a736d5a7214"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db1df3f1-6baa-4e52-8605-fc6032f53c9e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e1fa9eb-320b-4e8b-9c1f-2f825667b3c7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a15c5c2-fb96-42fa-9bd6-365fb0eb49ca"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KBM"",
            ""bindingGroup"": ""KBM"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player Controls
        m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
        m_PlayerControls_Throttle = m_PlayerControls.FindAction("Throttle", throwIfNotFound: true);
        m_PlayerControls_Pitch = m_PlayerControls.FindAction("Pitch", throwIfNotFound: true);
        m_PlayerControls_Yaw = m_PlayerControls.FindAction("Yaw", throwIfNotFound: true);
        m_PlayerControls_Roll = m_PlayerControls.FindAction("Roll", throwIfNotFound: true);
        m_PlayerControls_PrimaryFire = m_PlayerControls.FindAction("PrimaryFire", throwIfNotFound: true);
        m_PlayerControls_SecondaryFire = m_PlayerControls.FindAction("SecondaryFire", throwIfNotFound: true);
        m_PlayerControls_GetBestTarget = m_PlayerControls.FindAction("GetBestTarget", throwIfNotFound: true);
        m_PlayerControls_GetNextTarget = m_PlayerControls.FindAction("GetNextTarget", throwIfNotFound: true);
        m_PlayerControls_Regurgitate = m_PlayerControls.FindAction("Regurgitate", throwIfNotFound: true);
        m_PlayerControls_Continue = m_PlayerControls.FindAction("Continue", throwIfNotFound: true);
        m_PlayerControls_Pause = m_PlayerControls.FindAction("Pause", throwIfNotFound: true);
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

    // Player Controls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Throttle;
    private readonly InputAction m_PlayerControls_Pitch;
    private readonly InputAction m_PlayerControls_Yaw;
    private readonly InputAction m_PlayerControls_Roll;
    private readonly InputAction m_PlayerControls_PrimaryFire;
    private readonly InputAction m_PlayerControls_SecondaryFire;
    private readonly InputAction m_PlayerControls_GetBestTarget;
    private readonly InputAction m_PlayerControls_GetNextTarget;
    private readonly InputAction m_PlayerControls_Regurgitate;
    private readonly InputAction m_PlayerControls_Continue;
    private readonly InputAction m_PlayerControls_Pause;
    public struct PlayerControlsActions
    {
        private @Controls m_Wrapper;
        public PlayerControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Throttle => m_Wrapper.m_PlayerControls_Throttle;
        public InputAction @Pitch => m_Wrapper.m_PlayerControls_Pitch;
        public InputAction @Yaw => m_Wrapper.m_PlayerControls_Yaw;
        public InputAction @Roll => m_Wrapper.m_PlayerControls_Roll;
        public InputAction @PrimaryFire => m_Wrapper.m_PlayerControls_PrimaryFire;
        public InputAction @SecondaryFire => m_Wrapper.m_PlayerControls_SecondaryFire;
        public InputAction @GetBestTarget => m_Wrapper.m_PlayerControls_GetBestTarget;
        public InputAction @GetNextTarget => m_Wrapper.m_PlayerControls_GetNextTarget;
        public InputAction @Regurgitate => m_Wrapper.m_PlayerControls_Regurgitate;
        public InputAction @Continue => m_Wrapper.m_PlayerControls_Continue;
        public InputAction @Pause => m_Wrapper.m_PlayerControls_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Throttle.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnThrottle;
                @Throttle.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnThrottle;
                @Throttle.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnThrottle;
                @Pitch.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPitch;
                @Pitch.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPitch;
                @Pitch.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPitch;
                @Yaw.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnYaw;
                @Yaw.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnYaw;
                @Yaw.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnYaw;
                @Roll.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRoll;
                @PrimaryFire.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPrimaryFire;
                @PrimaryFire.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPrimaryFire;
                @PrimaryFire.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPrimaryFire;
                @SecondaryFire.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSecondaryFire;
                @SecondaryFire.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSecondaryFire;
                @SecondaryFire.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSecondaryFire;
                @GetBestTarget.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGetBestTarget;
                @GetBestTarget.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGetBestTarget;
                @GetBestTarget.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGetBestTarget;
                @GetNextTarget.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGetNextTarget;
                @GetNextTarget.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGetNextTarget;
                @GetNextTarget.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGetNextTarget;
                @Regurgitate.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRegurgitate;
                @Regurgitate.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRegurgitate;
                @Regurgitate.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRegurgitate;
                @Continue.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnContinue;
                @Continue.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnContinue;
                @Continue.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnContinue;
                @Pause.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Throttle.started += instance.OnThrottle;
                @Throttle.performed += instance.OnThrottle;
                @Throttle.canceled += instance.OnThrottle;
                @Pitch.started += instance.OnPitch;
                @Pitch.performed += instance.OnPitch;
                @Pitch.canceled += instance.OnPitch;
                @Yaw.started += instance.OnYaw;
                @Yaw.performed += instance.OnYaw;
                @Yaw.canceled += instance.OnYaw;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @PrimaryFire.started += instance.OnPrimaryFire;
                @PrimaryFire.performed += instance.OnPrimaryFire;
                @PrimaryFire.canceled += instance.OnPrimaryFire;
                @SecondaryFire.started += instance.OnSecondaryFire;
                @SecondaryFire.performed += instance.OnSecondaryFire;
                @SecondaryFire.canceled += instance.OnSecondaryFire;
                @GetBestTarget.started += instance.OnGetBestTarget;
                @GetBestTarget.performed += instance.OnGetBestTarget;
                @GetBestTarget.canceled += instance.OnGetBestTarget;
                @GetNextTarget.started += instance.OnGetNextTarget;
                @GetNextTarget.performed += instance.OnGetNextTarget;
                @GetNextTarget.canceled += instance.OnGetNextTarget;
                @Regurgitate.started += instance.OnRegurgitate;
                @Regurgitate.performed += instance.OnRegurgitate;
                @Regurgitate.canceled += instance.OnRegurgitate;
                @Continue.started += instance.OnContinue;
                @Continue.performed += instance.OnContinue;
                @Continue.canceled += instance.OnContinue;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    private int m_KBMSchemeIndex = -1;
    public InputControlScheme KBMScheme
    {
        get
        {
            if (m_KBMSchemeIndex == -1) m_KBMSchemeIndex = asset.FindControlSchemeIndex("KBM");
            return asset.controlSchemes[m_KBMSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerControlsActions
    {
        void OnThrottle(InputAction.CallbackContext context);
        void OnPitch(InputAction.CallbackContext context);
        void OnYaw(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnPrimaryFire(InputAction.CallbackContext context);
        void OnSecondaryFire(InputAction.CallbackContext context);
        void OnGetBestTarget(InputAction.CallbackContext context);
        void OnGetNextTarget(InputAction.CallbackContext context);
        void OnRegurgitate(InputAction.CallbackContext context);
        void OnContinue(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
