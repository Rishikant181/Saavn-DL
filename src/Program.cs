using Newtonsoft.Json;

class Program {
    /// <summary>
    /// The global HttpClient to be used.
    /// </summary>
    public static readonly HttpClient client = new HttpClient();

    /// <summary>
    /// Application entry point.
    /// </summary>
    static void Main() {
        Console.WriteLine("Hello World!");
    }
}