using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class DrawCardInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset CardGame;

    private InputAction drawCardAction;

    //singleton
    public static DrawCardInput Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        drawCardAction = CardGame.FindAction("CardGame/DrawCard", true);
        drawCardAction.Enable();
    }

    private void OnDisable()
    {
        drawCardAction.Disable();
    }

    #region Accessors
    public bool GetDrawCardInputDown() => drawCardAction.WasPressedThisFrame();
    #endregion
}