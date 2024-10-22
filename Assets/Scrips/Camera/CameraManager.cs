
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [Header("Cinemachine Screen")]
    [SerializeField] private Vector2 velecityShake;
    private CinemachineImpulseSource impulseSource;
    public Transform playerTransform;


    // Lưu trữ target hiện tại
    public CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        instance = this;
        impulseSource = GetComponent<CinemachineImpulseSource>();

    }
    public void ScreenShake(float shakeDirection)
    {
        impulseSource.m_DefaultVelocity = new Vector2(velecityShake.x * shakeDirection, velecityShake.y);
        impulseSource.GenerateImpulse();
    }
     // Assign this in the inspector or find it at runtime
    private void Start()
    {
        // Set the virtual camera to follow the player at the start
        if (playerTransform != null)
        {
            virtualCamera.Follow = playerTransform;
        }
    }
    public void OnPlayerRespawn(Transform newPlayerTransform)
    {
        playerTransform = newPlayerTransform;
        virtualCamera.Follow = playerTransform;
    }









}
