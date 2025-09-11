using DesignPatterns.Domain.Entities.Common;

namespace DesignPatterns.Application.Interfaces;

public interface ICreationalPatternsService
{
    object DemonstrateSingleton();
    object DemonstrateFactory();
    object DemonstrateAbstractFactory();
    object DemonstrateBuilder();
    object DemonstratePrototype();
    object DemonstrateAllCreationalPatterns();
    object CreateCustomProduct(string name, decimal price, string description, string category);
    object GetCreationalPatternsInfo();
}

public interface IStructuralPatternsService
{
    object DemonstrateAdapter();
    object DemonstrateBridge();
    object DemonstrateComposite();
    object DemonstrateDecorator();
    object DemonstrateFacade();
    object DemonstrateFlyweight();
    object DemonstrateProxy();
    object DemonstrateAllStructuralPatterns();
    object CreateCustomScenario(string patternType, Dictionary<string, object> parameters);
    object GetStructuralPatternsInfo();
}

public interface IBehavioralPatternsService
{
    object DemonstrateObserver();
    object DemonstrateStrategy();
    object DemonstrateCommand();
    object DemonstrateState();
    object DemonstrateTemplateMethod();
    object DemonstrateChainOfResponsibility();
    object DemonstrateIterator();
    object DemonstrateMediator();
    object DemonstrateMemento();
    object DemonstrateVisitor();
    object DemonstrateAllBehavioralPatterns();
    object GetPatternSummary();
    object GetBehavioralPatternsInfo();
}