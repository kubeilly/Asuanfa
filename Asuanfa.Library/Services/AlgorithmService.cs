namespace Asuanfa.Library.Services;

public class AlgorithmService : IAlgorithmService
{
    public event Action<int[]> OnAlgorithmStep;

    // 记录当前排序的步骤
    private int _currentOuterIndex = 0;
    private int _currentInnerIndex = 0;

    public void RunAlgorithm(int[] data)
    {
        // 完整运行算法
        for (int i = 0; i < data.Length - 1; i++)
        {
            for (int j = 0; j < data.Length - i - 1; j++)
            {
                if (data[j] > data[j + 1])
                {
                    (data[j], data[j + 1]) = (data[j + 1], data[j]);

                    OnAlgorithmStep?.Invoke((int[])data.Clone());
                }
            }
        }
    }

    // 逐步执行排序
    public void StepAlgorithm(int[] data, int step)
    {
        if (_currentOuterIndex >= data.Length - 1)
        {
            return; // 排序完成
        }

        if (_currentInnerIndex < data.Length - _currentOuterIndex - 1)
        {
            // 执行一步
            if (data[_currentInnerIndex] > data[_currentInnerIndex + 1])
            {
                (data[_currentInnerIndex], data[_currentInnerIndex + 1]) = (data[_currentInnerIndex + 1], data[_currentInnerIndex]);
            }

            _currentInnerIndex++;
            OnAlgorithmStep?.Invoke((int[])data.Clone());  // 通知更新
        }
        else
        {
            // 完成一轮内循环后，重置内部索引并推进外部循环
            _currentInnerIndex = 0;
            _currentOuterIndex++;
        }
    }

    // 重置步骤索引
    public void ResetStep()
    {
        _currentOuterIndex = 0;
        _currentInnerIndex = 0;
    }
}

