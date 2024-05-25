namespace interpreter_pattern
{
    //A demonstration of the Interpreter pattern in C#
    internal class Program
    {
        static void Main(string[] args)
        {
            //Construct the 'parse tree'
            List<Expression> tree = 
                [
                new ThousandExpression(),
                new HundredExpression(),
                new TenExpression(),
                new OneExpression()
                ];

            //Create the context (i.e. roman value)
            var roman = "MCMXXVIII";
            var context = new Context { Input = roman };

            //Interpret
            tree.ForEach(e => e.Interpret(context));

            Console.WriteLine($"{roman} = {context.Output}");
        }
    }

    public record Context
    {
        public string Input { get; set; } = null!;
        public int Output { get; set; }
    }

    public abstract class Expression
    {
        public void Interpret(Context context)
        {
            if (context.Input.Length == 0)
                return;
            if (context.Input.StartsWith(Nine()))
            {
                context.Output += (9 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Four()))
            {
                context.Output += (4 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Five()))
            {
                context.Output += (5 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
            while (context.Input.StartsWith(One()))
            {
                context.Output += (1 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
        }
        public abstract string One();
        public abstract string Four();
        public abstract string Five();
        public abstract string Nine();
        public abstract int Multiplier();
    }

    //Thousand checks for the Roman Numeral M
    public class ThousandExpression : Expression
    {
        public override string One() => "M";
        public override string Four() => " ";
        public override string Five() => " ";
        public override string Nine() => " ";
        public override int Multiplier() => 1000;
    }

    //Hundred checks C, CD, D or CM
    public class HundredExpression : Expression
    {
        public override string One() => "C";
        public override string Four() => "CD";
        public override string Five() => "D";
        public override string Nine() => "CM";
        public override int Multiplier() => 100;
    }

    //Ten checks for X, XL, L and XC
    public class TenExpression : Expression
    {
        public override string One() => "X";
        public override string Four() => "XL";
        public override string Five() => "L";
        public override string Nine() => "XC";
        public override int Multiplier() => 10;
    }

    //One checks for I, II, III, IV, V, VI, VI, VII, VIII, IX
    public class OneExpression : Expression
    {
        public override string One() => "I";
        public override string Four() => "IV";
        public override string Five() => "V";
        public override string Nine() => "IX";
        public override int Multiplier() => 1;
    }
}
