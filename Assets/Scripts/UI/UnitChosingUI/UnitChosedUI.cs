using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnitChosedUI : MonoBehaviour
{
    [SerializeField]
    private GameObject humanChosedPanel, elfChosedPanel;

    [SerializeField]
    private Image firstHumanSprite, secondHumanSprite, thirdHumanSprite, fourthHumanSprite, fifthHumanSprite, sixthHumanSprite, firstElfSprite, secondElfSprite, thirdElfSprite, fourthElfSprite, fifthElfSprite, sixthElfSprite;
    
    [SerializeField]
    private List<GameObject> choosedHumanUnitList, choosedElfUnitList;

    bool isMute;

    private void Start()
    {
        UIEventBroker.HumanChosedUnitPanel += HumanChoosedPanel;
        UIEventBroker.ElfChosedUnitPanel += ElfChoosedPanel;
        UIEventBroker.ShowHumanUnitsChoosed += ShowHumanChoosedUnit;
        UIEventBroker.ShowElfesUnitsChoosed += ShowElfChoosedUnit;
    }

    private void HumanChoosedPanel()
    {
        humanChosedPanel.SetActive(true);
        StartCoroutine(RemoveAfterSeconds(2, humanChosedPanel));
    }

    private void ElfChoosedPanel()
    {
        elfChosedPanel.SetActive(true);
        StartCoroutine(RemoveAfterSeconds(2, elfChosedPanel));
    }
    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
    }

    private void ShowHumanChoosedUnit(GameObject humans)
    {
        if (humans.GetComponent<Unit>().owner == Unit.Owner.Humans)
        {
            choosedHumanUnitList.Add(humans);

            for (int i = 0; i < choosedHumanUnitList.Count; i++)
            {
                firstHumanSprite.GetComponent<Image>().sprite = choosedHumanUnitList[0].GetComponent<SpriteRenderer>().sprite;
            }
            for (int i = 1; i < choosedHumanUnitList.Count; i++)
            {
                secondHumanSprite.GetComponent<Image>().sprite = choosedHumanUnitList[1].GetComponent<SpriteRenderer>().sprite;
            }
            for (int i = 2; i < choosedHumanUnitList.Count; i++)
            {
                thirdHumanSprite.GetComponent<Image>().sprite = choosedHumanUnitList[2].GetComponent<SpriteRenderer>().sprite;
            }
            for (int i = 3; i < choosedHumanUnitList.Count; i++)
            {
                fourthHumanSprite.GetComponent<Image>().sprite = choosedHumanUnitList[3].GetComponent<SpriteRenderer>().sprite;
            }
            for (int i = 4; i < choosedHumanUnitList.Count; i++)
            {
                fifthHumanSprite.GetComponent<Image>().sprite = choosedHumanUnitList[4].GetComponent<SpriteRenderer>().sprite;
            }
            for (int i = 5; i < choosedHumanUnitList.Count; i++)
            {
                sixthHumanSprite.GetComponent<Image>().sprite = choosedHumanUnitList[5].GetComponent<SpriteRenderer>().sprite;
            }  
        }
    }

    private void ShowElfChoosedUnit(GameObject elfes)
    {
        if (elfes.GetComponent<Unit>().owner == Unit.Owner.Elfes)
        {
            choosedElfUnitList.Add(elfes);

            for (int i = 0; i < choosedElfUnitList.Count; i++)
            {
                firstElfSprite.GetComponent<Image>().sprite = choosedElfUnitList[0].GetComponent<SpriteRenderer>().sprite;
            }
            for (int i = 1; i < choosedElfUnitList.Count; i++)
            {
                secondElfSprite.GetComponent<Image>().sprite = choosedElfUnitList[1].GetComponent<SpriteRenderer>().sprite;
            }
            for (int i = 2; i < choosedElfUnitList.Count; i++)
            {
                thirdElfSprite.GetComponent<Image>().sprite = choosedElfUnitList[2].GetComponent<SpriteRenderer>().sprite;
            }
            for (int i = 3; i < choosedElfUnitList.Count; i++)
            {
                fourthElfSprite.GetComponent<Image>().sprite = choosedElfUnitList[3].GetComponent<SpriteRenderer>().sprite;
            }
            for (int i = 4; i < choosedElfUnitList.Count; i++)
            {
                fifthElfSprite.GetComponent<Image>().sprite = choosedElfUnitList[4].GetComponent<SpriteRenderer>().sprite;
            }
            for (int i = 5; i < choosedElfUnitList.Count; i++)
            {
                sixthElfSprite.GetComponent<Image>().sprite = choosedElfUnitList[5].GetComponent<SpriteRenderer>().sprite;
            } 
        }
    }


    public void SaundOffBtn()
    {
        isMute = !isMute;
     AudioListener.volume = isMute ? 0 : 1;
    }
    public void ExitBtn()
    {
       SceneManager.LoadScene("Menu");
    }
}
