using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterStats : MonoBehaviour, IFighter, IComparable
{
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject healthFill;
    [SerializeField] private GameObject magicFill;

    [Header("Stats")]
    [SerializeField] private float health;
    [SerializeField] private float magic;
    [SerializeField] private float melee;
    [SerializeField] private float magicRange;
    [SerializeField] private float defense;
    [SerializeField] private float speed;
    [SerializeField] private float experience;

    private float startHealth;
    private float startMagic;

    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    // Barların boyutunu ayarlamak için kullandığımız referanslar
    private Transform healthTransform;
    private Transform magicTransform;

    private Vector2 healthScale;
    private Vector2 magicScale;

    private float xNewHealthScale;
    private float xNewMagicScale;

    private GameObject gameControllerObj;

    // IFighter arayüzü için özellik implementasyonu
    public float Health
    {
        get => health;
        set => health = value;
    }

    public float Magic
    {
        get => magic;
        set => magic = value;
    }

    public float Melee
    {
        get => melee;
        set => melee = value;
    }

    public float MagicRange
    {
        get => magicRange;
        set => magicRange = value;
    }

    public float Defense
    {
        get => defense;
        set => defense = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float Experience
    {
        get => experience;
        set => experience = value;
    }

    void Awake()
    {
        // Health/Magic bar ayarları
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        magicTransform = magicFill.GetComponent<RectTransform>();
        magicScale = magicFill.transform.localScale;

        gameControllerObj = GameObject.Find("GameControllerObject");

        // HeroManager varsa ordan statları yüklemek
        if (HeroManager.Instance != null)
        {
            LoadStatsFromManager();
        }
    }

    void Start()
    {
        startHealth = Health;
        startMagic = Magic;
    }

    private void LoadStatsFromManager()
    {
        // HeroManager'dan statları çek
        Health = HeroManager.Instance.GetStat("health");
        Magic = HeroManager.Instance.GetStat("magic");
        Melee = HeroManager.Instance.GetStat("melee");

        // magicRange HeroManager'da yoksa varsayılan bir değer de kullanılabilir
        // Ama orada "magicrange" diye stat eklemek isterseniz ekleyebilirsiniz.
        float magicRangeStat = HeroManager.Instance.GetStat("magicrange");
        if (magicRangeStat > 0)
            MagicRange = magicRangeStat;

        Defense = HeroManager.Instance.GetStat("defense");
        Speed = HeroManager.Instance.GetStat("speed");
        Experience = HeroManager.Instance.GetStat("experience");

        Debug.Log($"FighterStats statları yüklendi: " +
                  $"Health={Health}, Magic={Magic}, " +
                  $"Melee={Melee}, MagicRange={MagicRange}, " +
                  $"Defense={Defense}, Speed={Speed}, Experience={Experience}");
    }

    /// <summary>
    /// Hasar alma metodumuz.
    /// </summary>
    /// <param name="damage">Uygulanacak hasar miktarı</param>
    public void ReceiveDamage(float damage)
    {
        Health -= damage;

        // Hasar animasyonu için ufak bir gecikme
        Invoke(nameof(DamageAnimation), 0.5f);

        // Eğer sağlık 0 veya altına inerse, bu fighter ölü
        if (Health <= 0)
        {
            dead = true;
            gameObject.tag = "Dead";

            // Can barını veya bu objeyi yok ediyoruz (oyuna göre değişir)
            Destroy(healthFill);
            Destroy(gameObject);
        }
        else if (damage > 0)
        {
            xNewHealthScale = healthScale.x * (Health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
        }

        if (damage > 0 && gameControllerObj != null)
        {
            var gc = gameControllerObj.GetComponent<GameController>();
            gc.battleText.gameObject.SetActive(true);
            gc.battleText.text = damage.ToString();
        }

        // 2 saniye sonra bir sonraki tura geç
        Invoke(nameof(ContinueGame), 2f);
    }

    /// <summary>
    /// Decorator veya normal stat güncellemeleri. 
    /// HeroManager'a da iletiyoruz.
    /// </summary>
    public void UpdateStat(string statName, float amount)
    {
        switch (statName.ToLower())
        {
            case "speed":
                Speed += amount;
                break;
            case "health":
                Health += amount;
                break;
            case "melee":
                Melee += amount;
                break;
            case "magic":
                Magic += amount;
                break;
            case "defense":
                Defense += amount;
                break;
            case "experience":
                Experience += amount;
                break;
            case "magicrange":
                MagicRange += amount;
                break;
            default:
                Debug.LogWarning($"Bilinmeyen stat: {statName}");
                break;
        }

        // HeroManager'a da stat değişikliğini yansıt
        if (HeroManager.Instance != null)
        {
            HeroManager.Instance.UpdateStat(statName, amount);
        }
    }

    /// <summary>
    /// Magic barı güncellemek için kullanılır. (Büyü maliyeti vs.)
    /// </summary>
    public void updateMagicFill(float cost)
    {
        if (cost > 0)
        {
            Magic -= cost;
            xNewMagicScale = magicScale.x * (Magic / startMagic);
            magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);

            // HeroManager'a da iletiyoruz
            if (HeroManager.Instance != null)
            {
                HeroManager.Instance.UpdateStat("magic", -cost);
            }
        }
    }

    /// <summary>
    /// Ölü olup olmadığını geri döndürür.
    /// </summary>
    public bool GetDead()
    {
        return dead;
    }

    private void DamageAnimation()
    {
        animator.Play("Damage");
    }

    private void ContinueGame()
    {
        if (gameControllerObj != null)
        {
            gameControllerObj.GetComponent<GameController>().NextTurn();
        }
    }

    /// <summary>
    /// Sıradaki eylemin ne zaman gerçekleşeceğini hesaplayan basit bir mantık.
    /// </summary>
    public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / Speed);
    }

    /// <summary>
    /// Tur sıralamasında karşılaştırma yapmak için IComparable implementasyonu.
    /// </summary>
    public int CompareTo(object otherStats)
    {
        return nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
    }
}
