 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    void Start()
    {
        StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        slider.maxValue = StatsManager.Instance.maxHealth;
        slider.value = StatsManager.Instance.currentHealth;
    }
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;
        slider.value = StatsManager.Instance.currentHealth;
        if (StatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
