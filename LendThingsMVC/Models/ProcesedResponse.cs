namespace LendThingsMVC.Models
{
    public class ProcesedResponse<T>
    {
        public string? PersonalizedError { get; set; }
        public T? Body { get; set; }
        public HttpResponseMessage Response { get; set; }

        public ProcesedResponse()
        {
        }

        public ProcesedResponse(string? personalizedError, T? body, HttpResponseMessage response)
        {
            PersonalizedError = personalizedError;
            Body = body;
            Response = response;
        }
    }
}
