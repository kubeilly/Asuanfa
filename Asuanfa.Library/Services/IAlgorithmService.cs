namespace Asuanfa.Library.Services;

public interface IAlgorithmService
{
    event Action<int[]> OnAlgorithmStep;

    // 执行完整的算法排序
    void RunAlgorithm(int[] data);

    // 可以逐步执行算法
    void StepAlgorithm(int[] data, int step);

    void ResetStep();
}
