using DesignPatterns.Domain.Interfaces.Structural;

namespace DesignPatterns.Domain.Patterns.Structural.Flyweight
{
    // Flyweight Pattern - Minimize memory usage for large numbers of objects

    // Intrinsic state (shared)
    public class CharacterFlyweight : ICharacterFlyweight
    {
        private readonly char _character;
        private readonly string _font;
        private readonly int _size;

        public CharacterFlyweight(char character, string font, int size)
        {
            _character = character;
            _font = font;
            _size = size;
        }

        // Extrinsic state passed as parameters
        public void Render(int x, int y, string color)
        {
            Console.WriteLine($"Rendering '{_character}' at ({x},{y}) in {color} using {_font} size {_size}");
        }

        public override string ToString()
        {
            return $"Character: '{_character}', Font: {_font}, Size: {_size}";
        }
    }

    // Flyweight Factory
    public class CharacterFlyweightFactory
    {
        private readonly Dictionary<string, ICharacterFlyweight> _flyweights = new();

        public ICharacterFlyweight GetCharacterFlyweight(char character, string font, int size)
        {
            string key = $"{character}_{font}_{size}";
            
            if (!_flyweights.ContainsKey(key))
            {
                _flyweights[key] = new CharacterFlyweight(character, font, size);
                Console.WriteLine($"Created new flyweight for: {key}");
            }
            else
            {
                Console.WriteLine($"Reusing existing flyweight for: {key}");
            }

            return _flyweights[key];
        }

        public int GetFlyweightCount()
        {
            return _flyweights.Count;
        }

        public List<string> GetAllFlyweights()
        {
            return _flyweights.Values.Select(f => f.ToString()).ToList();
        }
    }

    // Context class that uses flyweights
    public class TextDocument
    {
        private readonly List<CharacterContext> _characters = new();
        private readonly CharacterFlyweightFactory _factory;

        public TextDocument(CharacterFlyweightFactory factory)
        {
            _factory = factory;
        }

        public void AddCharacter(char character, int x, int y, string color, string font = "Arial", int size = 12)
        {
            var flyweight = _factory.GetCharacterFlyweight(character, font, size);
            _characters.Add(new CharacterContext(flyweight, x, y, color));
        }

        public void Render()
        {
            Console.WriteLine("\n--- Rendering Document ---");
            foreach (var charContext in _characters)
            {
                charContext.Render();
            }
            Console.WriteLine($"Total characters: {_characters.Count}");
            Console.WriteLine($"Flyweight objects created: {_factory.GetFlyweightCount()}");
        }

        public string GetDocumentInfo()
        {
            return $"Document contains {_characters.Count} characters using {_factory.GetFlyweightCount()} flyweight objects";
        }
    }

    // Context that holds extrinsic state
    public class CharacterContext
    {
        private readonly ICharacterFlyweight _flyweight;
        private readonly int _x;
        private readonly int _y;
        private readonly string _color;

        public CharacterContext(ICharacterFlyweight flyweight, int x, int y, string color)
        {
            _flyweight = flyweight;
            _x = x;
            _y = y;
            _color = color;
        }

        public void Render()
        {
            _flyweight.Render(_x, _y, _color);
        }
    }

    // Service to demonstrate flyweight pattern
    public class TextEditorService
    {
        private readonly CharacterFlyweightFactory _factory;

        public TextEditorService()
        {
            _factory = new CharacterFlyweightFactory();
        }

        public TextDocument CreateSampleDocument()
        {
            var document = new TextDocument(_factory);
            
            // Add text "HELLO WORLD" with repeated characters
            string text = "HELLO WORLD";
            int x = 0;
            
            foreach (char c in text)
            {
                if (c != ' ')
                {
                    document.AddCharacter(c, x, 0, "Black", "Arial", 12);
                }
                x += 10;
            }

            // Add same text again in different color (reuses flyweights)
            x = 0;
            foreach (char c in text)
            {
                if (c != ' ')
                {
                    document.AddCharacter(c, x, 20, "Red", "Arial", 12);
                }
                x += 10;
            }

            return document;
        }

        public string RenderDocument()
        {
            var document = CreateSampleDocument();
            
            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);
            
            document.Render();
            
            Console.SetOut(originalOut);
            return stringWriter.ToString();
        }

        public List<string> GetFlyweightInfo()
        {
            CreateSampleDocument(); // Create some flyweights
            return _factory.GetAllFlyweights();
        }
    }
}