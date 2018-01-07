using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using WW4.EventSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text _grabText;
    [SerializeField] private Text _interactText;
    [SerializeField] private Text _throwText;

    private Coroutine _grabTooltip;
    private Coroutine _interactTooltip;
    private Coroutine _throwTooltip;

    private float _lastCanGrabEvent;
    private float _lastCanInteractEvent;

    private RigidbodyFirstPersonControllerWorkaround _controller;

    public float TooltipDeactivationDelay;

    private void Start()
    {
        MessageSystem.CanInteractEventHandler.AddListener(x => ShowInteractTooltip());
        MessageSystem.CanGrabEventHandler.AddListener((x, y) => ShowGrabTooltip());
        MessageSystem.EntityGrabbedEventHandler.AddListener((x, y) => ShowThrowTooltip());
        _controller = GetComponent<RigidbodyFirstPersonControllerWorkaround>();
    }

    private void ShowGrabTooltip()
    {
        print("showing interact tooltip..");
        _lastCanGrabEvent = Time.time;
        if (_grabTooltip == null)
        {
            _grabText.gameObject.SetActive(true);
            _grabTooltip = StartCoroutine(DeactivateTooltipAfterDelay(
                () =>
                {
                    _grabText.gameObject.SetActive(false);
                    _grabTooltip = null;
                },
                () => _lastCanGrabEvent + TooltipDeactivationDelay > Time.time));
        }
    }

    private void ShowInteractTooltip()
    {
        print("showing grab tooltip..");
        _lastCanInteractEvent = Time.time;
        if (_interactTooltip == null)
        {
            _interactText.gameObject.SetActive(true);
            _interactTooltip = StartCoroutine(DeactivateTooltipAfterDelay(
                () =>
                {
                    _interactText.gameObject.SetActive(false);
                    _interactTooltip = null;
                },
                () => _lastCanInteractEvent + TooltipDeactivationDelay > Time.time));
        }
    }

    private void ShowThrowTooltip()
    {
        if (_throwTooltip == null)
        {
            _throwText.gameObject.SetActive(true);
            _throwTooltip = StartCoroutine(DeactivateTooltipAfterDelay(
                () =>
                {
                    _throwText.gameObject.SetActive(false);
                    _throwTooltip = null;
                },
                () => _controller.IsHoldingObject));
        }
    }

    private IEnumerator DeactivateTooltipAfterDelay(Action action, Func<bool> predicate)
    {
        while (predicate())
            yield return null;
        action();
    }
}