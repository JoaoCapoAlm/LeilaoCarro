namespace LeilaoCarro.Data.ViewModels
{
    public class ExceptionVM
    {
        public ExceptionVM()
        {
            Title = "Aconteceu um erro inesperado";
            Errors = new Dictionary<string, IEnumerable<string>>();
        }

        public ExceptionVM(string title, int statusCode)
        {
            Title = title;
            StatusCode = statusCode;
            Errors = new Dictionary<string, IEnumerable<string>>();
        }

        public string Title { get; init; }
        public int StatusCode { get; init; }
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
