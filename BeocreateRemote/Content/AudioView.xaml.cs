namespace BeocreateRemote.Content;



public partial class AudioView : ContentView
{

    public AudioView()
    {
        InitializeComponent();
        BindingContext = new AudioViewModel();
    }
}