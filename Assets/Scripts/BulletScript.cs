using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float _BulletSpeed = 12f;
    private float _EnemyBulletSpeed = 7f;
    private bool IsEnemyBullet = false;
    void Update()
    {
        if (IsEnemyBullet == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * _BulletSpeed * Time.deltaTime);
        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
    private void MoveDown()
    {
        transform.Translate(Vector3.down * _EnemyBulletSpeed * Time.deltaTime);
        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void EnemyLaserActive()
    {
        IsEnemyBullet = true;
    }


}
