using DesignPatterns.Domain.Interfaces.Common;
using DesignPatterns.Domain.Entities.Common;

namespace DesignPatterns.Domain.Patterns.Behavioral.Visitor
{
    // Visitor Pattern - Define operations on object structure without changing classes

    // Visitor interfaces and implementations
    public interface IShapeVisitor : IVisitor
    {
        void Visit(Circle circle);
        void Visit(Rectangle rectangle);
        void Visit(Triangle triangle);
    }

    public interface IShapeElement : IElement
    {
        void Accept(IShapeVisitor visitor);
    }

    // Shape classes
    public abstract class Shape : IShapeElement
    {
        public string Color { get; set; } = "Black";
        public abstract void Accept(IShapeVisitor visitor);
        
        // Legacy Accept method for IElement interface
        public void Accept(IVisitor visitor)
        {
            if (visitor is IShapeVisitor shapeVisitor)
                Accept(shapeVisitor);
        }
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }
        public (double x, double y) Center { get; set; }

        public Circle(double radius, double x = 0, double y = 0)
        {
            Radius = radius;
            Center = (x, y);
        }

        public override void Accept(IShapeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Circle(r={Radius}, center=({Center.x},{Center.y}), color={Color})";
        }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public (double x, double y) TopLeft { get; set; }

        public Rectangle(double width, double height, double x = 0, double y = 0)
        {
            Width = width;
            Height = height;
            TopLeft = (x, y);
        }

