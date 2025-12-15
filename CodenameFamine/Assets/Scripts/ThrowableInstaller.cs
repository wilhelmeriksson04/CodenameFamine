using UnityEngine;

public class ThrowableInstaller : MonoBehaviour
{
    [SerializeField] private float throwForceMultiplier;
    [SerializeField] private float grabFrequency;
    [SerializeField] private float grabDamping;

    private void Start()
    {
        //find objects with tag "ThrowableShape"
        GameObject[] throwables = GameObject.FindGameObjectsWithTag("ThrowableShape");

        foreach (GameObject obj in throwables)
        {
            if (!obj.TryGetComponent(out Throwable throwable))
            {
                throwable = obj.AddComponent<Throwable>();
            }

            throwable.SetThrowForceMultiplier(throwForceMultiplier);
            throwable.SetGrabFrequency(grabFrequency);
            throwable.SetGrabDamping(grabDamping);
        }
    }
}