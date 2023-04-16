namespace EasyTestMaker.ViewModels
{
	public class MainViewModel : ViewModelBase
    {

		private ViewModelBase _currentViewModel;

		public ViewModelBase CurrentViewModel
		{
			get { return _currentViewModel; }
			set
			{
				this.SetProperty(ref _currentViewModel, value);
			}
		}
		public MainViewModel()
		{
			
		}

		public void OnLoad()
		{

		}
	}
}
