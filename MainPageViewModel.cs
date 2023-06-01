using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace InfiniteScrollingMauiVersion
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GroupedItems> ListItems { get; private set; } = new ObservableCollection<GroupedItems>();

        private Command _remainingItemsReachedCommand;

        private int _numberOfItems;

        public Command RemainingItemsReachedCommand
        {
            get
            {
                return (_remainingItemsReachedCommand) ?? (_remainingItemsReachedCommand = new Command(() =>
                    {
                        var numbersList = new List<string>();
                        for (int i = 0; i < 25; i++)
                        {
                            numbersList.Add(_numberOfItems++.ToString());
                        }

                        ListItems.Add(new GroupedItems(numbersList));
                    }
                ));
            }
        }

        public MainPageViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            var numbersList = new List<string>();
            for (int i = 0; i < 50; i++)
            {
                numbersList.Add(_numberOfItems++.ToString());
            }

            ListItems.Add(new GroupedItems(numbersList));
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
