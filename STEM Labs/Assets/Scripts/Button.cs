using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject objectToChangeTo;

    void Start(){
        Button btn = this.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick(){
        BuildSystem.current.changeObject(objectToChangeTo);
        BuildSystem.current.cloneObject();
    }

    public void Clear(){ //Button Inspector Only Accepts and Object, Then It Accesses the Object's Script's Function. Clear Button Accesses Button 1's Object to Access this Function
        GameObject gObj =  GameObject.Find("Build_1");
        for(int i = gObj.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(gObj.transform.GetChild(i).gameObject);
        }
    }

}
