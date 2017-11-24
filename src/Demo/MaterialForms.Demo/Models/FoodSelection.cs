using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialForms.Wpf.Annotations;

namespace MaterialForms.Demo.Models
{
    public class FoodSelection : INotifyPropertyChanged
    {
        private string firstFood = "Pizza";
        private string secondFood = "Steak";
        private string thirdFood = "Salad";
        private string yourFavoriteFood;

        [Value(Must.NotBeEmpty)]
        public string FirstFood
        {
            get => firstFood;
            set
            {
                firstFood = value;
                OnPropertyChanged();
            }
        }

        [Value(Must.NotBeEmpty)]
        public string SecondFood
        {
            get => secondFood;
            set
            {
                secondFood = value;
                OnPropertyChanged();
            }
        }

        [Value(Must.NotBeEmpty)]
        public string ThirdFood
        {
            get => thirdFood;
            set
            {
                thirdFood = value;
                OnPropertyChanged();
            }
        }

        [Text("You have selected {Binding YourFavoriteFood}", InsertAfter = true)]

        [SelectFrom(new[] { "{Binding FirstFood}, obviously.", "{Binding SecondFood} is best!", "I love {Binding ThirdFood}" })]
        public string YourFavoriteFood
        {
            get => yourFavoriteFood;
            set
            {
                yourFavoriteFood = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
