using UnityEngine;
public class Movement : MonoBehaviour
{
    float ScreenHalfSize;
    Vector2 speedMM = new Vector2(4.5f, 6f);
    float RocketTime,FoodCDStat=1f;
    public static int hp = 3;
    private AudioSource audoisas;
    public AudioClip clip_hiding, clip_eating, shieldSound, clip_damaged, clip_damaged_small, RocketSound, respawnclip;
    public float skillTime = 0.7f;
    public static float ImmTime = 1f;
    bool shield = false;
    public static bool Rocket = false, HpChanged = false;
    private Animator animator;
    bool PuckActive = false, abilityused = false, TimerSkill = false, Respawn =false, _spartaDeath = false;
    int skin, astraldamage;
    public static bool RocketAnim = false, RocketDown =false, Immortal = false;
    float ShieldDuration, shieldoffTime, RocketDuration, RocketAnimTime, MagSkillTimer = 5f, TimerSkillTimer = 5f;
    public float Button_Scale_x, Button_center_x, Button_center_y, Pause_x, Pause_y, PauseScale_x, PauseScale_y;
    int shieldcounter = 0, ability, _hpWasSparta;
    public static int Hplost = 0;
    AchInc Ach;
    private void Awake()
    {
        audoisas = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        hp = 3;
        HpChanged = true;
    }
    void Start()
    {   
        ScreenHalfSize = Camera.main.aspect * Camera.main.orthographicSize;
        if (ScreenHalfSize > 2.44f) { 
            ScreenHalfSize = 2.44f;
        }
        shieldoffTime = Player.ShieldLevel * 5f + 5f;
        if (skin == 10) shieldoffTime += 10f;
        RocketDuration = Player.RocketLevel * 1f + 5f;
        RocketAnim = false;
        Rocket = false;
        skin = Player.Skin;
        if (skin == 19)
        {
            RocketDuration *= 2;
            shieldoffTime *= 2;
        }
        ability = Player.Ability;
        Ach = gameObject.AddComponent<AchInc>();
        astraldamage = 200 - 25 * Player.AbilityLevel[2];
        Immortal = false;
        ImmTime = 1f;
        Respawn = false;
        Hplost = 0;
        _spartaDeath = false;
    }

    private void FixedUpdate()
    {
#if UNITY_IOS || UNITY_ANDROID
        if (!Gameover.gameover)
        {
            if (skin == 8 || skin == 13) {
                if (!Gameover.PuckSkill && !shield && !Rocket)   
                {
                    if (Time.timeSinceLevelLoad > MagSkillTimer && skin == 8)
                    {
                        MagSkillTimer = Time.timeSinceLevelLoad + 5f;
                        int x = Random.Range(1, 1000);
                        if (x % 20 == 0)
                        {
                            ShieldAct();
                        }
                        else if (x % 23 == 0)
                        {
                            RocketAct();
                        }
                    }
                    else if (Time.timeSinceLevelLoad > TimerSkillTimer && skin == 13)
                    {
                        TimerSkillTimer = Time.timeSinceLevelLoad + 5f;
                        int x = Random.Range(1, 1000);
                        if (x % 20 == 0)
                        {
                            Time.timeScale = 0.6f;
                            TimerSkill = true;
                        }
                    }
                }
            }
            if (Time.timeSinceLevelLoad < TimerSkillTimer && Time.timeSinceLevelLoad > TimerSkillTimer - 3.8f && TimerSkill)
            {
                Time.timeScale = 1;
                TimerSkill = false;
            }
            if (PuckActive && !Gameover.PuckTimer)
            {
                audoisas.clip = clip_hiding;
                audoisas.Play();
                animator.SetTrigger("PuckOff");
                PuckActive = false;
            }
            else if (Gameover.PuckSkill && !PuckActive && Gameover.PuckTimer)
            {
                audoisas.clip = clip_hiding;
                audoisas.Play();
                animator.SetTrigger("PuckOn");
                PuckActive = true;
            }
            if (RocketAnim && Rocket)
            {
                if (Time.timeSinceLevelLoad > RocketAnimTime && Rocket && !RocketDown)
                {
                    RocketAnim = false;
                    Time.timeScale = 1.5f;
                }
                if (Time.timeSinceLevelLoad > RocketAnimTime && RocketDown && RocketAnim)
                {
                    RocketAnim = false;
                }
            }   
            else if (!RocketAnim && Rocket && Time.timeSinceLevelLoad > RocketTime - 2.7f && Time.timeSinceLevelLoad < RocketTime - 2.4f)
            {
                RocketAnim = true;
                RocketAnimTime = Time.timeSinceLevelLoad + 2.5f;
                animator.SetTrigger("RocketOff");
            }
            else if (!RocketAnim && Rocket && Time.timeSinceLevelLoad > RocketTime - 2.9f && Time.timeSinceLevelLoad < RocketTime - 2.7f)
            {
                Time.timeScale = 1;
                RocketDown = true;
 
            }
            if (Immortal && Time.timeSinceLevelLoad > ImmTime)
            {
                Immortal = false;
            }
            if (Gameover.AbilityTimer && !abilityused)
            {
                abilityused = true;
                if (ability == 1)
                {
                    float pos = transform.position.x;
                    transform.Translate(-2 * pos, 0, 0);
                }
                else if (ability == 3) {
                    int i = 4, max = 11;
                    if (!((skin < 3 || skin > 6) && skin != 14))
                    {
                        i = 5;
                        max = 12;
                    }
                    for (; i < max; i++)
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
                    }
                }
            }
            else if (!Gameover.AbilityTimer && abilityused)
            {
                abilityused = false;
                if (ability == 3)
                {
                    int i = 4, max = 11;
                    if (!((skin < 3 || skin > 6) && skin != 14))
                    {
                        i = 5;
                        max = 12;
                    }
                    for (; i < max; i++)
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    }
                }
            }

