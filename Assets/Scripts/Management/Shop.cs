using System;

using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
     [SerializeField]private TextMeshPro[] spritePricetmps;
    [SerializeField] private GameObject[] spritePriceStars;
    [SerializeField] private GameObject[] spriteBorders;
    [SerializeField] private Color notBoughtColor;
    [SerializeField] private Color boughtColor;
    [SerializeField] private Color selectedColor;


    private void Awake()
    {
        setAllToDefault();
        setSelected(PlayerPrefs.GetInt("Player Object Index"));
    }

    public void buyOrSelectSprite(int spriteIndex)
    {
        setAllToDefault();
        Debug.Log("Buy or Select Called " +
                  "Sprite price is " + spritePricetmps[spriteIndex].text);


        //create key for the sprite if there is not yet
        string key = "sprite " + spriteIndex + " is bought";
        int spritePrice = int.Parse(spritePricetmps[spriteIndex].text);
        //if the sprite isn't bought yet and there are enough stars to buy it:
        if (PlayerPrefs.GetInt(key, 0) == 0)
        {
            if (PlayerPrefs.GetInt("Total Stars") >= spritePrice)//Buying it
            {
                PlayerPrefs.SetInt(key, 1);
                PlayerPrefs.SetInt("Player Object Index", spriteIndex);
                PlayerPrefs.SetInt("Total Stars", PlayerPrefs.GetInt("Total Stars") - spritePrice);

                // Delete the sprite price from the shop menu
                Destroy(spritePriceStars[spriteIndex], .2f);
                Destroy(spritePricetmps[spriteIndex], .2f);
                setSelected(spriteIndex);
                FindObjectOfType<StartMenuUI>().setStarUi();
            }
            else
            {
                //if player isnt able to buy the sprite, shake the object
                Debug.Log("couldnt bought it bec to expensive: price is " + spritePrice);
                setSelected(PlayerPrefs.GetInt("Player Object Index"));
                //buttonTRs[spriteIndex].DOShakePosition(.3f, 1f, 5, 45f, false, false);
            }
        }
        else if (PlayerPrefs.GetInt(key, 0) == 1) //Select the sprite if the player already has it
        {
            PlayerPrefs.SetInt("Player Object Index", spriteIndex);
            setSelected(spriteIndex);
        }
        
    }

    public void test(TextMeshProUGUI spritePriceTMP)
    {
        Debug.Log("test");
    }

    //Border set methods
    private void setAllToDefault()
    {
        for (int i = 0; i <= 5; i++)
        {
            
            
            // Get the key to the sprite
            string key = "sprite " + i + " is bought";
            

            if (PlayerPrefs.GetInt(key,0) == 0)// if it is not bought
            {

                spriteBorders[i].GetComponent<SpriteRenderer>().color = notBoughtColor;
            }else//if the sprite is bought
            {
                spriteBorders[i].GetComponent<SpriteRenderer>().color = boughtColor;

                Destroy(spritePricetmps[i]);
                Destroy(spritePriceStars[i]);
            }
        }
    }

    private void setSelected(int spriteIndex)
    { 
        string key = "sprite " + spriteIndex + " is bought";
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.SetInt("Player Object Index", spriteIndex);

        spriteBorders[spriteIndex].GetComponent<SpriteRenderer>().color = selectedColor;
    }
}