// ©2020 Maxime Boudreault. All Rights Reserved.

using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private float maxPower = 100;
    [SerializeField] private Light hope = null;
    [SerializeField] private StarGenerator stars = null;
    [SerializeField] private Ground ground = null;
    [SerializeField] private AudioSource propelSound = null;

    [Header("Narration")] [SerializeField] private Narration narration = null;
    [SerializeField] private int starsToFirstAct = 5;
    [SerializeField] private Planet[] planets = null;
    [SerializeField] private Colorizer view = null;

    [Header("UI")]
    [SerializeField] private HeightDisplay heightDisplay = null;
    [SerializeField] private CanvasGroup ui = null;
    [SerializeField] private AngleBar angleBar = null;
    [SerializeField] private PowerGauge powerGauge = null;

    private float _angle = 0;
    private float _power = 0;
    private int _nextPlanetAct = 0;
    private bool _firstRun = true;

    void Start()
    {
        ground.OnCollision += ShowFirstAct;
        ui.alpha = 0;
        narration.BeginShowNextSegment(PrepareBasicPropulsion);
        PreparePlanets();
        heightDisplay.SetGoal(planets[_nextPlanetAct].transform);
        powerGauge.enabled = false;
    }

    void ShowFirstAct()
    {
        if (stars.Count >= starsToFirstAct)
        {
            narration.BeginShowNextSegment(PrepareBasicPropulsion);
            hope.FreezeAtStart();
            ground.OnCollision -= ShowFirstAct;
            ground.OnCollision += PrepareBasicPropulsion;
        }
        else
        {
            PrepareBasicPropulsion();
        }
    }

    void PreparePlanets()
    {
        int lastPlanet = planets.Length - 1;
        for (int i = 0; i < lastPlanet; i++)
        {
            planets[i].OnReach += ShowPlanetAct;
        }
        planets[lastPlanet].OnReach += ShowEnding;
    }

    void ShowPlanetAct()
    {
        planets[_nextPlanetAct].OnReach -= ShowPlanetAct;
        planets[_nextPlanetAct].OnReach += RevealPropulsionUI;
        
        stars.EndSpawning();
        narration.BeginShowNextSegment(RevealPropulsionUI);
        _nextPlanetAct++;
        heightDisplay.SetGoal(planets[_nextPlanetAct].transform);
        stars.Boost();
    }

    void ShowEnding()
    {
        planets[_nextPlanetAct].OnReach -= ShowPlanetAct;
        stars.EndSpawning();
        narration.BeginShowEnding();
    }

    void PrepareBasicPropulsion()
    {
        if (_firstRun)
        {
            _firstRun = false;
            stars.Boost();
        }
        hope.FreezeAtStart();
        RevealPropulsionUI();
        stars.ChargeStars();
        foreach (var planet in planets) { planet.Charged = true; }
    }

    void RevealPropulsionUI()
    {
        StartCoroutine(FadeUI(1));
        angleBar.Launch(SetAngle);
        powerGauge.enabled = false;
        powerGauge.Reset();
        stars.EndSpawning();
    }

    void SetAngle(float radian)
    {
        _angle = radian;
        powerGauge.gameObject.SetActive(true);
        powerGauge.Launch(SetPower);
    }

    void SetPower(float ratio)
    {
        _power = ratio * maxPower;
        PropelLight();
    }

    void PropelLight()
    {
        propelSound.Play();
        hope.Propel(_power, _angle);
        stars.BeginSpawning();
        heightDisplay.Show();
        StartCoroutine(FadeUI(0));
    }

    IEnumerator FadeUI(float alpha)
    {
        float t = 0;
        float baseAlpha = ui.alpha;
        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime;
            ui.alpha = Mathf.Lerp(baseAlpha, alpha, t);
        }

        ui.alpha = alpha;
    }

    void Update()
    {
        float normalizedDistanceToSun = hope.transform.position.y / planets[planets.Length - 1].transform.position.y;
        view.SetProgress(normalizedDistanceToSun);
        
        #if !UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(0);
        }
        #endif
    }
}