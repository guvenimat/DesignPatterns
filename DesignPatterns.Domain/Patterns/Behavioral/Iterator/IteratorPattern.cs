using DesignPatterns.Domain.Interfaces.Common;

namespace DesignPatterns.Domain.Patterns.Behavioral.Iterator
{
    // Iterator Pattern - Provide way to access elements sequentially without exposing structure

    // Concrete Iterator for Books
    public class BookIterator : IIterator<Book>
    {
        private readonly List<Book> _books;
        private int _position = 0;

        public BookIterator(List<Book> books)
        {
            _books = books;
        }

        public bool HasNext()
        {
            return _position < _books.Count;
        }

        public Book Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No more elements");
            
            return _books[_position++];
        }

        public void Reset()
        {
            _position = 0;
        }
    }

    // Book class
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"'{Title}' by {Author} ({Year}) - {Genre} - ${Price:F2}";
        }
    }

    // Concrete Aggregate
    public class BookCollection : IAggregate<Book>
    {
        private readonly List<Book> _books = new();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            _books.Remove(book);
        }

        public IIterator<Book> CreateIterator()
        {
            return new BookIterator(_books);
        }

        public IIterator<Book> CreateGenreIterator(string genre)
        {
            var filteredBooks = _books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
            return new BookIterator(filteredBooks);
        }

        public IIterator<Book> CreateAuthorIterator(string author)
        {
            var filteredBooks = _books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
            return new BookIterator(filteredBooks);
        }

        public int Count => _books.Count;

        public List<Book> GetBooks() => new(_books);
    }

    // Tree structure with custom iterator
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; } = new();

        public TreeNode(T value)
        {
            Value = value;
        }

        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }
    }

    public class TreeIterator<T> : IIterator<T>
    {
        private readonly Queue<TreeNode<T>> _queue = new();
        private readonly Stack<TreeNode<T>> _stack = new();
        private readonly bool _breadthFirst;

        public TreeIterator(TreeNode<T> root, bool breadthFirst = true)
        {
            _breadthFirst = breadthFirst;
            if (breadthFirst)
            {
                _queue.Enqueue(root);
            }
            else
            {
                _stack.Push(root);
            }
        }

        public bool HasNext()
        {
            return _breadthFirst ? _queue.Count > 0 : _stack.Count > 0;
        }

        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("No more elements");

            TreeNode<T> current;
            
            if (_breadthFirst)
            {
                current = _queue.Dequeue();
                foreach (var child in current.Children)
                {
                    _queue.Enqueue(child);
                }
            }
            else
            {
                current = _stack.Pop();
                for (int i = current.Children.Count - 1; i >= 0; i--)
                {
                    _stack.Push(current.Children[i]);
                }
            }

            return current.Value;
        }
    }

    public class Tree<T> : IAggregate<T>
    {
        private TreeNode<T>? _root;

        public Tree(T rootValue)
        {
            _root = new TreeNode<T>(rootValue);
        }

        public TreeNode<T>? Root => _root;

        public IIterator<T> CreateIterator()
        {
            if (_root == null) throw new InvalidOperationException("Tree is empty");
            return new TreeIterator<T>(_root, true); // Breadth-first by default
        }

        public IIterator<T> CreateDepthFirstIterator()
        {
            if (_root == null) throw new InvalidOperationException("Tree is empty");
            return new TreeIterator<T>(_root, false);
        }
    }

    // Iterator Service
    public class IteratorService
    {
        public List<string> DemonstrateBookCollection()
        {
            var results = new List<string>();
            var bookCollection = new BookCollection();

            // Add sample books
            var books = new List<Book>
            {
                new() { Id = 1, Title = "Clean Code", Author = "Robert Martin", Genre = "Programming", Year = 2008, Price = 39.99m },
                new() { Id = 2, Title = "Design Patterns", Author = "Gang of Four", Genre = "Programming", Year = 1994, Price = 49.99m },
                new() { Id = 3, Title = "The Hobbit", Author = "J.R.R. Tolkien", Genre = "Fantasy", Year = 1937, Price = 14.99m },
                new() { Id = 4, Title = "1984", Author = "George Orwell", Genre = "Dystopian", Year = 1949, Price = 12.99m },
                new() { Id = 5, Title = "Refactoring", Author = "Martin Fowler", Genre = "Programming", Year = 1999, Price = 44.99m },
                new() { Id = 6, Title = "Lord of the Rings", Author = "J.R.R. Tolkien", Genre = "Fantasy", Year = 1954, Price = 29.99m }
            };

            foreach (var book in books)
            {
                bookCollection.AddBook(book);
            }

            results.Add("=== Complete Book Collection ===");
            var iterator = bookCollection.CreateIterator();
            while (iterator.HasNext())
            {
                results.Add($"📚 {iterator.Next()}");
            }

            results.Add("\n=== Programming Books Only ===");
            var programmingIterator = bookCollection.CreateGenreIterator("Programming");
            while (programmingIterator.HasNext())
            {
                results.Add($"💻 {programmingIterator.Next()}");
            }

            results.Add("\n=== Books by J.R.R. Tolkien ===");
            var tolkienIterator = bookCollection.CreateAuthorIterator("J.R.R. Tolkien");
            while (tolkienIterator.HasNext())
            {
                results.Add($"🧙 {tolkienIterator.Next()}");
            }

            results.Add($"\nTotal books in collection: {bookCollection.Count}");

            return results;
        }

        public List<string> DemonstrateTreeIteration()
        {
            var results = new List<string>();

            // Create a file system tree
            var tree = new Tree<string>("Root");
            var documents = new TreeNode<string>("Documents");
            var photos = new TreeNode<string>("Photos");
            var music = new TreeNode<string>("Music");

            // Add children to documents
            documents.AddChild(new TreeNode<string>("Resume.pdf"));
            documents.AddChild(new TreeNode<string>("Cover_Letter.docx"));
            
            var projects = new TreeNode<string>("Projects");
            projects.AddChild(new TreeNode<string>("ProjectA"));
            projects.AddChild(new TreeNode<string>("ProjectB"));
            documents.AddChild(projects);

            // Add children to photos
            photos.AddChild(new TreeNode<string>("Vacation"));
            photos.AddChild(new TreeNode<string>("Family"));

            // Add children to music
            music.AddChild(new TreeNode<string>("Rock"));
            music.AddChild(new TreeNode<string>("Classical"));

            // Add main folders to root
            tree.Root!.AddChild(documents);
            tree.Root.AddChild(photos);
            tree.Root.AddChild(music);

            results.Add("=== Breadth-First Traversal ===");
            var breadthFirstIterator = tree.CreateIterator();
            while (breadthFirstIterator.HasNext())
            {
                results.Add($"📁 {breadthFirstIterator.Next()}");
            }

            results.Add("\n=== Depth-First Traversal ===");
            var depthFirstIterator = tree.CreateDepthFirstIterator();
            while (depthFirstIterator.HasNext())
            {
                results.Add($"📁 {depthFirstIterator.Next()}");
            }

            return results;
        }

        public List<string> CreateCustomBookCollection(List<(string title, string author, string genre, int year, decimal price)> bookData)
        {
            var results = new List<string>();
            var collection = new BookCollection();

            foreach (var (title, author, genre, year, price) in bookData)
            {
                collection.AddBook(new Book
                {
                    Id = collection.Count + 1,
                    Title = title,
                    Author = author,
                    Genre = genre,
                    Year = year,
                    Price = price
                });
            }

            results.Add("=== Custom Book Collection ===");
            var iterator = collection.CreateIterator();
            while (iterator.HasNext())
            {
                results.Add($"📚 {iterator.Next()}");
            }

            // Group by genre
            var genres = collection.GetBooks().Select(b => b.Genre).Distinct().ToList();
            foreach (var genre in genres)
            {
                results.Add($"\n=== {genre} Books ===");
                var genreIterator = collection.CreateGenreIterator(genre);
                while (genreIterator.HasNext())
                {
                    results.Add($"📖 {genreIterator.Next()}");
                }
            }

            return results;
        }
    }
}