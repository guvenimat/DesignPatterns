using DesignPatterns.Domain.Interfaces.Behavioral;

namespace DesignPatterns.Domain.Patterns.Behavioral.Observer
{
    // Observer Pattern - One-to-many dependency between objects

    // Concrete Observer
    public class NewsSubscriber : INewsSubscriber
    {
        private readonly string _name;
        private readonly string _interests;

        public NewsSubscriber(string name, string interests)
        {
            _name = name;
            _interests = interests;
        }

        public void Update(string news)
        {
            if (news.ToLower().Contains(_interests.ToLower()))
            {
                Console.WriteLine($"📧 {_name} received relevant news: {news}");
            }
            else
            {
                Console.WriteLine($"📭 {_name} received news: {news} (not interested in {_interests})");
            }
        }

        public string GetSubscriberName()
        {
            return $"{_name} (interested in {_interests})";
        }
    }

    // Concrete Subject
    public class NewsAgency : INewsPublisher
    {
        private readonly List<INewsSubscriber> _subscribers = new();
        private readonly List<string> _newsHistory = new();

        public void Subscribe(INewsSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
            Console.WriteLine($"✅ {subscriber.GetSubscriberName()} subscribed to news");
        }

        public void Unsubscribe(INewsSubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
            Console.WriteLine($"❌ {subscriber.GetSubscriberName()} unsubscribed from news");
        }

        public void PublishNews(string news)
        {
            _newsHistory.Add($"{DateTime.Now:HH:mm:ss} - {news}");
            Console.WriteLine($"\n📰 NEWS PUBLISHED: {news}");
            Console.WriteLine($"Notifying {_subscribers.Count} subscribers...\n");

            foreach (var subscriber in _subscribers)
            {
                subscriber.Update(news);
            }
            Console.WriteLine();
        }

        public List<string> GetNewsHistory()
        {
            return new List<string>(_newsHistory);
        }

        public List<string> GetSubscribers()
        {
            return _subscribers.Select(s => s.GetSubscriberName()).ToList();
        }

        public int GetSubscriberCount()
        {
            return _subscribers.Count;
        }
    }

    // Observable Stock Price
    public class Stock
    {
        private readonly List<IStockObserver> _observers = new();
        private decimal _price;
        private readonly string _symbol;

        public Stock(string symbol, decimal initialPrice)
        {
            _symbol = symbol;
            _price = initialPrice;
        }

        public string Symbol => _symbol;
        public decimal Price => _price;

        public void AddObserver(IStockObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IStockObserver observer)
        {
            _observers.Remove(observer);
        }

        public void SetPrice(decimal newPrice)
        {
            var oldPrice = _price;
            _price = newPrice;
            var change = newPrice - oldPrice;
            var changePercent = oldPrice > 0 ? (change / oldPrice) * 100 : 0;

            Console.WriteLine($"📈 {_symbol} price changed from ${oldPrice:F2} to ${newPrice:F2} ({change:+0.00;-0.00} {changePercent:+0.0;-0.0}%)");
            
            NotifyObservers(change, changePercent);
        }

        private void NotifyObservers(decimal change, decimal changePercent)
        {
            foreach (var observer in _observers)
            {
                observer.OnPriceChange(_symbol, _price, change, changePercent);
            }
        }
    }

    public interface IStockObserver
    {
        void OnPriceChange(string symbol, decimal newPrice, decimal change, decimal changePercent);
    }

    public class StockPortfolio : IStockObserver
    {
        private readonly string _ownerName;
        private readonly Dictionary<string, int> _holdings = new();

        public StockPortfolio(string ownerName)
        {
            _ownerName = ownerName;
        }

        public void AddHolding(string symbol, int shares)
        {
            _holdings[symbol] = shares;
        }

        public void OnPriceChange(string symbol, decimal newPrice, decimal change, decimal changePercent)
        {
            if (_holdings.TryGetValue(symbol, out var shares))
            {
                var totalChange = change * shares;
                Console.WriteLine($"💼 {_ownerName}'s portfolio: {shares} shares of {symbol} changed by ${totalChange:F2}");
            }
        }

        public string GetPortfolioSummary()
        {
            return $"Portfolio Owner: {_ownerName}, Holdings: {string.Join(", ", _holdings.Select(h => $"{h.Value} {h.Key}"))}";
        }
    }

    // Observer Service
    public class ObserverService
    {
        public List<string> DemonstrateNewsObserver()
        {
            var results = new List<string>();
            var newsAgency = new NewsAgency();

            // Create subscribers with different interests
            var techSubscriber = new NewsSubscriber("Alice", "Technology");
            var sportsSubscriber = new NewsSubscriber("Bob", "Sports");
            var generalSubscriber = new NewsSubscriber("Charlie", "General");

            // Subscribe to news
            newsAgency.Subscribe(techSubscriber);
            newsAgency.Subscribe(sportsSubscriber);
            newsAgency.Subscribe(generalSubscriber);

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            // Publish different types of news
            newsAgency.PublishNews("New iPhone released with advanced technology features");
            newsAgency.PublishNews("Football world cup finals this weekend");
            newsAgency.PublishNews("General election results announced");

            // Unsubscribe one subscriber
            newsAgency.Unsubscribe(sportsSubscriber);
            newsAgency.PublishNews("Latest technology breakthrough in AI development");

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            results.AddRange(newsAgency.GetNewsHistory());
            return results;
        }

        public List<string> DemonstrateStockObserver()
        {
            var results = new List<string>();
            
            // Create stocks
            var appleStock = new Stock("AAPL", 150.00m);
            var googleStock = new Stock("GOOGL", 2500.00m);

            // Create portfolios
            var portfolio1 = new StockPortfolio("John Doe");
            portfolio1.AddHolding("AAPL", 100);
            portfolio1.AddHolding("GOOGL", 50);

            var portfolio2 = new StockPortfolio("Jane Smith");
            portfolio2.AddHolding("AAPL", 200);

            // Subscribe portfolios to stocks
            appleStock.AddObserver(portfolio1);
            appleStock.AddObserver(portfolio2);
            googleStock.AddObserver(portfolio1);

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            // Simulate price changes
            appleStock.SetPrice(155.50m);
            googleStock.SetPrice(2485.75m);
            appleStock.SetPrice(148.25m);

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            results.Add(portfolio1.GetPortfolioSummary());
            results.Add(portfolio2.GetPortfolioSummary());

            return results;
        }
    }
}