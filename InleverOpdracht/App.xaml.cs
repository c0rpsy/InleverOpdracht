namespace InleverOpdracht
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Makes the mainpage a navigationpage
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
