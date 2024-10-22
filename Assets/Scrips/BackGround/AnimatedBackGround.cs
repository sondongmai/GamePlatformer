using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BackGroundType { Blue, Brown, Gray, Green, Pink, Purple, Yelow}

public class AnimatedBackGround : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private Texture2D[] textures;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private BackGroundType backGroundType;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        UpdateBackGroud();

    }
    private void Update()
    {
        meshRenderer.material.mainTextureOffset += moveDirection*Time.deltaTime;
    }
    private void UpdateBackGroud()
    {
        meshRenderer.material.mainTexture = textures[((int)backGroundType)];
    }    


}
