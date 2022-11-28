namespace Controller.Exceptions;


public class PathException : Exception
{
    public string Path { get; set; }
    public override string Message => $"O caminho {Path} não existe.";


    public PathException(string path)
    {
        Path = path;
    }
}
