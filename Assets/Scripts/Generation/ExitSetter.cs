using UnityEngine;

public class ExitSetter : MonoBehaviour
{
    [SerializeField]
    private GameObject exit;

    [SerializeField]
    private int iterations = 16;

    [SerializeField]
    private float dist = 100;

    [SerializeField]
    private LayerMask layerMask;

    private float mx = 0;

    private Vector2 pos = Vector2.zero;

    public void Start()
    {
        Invoke("Iterate", 0.1f);
    }

    private void Iterate()
    {
        if (iterations > 0)
        {
            iterations--;

            RaycastHit2D hit;

            hit = Physics2D.Raycast(Vector2.zero, Random.insideUnitCircle, dist, layerMask);

            if(hit.transform != null)
            {
                if((hit.point - Vector2.zero).magnitude > mx)
                {
                    mx = (hit.point - Vector2.zero).magnitude;
                    pos = hit.point + (Vector2.zero - hit.point).normalized;
                }
            }

            Iterate();
        }
        else
        {
            Instantiate(exit, pos, Quaternion.identity);
            Destroy(this);
        }
    }
}
