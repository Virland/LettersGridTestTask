using UnityEngine;
using Grid;
using UI;

public class Mediator : MonoBehaviour
{
    [SerializeField] private TileBoard m_Grid;
    [SerializeField] private InputPanel m_InputPanel;
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_Grid.Init();
        m_Grid.OnCanResizeChanged += m_InputPanel.HandleGenerateButtonState;
        m_Grid.OnCanShuffleChanged += m_InputPanel.HandleShuffleButtonState;
        m_InputPanel.OnGenerateClick += m_Grid.SetSize;
        m_InputPanel.OnShuffleClick += m_Grid.Shuffle;
        m_InputPanel.HandleGenerateButtonState(m_Grid.CanResize);
        m_InputPanel.HandleShuffleButtonState(m_Grid.CanShuffle);
    }
}