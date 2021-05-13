using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Animations;
public class MainPlayer : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject LosePanel;

    private Camera mainCamera;
    private NavMeshAgent agent;
    Animation anim;
    private void Start()
    {
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
    }
    private void Update()
    {
        Moving();
    }

    void Moving()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyZone")
        {
            Lose();
        }
         
        if(other.tag == "Finish")
        {
            if (!PlayerPrefs.HasKey("Save") || PlayerPrefs.GetInt("Save") < SceneManager.GetActiveScene().buildIndex)
                PlayerPrefs.SetInt("Save", SceneManager.GetActiveScene().buildIndex + 1);

            Win();
        }
    }

    void Lose()
    {
        LosePanel.SetActive(true);
        anim.Play("Lose");
        Time.timeScale = 0f;
    }

    void Win()
    {
        
        WinPanel.SetActive(true);
        //anim.Play("Win");
        Time.timeScale = 0f;  
    }
    public void NextLevel() //������ � �������� ����� ����
    {
        Time.timeScale = 1f;
        int CurrentLvlIndex = SceneManager.GetActiveScene().buildIndex;  //���������� � �������� �������� ������
        if (CurrentLvlIndex + 1 == SceneManager.sceneCountInBuildSettings)   //���� ��������� �������, �� ��������� ������
        {
            CurrentLvlIndex = 0;  //������� ��� 0, �� ����� ����� +1 ������� ����������� ���1
        }
        SceneManager.LoadScene(CurrentLvlIndex + 1);  //�������� ����� ����
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

