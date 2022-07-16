using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private GameObject childPanel;
    [SerializeField]
    public HealthHandler healthHandler;
    [SerializeField]
    private Image healthImage;
    // Update is called once per frame
    void Update()
    {
        if (!healthHandler)
            return;
        healthImage.fillAmount = healthHandler.currentNormalizedHealth;
        if((healthImage.fillAmount <= 0) && childPanel.activeInHierarchy)
        {
            childPanel.SetActive(false);
        }
        else if(healthImage.fillAmount > 0 && !childPanel.activeInHierarchy)
        {
            childPanel.SetActive(true);
        }
    }
}
