using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpearPoint : MonoBehaviour {

    [SerializeField]
    Enemy m_enemyAnimator;

    private HitEffectPoolManager hitEffectPoolManager;

    // Use this for initialization
    void Start()
    {
        hitEffectPoolManager = HitEffectPoolManager.instance;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("PlayerAttack"))
        {
            // get blocked
            Debug.Log("Spear get blocked");

            GetComponent<Collider>().enabled = false;

            // generate hit effect
            GameObject h = hitEffectPoolManager.getBlockArrowHitEfects();
            h.transform.position = transform.position;
            h.GetComponent<ParticleSystem>().Play();
            h.GetComponent<AudioSource>().Play();

            // affect enemy (parent holder) animator
            m_enemyAnimator.executeGetBlocked();
        }
    }

}
