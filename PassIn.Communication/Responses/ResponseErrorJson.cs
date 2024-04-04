namespace PassIn.Communication.Responses;
public class ResponseErrorJson
{
    public string Message { get; set; } = string.Empty;

    // Forçando que sempre que tiver um new nessa classe é preciso passar uma string
    public ResponseErrorJson(string message)
    {
        Message = message;
    }
}
