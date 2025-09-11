using DesignPatterns.Domain.Interfaces.Structural;

namespace DesignPatterns.Domain.Patterns.Structural.Proxy
{
    // Proxy Pattern - Control access to another object

    // Real subject - Expensive to create/load
    public class RealImage : IImageLoader
    {
        private readonly string _filename;
        private readonly int _size;

        public RealImage(string filename, int size)
        {
            _filename = filename;
            _size = size;
            LoadFromDisk();
        }

        private void LoadFromDisk()
        {
            Console.WriteLine($"Loading {_filename} ({_size}MB) from disk... This is expensive!");
            // Simulate expensive loading operation
            Thread.Sleep(100);
        }

        public void Display()
        {
            Console.WriteLine($"Displaying {_filename} ({_size}MB)");
        }

        public string GetImageInfo()
        {
            return $"Real Image: {_filename} - {_size}MB";
        }
    }

    // Virtual Proxy - Lazy loading
    public class ImageProxy : IImageLoader
    {
        private RealImage? _realImage;
        private readonly string _filename;
        private readonly int _size;

        public ImageProxy(string filename, int size)
        {
            _filename = filename;
            _size = size;
        }

        public void Display()
        {
            if (_realImage == null)
            {
                Console.WriteLine($"Proxy: First time accessing {_filename}, loading real image...");
                _realImage = new RealImage(_filename, _size);
            }
            else
            {
                Console.WriteLine($"Proxy: Using cached image for {_filename}");
            }
            
            _realImage.Display();
        }

        public string GetImageInfo()
        {
            return $"Image Proxy: {_filename} - {_size}MB {(_realImage != null ? "(Loaded)" : "(Not Loaded)")}";
        }
    }

    // Protection Proxy - Access control
    public class SecureImageProxy : IImageLoader
    {
        private readonly RealImage _realImage;
        private readonly string _userRole;
        private readonly bool _isSecureImage;

        public SecureImageProxy(string filename, int size, string userRole, bool isSecureImage = false)
        {
            _realImage = new RealImage(filename, size);
            _userRole = userRole;
            _isSecureImage = isSecureImage;
        }

        public void Display()
        {
            if (_isSecureImage && _userRole != "Admin")
            {
                Console.WriteLine("Access denied: Insufficient privileges to view secure image");
                return;
            }

            Console.WriteLine($"Access granted for user role: {_userRole}");
            _realImage.Display();
        }

        public string GetImageInfo()
        {
            return $"Secure Image Proxy: {(_isSecureImage ? "SECURE" : "PUBLIC")} - User: {_userRole}";
        }
    }

    // Caching Proxy
    public class CachingImageProxy : IImageLoader
    {
        private static readonly Dictionary<string, RealImage> _cache = new();
        private readonly string _filename;
        private readonly int _size;

        public CachingImageProxy(string filename, int size)
        {
            _filename = filename;
            _size = size;
        }

        public void Display()
        {
            if (!_cache.ContainsKey(_filename))
            {
                Console.WriteLine($"Cache miss for {_filename}, loading and caching...");
                _cache[_filename] = new RealImage(_filename, _size);
            }
            else
            {
                Console.WriteLine($"Cache hit for {_filename}, using cached version");
            }

            _cache[_filename].Display();
        }

        public string GetImageInfo()
        {
            var cached = _cache.ContainsKey(_filename) ? "(Cached)" : "(Not Cached)";
            return $"Caching Image Proxy: {_filename} - {_size}MB {cached}";
        }

        public static void ClearCache()
        {
            _cache.Clear();
            Console.WriteLine("Image cache cleared");
        }

        public static List<string> GetCacheInfo()
        {
            return _cache.Keys.ToList();
        }
    }

    // Image Gallery Service
    public class ImageGalleryService
    {
        public List<string> DemonstrateVirtualProxy()
        {
            var results = new List<string>();
            var images = new List<IImageLoader>
            {
                new ImageProxy("vacation1.jpg", 5),
                new ImageProxy("vacation2.jpg", 8),
                new ImageProxy("family.jpg", 3)
            };

            results.Add("=== Virtual Proxy Demo ===");
            
            // First access - will load
            foreach (var image in images)
            {
                using var stringWriter = new StringWriter();
                var originalOut = Console.Out;
                Console.SetOut(stringWriter);
                
                image.Display();
                
                Console.SetOut(originalOut);
                results.Add(stringWriter.ToString().Trim());
            }

            results.Add("\n=== Second access - using cached ===");
            
            // Second access - should use cached
            foreach (var image in images)
            {
                using var stringWriter = new StringWriter();
                var originalOut = Console.Out;
                Console.SetOut(stringWriter);
                
                image.Display();
                
                Console.SetOut(originalOut);
                results.Add(stringWriter.ToString().Trim());
            }

            return results;
        }

        public List<string> DemonstrateSecurityProxy()
        {
            var results = new List<string>();
            var users = new[] { "User", "Admin" };
            
            results.Add("=== Security Proxy Demo ===");

            foreach (var userRole in users)
            {
                results.Add($"\n--- Testing with {userRole} role ---");
                
                var publicImage = new SecureImageProxy("public.jpg", 2, userRole, false);
                var secureImage = new SecureImageProxy("classified.jpg", 10, userRole, true);

                using var stringWriter = new StringWriter();
                var originalOut = Console.Out;
                Console.SetOut(stringWriter);
                
                publicImage.Display();
                secureImage.Display();
                
                Console.SetOut(originalOut);
                results.Add(stringWriter.ToString().Trim());
            }

            return results;
        }

        public List<string> DemonstrateCachingProxy()
        {
            CachingImageProxy.ClearCache();
            var results = new List<string>();
            
            results.Add("=== Caching Proxy Demo ===");
            
            var images = new List<CachingImageProxy>
            {
                new("photo1.jpg", 4),
                new("photo2.jpg", 6),
                new("photo1.jpg", 4) // Same as first
            };

            foreach (var image in images)
            {
                using var stringWriter = new StringWriter();
                var originalOut = Console.Out;
                Console.SetOut(stringWriter);
                
                image.Display();
                
                Console.SetOut(originalOut);
                results.Add(stringWriter.ToString().Trim());
            }

            results.Add($"\nCached images: {string.Join(", ", CachingImageProxy.GetCacheInfo())}");
            return results;
        }
    }
}