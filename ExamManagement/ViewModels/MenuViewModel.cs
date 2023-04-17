﻿using EasyTestMaker.PubSub;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace EasyTestMaker.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {

        private IEventAggregator @event = App.GetService<IEventAggregator>();



        public ICommand MenuCommand { get;}
        public MenuViewModel()
        {
            MenuCommand = new DelegateCommand<object>(ChangeView);
        }

        private void ChangeView(object obj)
        {
            if(obj is Views view)
            {
                @event.GetEvent<MenuViewSelection>().Publish(view);
            }
        }
    }
}