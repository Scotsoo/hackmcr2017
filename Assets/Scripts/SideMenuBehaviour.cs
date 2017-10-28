using System;
using UnityEngine;
using UnityEngine.UI;

public class SideMenuBehaviour : MonoBehaviour {

    public class SideMenu
    {
        public SideMenu(string tag, bool defaultVisible = false)
        {
            this._tag = tag;
            this.Canvas = GameObject.FindGameObjectWithTag(this._tag).GetComponent<Canvas>();
            IsVisible = defaultVisible;
        }
        public Canvas Canvas { get; private set; }
        private string _tag { get; set; }
        public bool IsVisible { get; set; }

        public void Update()
        {
            Canvas.gameObject.SetActive(IsVisible);
        }
    }

    private Button showMenuButton;

    private Text showMenuButtonText;

    // main menu wrapper
    private SideMenu mainMenu;
    // login, register ect
    private SideMenu defaultMenu;
    // register stuff
    private SideMenu registerMenu;
    // login stuff
    private SideMenu loginMenu;

    // Use this for initialization
    void Start ()
    {
        mainMenu = new SideMenu("SideMenu_Main", true);
        defaultMenu = new SideMenu("SideMenu_Default", true);
        loginMenu = new SideMenu("SideMenu_Login");
        registerMenu = new SideMenu("SideMenu_Register");
        showMenuButton = GameObject.FindGameObjectWithTag("SideMenu_ShowMenu").GetComponent<Button>();
        showMenuButtonText = showMenuButton.GetComponentInChildren(typeof(Text)).GetComponent<Text>();
        SetMenuText();
    }

    void SetMenuText()
    {
        showMenuButtonText.text = (mainMenu.IsVisible ? "Hide" : "Show") + " Menu";
    }
    public void Login_OnClick()
    {
        mainMenu.IsVisible = false;
        loginMenu.IsVisible = true;
        mainMenu.Update();
        loginMenu.Update();
    }

    public void HideMenu_OnClick()
    {
        mainMenu.IsVisible = !mainMenu.IsVisible;
        mainMenu.Update();
        SetMenuText();
    }

    public void Back_OnClick()
    {
        loginMenu.IsVisible = false;
        registerMenu.IsVisible = false;

    }
    // Update is called once per frame
    void Update ()
    {
        mainMenu.Update();
        defaultMenu.Update();
        registerMenu.Update();
        loginMenu.Update();
        
    }
}
