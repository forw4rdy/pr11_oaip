Console.WriteLine("Задание 1. Перегрузка методов - класс Fraction");

Console.Write("Введите числитель первой дроби: ");
int chislitel1 = int.Parse(Console.ReadLine()!);
Console.Write("Введите знаменатель первой дроби: ");
int znamenatel1 = int.Parse(Console.ReadLine()!);
Fraction drob1 = new Fraction(chislitel1, znamenatel1);

Console.Write("Введите числитель второй дроби: ");
int chislitel2 = int.Parse(Console.ReadLine()!);
Console.Write("Введите знаменатель второй дроби: ");
int znamenatel2 = int.Parse(Console.ReadLine()!);
Fraction drob2 = new Fraction(chislitel2, znamenatel2);

Console.WriteLine($"Первая дробь: {drob1}, вторая дробь: {drob2}");
Console.WriteLine($"Сумма дробей: {drob1.Add(drob2)}, дробь плюс целое число: {drob1.Add(3)}, дробь плюс вещественное число: {drob1.Add(0.5)}");
Console.WriteLine($"Статическая версия сложения: {Fraction.Add(drob1, drob2)}");
Console.WriteLine();

Console.WriteLine("Задание 2. Перегрузка операторов - класс Vector2D");

Console.Write("Введите X вектора: ");
double x = double.Parse(Console.ReadLine()!);
Console.Write("Введите Y вектора: ");
double y = double.Parse(Console.ReadLine()!);
Vector2D vektor1 = new Vector2D(x, y);
Vector2D vektor2 = new Vector2D(2, 3);

Vector2D uvelichennyy = ++vektor1;
Vector2D umenshennyy = --vektor1;

Console.WriteLine($"Первый вектор: {vektor1}, второй вектор: {vektor2}, длина первого: {vektor1.Length:F2}");
Console.WriteLine($"Сумма: {vektor1 + vektor2}, разность: {vektor1 - vektor2}, умножение на число: {vektor1 * 2}, 2 * вектор: {2 * vektor1}, скалярное произведение: {vektor1 * vektor2}");
Console.WriteLine($"Отрицание: {-vektor1}, инкремент: {uvelichennyy}, декремент: {umenshennyy}, вектор не изменился: {vektor1}");
Console.WriteLine($"Сравнения: == {vektor1 == vektor2}, != {vektor1 != vektor2}, < {vektor1 < vektor2}, > {vektor1 > vektor2}, <= {vektor1 <= vektor2}, >= {vektor1 >= vektor2}");
Console.WriteLine();

Console.WriteLine("Задание 3. Перегрузка преобразований типов - класс Temperature");

Console.Write("Введите температуру в градусах Цельсия: ");
double celsiy = double.Parse(Console.ReadLine()!);
Temperature temperatura = celsiy;
double farengeit = temperatura.Fahrenheit;
double kelvin = temperatura.Kelvin;
string tekst = (string)temperatura;

Console.WriteLine($"Температура: {temperatura}, Фаренгейт: {farengeit:F2}, Кельвин: {kelvin:F2}");
Console.WriteLine($"Преобразование в double: {(double)temperatura}, в строку: {tekst}, обратно в Temperature: {(Temperature)tekst}");

class Fraction
{
    private int _numerator;
    private int _denominator;

    public Fraction(int numerator, int denominator)
    {
        _numerator = numerator;
        _denominator = denominator;
    }

    public Fraction(int numerator) : this(numerator, 1) { }
    public Fraction() : this(0, 1) { }

    public Fraction Add(Fraction other) => new Fraction(_numerator * other._denominator + other._numerator * _denominator, _denominator * other._denominator).Reduce();
    public Fraction Add(int number) => new Fraction(_numerator + number * _denominator, _denominator).Reduce();
    public Fraction Add(double number) => Add(new Fraction((int)Math.Round(number * 100), 100));
    public static Fraction Add(Fraction first, Fraction second) => first.Add(second);

    public Fraction Reduce()
    {
        for (int divisor = _denominator; divisor > 1; divisor--)
            if (_numerator % divisor == 0 && _denominator % divisor == 0)
                return new Fraction(_numerator / divisor, _denominator / divisor);
        return this;
    }

    public override string ToString() => $"{_numerator}/{_denominator}";
}

class Vector2D
{
    public double X { get; }
    public double Y { get; }

    public Vector2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double Length => Math.Sqrt(X * X + Y * Y);
    public override string ToString() => $"({X}; {Y})";

    public static Vector2D operator +(Vector2D first, Vector2D second) => new Vector2D(first.X + second.X, first.Y + second.Y);
    public static Vector2D operator -(Vector2D first, Vector2D second) => new Vector2D(first.X - second.X, first.Y - second.Y);
    public static Vector2D operator *(Vector2D vector, double number) => new Vector2D(vector.X * number, vector.Y * number);
    public static Vector2D operator *(double number, Vector2D vector) => vector * number;
    public static double operator *(Vector2D first, Vector2D second) => first.X * second.X + first.Y * second.Y;
    public static Vector2D operator -(Vector2D vector) => new Vector2D(-vector.X, -vector.Y);
    public static Vector2D operator ++(Vector2D vector) => new Vector2D(vector.X + 1, vector.Y + 1);
    public static Vector2D operator --(Vector2D vector) => new Vector2D(vector.X - 1, vector.Y - 1);
    public static bool operator ==(Vector2D first, Vector2D second) => first.X == second.X && first.Y == second.Y;
    public static bool operator !=(Vector2D first, Vector2D second) => !(first == second);
    public static bool operator <(Vector2D first, Vector2D second) => first.Length < second.Length;
    public static bool operator >(Vector2D first, Vector2D second) => first.Length > second.Length;
    public static bool operator <=(Vector2D first, Vector2D second) => first.Length <= second.Length;
    public static bool operator >=(Vector2D first, Vector2D second) => first.Length >= second.Length;

    public override bool Equals(object? obj) => obj is Vector2D vector && this == vector;
    public override int GetHashCode() => HashCode.Combine(X, Y);
}

class Temperature
{
    public double Celsius { get; }

    public Temperature(double celsius)
    {
        Celsius = celsius;
    }

    public double Fahrenheit => Celsius * 9 / 5 + 32;
    public double Kelvin => Celsius + 273.15;
    public override string ToString() => $"{Celsius:F2} °C";

    public static implicit operator Temperature(double celsius) => new Temperature(celsius);
    public static explicit operator double(Temperature temperature) => temperature.Celsius;
    public static explicit operator string(Temperature temperature) => temperature.ToString();
    public static explicit operator Temperature(string text) => new Temperature(double.Parse(text.Replace(" °C", "")));
}
