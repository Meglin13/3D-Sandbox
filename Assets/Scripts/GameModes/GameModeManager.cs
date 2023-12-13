using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public List<GameObject> gameModes = new List<GameObject>(); // ������ ������� �������

    private int currentModeIndex = 0; // ������� ������ ������

    private void Start()
    {
        SwitchToMode(currentModeIndex);
    }

    private void Update()
    {
       
    }

    public void SwitchToNextMode()
    {
        currentModeIndex = (currentModeIndex + 1) % gameModes.Count;
        SwitchToMode(currentModeIndex);
    }

    private void SwitchToMode(int index)
    {
        foreach (GameObject mode in gameModes)
        {
            mode.SetActive(false);
        }

        gameModes[index].SetActive(true);
    }
}
