using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _Enemies, _EnemyContainer;
    [SerializeField]
    private GameObject[] Powerups;
    
    private bool isAlive = true; 
    // Start is called before the first frame update
    public void StartSpawnManagers()
    {
        StartCoroutine(Enemy_SpawnManager());
        StartCoroutine(PowerUps_SpawnManager());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Enemy_SpawnManager()
    {
        yield return new WaitForSeconds(3.0f);
        while(isAlive == true)
        {
            Vector3 SpawnPoint = new Vector3(Random.Range(-9.0f, 9.0f), 8f, 0);
            GameObject NewEnemy = Instantiate(_Enemies, SpawnPoint, Quaternion.identity);
            NewEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator PowerUps_SpawnManager()
    {
        yield return new WaitForSeconds(3.0f);
        while (isAlive == true)
        {
            Vector3 SpawnPoint = new Vector3(Random.Range(-9.0f, 9.0f), 8f, 0);
            Instantiate(Powerups[Random.Range(0, 3)], SpawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 15));
        }
    }
   
    public void OnPlayerDeadth()
    {
        isAlive = false;
    }
}
