using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRender;

    private bool triggered; //trap get trigger
    private bool active; //trap is active and can hurt player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        //change color to notify player, trap is triggered
        triggered = true;
        spriteRender.color = Color.red;
        
        //wait for delay, activate trap, animation on, return trap color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRender.color = Color.white; //change to initial color
        active = true;
        anim.SetBool("activated", true);

        //wait for x secs, deactivate trap and animations
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
