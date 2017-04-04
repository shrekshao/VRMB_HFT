using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectPoolManager : MonoBehaviour {

    [SerializeField]
    GameObject AttackHitEffectPrefab;

    [SerializeField]
    GameObject BlockArrowHitEffectPrefab;


    public static HitEffectPoolManager instance;


    //List<GameObject> attackHitEffects;

    int sizeBlockArrowHitEffects = 3;
    List<GameObject> blockArrowHitEffects;

    int idBlock = 0;


    // Use this for initialization
    void Start () {
        instance = GameObject.Find("HitEffectPool").GetComponent<HitEffectPoolManager>();

        blockArrowHitEffects = new List<GameObject>();
        for (int i = 0; i < sizeBlockArrowHitEffects; i++)
        {
            GameObject go = Instantiate(BlockArrowHitEffectPrefab);
            go.SetActive(false);
            blockArrowHitEffects.Add( go );
        }
	}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public GameObject getBlockArrowHitEfects()
    {
        GameObject result = blockArrowHitEffects[idBlock];
        result.SetActive(true);
        idBlock++;
        if (idBlock >= sizeBlockArrowHitEffects)
        {
            idBlock = 0;
        }
        return result;
    }
}
