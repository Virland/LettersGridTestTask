using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Input;

public class Root : MonoBehaviour
{
    [SerializeField] private TileBoard m_Grid;
    [SerializeField] private InputPanel m_InputPanel;
    void Awake()
    {
        m_Grid.Init(m_InputPanel);
    }
}