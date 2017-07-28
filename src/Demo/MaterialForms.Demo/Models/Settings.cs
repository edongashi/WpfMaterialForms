using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialForms.Wpf.Annotations;

namespace MaterialForms.Demo.Models
{
    public class Settings : INotifyPropertyChanged
    {
        private string deviceName;
        private bool wiFi;
        private bool hotspot;
        private string hotspotName;

        public string DeviceName
        {
            get => deviceName;
            set
            {
                deviceName = value;
                OnPropertyChanged();
            }
        }

        [Display.Toggle]
        public bool WiFi
        {
            get => wiFi;
            set
            {
                wiFi = value;
                OnPropertyChanged();
            }
        }

        [Display.Toggle]
        public bool Hotspot
        {
            get => hotspot;
            set
            {
                hotspot = value; 
                OnPropertyChanged();
            }
        }

        [Field(IsVisible = "{Binding Hotspot}")]
        public string HotspotName
        {
            get => hotspotName;
            set
            {
                hotspotName = value; 
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return "Settings";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
