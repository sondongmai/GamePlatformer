using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    
    public static CreateObject instance;
    [Header("Trap")]
    public GameObject ArrowPrefab;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance == null)
        {
            instance = this;
        }    
        else
        {
            DontDestroyOnLoad(gameObject);
        }    
    }
    public void CreateObeject(GameObject prefab, Transform target, float delay = 0)
    {
        StartCoroutine(CreateObjectCoroutine(prefab, target, delay));
    }
    private IEnumerator CreateObjectCoroutine(GameObject prefab, Transform target, float delay)
    {
        Vector3 newPos = target.position;
        yield return new WaitForSeconds(delay);
        //gameObject.transform.position = newPos;
        //gameObject.SetActive(true);
        GameObject gameObject = Instantiate(prefab, newPos, Quaternion.identity);
    }

}
