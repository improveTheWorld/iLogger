namespace iCode.Log
{
    public class NameSpaceListWatcher : ListWatcher<NameSpace>
    {
        override public bool isWatched(NameSpace nameSpaceToCheck)
        {
            if (watchedList == null)
            {
                return true;
            } 
            else
            {
                foreach (NameSpace nameSpace in watchedList)
                {
                    if (nameSpace.isMatching(nameSpaceToCheck))
                        return true;
                }
                return false;
            }
        }

        public bool isWatchedObject(object instance)
        {
            return isWatched(new NameSpace(instance.GetType().ToString()).ParentNameSpace(1));
        }

    }
}
