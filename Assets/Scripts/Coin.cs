using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    Collectables coin;

    private void Awake() {
        coin = new Collectables("coin", 1, 0);
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("enters collash");
        if(other.gameObject.tag == "Player")
        {
            coin.UpdateScore();
            Destroy(gameObject);
            
        }
    }
}
