using System.Collections.Generic;
using Tools.PriorityTools;

namespace BuffLogic
{
    public class PrioritizedBuffLinkedList<TValue> : PrioritizeLinkedList<IBuff<TValue>>
    {
        private readonly List<IBuff<TValue>> _removedBuffs;
        private readonly IBuffableValue<TValue> _buffableValueTarget;
        
        public bool IsAlive => ValueList.Count > 0;

        public PrioritizedBuffLinkedList(IBuffableValue<TValue> buffableValue)
        {
            _removedBuffs = new List<IBuff<TValue>>();
            _buffableValueTarget = buffableValue;
        }

        public void AddBuff(IBuff<TValue> buff)
        {
            Add(buff);
            _buffableValueTarget.UpdateAddBuff(this, buff);
        }

        public void Update()
        {
            _removedBuffs.Clear();
            bool wasUpdated = false;
            
            for (var valueNode = ValueList.First; valueNode != null; )
            {
                if (valueNode.Value.EndConditionExec())
                {
                    wasUpdated = true;

                    var nextNode = valueNode.Next;
                    _removedBuffs.Add(valueNode.Value);
                    
                    ValueList.Remove(valueNode);
                    valueNode = nextNode;
                }
                else
                    valueNode = valueNode.Next;
            }

            if(wasUpdated)
                _buffableValueTarget.UpdateRemoveBuffs(this, _removedBuffs);
        }
    }
}