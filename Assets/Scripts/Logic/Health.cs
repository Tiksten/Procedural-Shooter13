using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;

    public float hp;

    [SerializeField]
    private GameObject deadBody;

    [SerializeField]
    private Slider slider;

    private void Start()
    {
        hp = maxHealth;

        if (slider != null)
            slider.value = hp;
    }

    public void RecieveDmg(float dmg)
    {
        hp -= dmg;

        if (hp > maxHealth)
            hp = maxHealth;

        if (slider != null)
            slider.value = hp;

        if (hp <= 0)
        {
            Destroy(Instantiate(deadBody, transform.position, transform.rotation), 12);
            Destroy(gameObject);
        }
    }
}
