class Program {
    string name = "";

    Program(string name) {
        this.name = name;
    }

    void greet() {
        Console.WriteLine($"Hello {this.name}");
    }

    static void Main() {
        new Program("Rishikant").greet();
    }
}