using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet {
	
    void Start () {
        Destroy(gameObject, 2f);
    }
}
