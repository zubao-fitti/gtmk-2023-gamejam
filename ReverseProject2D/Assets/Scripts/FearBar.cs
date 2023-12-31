using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FearBar : MonoBehaviour
{

    CharacterMovement cMove; // To get character atributes

    public LevelController levelCtrl;

    float _fearRange;
    int _fearLimit = 100;
    int _fear = 0;
    float _counter = 0f;
    public float _fearDelay = 1.0f;
    public int _fearDefaultVal = 1;
    public Sprite[] _fearStates;
    public Image _fearBar;

    private bool _safeZone = true;
    private AudioSource crySound; // Referência ao AudioSource para o som de choro




    void Awake()
    {
        crySound = GetComponent<AudioSource>();
        cMove = GetComponent<CharacterMovement>();
        _fearRange = cMove._followRange;
        UpdateFear(0);
    }
    void Start()
    {
        if (GameController.instance) GameController.instance.SetGameStarted(true);
    }

    void Update()
    {
        //Debug.Log(_safeZone);
            int fearValue = 0; // Valor padrão de aumento do medo

            if (IsCollidingWithLightSource())
            {
                SetSafeZone(true);
                fearValue -= _fearDefaultVal * 2;
                if (crySound.isPlaying)
                {
                    crySound.Stop();
                }
            } 
            else if (IsCollidingWithGhost())
            {
                SetSafeZone(false);
                fearValue = 0;
                if (crySound.isPlaying)
                {
                    crySound.Stop();
                }
            }
            else {
                SetSafeZone(false);
                fearValue += _fearDefaultVal;
            }

            _counter += Time.deltaTime;

            if (_counter >= _fearDelay)
            {
                _counter = 0f;
                UpdateFear(fearValue);
            }

    }

     bool IsCollidingWithGhost()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ghost"))
            {
                return true;
            }
        }
        return false;
    }

    bool IsCollidingWithLightSource()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("LightSource"))
            {
                return true;
            }
        }
        return false;
    }


    void UpdateFear(int value)
    {
        _fear += value;

        if (_fear >= _fearLimit)
        {
            _fear = _fearLimit;
            Endgame();
        }

        if (_fear <= 0)
        {
            _fear = 0;
        }

        if (_fear <= 30)
        {
            _fearBar.sprite = _fearStates[0];

            // Parar o som de choro quando a barra de medo estiver abaixo de 30
            if (crySound.isPlaying)
            {
                crySound.Stop();
            }
        }
        else if (_fear <= 60)
            _fearBar.sprite = _fearStates[1];
        else if (_fear <= 90)
            _fearBar.sprite = _fearStates[2];
        else
            _fearBar.sprite = _fearStates[3];

        if (_fear > 30)
        {
            // Iniciar o som de choro quando a barra de medo estiver maior que 30
            if (!crySound.isPlaying)
            {
                crySound.Play();
            }
        }

        // Old bar with text
        // Debug.Log("O nivel de medo é " + _fear);
    }

    public bool GetSafeZone()
    {
        return _safeZone;
    }

    public void SetSafeZone(bool newValue)
    {
        _safeZone = newValue;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            this.Endgame();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            this.Endgame();
        }
    }

    void Endgame()
    {
        levelCtrl.Defeat();
        _fear = 0;
        // Parar o som de choro quando o jogo terminar
        if (crySound.isPlaying)
        {
            crySound.Stop();
        }
    }

}
