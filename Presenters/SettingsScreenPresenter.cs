using System;
using Models;
using UniRx;
using VContainer.Unity;
using Views;
using Views.Screens;

namespace Presenters
{
    public class SettingsScreenPresenter : IInitializable, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public void Initialize()
        {

        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}