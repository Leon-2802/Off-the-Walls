﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{
    MenuCtrl inputCtrl;

    public GameObject startMenu, mainMenu, optionsMenu, aboutMenu;
    public GameObject mainMenuFirstBtn, optionsMenuFirstBtn, aboutMenuFirstBtn, optionsBackBtn, aboutBackBtn;

    [SerializeField] private GameObject[] mainMenuBtns = null;
    [SerializeField] private GameObject[] settingsMenuBtns = null;
    [SerializeField] private GameObject[] settings = null;
    [SerializeField] private Slider[] sliders = null;
    private bool inSettings = false;
    private bool sliderMusic = false;
    private bool sliderSFX = false;
    [SerializeField] private GameObject[] aboutMenuBtns = null;

    private int positionCount = 0;
    private int settingsCount = 0;
    private int aboutCount = 1;

    private void Awake() {
        inputCtrl = new MenuCtrl();

        inputCtrl.input.start.performed += ctx => enterMenu();

        inputCtrl.input.down.performed += ctx => moveDown();

        inputCtrl.input.up.performed += ctx => moveUp();

        inputCtrl.input.confirm.performed += ctx => select();

        inputCtrl.input.back.performed += ctx => back();

        inputCtrl.input.switchRight.performed += ctx => switchR();
        inputCtrl.input.switchLeft.performed += ctx => switchL();

        inputCtrl.input.sliderLeft.performed += ctx => sliderLeft();
        inputCtrl.input.sliderRight.performed += ctx => sliderRight();
    }

    void enterMenu() 
    {
        if(!mainMenu.activeInHierarchy) {
            startMenu.SetActive(false);
            mainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(mainMenuFirstBtn);
        }
    }

    void moveDown()
    {
        if(mainMenu.activeInHierarchy && positionCount >= 0 && positionCount <= 2) {
            positionCount++;
            EventSystem.current.SetSelectedGameObject(mainMenuBtns[positionCount]);
        }
        else if(optionsMenu.activeInHierarchy && settingsCount >= 0 && settingsCount <= 2) {
            settingsCount++;
            EventSystem.current.SetSelectedGameObject(settingsMenuBtns[settingsCount]);
        }
    }
    void moveUp() 
    {
        if(mainMenu.activeInHierarchy && positionCount <= 3 && positionCount >= 1) {
            positionCount--;
            EventSystem.current.SetSelectedGameObject(mainMenuBtns[positionCount]);
        }
        else if(optionsMenu.activeInHierarchy && settingsCount <= 3 && settingsCount >= 1) {
            settingsCount--;
            EventSystem.current.SetSelectedGameObject(settingsMenuBtns[settingsCount]);
        }
    }
    void switchR() {
        if(aboutMenu.activeInHierarchy && aboutCount >= 0 && aboutCount <= 1) {
            aboutCount++;
            EventSystem.current.SetSelectedGameObject(aboutMenuBtns[aboutCount]);
        }
    }
    void switchL() {
        if(aboutMenu.activeInHierarchy && aboutCount >= 1 && aboutCount <= 2) {
            aboutCount--;
            EventSystem.current.SetSelectedGameObject(aboutMenuBtns[aboutCount]);
        }
    }

    void back() {
        if(optionsMenu.activeInHierarchy) {
            if(inSettings == false) {

                optionsMenu.SetActive(false);
                mainMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(optionsBackBtn);
                positionCount = 1;
            }
            else if(inSettings == true) {
                EventSystem.current.SetSelectedGameObject(settingsMenuBtns[settingsCount]);
                inSettings = false;
                sliderMusic = false;
                sliderSFX = false;
            }
        }
        else if(aboutMenu.activeInHierarchy) {
            aboutMenu.SetActive(false);
            mainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(aboutBackBtn);
            positionCount = 2;
        }
    }

    void select() {
        if(mainMenu.activeInHierarchy) {
            switch(positionCount) {
                case 0:
                    SceneManager.LoadScene("battle");
                    break;
                case 1:
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(optionsMenuFirstBtn);
                    settingsCount = 0;
                    break;
                case 2:
                    mainMenu.SetActive(false);
                    aboutMenu.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(aboutMenuFirstBtn);
                    aboutCount = 1;
                    break;
                case 3:
                    Application.Quit();
                    break;
            }
        }
        else if(optionsMenu.activeInHierarchy) {
            inSettings = true;
            switch(settingsCount) {
                case 0:
                    EventSystem.current.SetSelectedGameObject(settings[settingsCount]);
                    sliderMusic = false;
                    sliderSFX = false;
                    break;
                case 1:
                    EventSystem.current.SetSelectedGameObject(settings[settingsCount]);
                    sliderSFX = false;
                    sliderMusic = true;
                    break;
                case 2:
                    EventSystem.current.SetSelectedGameObject(settings[settingsCount]);
                    sliderMusic = false;
                    sliderSFX = true;
                    break;
                case 3:
                    inSettings = false;
                    sliderMusic = false;
                    sliderSFX = false;
                    optionsMenu.SetActive(false);
                    mainMenu.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(optionsBackBtn);
                    positionCount = 1;
                    break;
            }
        }
        else if(aboutMenu.activeInHierarchy) {
            switch(aboutCount) {
                case 0:
                    aboutMenu.SetActive(false);
                    mainMenu.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(aboutBackBtn);
                    positionCount = 2;
                    break;
            }
        }
    }

    void sliderRight() {
        if(sliderMusic == true && sliders[0].value <= 1) {
            sliders[0].value += 0.1f;
        } else if(sliderSFX == true && sliders[1].value <= 1) {
            sliders[1].value += 0.1f;
        }
    }
    void sliderLeft() {
        if(sliderMusic == true && sliders[0].value >= 0) {
            sliders[0].value -= 0.1f;
        } else if(sliderSFX == true && sliders[1].value >= 0) {
            sliders[1].value -= 0.1f;
        }
    }
    
    private void OnEnable() {
        inputCtrl.Enable();
    }
    private void OnDisable() {
        inputCtrl.Disable();
    }
}
