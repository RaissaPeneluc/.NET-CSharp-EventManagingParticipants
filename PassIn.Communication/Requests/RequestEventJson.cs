namespace PassIn.Communication.Requests;

// Todo evento criado é preciso um título, detalhes e um número máximo de participantes
public class RequestEventJson
{
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public int MaximumAttendees { get; set; }
}