        public override void Accept(IShapeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Rectangle({Width}x{Height}, topLeft=({TopLeft.x},{TopLeft.y}), color={Color})";
        }
    }

    public class Triangle : Shape
    {
        public (double x, double y) Point1 { get; set; }
        public (double x, double y) Point2 { get; set; }
        public (double x, double y) Point3 { get; set; }

        public Triangle((double x, double y) p1, (double x, double y) p2, (double x, double y) p3)
        {
            Point1 = p1;
            Point2 = p2;
            Point3 = p3;
        }

        public override void Accept(IShapeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Triangle(({Point1.x},{Point1.y}), ({Point2.x},{Point2.y}), ({Point3.x},{Point3.y}), color={Color})";
        }
    }

    // Concrete Visitors
    public class AreaCalculatorVisitor : IShapeVisitor
    {
        public double TotalArea { get; private set; }
        private readonly List<string> _calculations = new();

        public void Visit(Circle circle)
        {
            var area = Math.PI * circle.Radius * circle.Radius;
            TotalArea += area;
            var calculation = $"Circle area: π × {circle.Radius}² = {area:F2}";
            _calculations.Add(calculation);
            Console.WriteLine(calculation);
        }

        public void Visit(Rectangle rectangle)
        {
            var area = rectangle.Width * rectangle.Height;
            TotalArea += area;
            var calculation = $"Rectangle area: {rectangle.Width} × {rectangle.Height} = {area:F2}";
            _calculations.Add(calculation);
            Console.WriteLine(calculation);
        }

        public void Visit(Triangle triangle)
        {
            // Using Shoelace formula for triangle area
            var area = 0.5 * Math.Abs(
                (triangle.Point1.x * (triangle.Point2.y - triangle.Point3.y)) +
                (triangle.Point2.x * (triangle.Point3.y - triangle.Point1.y)) +
                (triangle.Point3.x * (triangle.Point1.y - triangle.Point2.y))
            );
            TotalArea += area;
            var calculation = $"Triangle area: {area:F2}";
            _calculations.Add(calculation);
            Console.WriteLine(calculation);
        }

        public void Visit(object element)
        {
            Console.WriteLine($"Unknown shape type: {element.GetType().Name}");
        }

        public List<string> GetCalculations() => new(_calculations);

        public void Reset()
        {
            TotalArea = 0;
            _calculations.Clear();
        }
    }

    public class PerimeterCalculatorVisitor : IShapeVisitor
    {
        public double TotalPerimeter { get; private set; }
        private readonly List<string> _calculations = new();

        public void Visit(Circle circle)
        {
            var perimeter = 2 * Math.PI * circle.Radius;
            TotalPerimeter += perimeter;
            var calculation = $"Circle perimeter: 2π × {circle.Radius} = {perimeter:F2}";
            _calculations.Add(calculation);
            Console.WriteLine(calculation);
        }

        public void Visit(Rectangle rectangle)
        {
            var perimeter = 2 * (rectangle.Width + rectangle.Height);
            TotalPerimeter += perimeter;
            var calculation = $"Rectangle perimeter: 2 × ({rectangle.Width} + {rectangle.Height}) = {perimeter:F2}";
            _calculations.Add(calculation);
            Console.WriteLine(calculation);
        }

        public void Visit(Triangle triangle)
        {
            var side1 = Distance(triangle.Point1, triangle.Point2);
            var side2 = Distance(triangle.Point2, triangle.Point3);
            var side3 = Distance(triangle.Point3, triangle.Point1);
            var perimeter = side1 + side2 + side3;
            TotalPerimeter += perimeter;
            var calculation = $"Triangle perimeter: {side1:F2} + {side2:F2} + {side3:F2} = {perimeter:F2}";
            _calculations.Add(calculation);
            Console.WriteLine(calculation);
        }

        public void Visit(object element)
        {
            Console.WriteLine($"Unknown shape type: {element.GetType().Name}");
        }

        private double Distance((double x, double y) p1, (double x, double y) p2)
        {
            return Math.Sqrt(Math.Pow(p2.x - p1.x, 2) + Math.Pow(p2.y - p1.y, 2));
        }

        public List<string> GetCalculations() => new(_calculations);

        public void Reset()
        {
            TotalPerimeter = 0;
            _calculations.Clear();
        }
    }

    public class DrawingVisitor : IShapeVisitor
    {
        private readonly List<string> _drawings = new();

        public void Visit(Circle circle)
        {
            var drawing = $"🔵 Drawing {circle.Color} circle at ({circle.Center.x},{circle.Center.y}) with radius {circle.Radius}";
            _drawings.Add(drawing);
            Console.WriteLine(drawing);
        }

        public void Visit(Rectangle rectangle)
        {
            var drawing = $"🟦 Drawing {rectangle.Color} rectangle at ({rectangle.TopLeft.x},{rectangle.TopLeft.y}) size {rectangle.Width}x{rectangle.Height}";
            _drawings.Add(drawing);
            Console.WriteLine(drawing);
        }

        public void Visit(Triangle triangle)
        {
            var drawing = $"🔺 Drawing {triangle.Color} triangle with vertices ({triangle.Point1.x},{triangle.Point1.y}), ({triangle.Point2.x},{triangle.Point2.y}), ({triangle.Point3.x},{triangle.Point3.y})";
            _drawings.Add(drawing);
            Console.WriteLine(drawing);
        }

        public void Visit(object element)
        {
            Console.WriteLine($"Cannot draw unknown shape: {element.GetType().Name}");
        }

        public List<string> GetDrawings() => new(_drawings);

        public void Clear()
        {
            _drawings.Clear();
        }
    }

    public class ColoringVisitor : IShapeVisitor
    {
        private readonly string _color;
        private readonly List<string> _coloringLog = new();

        public ColoringVisitor(string color)
        {
            _color = color;
        }

        public void Visit(Circle circle)
        {
            var oldColor = circle.Color;
            circle.Color = _color;
            var log = $"🎨 Changed circle color from {oldColor} to {_color}";
            _coloringLog.Add(log);
            Console.WriteLine(log);
        }

        public void Visit(Rectangle rectangle)
        {
            var oldColor = rectangle.Color;
            rectangle.Color = _color;
            var log = $"🎨 Changed rectangle color from {oldColor} to {_color}";
            _coloringLog.Add(log);
            Console.WriteLine(log);
        }

        public void Visit(Triangle triangle)
        {
            var oldColor = triangle.Color;
            triangle.Color = _color;
            var log = $"🎨 Changed triangle color from {oldColor} to {_color}";
            _coloringLog.Add(log);
            Console.WriteLine(log);
        }

        public void Visit(object element)
        {
            Console.WriteLine($"Cannot color unknown shape: {element.GetType().Name}");
        }

        public List<string> GetColoringLog() => new(_coloringLog);
    }

    // Document structure for another visitor example
    public interface IDocumentElement : IElement
    {
        void Accept(IDocumentVisitor visitor);
    }

    public interface IDocumentVisitor : IVisitor
    {
        void Visit(Document document);
        void Visit(Paragraph paragraph);
        void Visit(Sentence sentence);
        void Visit(Word word);
    }

    public class Document : IDocumentElement
    {
        public string Title { get; set; }
        public List<Paragraph> Paragraphs { get; set; } = new();

        public Document(string title)
        {
            Title = title;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var paragraph in Paragraphs)
            {
                paragraph.Accept(visitor);
            }
        }

        public void Accept(IVisitor visitor)
        {
            if (visitor is IDocumentVisitor docVisitor)
                Accept(docVisitor);
        }
    }

    public class Paragraph : IDocumentElement
    {
        public List<Sentence> Sentences { get; set; } = new();

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var sentence in Sentences)
            {
                sentence.Accept(visitor);
            }
        }

        public void Accept(IVisitor visitor)
        {
            if (visitor is IDocumentVisitor docVisitor)
                Accept(docVisitor);
        }
    }

    public class Sentence : IDocumentElement
    {
        public List<Word> Words { get; set; } = new();

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var word in Words)
            {
                word.Accept(visitor);
            }
        }

        public void Accept(IVisitor visitor)
        {
            if (visitor is IDocumentVisitor docVisitor)
                Accept(docVisitor);
        }
    }

    public class Word : IDocumentElement
    {
        public string Text { get; set; }

        public Word(string text)
        {
            Text = text;
        }

        public void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Accept(IVisitor visitor)
        {
            if (visitor is IDocumentVisitor docVisitor)
                Accept(docVisitor);
        }
    }

    public class WordCountVisitor : IDocumentVisitor
    {
        public int DocumentCount { get; private set; }
        public int ParagraphCount { get; private set; }
        public int SentenceCount { get; private set; }
        public int WordCount { get; private set; }

        public void Visit(Document document)
        {
            DocumentCount++;
            Console.WriteLine($"📄 Processing document: {document.Title}");
        }

        public void Visit(Paragraph paragraph)
        {
            ParagraphCount++;
            Console.WriteLine($"📝 Processing paragraph {ParagraphCount}");
        }

        public void Visit(Sentence sentence)
        {
            SentenceCount++;
            Console.WriteLine($"📋 Processing sentence {SentenceCount}");
        }

        public void Visit(Word word)
        {
            WordCount++;
            Console.WriteLine($"🔤 Processing word: '{word.Text}'");
        }

        public void Visit(object element)
        {
            Console.WriteLine($"Unknown document element: {element.GetType().Name}");
        }

        public string GetStatistics()
        {
            return $"Document Statistics:\n" +
                   $"- Documents: {DocumentCount}\n" +
                   $"- Paragraphs: {ParagraphCount}\n" +
                   $"- Sentences: {SentenceCount}\n" +
                   $"- Words: {WordCount}";
        }
    }

    // Visitor Service
    public class VisitorService
    {
        public List<string> DemonstrateShapeVisitors()
        {
            var results = new List<string>();
            
            // Create shapes
            var shapes = new List<Shape>
            {
                new Circle(5, 0, 0) { Color = "Red" },
                new Rectangle(10, 6, 2, 3) { Color = "Blue" },
                new Triangle((0, 0), (4, 0), (2, 3)) { Color = "Green" },
                new Circle(3, 10, 10) { Color = "Yellow" }
            };

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            Console.WriteLine("=== Shape Collection ===");
            foreach (var shape in shapes)
            {
                Console.WriteLine(shape);
            }

            // Calculate areas
            Console.WriteLine("\n=== Area Calculations ===");
            var areaVisitor = new AreaCalculatorVisitor();
            foreach (var shape in shapes)
            {
                shape.Accept(areaVisitor);
            }
            Console.WriteLine($"Total Area: {areaVisitor.TotalArea:F2}");

            // Calculate perimeters
            Console.WriteLine("\n=== Perimeter Calculations ===");
            var perimeterVisitor = new PerimeterCalculatorVisitor();
            foreach (var shape in shapes)
            {
                shape.Accept(perimeterVisitor);
            }
            Console.WriteLine($"Total Perimeter: {perimeterVisitor.TotalPerimeter:F2}");

            // Draw shapes
            Console.WriteLine("\n=== Drawing Shapes ===");
            var drawingVisitor = new DrawingVisitor();
            foreach (var shape in shapes)
            {
                shape.Accept(drawingVisitor);
            }

            // Change colors
            Console.WriteLine("\n=== Coloring Shapes ===");
            var coloringVisitor = new ColoringVisitor("Purple");
            foreach (var shape in shapes)
            {
                shape.Accept(coloringVisitor);
            }

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }

        public List<string> DemonstrateDocumentVisitor()
        {
            var results = new List<string>();

            // Create document structure
            var document = new Document("Design Patterns Guide");
            
            var intro = new Paragraph();
            intro.Sentences.Add(new Sentence 
            { 
                Words = { new Word("Design"), new Word("patterns"), new Word("are"), new Word("important") }
            });
            intro.Sentences.Add(new Sentence 
            { 
                Words = { new Word("They"), new Word("provide"), new Word("reusable"), new Word("solutions") }
            });
            
            var visitor = new Paragraph();
            visitor.Sentences.Add(new Sentence 
            { 
                Words = { new Word("Visitor"), new Word("pattern"), new Word("separates"), new Word("algorithms"), new Word("from"), new Word("objects") }
            });

            document.Paragraphs.Add(intro);
            document.Paragraphs.Add(visitor);

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            Console.WriteLine("=== Document Analysis ===");
            var wordCountVisitor = new WordCountVisitor();
            document.Accept(wordCountVisitor);
            
            Console.WriteLine("\n" + wordCountVisitor.GetStatistics());

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }

        public List<string> CreateCustomShapeScenario(List<(string type, Dictionary<string, object> parameters)> shapeData)
        {
            var results = new List<string>();
            var shapes = new List<Shape>();

            // Create shapes from data
            foreach (var (type, parameters) in shapeData)
            {
                Shape? shape = type.ToLower() switch
                {
                    "circle" => new Circle(
                        (double)parameters.GetValueOrDefault("radius", 5.0),
                        (double)parameters.GetValueOrDefault("x", 0.0),
                        (double)parameters.GetValueOrDefault("y", 0.0)
                    ),
                    "rectangle" => new Rectangle(
                        (double)parameters.GetValueOrDefault("width", 10.0),
                        (double)parameters.GetValueOrDefault("height", 5.0),
                        (double)parameters.GetValueOrDefault("x", 0.0),
                        (double)parameters.GetValueOrDefault("y", 0.0)
                    ),
                    "triangle" => new Triangle(
                        ((double)parameters.GetValueOrDefault("x1", 0.0), (double)parameters.GetValueOrDefault("y1", 0.0)),
                        ((double)parameters.GetValueOrDefault("x2", 5.0), (double)parameters.GetValueOrDefault("y2", 0.0)),
                        ((double)parameters.GetValueOrDefault("x3", 2.5), (double)parameters.GetValueOrDefault("y3", 5.0))
                    ),
                    _ => null
                };

                if (shape != null)
                {
                    if (parameters.ContainsKey("color"))
                        shape.Color = parameters["color"].ToString() ?? "Black";
                    shapes.Add(shape);
                }
            }

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            Console.WriteLine("=== Custom Shape Analysis ===");
            
            // Apply all visitors
            var areaVisitor = new AreaCalculatorVisitor();
            var perimeterVisitor = new PerimeterCalculatorVisitor();
            var drawingVisitor = new DrawingVisitor();

            foreach (var shape in shapes)
            {
                Console.WriteLine($"\nAnalyzing: {shape}");
                shape.Accept(areaVisitor);
                shape.Accept(perimeterVisitor);
                shape.Accept(drawingVisitor);
            }

            Console.WriteLine($"\nSummary:");
            Console.WriteLine($"Total shapes: {shapes.Count}");
            Console.WriteLine($"Total area: {areaVisitor.TotalArea:F2}");
            Console.WriteLine($"Total perimeter: {perimeterVisitor.TotalPerimeter:F2}");

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }
    }
}