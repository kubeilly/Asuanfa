using System.ComponentModel;
using System.Reactive;
using System.Windows.Input;
using Asuanfa.Library.Services;
using Avalonia;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;

namespace Asuanfa.Library.ViewModels;

using ReactiveUI;


public class AlgorithmVisualizerViewModel : INotifyPropertyChanged
{
    private int[] _data;
    public int[] Data
    {
        get => _data;
        set
        {
            _data = value;
            OnPropertyChanged(nameof(Data));
        }
    }

    private readonly IAlgorithmService _algorithmService;
    public ICommand StartCommand { get; }

    

    private async void ExecuteStartCommand()
    {
        var initialData = new[] { 10, 20, 5, 6, 1 };
        Data = initialData; // 初始化数据

        _algorithmService.ResetStep(); // 重置步骤

        // 逐步执行算法
        for (int step = 0; step < initialData.Length * initialData.Length; step++)
        {
            _algorithmService.StepAlgorithm(Data, step);
            await Task.Delay(500);  // 等待500ms展示动画效果
        }
    }
    public AlgorithmVisualizerViewModel(IAlgorithmService algorithmService)
    {
        _algorithmService = algorithmService;
        _algorithmService.OnAlgorithmStep += UpdateData;

        StartCommand = new RelayCommand(ExecuteStartCommand);
    }

    private void UpdateData(int[] updatedData)
    {
        // 在 UI 线程上更新数据
        Dispatcher.UIThread.Post(() =>
        {
            Data = updatedData;
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}



