using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class DrawCardInput : MonoBehaviour
{
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

    private void Start()
    {
        //finds action by name from Input Actions asset
        drawCardAction = InputAction.actions.FindAction("CardGameplay/DrawCard");

        drawCardAction.Enable();
    }

    #region Accessors
    public bool GetDrawCardInputDown() => drawCardAction.WasPressedThisFrame();
    #endregion
}