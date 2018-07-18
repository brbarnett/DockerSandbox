namespace DotNetStandardLib
{
    public interface IHomePageHeaderService
    {
        string GetHomePageHeaderText();
    }

    public class HomePageHeaderService : IHomePageHeaderService
    {
        public string GetHomePageHeaderText()
        {
            return "Running ASP.NET Core MVC in containers!";
        }
    }
}
