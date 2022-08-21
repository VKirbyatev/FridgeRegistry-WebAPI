using FridgeRegistry.Domain.Common.Exceptions;
using FridgeRegistry.Domain.Common.Interfaces;

namespace FridgeRegistry.Domain.Common;

public abstract class Entity
{
    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}