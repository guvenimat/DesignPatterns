// Common interfaces for design patterns
namespace DesignPatterns.Domain.Interfaces.Common
{
    // Common pattern interfaces
    public interface IPrototype<T>
    {
        T Clone();
    }

    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public interface IStrategy
    {
        string Execute(object data);
    }

    public interface IObserver
    {
        void Update(string message);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string message);
    }

    public interface IVisitor
    {
        void Visit(object element);
    }

    public interface IElement
    {
        void Accept(IVisitor visitor);
    }

    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }

    public interface IMediator
    {
        void Notify(object sender, string @event);
    }

    public interface IOriginator
    {
        object CreateMemento();
        void RestoreMemento(object memento);
    }

    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }

    public interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
    }
}