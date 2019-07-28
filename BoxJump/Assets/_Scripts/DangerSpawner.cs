using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSpawner : MonoBehaviour
{
    public GameObject bird;
    public GameObject exclamationMark;
    public float minTime;
    public float maxTime;
    [SerializeField]Transform[] dangerPoints;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            StartCoroutine(SpawnBird());
            timer = Random.Range(minTime, maxTime);
        }
    }
    IEnumerator SpawnBird()
    {
        Transform place = dangerPoints[Random.Range(0, dangerPoints.Length)] ;
        GameObject mark = Instantiate(exclamationMark, (Vector2)place.position, Quaternion.identity);
        mark.transform.SetParent(place);
        Destroy(mark, .5f);
        yield return new WaitForSeconds(.5f);
        Instantiate(bird, (Vector2)place.position + Vector2.right * 2, Quaternion.identity);
    }
}