            if (Input.touchCount > 0 && !RocketAnim)
            {
                Touch Mytouch = Input.GetTouch(0);
                if (Mytouch.phase == TouchPhase.Began || Mytouch.phase == TouchPhase.Stationary || Mytouch.phase == TouchPhase.Moved)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Mytouch.position);
                    {
                        if (!(Mathf.Abs(ray.origin.x - Pause_x) < PauseScale_x * 1.5f && Mathf.Abs(ray.origin.y - Pause_y) < PauseScale_y * 1.5f))
                        {
                            if (ray.origin.x < 0)
                            {
                                float speed = Mathf.Lerp(speedMM.x, speedMM.y, Difficulty.GetDifficultyPercent());
                                transform.Translate(-Vector2.right * speed * Time.deltaTime);

                                animator.SetFloat("Speed", -5f);
                            }
                            else if (ray.origin.x >= 0 && (Mathf.Abs(ray.origin.x - Button_center_x) >= Button_Scale_x / 2f || Mathf.Abs(ray.origin.y - Button_center_y) >= Button_Scale_x / 2f))
                            {
                                float speed = Mathf.Lerp(speedMM.x, speedMM.y, Difficulty.GetDifficultyPercent());
                                if (skin != 9)
                                {
                                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                                }
                                else 
                                {
                                    transform.Translate(Vector2.right * speed * 0.92f * Time.deltaTime);
                                }
                                animator.SetFloat("Speed", 5f);
                            }
                        }
                    }
                }
            }
            else
            {
                animator.SetFloat("Speed", 0.1f);
                
            }
            if (transform.position.x < -ScreenHalfSize + transform.localScale.x * 1.25f)
            {
                transform.position = new Vector2(-ScreenHalfSize + transform.localScale.x * 1.25f, transform.position.y);
            }
            if (transform.position.x > ScreenHalfSize - transform.localScale.x * 1.25f)
            {
                transform.position = new Vector2(ScreenHalfSize - transform.localScale.x * 1.25f, transform.position.y);
            }
           
            if (Rocket && Time.timeSinceLevelLoad > RocketTime + 0.3f)
            { 
                RocketAnim = false;
                Rocket = false;
                RocketDown = false;
                transform.position = new Vector2(transform.position.x, -0.866f);
                if (_spartaDeath) SpartaDeath();
            }
            if (shield && Time.timeSinceLevelLoad > ShieldDuration)
            {
                shield = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (shield && Time.timeSinceLevelLoad > ShieldDuration - 3.1f)
            {
                transform.GetChild(0).gameObject.GetComponent<Animation>().Play();
            }
        }
        
