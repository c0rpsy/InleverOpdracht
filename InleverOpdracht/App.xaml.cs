namespace InleverOpdracht
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Use a NavigationPage
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
