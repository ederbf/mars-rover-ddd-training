﻿using System;
using System.Collections.Generic;

namespace MarsRoverKata
{
    public class TakePicture
    {
        private GoProDuskWhite _goPro;
        private PicturesStorage _storage;

        public TakePicture(GoProDuskWhite goPro, PicturesStorage storage)
        {
            _goPro = goPro;
            _storage = storage;
        }

        public void TakePhoto(string roverId)
        {
            var bitmap = _goPro.TakePhotos("default obturation", "0X", 1);
            var rover = _storage.Read(roverId);
            var position = rover.Position;
            rover.Pictures.Add(position, bitmap);

            if (rover.Pictures.Count > 3) {
                throw new PhotoStorageFullException();
            }
        }
    }

    public class GoProDuskWhite
    { 
        public string TakePhotos(string obturation, string zoom, int numberOfPhotos)
        {
            var random = new Random();

            return random.Next(1000, 9999).ToString();
        }
    }

    public class PicturesStorage
    {
        private Dictionary<string, StorageItem> StorageDict;
        public PicturesStorage()
        {
            StorageDict = new Dictionary<string, StorageItem>();
        }

        public void Store(string id, StorageItem storageItem)
        { 
            StorageDict.Add(id, storageItem);
        }

        public StorageItem Read(string id)
        {
            StorageItem result = new StorageItem();
            StorageDict.TryGetValue(id, out result);

            return result;
        }
    }

    public class StorageItem
    {
        public string Position { get; set; }
        public Dictionary<string, string> Pictures { get; set; }
    }

    public class PhotoStorageFullException : Exception
    {

    }
}