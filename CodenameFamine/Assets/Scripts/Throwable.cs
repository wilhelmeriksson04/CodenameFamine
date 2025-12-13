using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Throwable : MonoBehaviour
{
    [SerializeField] private float throwForceMultiplier = .2f;

    private Rigidbody2D rb;
    private Camera mainCam;

    private InputAction pointAction;
    private InputAction grabAction;

    //runtime states
    private bool isGrabbed;
    private Vector2 lastMouseWorldPos;
    private Vector2 throwVelocity;

    public void SetThrowForceMultiplier(float _throwForceMultiplier)
    {
        throwForceMultiplier = _throwForceMultiplier;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

        pointAction = InputSystem.actions.FindAction("Player/Point");
        grabAction = InputSystem.actions.FindAction("Player/Grab");
    }

    private void OnEnable()
    {
        pointAction.Enable();
        grabAction.Enable();

        grabAction.started += OnGrabStarted;
        grabAction.canceled += OnGrabReleased;
    }

    private void OnDisable()
    {
        grabAction.started -= OnGrabStarted;
        grabAction.canceled -= OnGrabReleased;
    }

    private void Update()
    {
        if (!isGrabbed)
            return;

        //convert mouse position to world position
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(pointAction.ReadValue<Vector2>());

        //calculate velocity based on mouse movement
        throwVelocity = (mouseWorldPos - lastMouseWorldPos) / Time.deltaTime;

        //move object directly while grabbed
        rb.position = mouseWorldPos;

        lastMouseWorldPos = mouseWorldPos;
    }

    private void OnGrabStarted(InputAction.CallbackContext ctx)
    {
        //check if mouse is over collider
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(pointAction.ReadValue<Vector2>());
        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

        if (hit == null || hit.gameObject != gameObject)
            return;

        isGrabbed = true;

        //disable physics on grab
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;

        lastMouseWorldPos = mouseWorldPos;
    }

    private void OnGrabReleased(InputAction.CallbackContext ctx)
    {
        if (!isGrabbed)
            return;

        isGrabbed = false;

        //enable physics
        rb.isKinematic = false;

        rb.AddForce(throwVelocity * throwForceMultiplier, ForceMode2D.Impulse);
    }
}