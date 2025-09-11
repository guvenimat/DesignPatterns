using DesignPatterns.Domain.Interfaces.Common;

namespace DesignPatterns.Domain.Patterns.Behavioral.Memento
{
    // Memento Pattern - Capture and restore object state without violating encapsulation

    // Text Editor Memento Example
    public class TextEditorMemento
    {
        public string Content { get; private set; }
        public int CursorPosition { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string OperationType { get; private set; }

        public TextEditorMemento(string content, int cursorPosition, string operationType)
        {
            Content = content;
            CursorPosition = cursorPosition;
            Timestamp = DateTime.Now;
            OperationType = operationType;
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {OperationType} - Cursor: {CursorPosition}, Content: \"{Content}\"";
        }
    }

    public class TextEditor : IOriginator
    {
        private string _content = "";
        private int _cursorPosition = 0;

        public string Content => _content;
        public int CursorPosition => _cursorPosition;

        public void Write(string text)
        {
            _content = _content.Insert(_cursorPosition, text);
            _cursorPosition += text.Length;
            Console.WriteLine($"Written: '{text}' | Content: '{_content}' | Cursor: {_cursorPosition}");
        }

        public void Delete(int length)
        {
            if (_cursorPosition >= length)
            {
                _content = _content.Remove(_cursorPosition - length, length);
                _cursorPosition -= length;
                Console.WriteLine($"Deleted {length} chars | Content: '{_content}' | Cursor: {_cursorPosition}");
            }
        }

        public void MoveCursor(int position)
        {
            if (position >= 0 && position <= _content.Length)
            {
                _cursorPosition = position;
                Console.WriteLine($"Cursor moved to position: {_cursorPosition}");
            }
        }

        public void Clear()
        {
            _content = "";
            _cursorPosition = 0;
            Console.WriteLine("Content cleared");
        }

        public object CreateMemento()
        {
            return new TextEditorMemento(_content, _cursorPosition, "Save");
        }

        public void RestoreMemento(object memento)
        {
            if (memento is TextEditorMemento editorMemento)
            {
                _content = editorMemento.Content;
                _cursorPosition = editorMemento.CursorPosition;
                Console.WriteLine($"Restored state: Content='{_content}', Cursor={_cursorPosition}");
            }
        }

        public string GetState()
        {
            return $"Content: '{_content}', Cursor Position: {_cursorPosition}";
        }
    }

    // Game Character Memento Example
    public class GameCharacterMemento
    {
        public int Level { get; private set; }
        public int Health { get; private set; }
        public int Experience { get; private set; }
        public int Gold { get; private set; }
        public List<string> Inventory { get; private set; }
        public (int x, int y) Position { get; private set; }
        public DateTime SaveTime { get; private set; }
        public string SaveName { get; private set; }

        public GameCharacterMemento(int level, int health, int experience, int gold, 
            List<string> inventory, (int x, int y) position, string saveName)
        {
            Level = level;
            Health = health;
            Experience = experience;
            Gold = gold;
            Inventory = new List<string>(inventory);
            Position = position;
            SaveTime = DateTime.Now;
            SaveName = saveName;
        }

        public override string ToString()
        {
            return $"{SaveName} [{SaveTime:HH:mm:ss}] - Level {Level}, HP: {Health}, XP: {Experience}, Gold: {Gold}, Pos: ({Position.x},{Position.y})";
        }
    }

    public class GameCharacter : IOriginator
    {
        public string Name { get; private set; }
        public int Level { get; private set; } = 1;
        public int Health { get; private set; } = 100;
        public int MaxHealth { get; private set; } = 100;
        public int Experience { get; private set; } = 0;
        public int Gold { get; private set; } = 0;
        public List<string> Inventory { get; private set; } = new();
        public (int x, int y) Position { get; private set; } = (0, 0);

        public GameCharacter(string name)
        {
            Name = name;
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            Console.WriteLine($"{Name} gained {amount} XP. Total: {Experience}");
            
            // Level up logic
            var newLevel = (Experience / 100) + 1;
            if (newLevel > Level)
            {
                Level = newLevel;
                MaxHealth += 20;
                Health = MaxHealth;
                Console.WriteLine($"🎉 {Name} leveled up to Level {Level}! Max Health: {MaxHealth}");
            }
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - damage);
            Console.WriteLine($"{Name} took {damage} damage. Health: {Health}/{MaxHealth}");
        }

