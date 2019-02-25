using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour {

    Vector3 diff;

    //追従ターゲットのパラメータ
    public GameObject target;
    public float followSpeed;

	// Use this for initialization
	void Start () {
        //追従距離の計算
        diff = target.transform.position - transform.position;
	}

    private void LateUpdate()
    {
        //線形補間関数によるスムージング
        transform.position = Vector3.Lerp(transform.position, target.transform.position - diff, Time.deltaTime * followSpeed);
    }
    // Update is called once per frame
    void Update () {
		
	}

}
