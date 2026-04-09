using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Duocare2.Models
{
    public class Day : INotifyPropertyChanged
    {
        private int _number;
        private DateTime _date;
        private bool _isPlaceholder;

        private bool _hasTask;
        private bool _hasEvent;

        private string _taskDescription = "";
        private TimeSpan? _taskHour = new TimeSpan(12, 0, 0);

        public int Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public bool IsPlaceholder
        {
            get => _isPlaceholder;
            set => SetProperty(ref _isPlaceholder, value);
        }

        public bool HasTask
        {
            get => _hasTask;
            set => SetProperty(ref _hasTask, value);
        }

        public bool HasEvent
        {
            get => _hasEvent;
            set => SetProperty(ref _hasEvent, value);
        }

        public string TaskDescription
        {
            get => _taskDescription;
            set => SetProperty(ref _taskDescription, value);
        }

        public TimeSpan? TaskHour
        {
            get => _taskHour;
            set => SetProperty(ref _taskHour, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string name = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(name);
            return true;
        }
    }
}
