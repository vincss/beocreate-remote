namespace BeocreateRemote.Pages;

public partial class ConfigurationPage : ContentPage
{
    public ConfigurationPage(ConfigurationViewModel configurationViewModel)
    {
        InitializeComponent();
        BindingContext = configurationViewModel;

    }
}