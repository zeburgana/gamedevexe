using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour {

    public GameObject EnemyObject;
    public Slider HpSlider;
    float height;
    Character EnemyCharacter;

    Vector3 EnemyWorldPos;
    Quaternion rotation;
    void Start()
    {
        height = gameObject.transform.position.y - EnemyObject.transform.position.y;
        rotation = transform.rotation;

    }
    public void Initiate()
    {
        Debug.Log("initiate method");
        EnemyCharacter = EnemyObject.GetComponent<Character>();
        HpSlider.maxValue = EnemyCharacter.healthMax;
        HpSlider.value = EnemyCharacter.healthCurrent;
    }
    void LateUpdate()
    {
        transform.position = new Vector3(EnemyObject.transform.position.x, EnemyObject.transform.position.y + height, 0);
        transform.rotation = rotation;
    }
    public void SetHealth(float value)
    {
        Debug.Log("setting hp");
        HpSlider.value = value;
    }
}
