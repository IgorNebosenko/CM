using System.Collections;
using System.Collections.Generic;
using CM.Core.Utils;
using UnityEngine;

namespace CM.Core.MVP
{
    public abstract class View : MonoBehaviour
    {
        protected bool enableLogs = false;

        public static string StandardPathFormat => "Views/{0}";

        protected Presenter BasePresenter { get; set; }

        private IEnumerator<Transform> _subviewContainer => EnumeratorUtil.Single<Transform>(null);
        public virtual IEnumerator<Transform> SubviewContainer => _subviewContainer;

        public abstract void Init(Presenter presenter);

        public void Hide()
        {
            OnBeforeClose();
        }

        public void Show()
        {
            OnBeforeClose();
        }
        
        protected virtual void OnBeforeShow()
        {
        }
		
        protected virtual void OnShowComplete()
        {
        }
		
        protected virtual void OnBeforeClose()
        {
        }

        protected virtual void BeforeDestroy()
        {
            BasePresenter = null;
            OnBeforeDestroy();
        }

        protected virtual void OnBeforeDestroy()
        {
        }
    }
    
    public abstract class View<TPresenter> : View where TPresenter : Presenter
    {
        protected TPresenter Presenter { get; private set; }
         
        public sealed override void Init(Presenter presenter)
        {
            BasePresenter = presenter;
            Presenter = (TPresenter) presenter;
        }
    
        protected sealed override void BeforeDestroy()
        {
            Presenter = null;
            base.BeforeDestroy();
        }
    }
}