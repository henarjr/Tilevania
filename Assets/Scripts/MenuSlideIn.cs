using System.Collections;
using UnityEngine;

public class MenuSlideIn : MonoBehaviour
{
    [Header("UI Elements (By Order)")]
    [Tooltip("اسحبي هنا التايتل أولاً، ثم زرار ستارت، ثم كويت")]
    [SerializeField] private RectTransform[] menuElements;

    [Header("Animation Settings")]
    [SerializeField] private float slideDuration = 0.45f;       // سرعة دخول العنصر
    [SerializeField] private float delayBetweenElements = 0.18f; // الفارق الزمني بين كل عنصر والتاني
    [SerializeField] private float startOffsetX = -900f;        // مسافة البداية بره الشاشة شمال

    private Vector2[] targetPositions;

    void Awake()
    {
        if (menuElements == null || menuElements.Length == 0) return;

        targetPositions = new Vector2[menuElements.Length];

        for (int i = 0; i < menuElements.Length; i++)
        {
            if (menuElements[i] != null)
            {
                // حفظ المكان النهائي للعنصر
                targetPositions[i] = menuElements[i].anchoredPosition;

                // نقله مؤقتاً لخارج الشاشة على الشمال
                menuElements[i].anchoredPosition = new Vector2(targetPositions[i].x + startOffsetX, targetPositions[i].y);
            }
        }
    }

    void Start()
    {
        StartCoroutine(AnimateSequence());
    }

    IEnumerator AnimateSequence()
    {
        for (int i = 0; i < menuElements.Length; i++)
        {
            if (menuElements[i] != null)
            {
                StartCoroutine(SlideElement(menuElements[i], targetPositions[i]));
                yield return new WaitForSeconds(delayBetweenElements);
            }
        }
    }

    IEnumerator SlideElement(RectTransform rect, Vector2 targetPos)
    {
        float elapsed = 0f;
        Vector2 startPos = rect.anchoredPosition;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;

            // انحناء ناعم مع ارتداد ارتدادي خفيف (Overshoot)
            float ease = 1f + 2.70158f * Mathf.Pow(t - 1f, 3f) + 1.70158f * Mathf.Pow(t - 1f, 2f);

            rect.anchoredPosition = Vector2.LerpUnclamped(startPos, targetPos, ease);
            yield return null;
        }

        rect.anchoredPosition = targetPos;
    }
}
