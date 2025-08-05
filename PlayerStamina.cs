using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrain = 20f;
    public float staminaRegen = 10f;
    public float regenDelay = 2f;

    [Header("UI")]
    public Image staminaFill;

    private float regenTimer = 0f;

    void Start()
    {
        currentStamina = maxStamina;
        UpdateStaminaUI();
    }

    void Update()
    {
        bool isTryingToRun = Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0;

        if (isTryingToRun && currentStamina > 0f)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
            regenTimer = 0f;
        }
        else
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= regenDelay && currentStamina < maxStamina)
            {
                currentStamina += staminaRegen * Time.deltaTime;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateStaminaUI();
    }

    void UpdateStaminaUI()
    {
        if (staminaFill != null)
            staminaFill.fillAmount = currentStamina / maxStamina;
    }

    public bool HasStamina()
    {
        return currentStamina > 0f;
    }
}