        public void Heal(int amount)
        {
            Health = Math.Min(MaxHealth, Health + amount);
            Console.WriteLine($"{Name} healed {amount} HP. Health: {Health}/{MaxHealth}");
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"{Name} gained {amount} gold. Total: {Gold}");
        }

        public void AddItem(string item)
        {
            Inventory.Add(item);
            Console.WriteLine($"{Name} acquired: {item}");
        }

        public void MoveTo(int x, int y)
        {
            Position = (x, y);
            Console.WriteLine($"{Name} moved to ({x}, {y})");
        }

        public object CreateMemento()
        {
            return new GameCharacterMemento(Level, Health, Experience, Gold, 
                Inventory, Position, $"Save_{DateTime.Now:HHmmss}");
        }

        public void RestoreMemento(object memento)
        {
            if (memento is GameCharacterMemento gameMemento)
            {
                Level = gameMemento.Level;
                Health = gameMemento.Health;
                MaxHealth = Level * 20 + 80; // Recalculate max health
                Experience = gameMemento.Experience;
                Gold = gameMemento.Gold;
                Inventory = new List<string>(gameMemento.Inventory);
                Position = gameMemento.Position;
                Console.WriteLine($"Game loaded: {gameMemento.SaveName}");
            }
        }

        public string GetCharacterInfo()
        {
            return $"{Name} - Level {Level} | HP: {Health}/{MaxHealth} | XP: {Experience} | Gold: {Gold} | Items: {Inventory.Count} | Position: ({Position.x}, {Position.y})";
        }
    }

    // Caretaker - Manages mementos
    public class SaveGameManager
    {
        private readonly Dictionary<string, object> _saves = new();
        private readonly List<object> _autoSaves = new();
        private const int MaxAutoSaves = 5;

        public void SaveGame(string saveName, IOriginator character)
        {
            var memento = character.CreateMemento();
            _saves[saveName] = memento;
            Console.WriteLine($"💾 Game saved as: {saveName}");
        }

        public void LoadGame(string saveName, IOriginator character)
        {
            if (_saves.TryGetValue(saveName, out var memento))
            {
                character.RestoreMemento(memento);
                Console.WriteLine($"📁 Game loaded: {saveName}");
            }
            else
            {
                Console.WriteLine($"❌ Save file not found: {saveName}");
            }
        }

        public void AutoSave(IOriginator character)
        {
            var memento = character.CreateMemento();
            _autoSaves.Add(memento);
            
            if (_autoSaves.Count > MaxAutoSaves)
            {
                _autoSaves.RemoveAt(0);
            }
            
            Console.WriteLine($"🔄 Auto-saved (slot {_autoSaves.Count})");
        }

        public void LoadAutoSave(int slot, IOriginator character)
        {
            if (slot > 0 && slot <= _autoSaves.Count)
            {
                var memento = _autoSaves[slot - 1];
                character.RestoreMemento(memento);
                Console.WriteLine($"📁 Auto-save loaded: slot {slot}");
            }
            else
            {
                Console.WriteLine($"❌ Auto-save slot {slot} not found");
            }
        }

        public List<string> GetSaveList()
        {
            var saves = new List<string>();
            saves.AddRange(_saves.Keys.Select(name => $"Manual: {name}"));
            saves.AddRange(_autoSaves.Select((_, index) => $"Auto-save {index + 1}"));
            return saves;
        }

        public void DeleteSave(string saveName)
        {
            if (_saves.Remove(saveName))
            {
                Console.WriteLine($"🗑️ Deleted save: {saveName}");
            }
            else
            {
                Console.WriteLine($"❌ Save not found: {saveName}");
            }
        }
    }

    // Memento Service
    public class MementoService
    {
        public List<string> DemonstrateTextEditor()
        {
            var results = new List<string>();
            var editor = new TextEditor();
            var history = new List<object>();

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            Console.WriteLine("=== Text Editor Memento Demo ===");
            
            // Perform operations and save states
            editor.Write("Hello ");
            history.Add(editor.CreateMemento());
            
            editor.Write("World!");
            history.Add(editor.CreateMemento());
            
            editor.MoveCursor(5);
            editor.Write(" Beautiful");
            history.Add(editor.CreateMemento());
            
            editor.Delete(3);
            history.Add(editor.CreateMemento());

            Console.WriteLine($"\nCurrent state: {editor.GetState()}");
            
            // Restore previous states
            Console.WriteLine("\n=== Restoring Previous States ===");
            for (int i = history.Count - 2; i >= 0; i--)
            {
                Console.WriteLine($"Restoring state {i + 1}:");
                editor.RestoreMemento(history[i]);
            }

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }

        public List<string> DemonstrateGameSaveSystem()
        {
            var results = new List<string>();
            var character = new GameCharacter("Hero");
            var saveManager = new SaveGameManager();

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            Console.WriteLine("=== Game Save System Demo ===");
            Console.WriteLine($"Starting character: {character.GetCharacterInfo()}");
            
            // Initial save
            saveManager.SaveGame("NewGame", character);
            
            // Play the game
            character.MoveTo(10, 5);
            character.GainExperience(150);
            character.AddGold(100);
            character.AddItem("Iron Sword");
            character.AddItem("Health Potion");
            
            Console.WriteLine($"\nAfter playing: {character.GetCharacterInfo()}");
            saveManager.AutoSave(character);
            
            // Continue playing
            character.MoveTo(25, 15);
            character.GainExperience(200);
            character.TakeDamage(30);
            character.AddGold(50);
            character.AddItem("Magic Shield");
            
            Console.WriteLine($"\nAfter more playing: {character.GetCharacterInfo()}");
            saveManager.SaveGame("Checkpoint1", character);
            
            // Boss fight simulation
            character.TakeDamage(80);
            character.GainExperience(500);
            character.AddGold(200);
            
            Console.WriteLine($"\nAfter boss fight: {character.GetCharacterInfo()}");
            
            // Load previous save
            Console.WriteLine("\n=== Loading Previous Save ===");
            saveManager.LoadGame("Checkpoint1", character);
            Console.WriteLine($"After loading: {character.GetCharacterInfo()}");
            
            // Show all saves
            Console.WriteLine("\n=== Available Saves ===");
            foreach (var save in saveManager.GetSaveList())
            {
                Console.WriteLine($"📁 {save}");
            }

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }

        public List<string> CreateCustomGameScenario(string characterName, List<(string action, object value)> actions)
        {
            var results = new List<string>();
            var character = new GameCharacter(characterName);
            var saveManager = new SaveGameManager();

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            Console.WriteLine($"=== Custom Game Scenario: {characterName} ===");
            Console.WriteLine($"Starting: {character.GetCharacterInfo()}");
            
            saveManager.SaveGame("Start", character);

            foreach (var (action, value) in actions)
            {
                Console.WriteLine($"\nAction: {action}");
                switch (action.ToLower())
                {
                    case "move":
                        if (value is (int x, int y))
                            character.MoveTo(x, y);
                        break;
                    case "experience":
                        if (value is int exp)
                            character.GainExperience(exp);
                        break;
                    case "damage":
                        if (value is int dmg)
                            character.TakeDamage(dmg);
                        break;
                    case "heal":
                        if (value is int heal)
                            character.Heal(heal);
                        break;
                    case "gold":
                        if (value is int gold)
                            character.AddGold(gold);
                        break;
                    case "item":
                        if (value is string item)
                            character.AddItem(item);
                        break;
                    case "save":
                        if (value is string saveName)
                            saveManager.SaveGame(saveName, character);
                        break;
                    case "load":
                        if (value is string loadName)
                            saveManager.LoadGame(loadName, character);
                        break;
                    case "autosave":
                        saveManager.AutoSave(character);
                        break;
                }
                
                Console.WriteLine($"Current: {character.GetCharacterInfo()}");
            }

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }
    }
}