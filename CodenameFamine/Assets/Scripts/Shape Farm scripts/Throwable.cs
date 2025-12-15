using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Throwable : MonoBehaviour
{
    [SerializeField] private float throwForceMultiplier;
    [SerializeField] private float grabFrequency; //stiffness of grab
    [SerializeField] private float grabDamping; //reduce oscillation of grab

    private Rigidbody2D rb;
    private Camera mainCam;
    private TargetJoint2D grabJoint;

    private InputAction pointAction;
    private InputAction grabAction;

    private Vector2 lastMouseWorldPos;
    private Vector2 throwVelocity;

    public void SetThrowForceMultiplier(float throwForceMultiplierValue) => throwForceMultiplier = throwForceMultiplierValue;
    public void SetGrabFrequency(float grabFrequencyValue) => grabFrequency = grabFrequencyValue;
    public void SetGrabDamping(float grabDampingValue) => grabDamping = grabDampingValue;

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
        if (grabJoint == null)
            return;

        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(pointAction.ReadValue<Vector2>());
        throwVelocity = (mouseWorldPos - lastMouseWorldPos) / Time.deltaTime;
        lastMouseWorldPos = mouseWorldPos;

        grabJoint.target = mouseWorldPos;
    }

    private void OnGrabStarted(InputAction.CallbackContext context)
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(pointAction.ReadValue<Vector2>());
        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
        
        if (!hit || hit.gameObject != gameObject)
            return;

        grabJoint = gameObject.AddComponent<TargetJoint2D>();
        grabJoint.autoConfigureTarget = false;
        grabJoint.target = mouseWorldPos;

        grabJoint.frequency = grabFrequency;
        grabJoint.dampingRatio = grabDamping;

        grabJoint.anchor = rb.transform.InverseTransformPoint(mouseWorldPos);

        lastMouseWorldPos = mouseWorldPos;
    }


    private void OnGrabReleased(InputAction.CallbackContext context)
    {
        if (grabJoint == null)
            return;

        Destroy(grabJoint);
        grabJoint = null;

        rb.AddForce(throwVelocity * throwForceMultiplier, ForceMode2D.Impulse);
    }
}