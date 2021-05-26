using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using gm = GameManager;

[RequireComponent(typeof(Canvas))]
public class HealthBarManager : MonoBehaviour
{
    /// <summary>
    ///     There should only be one instance of HealthBarManager. This is that instance.
    /// </summary>
    public static HealthBarManager inst;

    [SerializeField] private Slider healthBarPrefab;

    /// <summary>
    ///     The positional offset of the health bar from the center of the defender it follows.
    /// </summary>
    [SerializeField] private Vector3 offset;

    /// <summary>
    ///     Maps each defender to the health bar that is supposed to follow them.
    /// </summary>
    private IDictionary<Health, Slider> _sliders;

    // Start is called before the first frame update
    private void Awake()
    {
        _sliders = new Dictionary<Health, Slider>();
        inst = this;
        Health.OnHealthCreated += AddHealthBar;
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var pair in _sliders)
        {
            var bar = pair.Value;
            var unit = pair.Key;

            bar.value = (float) unit.hp / unit.maxHp;
            bar.transform.position = gm.cam.WorldToScreenPoint(unit.transform.position + offset);
        }
    }

    /// <summary>
    ///     Adds a Health to the set of Health components who have health bars.
    /// </summary>
    /// <param name="def">The health to add.</param>
    private void AddHealthBar(Health def)
    {
        var bar = Instantiate(healthBarPrefab, transform);
        bar.transform.position = gm.cam.WorldToScreenPoint(def.transform.position + offset);
        _sliders.Add(def, bar);
        def.OnDeath += () =>
        {
            _sliders.Remove(def);
            Destroy(bar.gameObject);
        };
    }
}