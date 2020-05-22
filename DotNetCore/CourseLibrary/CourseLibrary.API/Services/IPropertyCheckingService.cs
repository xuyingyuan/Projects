namespace CourseLibrary.API.Services
{
    public interface IPropertyCheckingService
    {
        public bool TypeHasProperties<T>(string fields);
    }
}