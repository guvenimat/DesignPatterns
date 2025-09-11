using DesignPatterns.Domain.Interfaces.Structural;

namespace DesignPatterns.Domain.Patterns.Structural.Composite
{
    // Composite Pattern - Tree structure of objects

    // Leaf - File
    public class File : IFileSystemItem
    {
        public string Name { get; private set; }
        public int Size { get; private set; }

        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public void Display(int depth = 0)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}📄 {Name} ({Size} KB)");
        }
    }

    // Composite - Folder
    public class Folder : IFileSystemItem
    {
        private readonly List<IFileSystemItem> _items = new();
        
        public string Name { get; private set; }
        public int Size => _items.Sum(item => item.Size);

        public Folder(string name)
        {
            Name = name;
        }

        public void Add(IFileSystemItem item)
        {
            _items.Add(item);
        }

        public void Remove(IFileSystemItem item)
        {
            _items.Remove(item);
        }

        public void Display(int depth = 0)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}📁 {Name}/ ({Size} KB total)");
            foreach (var item in _items)
            {
                item.Display(depth + 1);
            }
        }

        public List<IFileSystemItem> GetItems()
        {
            return new List<IFileSystemItem>(_items);
        }
    }

    // File System Service
    public class FileSystemService
    {
        public IFileSystemItem CreateSampleFileSystem()
        {
            var root = new Folder("Root");
            
            // Documents folder
            var documents = new Folder("Documents");
            documents.Add(new File("resume.pdf", 150));
            documents.Add(new File("cover_letter.docx", 75));
            
            // Photos folder
            var photos = new Folder("Photos");
            photos.Add(new File("vacation1.jpg", 2048));
            photos.Add(new File("vacation2.jpg", 1856));
            
            // Work subfolder in Photos
            var work = new Folder("Work");
            work.Add(new File("presentation.pptx", 3072));
            work.Add(new File("meeting_notes.txt", 25));
            photos.Add(work);
            
            root.Add(documents);
            root.Add(photos);
            root.Add(new File("readme.txt", 10));
            
            return root;
        }

        public string GetFileSystemInfo(IFileSystemItem item)
        {
            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);
            
            item.Display();
            
            Console.SetOut(originalOut);
            return stringWriter.ToString();
        }

        public int CalculateTotalSize(IFileSystemItem item)
        {
            return item.Size;
        }

        public List<string> FindFiles(IFileSystemItem item, string extension)
        {
            var files = new List<string>();
            
            if (item is File file && file.Name.EndsWith(extension))
            {
                files.Add(file.Name);
            }
            else if (item is Folder folder)
            {
                foreach (var subItem in folder.GetItems())
                {
                    files.AddRange(FindFiles(subItem, extension));
                }
            }
            
            return files;
        }
    }
}