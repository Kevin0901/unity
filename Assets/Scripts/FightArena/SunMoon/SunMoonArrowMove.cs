﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoonArrowMove : MonoBehaviour
{
    public bool isSun, noEffect;
    public float speed;
    private Vector3 dir;
    private Rigidbody2D mrigibody2D;
    private void Start()
    {
        mrigibody2D = this.GetComponent<Rigidbody2D>();
    }
    //箭矢速度
    void FixedUpdate()
    {
        if (noEffect)
        {
            mrigibody2D.velocity = dir.normalized * speed;
        }
        else if (isSun)
        {
            mrigibody2D.velocity = dir.normalized * speed * 2.5f;
        }
        else
        {
            mrigibody2D.velocity = dir.normalized * speed * 0.35f;
        }
    }
    //箭矢摧毀
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 10)
        {
            Destroy(this.gameObject);
        }
    }
    //箭矢摧毀
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("background"))
        {
            Destroy(this.gameObject);
        }
    }
    //射擊方向
    public void setArrow()
    {
        Quaternion k = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward);
        dir = k * Vector3.up; //得知射的方向
        noEffect = true;
    }
}
