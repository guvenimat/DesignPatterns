using DesignPatterns.Domain.Interfaces.Common;

namespace DesignPatterns.Domain.Patterns.Behavioral.Command
{
    // Command Pattern - Encapsulate requests as objects

    // Receiver classes
    public class TextEditor
    {
        private string _content = "";
        
        public void Write(string text)
        {
            _content += text;
            Console.WriteLine($"Written: '{text}' | Content: '{_content}'");
        }

        public void Delete(int length)
        {
            if (length > _content.Length) length = _content.Length;
            _content = _content.Substring(0, _content.Length - length);
            Console.WriteLine($"Deleted {length} characters | Content: '{_content}'");
        }

        public void Clear()
        {
            _content = "";
            Console.WriteLine("Content cleared");
        }

        public string GetContent()
        {
            return _content;
        }

        public void SetContent(string content)
        {
            _content = content;
        }
    }

    // Concrete Commands
    public class WriteCommand : ICommand
    {
        private readonly TextEditor _editor;
        private readonly string _text;

        public WriteCommand(TextEditor editor, string text)
        {
            _editor = editor;
            _text = text;
        }

        public void Execute()
        {
            _editor.Write(_text);
        }

        public void Undo()
        {
            _editor.Delete(_text.Length);
        }
    }

    public class DeleteCommand : ICommand
    {
        private readonly TextEditor _editor;
        private readonly int _length;
        private string _deletedText = "";

        public DeleteCommand(TextEditor editor, int length)
        {
            _editor = editor;
            _length = length;
        }

        public void Execute()
        {
            var content = _editor.GetContent();
            var actualLength = Math.Min(_length, content.Length);
            _deletedText = content.Substring(content.Length - actualLength);
            _editor.Delete(actualLength);
        }

        public void Undo()
        {
            _editor.Write(_deletedText);
        }
    }

    public class ClearCommand : ICommand
    {
        private readonly TextEditor _editor;
        private string _previousContent = "";

        public ClearCommand(TextEditor editor)
        {
            _editor = editor;
        }

        public void Execute()
        {
            _previousContent = _editor.GetContent();
            _editor.Clear();
        }

        public void Undo()
        {
            _editor.SetContent(_previousContent);
            Console.WriteLine($"Restored content: '{_previousContent}'");
        }
    }

    // Macro Command - Composite Command
    public class MacroCommand : ICommand
    {
        private readonly List<ICommand> _commands = new();

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void Execute()
        {
            Console.WriteLine("=== Executing Macro Command ===");
            foreach (var command in _commands)
            {
                command.Execute();
            }
            Console.WriteLine("=== Macro Command Completed ===");
        }

        public void Undo()
        {
            Console.WriteLine("=== Undoing Macro Command ===");
            for (int i = _commands.Count - 1; i >= 0; i--)
            {
                _commands[i].Undo();
            }
            Console.WriteLine("=== Macro Command Undone ===");
        }
    }

