using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpdateManager : MonoBehaviour {
    public static UpdateManager instance;
    [SerializeField] Transform canvas;

    [SerializeField] GameObject update1;
    [SerializeField] GameObject update2;
    [SerializeField] GameObject update3;

    public UnityEvent<string> OnUpdateChoose;


    Option option1;
    Option option2;
    Option option3;

    // Start is called before the first frame update
    void Start() {
        UpdateManager.instance = this;        

        option1 = update1.GetComponent<Option>();
        option2 = update2.GetComponent<Option>();
        option3 = update3.GetComponent<Option>();

        option1.OnChoose.AddListener(Close);
        option2.OnChoose.AddListener(Close);
        option3.OnChoose.AddListener(Close);

        //Open("Jol1", "Jol2", "Jol3");
    }

    // Update is called once per frame
    void Update() {

    }

    public void Open(string optionTXT1, string optionTXT2, string optionTXT3) {
        canvas.gameObject.SetActive(true);

        option1.ChangeText(optionTXT1);
        option2.ChangeText(optionTXT2);
        option3.ChangeText(optionTXT3);


    }

    public void Close(string optionTXT) {
        canvas.gameObject.SetActive(false);
        OnUpdateChoose.Invoke(optionTXT);
    }
}
