using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    // I'll rewrite this ugly instance to events later
    // ugh so ugly...
    public static CameraShaker Instance { get; private set; }

    CinemachineImpulseSource mImpulseSource;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        mImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Recoil(Vector3 direction, float power) {
        if (!mImpulseSource)
            return;
        
        mImpulseSource.GenerateImpulseWithVelocity(Vector3.Normalize(direction) * power);
    }
}
