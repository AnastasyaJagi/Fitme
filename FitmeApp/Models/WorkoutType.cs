using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitmeApp.Models
{
    public class WorkoutType : INotifyPropertyChanged
    {
        public string _id { get; set; }
        public string workout_type { get; set; }
        public string workout_description { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        bool isSelected;
        public bool IsSelected
        {
            set
            {
                isSelected = value;
                onPropertyChanged();
            }
            get => isSelected;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
