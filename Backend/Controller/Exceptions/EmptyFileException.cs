namespace Controller.Exceptions;


public class EmptyFileException : Exception
{
    public override string Message => $"O arquivo {FileName} está vazio.";
    public string FileName { get; set; }


    public EmptyFileException(string fileName)
    {
        FileName = fileName;
    }
}
