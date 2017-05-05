


















// Generated on 12/11/2014 19:02:12
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class TrustCertificate
    {

        public new const int ID = 377;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public ulong id;
        public string hash;


        public TrustCertificate()
        {
        }

        public TrustCertificate(ulong id, string hash)
        {
            this.id = id;
            this.hash = hash;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteULong(id);
            writer.WriteUTF(hash);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            id = reader.ReadULong();
            if (id < 0)
                throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
            hash = reader.ReadUTF();


        }


    }


}