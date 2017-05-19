﻿using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using MageBot.Util.IO;

namespace MageBot.DataFiles.Data.D2p
{
    public class MapsManager
    {
        // Methods
        public static Map FromId(int id)
        {
            lock (CheckLock)
            {
                //if (MapsManager.MapId_Map.ContainsKey(id))
                //{
                //    return MapsManager.MapId_Map[id];
                //}
                string str = ((id % 10).ToString() + "/" + id.ToString() + ".dlm");
                if (D2pFileManager.MapExists(str))
                {
                    MemoryStream stream = new MemoryStream(D2pFileManager.method_1(str)) { Position = 2 };
                    DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Decompress);
                    byte[] buffer = new byte[50001];
                    MemoryStream destination = new MemoryStream(buffer);
                    stream2.CopyTo(destination);
                    destination.Position = 0;
                    BigEndianReader reader = new BigEndianReader(destination);
                    Map map2 = new Map();
                    map2.Init(reader);
                    //MapId_Map.Add(id, map2);
                    //if ((MapId_Map.Count > 1000))
                    //{
                    //    MapId_Map.Remove(MapId_Map.Keys.First());
                    //}
                    return map2;
                }
                //MapId_Map.Add(id, null);
                //if ((MapId_Map.Count > 1000))
                //{
                //    MapId_Map.Remove(MapId_Map.Keys.First());
                //}
                return null;
            }
        }

        public static void Init(string directory)
        {
            //if ((Assembly.GetCallingAssembly().GetName().Name != "RebirthBot"))
            //{
            //    throw new Exception();
            //}
            //MapId_Map = new Dictionary<int, Map>();
            D2pFileManager = new D2pFileManager(directory);
            MapsManager.CheckLock = new object();
        }

        // Fields
        private static D2pFileManager D2pFileManager;
        //private static Dictionary<int, Map> MapId_Map;
        private static object CheckLock;
    }
}
