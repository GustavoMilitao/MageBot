﻿using MageBot.Util.IO;

namespace MageBot.DataFiles.Data.D2p.Elements
{
    public abstract class BasicElement
    {
        // Methods
        protected BasicElement()
        {
        }

        public static BasicElement GetElementFromType(int typeId)
        {
            switch (typeId)
            {
                case 2:
                    return new GraphicalElement();
                case 33:
                    return new SoundElement();
            }
            return null;
        }

        internal abstract void Init(BigEndianReader reader, int mapVersion);
    }
}
