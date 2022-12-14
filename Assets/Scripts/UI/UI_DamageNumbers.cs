using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_DamageNumbers : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObjectPool damagePopupPool;
    [SerializeField] Canvas canvas;
    [SerializeField] Camera camera;
    [Space(8)]
    [SerializeField] float textLifetime;
    [SerializeField] float textFloatSpeed;

    RectTransform canvasTransform;

    private void Start()
    {
        EnemyInstance.OnDamageReceived.AddListener(ShowDamageForEnemy);
        PlayerHealth.OnPlayerHealthChanged.AddListener(ShowDamageForPlayer);
        canvasTransform = canvas.GetComponent<RectTransform>();
    }

    void ShowDamageForEnemy(float damage, EnemyInstance enemy)
    {
        GameObject popup = damagePopupPool.Pool.Get();
        popup.transform.SetParent(canvas.transform);
        popup.GetComponent<TextMeshProUGUI>().text = $"-{damage.ToString("0.0")}";

        Vector2 pos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, camera.WorldToScreenPoint(enemy.transform.position), null, out pos);
        popup.GetComponent<RectTransform>().anchoredPosition = pos;
        StartCoroutine(TextFloat(popup.GetComponent<RectTransform>()));
    }

    void ShowDamageForPlayer(float damage)
    {
        GameObject popup = damagePopupPool.Pool.Get();
        popup.transform.SetParent(canvas.transform);
        popup.GetComponent<TextMeshProUGUI>().text = $"-{damage.ToString("0.0")}";

        Vector2 pos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, camera.WorldToScreenPoint(player.position), null, out pos);
        popup.GetComponent<RectTransform>().anchoredPosition = pos;
        StartCoroutine(TextFloat(popup.GetComponent<RectTransform>()));
    }

    IEnumerator TextFloat(RectTransform text)
    {
        float time = 0.0f;
        while(time < textLifetime)
        {
            text.anchoredPosition += Vector2.up * textFloatSpeed;
            time += Time.deltaTime;
            yield return null;
        }
        damagePopupPool.Pool.Release(text.gameObject);
        yield return null;
    }
}
