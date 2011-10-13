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

        private List<CGameObject> objectList;
        
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
            objectList = new List<CGameObject>();
        }

        /// <summary>
        /// Clears the object list
        /// </summary>
        public void Shutdown()
        {
            objectList.Clear();
        }

        public void Update(float fElapsedTime)
        {
            for (int i = 0; i < objectList.Count; ++i)
            {
                objectList[i].Update(fElapsedTime);
            }
        }

        public void AddObject(CGameObject addObject)
        {
            if (addObject == null)
                return;

            addObject.setListID(objectList.Count);
            objectList.Add(addObject);
        }

        public void RemoveObject(CGameObject removeObject)
        {
            objectList.Remove(removeObject);
        }
    }
}
