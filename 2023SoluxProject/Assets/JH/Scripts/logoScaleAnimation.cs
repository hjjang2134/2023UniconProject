using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogoScaleAnimation : MonoBehaviour
{
    public Image targetImage; // Inspector에서 UI Image를 할당

    public float startScale = 1.0f;
    public float endScale = 2.0f;
    public float animationDuration = 2.0f;

    private Vector3 originalScale; // 원래 크기 저장

    private void Start()
    {
        if (targetImage == null)
        {
            Debug.LogError("Target image not assigned. Please assign the UI Image in the Inspector.");
            return;
        }

        // 초기 스케일 설정
        targetImage.transform.localScale = new Vector3(startScale, startScale, 1.0f);

        // 원래 크기 저장
        originalScale = targetImage.transform.localScale;

        // 스케일 애니메이션 시작
        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < animationDuration)
        {
            float t = Mathf.SmoothStep(0.0f, 1.0f, elapsedTime / animationDuration);
            float scale = Mathf.Lerp(startScale, endScale, t);

            targetImage.transform.localScale = new Vector3(scale, scale, 1.0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 애니메이션 종료 후 마지막 스케일 설정
        targetImage.transform.localScale = new Vector3(endScale, endScale, 1.0f);

        // 원래 크기로 돌아오는 애니메이션 시작
        StartCoroutine(ReverseScaleAnimation());
    }

    private IEnumerator ReverseScaleAnimation()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < animationDuration)
        {
            float t = Mathf.SmoothStep(0.0f, 1.0f, elapsedTime / animationDuration);
            float scale = Mathf.Lerp(endScale, startScale, t);

            targetImage.transform.localScale = new Vector3(scale, scale, 1.0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 애니메이션 종료 후 원래 크기로 설정
        targetImage.transform.localScale = originalScale;
    }
}