#endif
    }
    bool NinjaSkill()
    {  
        int x = Random.Range(1, 1000);
        if (x % 7 == 0 || x == 1)
        {
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
            return true;
        }
        return false;
    }
    bool GhostSkill()
    {
        int x = Random.Range(1, 1000);
        if (x % 20 == 0)
        {
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
            return true;
        }
        return false;
    }
    void DamagedSmall()
    {  
        
        if (!shield && !Immortal)
        {
            if (!(abilityused && ability == 3))
            {
                if (skin >= 3 && skin <= 6)
                {
                    if (NinjaSkill())
                    {
                        return;
                    }
                }
                else if (skin == 11)
                {
                    audoisas.clip = clip_damaged_small;
                    audoisas.Play();
                    return;
                }
                else if (skin == 15 && Spawner.DayTime == 0) { 
                    DamagedBig(); 
                    return;
                }
                if (hp > 1)
                {
                    hp--;
                    Hplost++;
                    audoisas.clip = clip_damaged_small;
                    audoisas.Play();
                    animator.SetTrigger("Damage");
                }
                else if(!Respawn && hp == 1)
                {
                    if (skin != 16)
                    {
                        Respawn = true;
                        hp--;
                        Hplost++;
                    }
                    else
                    {
                        _spartaDeath = true;
                        RocketAct();
                        _hpWasSparta = hp;
                    }
                }
                else if(Respawn && hp <=1)
                {
                    if (skin != 16)
                    {
                        hp = -1;
                        Hplost++;
                        Destroy(gameObject);
                    }
                    else
                    {
                        _spartaDeath = true;
                        RocketAct();
                        _hpWasSparta = hp;
                    }
                }
                HpChanged = true;
            }
            else if (abilityused && ability == 3)
            {
                if (skin != 14)
                {
                    Gameover.antiscore += astraldamage;
                    Gameover.antiscoreused = true;
                }
            }
        
        }
        else if(!Immortal)
        {
            if (skin >= 3 && skin <= 6)
            {
                if (NinjaSkill())
                {
                    return;
                }
            }
            audoisas.clip = clip_damaged_small;
            audoisas.Play();
            if (skin == 10)
            {
                shieldcounter++;
                if (shieldcounter < 2) return;
                else shieldcounter = 0;
            }
            shield = false;
            transform.GetChild(0).gameObject.SetActive(false);  
        }
    }

    void DamagedBig()
    {
        if (!shield && !Immortal)
        {
            if (!(abilityused && ability == 3))
            {
                if (hp > 2)
                {
                    if (skin >= 3 && skin <= 6)
                    {
                        if (NinjaSkill())
                        {
                            return;
                        }
                    }
                    else if (skin == 14)
                    {
                        if (GhostSkill())
                        {
                            return;
                        }
                    }
                    if (skin != 9 && skin != 15)
                    {
                        hp--;
                        hp--;
                        Hplost+=2;
                    }
                    else {
                        if ((skin == 15 && Spawner.DayTime == 1) || skin == 9)
                        {
                            hp--;
                            Hplost++;
                        }
                        else
                        {
                            hp -= 2;
                            Hplost += 2;
                        }
                    }
                    audoisas.clip = clip_damaged;
                    audoisas.Play();
                    animator.SetTrigger("Damage");
                }
                else if(hp <= 2 && hp > 0)
                {
                    if ((skin == 9 || skin == 15) && hp == 2)
                    {
                        if (skin == 9 || Spawner.DayTime == 1)
                        {
                            audoisas.clip = clip_damaged;
                            audoisas.Play();
                            hp--;
                            Hplost++;
                            HpChanged = true;
                            return;
                        }
                    }
                    if (!Respawn)
                    {
                        if (skin != 16)
                        {
                            Respawn = true;
                            if (hp == 2) Hplost += 2;
                            else Hplost++;
                            hp = 0;
                        }
                        else
                        {
                            _spartaDeath = true;
                            RocketAct();
                            _hpWasSparta = hp;
                        }
                    }
                    else if (Respawn)
                    {
                        if (skin != 16)
                        {
                            if (hp == 2) Hplost += 2;
                            else Hplost++;
                            hp = -1;
                            Destroy(gameObject);
                        }
                        else
                        {
                            _spartaDeath = true;
                            RocketAct();
                            _hpWasSparta = hp;
                        }
                    }
                }
                HpChanged = true;
            }
            else if (abilityused && ability == 3)
            {
                if (skin != 14)
                {
                    Gameover.antiscore += astraldamage;
                    Gameover.antiscoreused = true;
                }
            }
        }
        else if(!Immortal)
        {
            if (skin >= 3 && skin <= 6)
            {
                if (NinjaSkill())
                {
                    return;
                }
            }
            audoisas.clip = clip_damaged;
            audoisas.Play();
            if (skin == 10)
            {
                shieldcounter++;
                if (shieldcounter < 2) return;
                else shieldcounter = 0;
            }
            shield = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    void RocketAct()
    {
        if (skin == 19) { DamagedSmall();}
        Rocket = true;
        RocketAnim = true;
        RocketAnimTime = Time.timeSinceLevelLoad + 1.95f;
        RocketTime = Time.timeSinceLevelLoad + RocketDuration * 1.5f;
        audoisas.clip = RocketSound;
        audoisas.Play();
        animator.SetTrigger("RocketOn");
    }

    void ShieldAct()
    {
        if (skin == 19) { DamagedSmall(); }
        ShieldDuration = Time.timeSinceLevelLoad + shieldoffTime;
        shield = true;
        audoisas.clip = shieldSound;
        audoisas.Play();
        transform.GetChild(0).gameObject.SetActive(true);
        Animation anim = transform.GetChild(0).gameObject.GetComponent<Animation>();
        anim["shieldEnd"].normalizedTime = 1f;
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    void SpartaDeath()
    {
        if (_hpWasSparta >= hp)
        {
            if (!Respawn)
            {
                Respawn = true;
                hp = 0;
                HpChanged = true;
            }
            else {
                hp = -1;
                Destroy(gameObject);
                HpChanged = true;
            }
        }
        else
        {
            if (_hpWasSparta == 1) DamagedSmall();
            else DamagedBig();
        }
    }

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (!Gameover.gameover)
        {

            if (!Gameover.PuckSkill && !Rocket)
            {

                if (triggerCollider.CompareTag("Botl"))
                {
                    DamagedSmall();
                    Player.Objects[3]++;
                    Ach.UpdateIncremental(5);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Can"))
                {

                    DamagedSmall();
                    Player.Objects[4]++;
                    Ach.UpdateIncremental(6);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Nail"))
                {
                    DamagedSmall();
                    Player.Objects[5]++;
                    Ach.UpdateIncremental(7);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Jar"))
                {
                    DamagedSmall();
                    Player.Objects[6]++;
                    Ach.UpdateIncremental(8);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Mask"))
                {
                    DamagedSmall();
                    Player.Objects[7]++;
                    Ach.UpdateIncremental(9);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Paket"))
                {
                    DamagedSmall();
                    Player.Objects[8]++;
                    Ach.UpdateIncremental(10);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Bung"))
                {
                    DamagedSmall();
                    Player.Objects[9]++;
                    Ach.UpdateIncremental(11);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Stekl"))
                {       
                    Player.Objects[10]++;
                    Ach.UpdateIncremental(12);
                    Ach.UpdateIncremental(19);
                    if (skin != 12)
                    {
                        DamagedSmall();
                    }
                }
                else if (triggerCollider.CompareTag("Bo4ka"))
                {
                    DamagedBig();
                    Player.Objects[11]++;
                    Ach.UpdateIncremental(17);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Pila"))
                {
                    DamagedBig();
                    Player.Objects[12]++;
                    Ach.UpdateIncremental(16);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Kanistra"))
                {
                    DamagedBig();
                    Player.Objects[13]++;
                    Ach.UpdateIncremental(13);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Box"))
                {
                    DamagedBig();
                    Player.Objects[14]++;
                    Ach.UpdateIncremental(14);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Idro"))
                {
                    DamagedBig();
                    Player.Objects[15]++;
                    Ach.UpdateIncremental(18);
                    Ach.UpdateIncremental(19);
                }
                else if (triggerCollider.CompareTag("Anhor"))
                {
                    DamagedBig();
                    Player.Objects[16]++;
                    Ach.UpdateIncremental(15);
                    Ach.UpdateIncremental(19);
                }
            }
            else if (Gameover.PuckSkill && !shield)
            {
                Player.Objects[17]++;
                Ach.UpdateIncremental(2);
                if (triggerCollider.CompareTag("Anhor") || triggerCollider.CompareTag("Idro") || triggerCollider.CompareTag("Kanistra") || triggerCollider.CompareTag("Pila") || triggerCollider.CompareTag("Bo4ka"))
                {
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(true);
                }
            }
            if (triggerCollider.CompareTag("Food") && !Gameover.PuckSkill && skin != 11)
            {
                if (hp < 4 && skin != 7)
                {
                    hp++;
                }
                else if (hp < 4 && skin == 7)
                {
                    hp++;
                    hp++;
                }
                else if (skin == 7 && hp == 4)
                {
                    hp++;
                }
                audoisas.clip = clip_eating;
                audoisas.pitch.Equals(Random.Range(0.9f, 1.1f));
                audoisas.Play();
                audoisas.pitch.Equals(1f);
                if (Time.timeSinceLevelLoad > FoodCDStat)
                {
                    Player.Objects[0]++;
                    FoodCDStat = Time.timeSinceLevelLoad + 5f;
                    Ach.UpdateIncremental(1);
                }
                HpChanged = true;
            }
            if (triggerCollider.CompareTag("Shield") && !Gameover.PuckSkill && skin != 11 ) {
                ShieldAct();
                Player.Objects[1]++;
                Ach.UpdateIncremental(4);
            }
            if (triggerCollider.CompareTag("Rocket") && !Gameover.PuckSkill && skin != 11 && !Rocket)
            {
                RocketAct();
                Ach.UpdateIncremental(3);
                Player.Objects[2]++;
            }
        }
    }
}
