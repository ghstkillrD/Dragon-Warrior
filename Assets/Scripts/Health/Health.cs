using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float noOfFlashes;
    private SpriteRenderer spriteRender;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            //iframes
            StartCoroutine(Invulnerability_hurt());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        StartCoroutine(Invulnerability_healthup());
    }

    private IEnumerator Invulnerability_hurt()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < noOfFlashes; i++)
        {
            spriteRender.color = new Color(1, 0, 0, 1f);
            yield return new WaitForSeconds(iFramesDuration / (noOfFlashes * 2));
            spriteRender.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (noOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    private IEnumerator Invulnerability_healthup()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < noOfFlashes; i++)
        {
            spriteRender.color = new Color(0, 1, 0, 1f);
            yield return new WaitForSeconds(iFramesDuration / (noOfFlashes * 2));
            spriteRender.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (noOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

}