    // Invoker - Command Manager with undo/redo functionality
    public class CommandManager
    {
        private readonly Stack<ICommand> _undoStack = new();
        private readonly Stack<ICommand> _redoStack = new();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear(); // Clear redo stack when new command is executed
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
                Console.WriteLine("Command undone");
            }
            else
            {
                Console.WriteLine("Nothing to undo");
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var command = _redoStack.Pop();
                command.Execute();
                _undoStack.Push(command);
                Console.WriteLine("Command redone");
            }
            else
            {
                Console.WriteLine("Nothing to redo");
            }
        }

        public bool CanUndo() => _undoStack.Count > 0;
        public bool CanRedo() => _redoStack.Count > 0;

        public string GetStatus()
        {
            return $"Undo stack: {_undoStack.Count} commands, Redo stack: {_redoStack.Count} commands";
        }
    }

    // Light system for remote control example
    public class Light
    {
        private bool _isOn = false;
        private readonly string _location;

        public Light(string location)
        {
            _location = location;
        }

        public void TurnOn()
        {
            _isOn = true;
            Console.WriteLine($"{_location} light is ON");
        }

        public void TurnOff()
        {
            _isOn = false;
            Console.WriteLine($"{_location} light is OFF");
        }

        public bool IsOn => _isOn;
        public string Location => _location;
    }

    public class LightOnCommand : ICommand
    {
        private readonly Light _light;

        public LightOnCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.TurnOn();
        }

        public void Undo()
        {
            _light.TurnOff();
        }
    }

    public class LightOffCommand : ICommand
    {
        private readonly Light _light;

        public LightOffCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.TurnOff();
        }

        public void Undo()
        {
            _light.TurnOn();
        }
    }

    // Remote Control
    public class RemoteControl
    {
        private readonly ICommand?[] _onCommands;
        private readonly ICommand?[] _offCommands;
        private ICommand? _lastCommand;

        public RemoteControl(int slots = 7)
        {
            _onCommands = new ICommand[slots];
            _offCommands = new ICommand[slots];
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            _onCommands[slot] = onCommand;
            _offCommands[slot] = offCommand;
        }

        public void OnButtonPressed(int slot)
        {
            if (_onCommands[slot] != null)
            {
                _onCommands[slot]!.Execute();
                _lastCommand = _onCommands[slot];
            }
            else
            {
                Console.WriteLine($"No command set for slot {slot}");
            }
        }

        public void OffButtonPressed(int slot)
        {
            if (_offCommands[slot] != null)
            {
                _offCommands[slot]!.Execute();
                _lastCommand = _offCommands[slot];
            }
            else
            {
                Console.WriteLine($"No command set for slot {slot}");
            }
        }

        public void UndoButtonPressed()
        {
            if (_lastCommand != null)
            {
                _lastCommand.Undo();
            }
            else
            {
                Console.WriteLine("No command to undo");
            }
        }
    }

    // Command Service
    public class CommandService
    {
        public List<string> DemonstrateTextEditorCommands()
        {
            var results = new List<string>();
            var editor = new TextEditor();
            var commandManager = new CommandManager();

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            // Execute various commands
            commandManager.ExecuteCommand(new WriteCommand(editor, "Hello "));
            commandManager.ExecuteCommand(new WriteCommand(editor, "World!"));
            commandManager.ExecuteCommand(new WriteCommand(editor, " This is a test."));
            
            Console.WriteLine($"Current content: '{editor.GetContent()}'");
            Console.WriteLine(commandManager.GetStatus());

            // Undo some commands
            commandManager.Undo();
            commandManager.Undo();
            Console.WriteLine($"After 2 undos: '{editor.GetContent()}'");

            // Redo one command
            commandManager.Redo();
            Console.WriteLine($"After 1 redo: '{editor.GetContent()}'");

            // Delete and clear commands
            commandManager.ExecuteCommand(new DeleteCommand(editor, 6));
            commandManager.ExecuteCommand(new ClearCommand(editor));
            
            // Undo clear
            commandManager.Undo();
            Console.WriteLine($"After undoing clear: '{editor.GetContent()}'");

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }

        public List<string> DemonstrateMacroCommand()
        {
            var results = new List<string>();
            var editor = new TextEditor();

            // Create a macro command
            var macro = new MacroCommand();
            macro.AddCommand(new WriteCommand(editor, "Line 1\n"));
            macro.AddCommand(new WriteCommand(editor, "Line 2\n"));
            macro.AddCommand(new WriteCommand(editor, "Line 3\n"));

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            // Execute macro
            macro.Execute();
            Console.WriteLine($"Final content:\n{editor.GetContent()}");

            // Undo macro
            macro.Undo();
            Console.WriteLine($"After undo:\n{editor.GetContent()}");

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }

        public List<string> DemonstrateRemoteControl()
        {
            var results = new List<string>();
            
            // Create lights
            var livingRoomLight = new Light("Living Room");
            var kitchenLight = new Light("Kitchen");
            var bedroomLight = new Light("Bedroom");

            // Create commands
            var livingRoomOn = new LightOnCommand(livingRoomLight);
            var livingRoomOff = new LightOffCommand(livingRoomLight);
            var kitchenOn = new LightOnCommand(kitchenLight);
            var kitchenOff = new LightOffCommand(kitchenLight);

            // Set up remote control
            var remote = new RemoteControl();
            remote.SetCommand(0, livingRoomOn, livingRoomOff);
            remote.SetCommand(1, kitchenOn, kitchenOff);

            using var stringWriter = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(stringWriter);

            // Use remote control
            Console.WriteLine("=== Remote Control Demo ===");
            remote.OnButtonPressed(0);  // Living room on
            remote.OnButtonPressed(1);  // Kitchen on
            remote.OffButtonPressed(0); // Living room off
            
            Console.WriteLine("\nUndo last command:");
            remote.UndoButtonPressed(); // Undo living room off
            
            remote.OffButtonPressed(1); // Kitchen off
            remote.UndoButtonPressed(); // Undo kitchen off

            Console.SetOut(originalOut);
            results.Add(stringWriter.ToString());

            return results;
        }
    }
}