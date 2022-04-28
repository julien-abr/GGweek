using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] int numberOfHearts = 3;

    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    private void Update()
    {
        if (health > numberOfHearts)
            health = numberOfHearts;
        

        for(int i = 0; i< hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
            
            if(i < numberOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
