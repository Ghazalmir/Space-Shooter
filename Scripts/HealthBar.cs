using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    public Gradient gradient;
    Player player;
    public Image fill;


    void Start()
    {
        player = FindObjectOfType<Player>();
        slider.value = player.getHealth();
        fill.color = gradient.Evaluate(slider.normalizedValue);



    }
    
    void Update()
    {
        slider.value = player.getHealth();
        fill.color = gradient.Evaluate(slider.normalizedValue);

        
    }

}
