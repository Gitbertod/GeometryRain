using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Movement : MonoBehaviour
{
    public float newBestTime = 0; 
    float t = 0;
    float tScaleSlide, tScaleCircle = 0;
    float playAgainTime =0;
    float timeVolMenu = 0;

    public int bestScore = 0;
    int score = 0;
    float speed = 4;
    float movement = 0;
    float end = 1.55f;
    float time2 = 0;
    bool anim, animBestScore = false;

    [SerializeField] bool volMenu = false;

    public RectTransform volPanel;
    public RectTransform playBtn, playAgainRectTransform, scoreRectTransform, newBestScoreRectT,logo;
    public Text scoreText, newBestScoreText, best;
    public GameObject slide, circle, enemySpawner, playButtonGO,instructionButton,signature,gameTitle,volPanelGO,btnConfigGO;
    
    public AudioClip clip, whiteBox, greenBox;
    public AudioMixer myMixer;

  

    //VARIABLES PARA ANIMACION DE LAS INSTRUCCIONES-----------------------------------
    
    float tI,tI2 = 0;
    public float colorControl = 0;
    Color myColor;
    public Text myInstructions,myInstruction2;
    public RectTransform instructionRectT,instructionRectT2;
    public float  timeInstruction = 0;

    //---------------------------------------------------------------------------------
    enum PlayerStates {Oculto,Instructions, Gameplay, GameOver,Reset,Config }
    PlayerStates playerStates;
 
    [Range(0,1)]public float time = 0;
    private void Awake()
    {
        score = 0;
        scoreText.text = "" + score;
       
    }
    void Start()
    {
        playerStates = PlayerStates.Oculto;
       
    }

    void Update()
    {
        switch (playerStates)
        {
            case PlayerStates.Oculto:
                
                Oculto();
                Invoke("PlayBtnAnimation", 0.5f);
                float end = 0;                  //Animacion de Logo (Inicio de juego)
                float start = 2000;
                float lerpY = Mathf.Lerp(start, end, t);

                t += Time.deltaTime * 1.2f;
                logo.localPosition = new Vector2(0, lerpY);
                    
                if (t >= 1) t = 1;
                circle.transform.localScale = Vector2.zero;
                Debug.Log("Oculto state!");
                //started = false;
                
                
                
                break;

            case PlayerStates.Instructions:
              
                timeInstruction += Time.deltaTime;
                IntructionsAnimation();
                Debug.Log("Instrucciones");
                if (timeInstruction >= 6) 
                {
                    playerStates = PlayerStates.Gameplay;
                }
                signature.SetActive(false);
                break;

            case PlayerStates.Gameplay:

                Debug.Log("Gameplay");
                activatePlayer();
                PlayGame();
                CircleMovement();
                SlideAnimationOn();
                PlayAgainAnimationOff();
                NewBestAnimationOff();
                //animBestScore = true;
                //started = true;
                instructionButton.SetActive(false);
                btnConfigGO.SetActive(false);
                

                end = 0;                  //Animacion de Logo (Inicio de juego)
                start = 2000;
                lerpY = Mathf.Lerp(end, start, t);

                t += Time.deltaTime * 1.2f;
                logo.localPosition = new Vector2(0, lerpY);

                float lerpScaleX = Mathf.Lerp(0, 0.3f, tScaleCircle += Time.deltaTime * 0.5f);
                float lerpScaleY = Mathf.Lerp(0, 0.3f, tScaleCircle += Time.deltaTime * 0.5f);
                circle.transform.localScale = new Vector3(lerpScaleX, lerpScaleY, 0);
                
                if (tScaleCircle >= 1) tScaleCircle = 1;

                break;

            case PlayerStates.GameOver:
                Debug.Log("GameOver");
                GameOver();
                SlideAnimationOff();
                PlayAgainAnimationOn();
                tScaleCircle = 0;
                playButtonGO.SetActive(true);
                
                if (score > bestScore) 
                {
                    int aux = 0;
                    aux = score;
                    score = bestScore;
                    bestScore = aux;
                    newBestScoreText.text =  "" + bestScore;
                    best.text = "Best: " + bestScore;
                    
                    animBestScore = true;
                } 
                if(animBestScore) NewBestAnimationOn();
                
                break;

            case PlayerStates.Reset:
                Debug.Log("Reset");
                score = 0;
                playerStates = PlayerStates.Gameplay;
                break;

            case PlayerStates.Config:

               

                if (volMenu)
                {
                    timeVolMenu += Time.deltaTime;
                    volPanel.transform.position = new Vector3(Tweens.EaseOutElastic(8, 0, timeVolMenu * 1f), 0);
                    if (timeVolMenu >= 1) timeVolMenu = 1;
                    playButtonGO.SetActive(false);
                    gameTitle.SetActive(false);
                    volPanelGO.SetActive(true);

                }
                if (volMenu == false)
                {
                    timeVolMenu -= Time.deltaTime;
                    volPanel.transform.position = new Vector3(Tweens.EaseOutElastic(8, 0, timeVolMenu * 1f), 0);
                    if (timeVolMenu <= 0) timeVolMenu = 0;
                    playButtonGO.SetActive(true);
                    gameTitle.SetActive(true);
                    volPanelGO.SetActive(false);
                    
                }



                break;

        }

        if (anim) 
        {
            //float easeInOutElastic = Tweens.EaseInOutElastic(1f, 1.3f, time += Time.deltaTime * 1f);
            //scoreRectTransform.localScale = new Vector2(easeInOutElastic, easeInOutElastic);
        }

       // myMixer.GetFloat
       

    }

   
    public void OnTriggerEnter2D(Collider2D collision) //Colision del player con cubos
    {
        if (collision.gameObject.tag == "Cube2")
        {
            GetComponent<AudioSource>().PlayOneShot(whiteBox);
            score++;
            scoreText.text = "" + score;
            anim = true;
            Invoke("offAnim", 0.8f);
            
            Destroy(GameObject.Find("cube2"),0.2f);
        }
        if (collision.gameObject.tag == "Cube")
        {
            GetComponent<AudioSource>().PlayOneShot(greenBox);
            float x = Mathf.Lerp(0.3f, 0, time += Time.deltaTime * 0.1f);
            float y = Mathf.Lerp(0.3f, 0, time += Time.deltaTime * 0.1f);

            transform.localScale = new Vector3(0,0,0);
            GameOver();
            playerStates = PlayerStates.GameOver;
            
        }

    }

    void offAnim() 
    {
        anim = false;
        float easeInOutElastic = Tweens.EaseInOutElastic(1f, 0.5f, time2 += Time.deltaTime*1);
        scoreRectTransform.localScale = new Vector2(easeInOutElastic, easeInOutElastic);
        time = 0;
    }

    public void activatePlayer() 
    {
        gameObject.SetActive(true);
        playerStates = PlayerStates.Gameplay;
        t += Time.deltaTime;
        float lerpX = Mathf.Lerp(0,0.3f,t);
        float lerpY = Mathf.Lerp(0, 0.3f, t);
        gameObject.transform.localScale = new Vector3(lerpX, lerpY,0);
        enemySpawner.SetActive(true);
    }
    public void GameOver() 
    {
        enemySpawner.SetActive(false);
        PlayBtnAnimation();
    }
    void CircleMovement()
    {

        if (Input.anyKeyDown && speed == speed) 
        {
            speed = -speed;
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        
        if (Input.anyKeyDown && speed == -speed) speed = speed;

        movement = movement + (Time.deltaTime * speed);
        circle.transform.position = new Vector3(movement, -1.54f, 0);

        if (movement > 1.70f)
        {
            movement = 1.70f;
            speed = speed * -1;
        }
        if (movement < -1.70f)
        {
            movement = -1.70f;
            speed = speed * -1;
        }

    }
    public void PlayGame()
    {
        playerStates = PlayerStates.Gameplay;
        playButtonGO.SetActive(false);
        float animationBtn = Tweens.EaseInOutElastic(1, 0, time += Time.deltaTime * 1f);

        float t2 = 0;
        float end = 2000;
        float start = 0;
        t2 += Time.deltaTime * 1.2f;
        float lerpY = Mathf.Lerp(start, end, t * -1.2f);

        scoreRectTransform.transform.localPosition = new Vector3(0, -1000, 0);
      
    }
    
    public void PlayBtnAnimation()
    {
        //playButtonGO.SetActive(true);
        float animationBtn = Tweens.EaseOutElastic(0, 1, time += Time.deltaTime * 1f);
        playBtn.localScale = new Vector2(animationBtn, animationBtn);
    }

    public void Oculto() 
    {
        score = 0;
        scoreText.text = "" + score;
        enemySpawner.SetActive(false);
        
    }

    public void ResetScore() 
    {
        score = 0;
        scoreText.text = "" + score;

    } 
    
    public void SlideAnimationOn() 
    {
        float lerpScaleX = Mathf.Lerp(0, 0.10f, tScaleSlide += Time.deltaTime * 2.5f);
        slide.transform.localScale = new Vector3(lerpScaleX, 0.08f, 0);
        if (tScaleSlide >= 1) tScaleSlide = 1;
    }
     
    public void SlideAnimationOff() 
    {
        float lerpScaleX = Mathf.Lerp(0, 0.10f, tScaleSlide -= Time.deltaTime * 2.5f);
        slide.transform.localScale = new Vector3(lerpScaleX, 0.08f, 0);
        if (tScaleSlide <= 0) tScaleSlide = 0;

    }

    public void PlayAgainAnimationOn() //Animacion de texto "Play aganin"
    {
    
        float playAgainLerpY = Mathf.Lerp(-2000,-425,playAgainTime += Time.deltaTime * 2.5f);
        playAgainRectTransform.transform.localPosition = new Vector3(0,playAgainLerpY,0);
        if (playAgainTime >= 1) playAgainTime = 1;
    
    }
    public void PlayAgainAnimationOff() 
    {
        float playAgainLerpY = Mathf.Lerp(-2000, -425, playAgainTime -= Time.deltaTime * 2.5f);
        playAgainRectTransform.transform.localPosition = new Vector3(0, playAgainLerpY, 0);
        if (playAgainTime <= 0) playAgainTime = 0;

    }

    public void NewBestAnimationOn() 
    {
        float newBestLerpY = Mathf.Lerp(-2500, 450, newBestTime += Time.deltaTime * 2.5f);
        newBestScoreRectT.transform.localPosition = new Vector3(0, newBestLerpY, 0);
        if (newBestTime >= 1) newBestTime = 1;
    }

    public void NewBestAnimationOff()
    {
        float newBestLerpY = Mathf.Lerp(-2500, 450, newBestTime -= Time.deltaTime * 2.5f);
        newBestScoreRectT.transform.localPosition = new Vector3(0, newBestLerpY, 0);
        if (newBestTime <= 0) newBestTime = 0;
        animBestScore = false; 
    }

    public void IntructionsAnimation()
    {
        tI += Time.deltaTime;

        myColor = new Color(255, 255, 255, colorControl += Time.deltaTime * 0.7f);
        myInstructions.color = myColor;

        float start = 850;
        float end = -1500;

        float easeOutQuint = Tweens.easeOutQuint(tI, start, end, 3f);

        instructionRectT.transform.localPosition = new Vector3(0, easeOutQuint, 0);

        if (colorControl >= 3) 
        {
            colorControl = 3;
            myColor = new Color(255, 255, 255, colorControl -= Time.deltaTime * 0.7f);
            myInstructions.color = myColor;
        }
        

    }
    public void IntructionsAnimation2()
    {
        tI2 += Time.deltaTime;

        myColor = new Color(255, 255, 255, colorControl += Time.deltaTime * 0.7f);
        myInstruction2.color = myColor;

        float start = 850;
        float end = -1500;

        float easeOutQuint = Tweens.easeOutQuint(tI, start, end, 5f);

        instructionRectT2.transform.localPosition = new Vector3(0, easeOutQuint, 0);

    }

    public void parseToIntructions()
    {
        playerStates = PlayerStates.Instructions;

    }

    public void ActiveVolMenu()
    {
        volMenu = !volMenu;

        playerStates = PlayerStates.Config;
        Debug.Log("Config State");

    }


}
