using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] AidOptions options;
    [SerializeField] bool visualAidSlider;
    [SerializeField] bool audioAidSlider;
    [SerializeField, Range(0, 1f)] private float sliderValue;
    public bool currentValue { get; private set; }
    private Slider _slider;

    [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f;
    [SerializeField] private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;
    private Coroutine _animateSliderCoroutine;

    protected void OnValidate()
    {
        SetupToggleComponents();

        _slider.value = sliderValue;
    }

    private void SetupToggleComponents()
    {
        if (_slider != null) return;

        SetupSliderComponent();
    }

    private void SetupSliderComponent()
    {
        _slider = GetComponent<Slider>();

        if (_slider == null)
        {
            Debug.Log("No slider");
        }

        _slider.interactable = false;

        ColorBlock sliderColors = _slider.colors;
        sliderColors.disabledColor = Color.white;
        _slider.colors = sliderColors;
        _slider.transition = Selectable.Transition.None;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    private void Toggle()
    {
        SetStateAndStartAnimation(!currentValue);
    }

    private void SetStateAndStartAnimation(bool value)
    {
        currentValue = value;
        if (visualAidSlider) options.visualAids = value;
        if (audioAidSlider) options.audioAids = value;

        if (_animateSliderCoroutine != null) StopCoroutine(_animateSliderCoroutine);

        _animateSliderCoroutine = StartCoroutine(AnimateSlider());
    }

    private IEnumerator AnimateSlider()
    {
        float startValue = _slider.value;
        float endValue = currentValue ? 1 : 0;

        float time = 0;
        if (animationDuration > 0)
        {
            while  (time < animationDuration)
            {
                time += Time.deltaTime;
                float lerpFactor = slideEase.Evaluate(time / animationDuration);
                _slider.value = sliderValue = Mathf.Lerp(startValue, endValue, lerpFactor);
                backgroundImage.color = Color.Lerp(offColor, onColor, sliderValue);

                yield return null;
            }
        }

        _slider.value = endValue;
    }

    void Awake()
    {
        SetupToggleComponents();
    }

    void Update()
    {
        
    }
}
