using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestuct : MonoBehaviour
{
    private ParticleSystem Effect;
    void Awake()
    {
        Effect = GetComponent<ParticleSystem>();
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        Effect.Play();
        yield return new WaitForSeconds(Effect.main.duration + 0.05f);
        Destroy(gameObject);
    }


}
