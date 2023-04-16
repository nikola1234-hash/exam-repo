using EasyTestMaker.PubSub;
using Prism.Events;
using System;

namespace EasyTestMaker.ViewModels
{
    public class HomeViewModel : ViewModelBase, IDisposable
    {
		private ViewModelBase _leftViewModel;

		public ViewModelBase LeftViewModel
		{
			get { return _leftViewModel; }
			set => SetProperty(ref _leftViewModel, value);

		}
		private ViewModelBase _rightViewModel;

		public ViewModelBase RightViewModel
		{
			get { return _rightViewModel; }
			set => SetProperty(ref _rightViewModel, value);	
		}

		private IEventAggregator @event = App.GetService<IEventAggregator>();

		public HomeViewModel()
		{
			Initialize();
			@event.GetEvent<MenuViewSelection>().Subscribe(LeftWindowChange);
		}

        private void LeftWindowChange(object obj)
        {
            if(obj is Views view)
			{
                switch (view)
                {
                    case Views.Test:
                        break;
                    case Views.CreateStudent:
                        break;
                    case Views.CreateTest:
                        RightViewModel = new CreateTestViewModel();
                        break;
                    case Views.Results:
                        break;
                    case Views.ResultsDetails:
                        break;
                }
            }
        }

        private void Initialize()
		{
			LeftViewModel = new MenuViewModel();
		}

        public void Dispose()
        {
            @event.GetEvent<MenuViewSelection>().Unsubscribe(LeftWindowChange);
        }
    }
}
