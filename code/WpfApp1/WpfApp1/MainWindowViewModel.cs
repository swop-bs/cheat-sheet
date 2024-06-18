using System.ComponentModel;

namespace WpfApp1
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // Private Variable für den Label-Inhalt
        private string labelTest = "TestLabel";

        // Öffentliche Eigenschaft für den Label-Inhalt
        public string LabelTest
        {
            get => labelTest;
            set
            {
                labelTest = value;
                OnPropertyChanged(nameof(LabelTest));
            }
        }

        public void ChangeText()
        {
            LabelTest = "Text Changed!!!";
        }

        // Deklarieren des PropertyChanged-Ereignisses
        public event PropertyChangedEventHandler? PropertyChanged;

        // OnPropertyChanged löst das PropertyChanged-Ereignis aus und übergibt
        // die Quelleigenschaft, die aktualisiert wird.
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
