using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using shooter02.Managers;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;

namespace shooter02.Misc
{
    [Serializable]
    public struct SaveGameData
    {
        // data members
        public int screenWidth;
        public int screenHeight;
        public bool fullScreen;
    }

    class SaveInfo
    {
        private static readonly SaveInfo instance = new SaveInfo();
        
        // data members
        private SaveGameData saveGameData = new SaveGameData();
        private string fileName = "save.bin";

        //fields
        public static SaveInfo Instance
        {
            get
            {
                return instance;
            }
        }

        public Vector2 ScreenResolution
        {
            get
            {
                return new Vector2(saveGameData.screenWidth, saveGameData.screenHeight);
            }

            set
            {
                saveGameData.screenWidth = (int)value.X;
                saveGameData.screenHeight = (int)value.Y;
            }
        }

        public bool Fullscreen
        {
            get
            {
                return saveGameData.fullScreen;
            }

            set
            {
                saveGameData.fullScreen = value;
            }
        }

        // constructors
        private SaveInfo()
        {
            saveGameData.screenWidth = 1280;
            saveGameData.screenHeight = 720;
            saveGameData.fullScreen = false;
        }

        // methods
        private StorageDevice GetDevice()
        {
            // send the save request
            IAsyncResult saveRequest = StorageDevice.BeginShowSelector(null, null);

            // wait for device choice
            saveRequest.AsyncWaitHandle.WaitOne();
            StorageDevice returnStorage = StorageDevice.EndShowSelector(saveRequest);

            // kill the waitHandle
            saveRequest.AsyncWaitHandle.Close();

            return returnStorage;
        }

        private StorageContainer GetContainer(StorageDevice storageDevice)
        {
            //open storage container
            IAsyncResult result = storageDevice.BeginOpenContainer("Juntoki", null, null);

            // wait for the WaitHandle to be signaled
            result.AsyncWaitHandle.WaitOne();

            StorageContainer returnContainer = storageDevice.EndOpenContainer(result);

            // kill the wait handle
            result.AsyncWaitHandle.Close();

            return returnContainer;
        }

        public bool Load()
        {
            // get the storageDevice
            StorageDevice storageDevice = GetDevice();

            //get a storageContainer
            StorageContainer storageContainer = GetContainer(storageDevice);

            // check if file exists
            if (!storageContainer.FileExists(fileName))
            {
                // if it doesn't, dispose of container
                storageContainer.Dispose();
                return false;
            }

            // open the file
            Stream stream = storageContainer.OpenFile(fileName, FileMode.Open);

            // create Serializer
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));

            // load game file
            saveGameData = (SaveGameData)serializer.Deserialize(stream);

            // close stream
            stream.Close();

            //dispose of the container
            storageContainer.Dispose();

            return true;
        }

        public bool Save()
        {
            // get the storageDevice
            StorageDevice storageDevice = GetDevice();

            //get a storageContainer
            StorageContainer container = GetContainer(storageDevice);

            // check if a save already exists
            if (container.FileExists(fileName))
                // delete file to make room for new one
                container.DeleteFile(fileName);

            // create the new file
            Stream stream = container.CreateFile(fileName);

            // convert saveGameData to XML data
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));

            //write data to file
            serializer.Serialize(stream, saveGameData);

            //close stream
            stream.Close();

            //commit changes to device
            container.Dispose();

            return true;
        }
    }
}
