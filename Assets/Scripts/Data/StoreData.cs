using System;

namespace Data
{
    public struct StoreData
    {
        public bool CanBeStored;
        public int WeightOverflow;
        public Action StoreAction;
        public Action CancelAction;
    }
}
