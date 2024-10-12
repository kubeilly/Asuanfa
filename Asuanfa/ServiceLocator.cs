using System;
using Asuanfa.Library.Services;
using Asuanfa.Library.ViewModels;

namespace Asuanfa;

using Microsoft.Extensions.DependencyInjection;

    public class ServiceLocator
    {
        private readonly IServiceProvider _serviceProvider;

        // 全局访问 ServiceLocator 实例
        private static ServiceLocator _current;
        public static ServiceLocator Current => _current ??= new ServiceLocator();

        public AlgorithmVisualizerViewModel AlgorithmVisualizerViewModel =>
            _serviceProvider.GetRequiredService<AlgorithmVisualizerViewModel>();

        public ServiceLocator()
        {
            // 创建服务容器并注册依赖
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // 注册 ViewModel 和服务
            services.AddSingleton<IAlgorithmService, AlgorithmService>();
            services.AddSingleton<AlgorithmVisualizerViewModel>();
        }
    }
