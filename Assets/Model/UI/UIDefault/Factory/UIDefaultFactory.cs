﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    [UIFactory(UIType.UIDefault)]
    public class UIDefaultFactory : IUIFactoryExtend
    {
        private List<string> subs;

        public UI Create(Scene scene, string type, GameObject gameObject)
        {
            try
            {
                GameObject bundleGameObject = ((GameObject)ResourcesHelper.Load("KV")).Get<GameObject>(type);
                GameObject go = UnityEngine.Object.Instantiate(bundleGameObject);
                go.layer = LayerMask.NameToLayer(LayerNames.UI);
                UI ui = ComponentFactory.Create<UI, GameObject>(go);

                ui.AddUIBaseComponent<UIDefaultComponent>();

                AddSubComponent(ui);

                return ui;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        public void Remove(string type)
        {
            RemoveSubComponents();
        }

        public async Task<UI> CreateAsync(Scene scene, string type, GameObject parent)
        {
            try
            {
                UnityEngine.Object obj = await ResourcesHelper.LoadAsync("KV");
                GameObject bundleGameObject = ((GameObject)obj).Get<GameObject>(type);
                GameObject go = UnityEngine.Object.Instantiate(bundleGameObject);
                go.layer = LayerMask.NameToLayer(LayerNames.UI);
                UI ui = ComponentFactory.Create<UI, GameObject>(go);

                ui.AddUIBaseComponent<UIDefaultComponent>();

                AddSubComponent(ui);

                return ui;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        public void AddSubComponent(UI ui)
        {

        }

        public void RemoveSubComponents()
        {
            if (null == subs || subs.Count == 0)
                return;

            UIComponent uiComponent = Game.Scene.GetComponent<UIComponent>();
            for (int i = 0, n = subs.Count; i < n; i++)
            {
                uiComponent.RemoveSub(subs[i]);
            }
        }
    }
}