using UnityEngine;

public class ThrowableInstaller : MonoBehaviour
{
    [SerializeField] private float throwForceMultiplier = 5f;

    private void Awake()
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
        }
    }
}