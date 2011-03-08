using System.Collections.Generic;

namespace Cartography.Dsl
{
    public class ListModifierExpression<TPolicy>
    {
        private readonly IList<TPolicy> _policies;

        public ListModifierExpression(IList<TPolicy> policies)
        {
            _policies = policies;
        }

        public ListModifierExpression<TPolicy> Add<T>()
            where T : TPolicy, new()
        {
            return Add(new T());
        }

        public ListModifierExpression<TPolicy> Add(TPolicy policy)
        {
            _policies.Fill(policy);
            return this;
        }
    }
}