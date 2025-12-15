using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteBillboarding : MonoBehaviour
{
    [SerializeField] bool freezeAxisXZ;

    private void Update()
    {
        if (freezeAxisXZ)
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        else
            transform.rotation = Camera.main.transform.rotation;
    }
}