using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class UI_EnemyStatsWindow : MonoBehaviour
{
    GameObjectPool parentPool;
    EnemyInstance keptEnemy;
    EnemyData cachedData;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI lvlText;
    [SerializeField] Slider hpBarSlider;

    RectTransform rectTransform;
    RectTransform canvasTransform;
    Camera cam;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Init(Camera _cam, EnemyInstance _keptEnemy)
    {
        rectTransform = GetComponent<RectTransform>();
        cam = _cam;
        canvasTransform = rectTransform.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        keptEnemy = _keptEnemy;
        cachedData = _keptEnemy.GetData();
        nameText.text = keptEnemy.GetData().enemyName;
        lvlText.text = "Lvl. " + keptEnemy.enemyLvl.ToString();
    }

    public void SetParentPool(GameObjectPool pool) => parentPool = pool;

    public void UpdateHP()
    {
        hpBarSlider.value = Mathf.Lerp(hpBarSlider.value, keptEnemy.enemyCurrentHP / keptEnemy.enemyMaxHP, Time.deltaTime * 10f);
    }

    public void UpdatePosition()
    {
        Vector2 pos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, cam.WorldToScreenPoint(keptEnemy.transform.position + Vector3.up * cachedData.enemyUIStatsOffset), null, out pos);
        rectTransform.anchoredPosition = pos;
    }

    public void Update()
    {
        if (keptEnemy?.GetActive() ?? false)
        {
            UpdateHP();
            UpdatePosition();
        }
        else
        {
            parentPool.Pool.Release(gameObject);
        }
    }
}
