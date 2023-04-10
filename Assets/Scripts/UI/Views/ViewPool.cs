using System;
using System.Collections.Generic;
using Core.UI;
using Tools;
using Tools.UIContainer;
using UI.Popups;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.Views
{
    public class ViewPool : IViewPool
    {
        private readonly IAssetPathProvider _assetPathProvider;
        private readonly IAddressableProvider _addressableProvider;
        private readonly IUIContainer<Tools.UIContainer.ViewContainer> _viewContainer;
        private readonly IUIContainer<PopupContainer> _popupContainer;

        private Dictionary<IView, AssetReference> assetReferencesForViews = new Dictionary<IView, AssetReference>();
        private Dictionary<Type, GameObject> cachedObjectsByType = new Dictionary<Type, GameObject>();

        public ViewPool(IAssetPathProvider assetPathProvider, IAddressableProvider addressableProvider,
            IUIContainer<Tools.UIContainer.ViewContainer> viewContainer, IUIContainer<PopupContainer> popupContainer)
        {
            _assetPathProvider = assetPathProvider;
            _addressableProvider = addressableProvider;
            _viewContainer = viewContainer;
            _popupContainer = popupContainer;
        }

        public void SpawnView<T>(Action<T> callback) where T : IView
        {
            Spawn(_viewContainer.Container, callback);
        }

        public void SpawnPopup<T>(Action<T> callback) where T : IPopup
        {
            Spawn(_popupContainer.Container, callback);
        }

        private void Spawn<T>(Transform container, Action<T> callback) where T : IView
        {
            var objectType = typeof(T);
            if (cachedObjectsByType.TryGetValue(objectType, out var cachedObject))
            {
                callback?.Invoke(cachedObject.GetComponent<T>());
                return;
            }

            var path = _assetPathProvider.ProvidePathForAsset<T>();
            _addressableProvider.InstantiateAsset(path, container, o =>
            {
                var view = o.GetComponent<T>();
                if (!assetReferencesForViews.ContainsKey(view))
                {
                    assetReferencesForViews.Add(view, path);
                }

                if (view.IsCacheable)
                {
                    cachedObjectsByType.Add(objectType, o);
                }

                callback?.Invoke(view);
            });
        }

        public virtual void DeSpawn(IView view)
        {
            if (view.IsCacheable)
            {
                view.SetActive(false);
                view.ResetView();
                return;
            }

            var assetReference = assetReferencesForViews[view];
            _addressableProvider.ReleaseAsset(assetReference, view.ObjectReference);
        }
    }
}