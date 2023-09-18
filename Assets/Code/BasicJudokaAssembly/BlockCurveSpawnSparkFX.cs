using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCurveSpawnSparkFX : MonoBehaviour
{
    [SerializeField] GameObject sparksPrefab;
    [SerializeField] float SPARK_LIFE = 0.2f;
    [HideInInspector] public GameObject defendingFoot; // set in Foot to prevent sparks from detecting this foot

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Foot>() && collision.gameObject != defendingFoot)
        {
            GameObject sparks = Instantiate(sparksPrefab, collision.GetContact(0).point, Quaternion.identity);
            Destroy(sparks, SPARK_LIFE);
        }
    }
}
