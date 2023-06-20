using TechnoDemo.Spawn;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TechnoDemo.Scopes
{
    [DisallowMultipleComponent]
    public sealed class SceneScope : BaseScope
    {
        [SerializeField] private SceneScopeContainerDataSO m_containerDataSo;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMessageBrokers(builder, out var options);
            RegisterInHierarchy(builder);
            
            this.LogDIRegisterSuccess();
        }

        private void RegisterInHierarchy(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<Spawner>().As<ISpawner>().WithParameter(m_containerDataSo.SpawnerDataSo);
        }
    }
}