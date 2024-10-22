using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;


    [Header("PLAYER")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float reSpawnDelay;
    public Player player;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        if (respawnPoint == null)
        {
            respawnPoint = FindObjectOfType<StartPoint>().transform;
        }
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }    
    }

    public void RespawnPlayer()
    {
        DificultyManager dificultyManager = DificultyManager.instance;
        if (dificultyManager != null && dificultyManager.difficulty == DifficultyType.Hard)
            return;
        StartCoroutine(CouroutineResawnPlayer());
        //CreateObeject();
    }
    public IEnumerator CouroutineResawnPlayer()
    {
        yield return new WaitForSeconds(reSpawnDelay);
        GameObject newPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
        // dong nghi van
        CameraManager.instance.OnPlayerRespawn(player.transform);
        //dong nghi van

    }

    public void UpdateRespawnPosition(Transform newRespawn)
    {
        respawnPoint = newRespawn;
    }

}
