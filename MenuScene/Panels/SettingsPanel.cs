using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanel : ViewPanel
{
    [Header("Buttons")]
    [SerializeField]
    private Button _volume;
    [SerializeField]
    private Button _music;
    [SerializeField]
    private Button _tutorial;
    [SerializeField]
    private Button[] _tracks;
    [SerializeField]
    private Button _language;
    [SerializeField]
    private Button _back;
    [Space(10)]
    [Header("Images")]
    [SerializeField]
    private SpriteRenderer[] _trackImages;
    [Space(10)]
    [Header("Mixer")]
    [SerializeField]
    private AudioMixer _mixer;

    private readonly Color DisabledColor = new Color(0.47f, 0.47f, 0.47f);
    private readonly Color ActiveColor = new Color(1f, 1f, 1f);

    private int _currentLanguage = 0;

    private const short LanguagesLength = 1;

    private System.Action _languageChanged;

    public void Initialization(System.Action languageChanged)
    {
        this.enabled = true;
        _languageChanged = languageChanged;
    }

    private void OnEnable()
    {
        LanguageSystemStart();
        MusicSystemStart();
        TrackSystemStart();
        _music.onClick.AddListener(MusicButton);
        _volume.onClick.AddListener(VolumeButton);
        _language.onClick.AddListener(LanguageButton);
        _tutorial.onClick.AddListener(() => SceneManager.LoadScene(GameConstants.TutorialScene));
        _back.onClick.AddListener(() => Close(MenuPanelType.MainMenu));
        for (int i = 0; i < _tracks.Length - 1; i++) _tracks[i].onClick.AddListener(delegate { TrackChooser(i); });
        _tracks[_tracks.Length-1].onClick.AddListener(delegate { TrackChooser(-1); });
    }

    public void TrackChooser(int x)
    {
        PlayerPrefs.SetInt("Track", x);
        if (x == -1) x = _trackImages.Length - 1;
        for (int i = 0; i < _trackImages.Length; i++)
        {
            _trackImages[i].color = DisabledColor;
        }
        _trackImages[x].color = ActiveColor;
    }

    private void LanguageButton()
    {
        _language.transform.GetChild(_currentLanguage).gameObject.SetActive(false);
        _currentLanguage = ++_currentLanguage > LanguagesLength ? 0 : _currentLanguage;  
        Player.Language = _currentLanguage;
        _language.transform.GetChild(_currentLanguage).gameObject.SetActive(true);
        _languageChanged?.Invoke();
    }

    public void MusicButton()
    {
        bool flag = Player.mutedmus;
        int imageIndex = flag ? 1 : 0;
        Player.mutedmus = !flag;
        ActivateOrDisableSound(_music, imageIndex, "Music");
        _tracks[0].transform.root.gameObject.SetActive(flag);
    }
    public void VolumeButton()
    {
        bool flag = Player.muted;
        int imageIndex = flag ? 1 : 0;
        Player.muted = !flag;
        ActivateOrDisableSound(_volume, imageIndex, "Sound");
    }

    private void ActivateOrDisableSound(Button button, int imageIndex, string mixerName)
    {
        float soundValue = imageIndex == 0 ? -80f : 0f;
        button.transform.GetChild(1 - imageIndex).gameObject.SetActive(true);
        button.transform.GetChild(imageIndex).gameObject.SetActive(false);
        _mixer.SetFloat(mixerName, soundValue);
    }

    private void LanguageSystemStart()
    {
        int length = _language.transform.childCount;
        for (int i=0; i< length; i++)
            _language.transform.GetChild(i).gameObject.SetActive(false);
        _currentLanguage = Player.Language;
        _language.transform.GetChild(_currentLanguage).gameObject.SetActive(true);
    }

    private void MusicSystemStart()
    {
        Player.mutedmus = !Player.mutedmus;
        MusicButton();
    }

    private void TrackSystemStart()
    {
        int x = -1;
        if (PlayerPrefs.HasKey("Track"))
        {
            x = PlayerPrefs.GetInt("Track");
        }
        TrackChooser(x);
    }

    private void OnDisable()
    {
        _music.onClick.RemoveAllListeners();
        _volume.onClick.RemoveAllListeners();
        _language.onClick.RemoveAllListeners();
        _tutorial.onClick.RemoveAllListeners();
        _back.onClick.RemoveAllListeners();
        for (int i = 0; i < _tracks.Length; i++) _tracks[i].onClick.RemoveAllListeners();
    }

    public override MenuPanelType PanelType()
    {
        return MenuPanelType.Settings;
    }

}
