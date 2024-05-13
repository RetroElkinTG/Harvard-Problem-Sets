using UnityEngine;
using UnityEngine.UI;

// Heart behaviour
public class HeartManager : MonoBehaviour
{
    [Header("Heart Variables")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite threeQuarterHeart;
    public Sprite halfHeart;
    public Sprite quarterHeart;
    public Sprite emptyHeart;
    public HealthValues heartContainers;
    public HealthValues playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        SetHearts();
    }

    // Set hearts
    public void SetHearts()
    {
        for (int index = 0; index < heartContainers.runtimeValue; index ++)
        {
            hearts[index].gameObject.SetActive(true);
            UpdateHearts();
        }
    }

    // Update hearts
    public void UpdateHearts()
    {
        float tempHealth = playerHealth.runtimeValue / 4;
        for (int index = 0; index < heartContainers.runtimeValue; index ++)
        {
            UpdateHeartSprite(index, tempHealth);
        }
    }

    // Update heart sprite
    private void UpdateHeartSprite(int index, float tempHealth)
    {
        float currentHeart = Mathf.Ceil(tempHealth - 1);
        if (index <= tempHealth - 1)
        {
            hearts[index].sprite = fullHeart;
        }
        else if (index >= tempHealth)
        {
            hearts[index].sprite = emptyHeart;
        }
        else if (index == currentHeart && (tempHealth % 1) == .50)
        {
            hearts[index].sprite = halfHeart;
        }
        else if (index == currentHeart && (tempHealth % 1) == .25)
        {
            hearts[index].sprite = quarterHeart;
        }
        else if (index == currentHeart && (tempHealth % 1) == .75)
        {
            hearts[index].sprite = threeQuarterHeart;
        }
    }
}