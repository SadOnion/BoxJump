using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSpawner : MonoBehaviour
{
    public GameObject meteor;
    public GameObject exclamationMark;
    private readonly float startTime = 6f;
    private readonly float endTime = 2.5f;
    [SerializeField]Transform[] dangerPoints;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            StartCoroutine(SpawnMeteor());
            timer = Mathf.Clamp(startTime - 0.05f*GameManager.instance.score,endTime,startTime);
        }
    }
    IEnumerator SpawnMeteor()
    {
        Transform place = dangerPoints[Random.Range(0, dangerPoints.Length)] ;
        GameObject mark = Instantiate(exclamationMark, (Vector2)place.position, Quaternion.identity);
        mark.transform.SetParent(place);
        Destroy(mark, .5f);
        yield return new WaitForSeconds(.5f);
        Instantiate(meteor, (Vector2)place.position + Vector2.right * 2, Quaternion.identity);
    }
}
