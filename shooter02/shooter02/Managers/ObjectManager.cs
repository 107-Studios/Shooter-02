using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.GameObjects;

namespace shooter02.ObjectManager
{
    class CObjectManager
    {
        static readonly CObjectManager instance = new CObjectManager();

        private LinkedList<CGameObject> objectList;

        public CObjectManager()
        {
            objectList = null;
        }

        /// <summary>
        /// Gets the instance of the ObjectManager
        /// </summary>
        public static CObjectManager Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Initializes the ObjectManager
        /// Creates a new instance of the object list
        /// </summary>
        public void Initialize()
        {
            objectList = new LinkedList<CGameObject>();
        }

        /// <summary>
        /// Clears the object list
        /// </summary>
        public void Shutdown()
        {
            objectList.Clear();
        }

        /// <summary>
        /// Calls each object's update function
        /// </summary>
        /// <param name="fElapsedTime">The amount of time that has passed since last frame</param>
        public void Update(double fElapsedTime)
        {
            for (int i = 0; i < objectList.Count; ++i)
                if (false == objectList.ElementAt(i).getIsDirty())
                   objectList.ElementAt(i).Update(fElapsedTime);
        }

        /// <summary>
        /// Adds a new object to the objectList
        /// </summary>
        /// <param name="newObject">The new object to be added to the objectList</param>
        public void AddObject(CGameObject newObject)
        {
            // don't add nothing
            if (newObject == null)
                return;

            // no duplicates
            if (objectList.Contains(newObject))
                return;

            // Add object to objectList
            newObject.setListID(objectList.Count);
            objectList.AddLast(newObject);
        }

        /// <summary>
        /// Removes the specified object.
        /// </summary>
        /// <param name="removeObject">The object to remove</param>
        public void RemoveObject(CGameObject removeObject)
        {
            // don't remove nothing
            if (removeObject == null)
                return;

            // Find the object to remove
            LinkedListNode<CGameObject> current = objectList.Find(removeObject);

            // If object wasn't found, leave.
            if (current == null)
                return;

            // Set objects after the object to be removed to the correct listIndex.
            for (; current.Value.Equals(null) == false; current = current.Next)
                current.Value.setListID(current.Value.getListID() - 1);

            // Remove the object from the objectList
            objectList.Remove(removeObject);
        }

        /// <summary>
        /// Removes an object at the specified index.
        /// </summary>
        /// <param name="index">The index of the object to remove.</param>
        public void RemoveAt(int index)
        {
            // If index is invalid, leave.
            if (index >= objectList.Count || index < 0)
                return;

            // Remove the object
            RemoveObject(objectList.ElementAt(index));
        }
    }
}
