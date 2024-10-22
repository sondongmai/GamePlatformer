using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct Skin
{
    public string skinName;
    public int skinPrice;
    public bool unlocked;
}
public class SkinSelectionUI : MonoBehaviour
{
    // Start is called before the first frame update
    private UI_LevelSelection uiLevelSelection;
    private UI_MainMenu uiMainMenu;

    [SerializeField] Skin []skins;
    [SerializeField] private int skinIndex;
    [SerializeField] private int maxIndex;
    [SerializeField] private Animator skindisplay;

    [SerializeField] private TextMeshProUGUI buySelectText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI bankText;

    private void Start()
    {
        uiMainMenu = GetComponentInParent<UI_MainMenu>();
        uiLevelSelection = uiMainMenu.GetComponentInChildren<UI_LevelSelection>(true);
        LoadSkinIndex();
        updateSkin();
    }
    public void LoadSkinIndex()
    {
        for(int i = 0; i < skins.Length; i++)
        {
            string skinName1 = skins[i].skinName;
            bool SkinUnlocked = PlayerPrefs.GetInt(skinName1 + "unlocked", 0)==1;
            if( SkinUnlocked || i == 0)
            {
                skins[i].unlocked = true;
            }   
        }    
    }
    public void NextSkin()
    {
        skinIndex++;
        if (skinIndex > maxIndex)
        {
            skinIndex = 0;
        }
        AudioManager.instance.PlaySFX(4);
        updateSkin();
    }
    public void PrevSkin() 
    {
        skinIndex--;
        if(skinIndex < 0) 
        {
            skinIndex = maxIndex;
        }
        AudioManager.instance.PlaySFX(4);
        updateSkin();
    }
    public void updateSkin()
    {
        bankText.text = "Bank: "+FruitInBank().ToString();
        for(int i = 0; i< skindisplay.layerCount; i++) 
        {
            skindisplay.SetLayerWeight(i, 0f);
        }
        skindisplay.SetLayerWeight(skinIndex, 1f);
        if (skins[skinIndex].unlocked)
        {
            priceText.transform.parent.gameObject.SetActive(false);
            buySelectText.text = "Select";
        }    
        else
        {
            priceText.transform.parent.gameObject.SetActive(true);
            priceText.text = "Price: "+skins[skinIndex].skinPrice.ToString();
            buySelectText.text = "Buy";
        }    
    }    

    private void buySkin(int  index)
    {

        if (HaveEnoughFruit(skins[index].skinPrice) == false)
        {
            AudioManager.instance.PlaySFX(6);
            return;
        }


        AudioManager.instance.PlaySFX(10);
        string skinName = skins[skinIndex].skinName;
        skins[skinIndex].unlocked = true;
        PlayerPrefs.SetInt(skinName+"unlocked", 1);
    }    

    private bool HaveEnoughFruit(int price)
    {
        if(FruitInBank()>=price)
        {
            PlayerPrefs.SetInt("AmountFruitCollected",FruitInBank()-price);
            return true;
        }    
        else { return false; }  
    }    
    private int FruitInBank() => PlayerPrefs.GetInt("AmountFruitCollected");
    public void selectSkin()
    {
        if (skins[skinIndex].unlocked == false)
        {
            buySkin(skinIndex);
        }
        else
        {
            SkinManager.instance.setSkinID(skinIndex);
            uiMainMenu.switchUI(uiLevelSelection.gameObject);
        }
        AudioManager.instance.PlaySFX(4);
        updateSkin();
    }    
}
