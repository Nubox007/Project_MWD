using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YESorNO_Btn : MonoBehaviour
{
    #region No ��ư�� ������ �� �ٽ� Scene[4_ChooseHero]�� �ҷ�����
    public void NO_AgainScene_4_ChooseHero()
    {
        //�ٸ� ���� ������Ʈ �ҷ�����(GetRootGameObjects()[2]�� Canva�� 3��°�� �־)
        GameObject canvas = SceneManager.GetSceneByName("4_ChooseHero").GetRootGameObjects()[2]; 
        Button[] button = canvas.GetComponentsInChildren<Button>(); //��ư�� �������� �־ �ڽĿ������� �迭�� �ҷ���
        for(int i = 0; i<button.Length; ++i)
        {
            if (!button[i].interactable)
            {
                button[i].interactable = true;
            }
        }
        SceneManager.UnloadSceneAsync("4-1_HeroChooseYESorNO");
    }
    #endregion

    public void Yes_NestScene_5_HeroScreen()
    {
        SceneManager.LoadScene("5_HeroScreen"); //���� ������
    }
}
