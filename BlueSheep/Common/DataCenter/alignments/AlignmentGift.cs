//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.DataCenter
{
    using System;


    [Serializable()]
    public class AlignmentGift : IData
    {
        
        public virtual int Id
        {
            get
            {
                return mId;
            }
            set
            {
                mId = value;
            }
        }
        
        private int mId;
        
        public virtual uint NameId
        {
            get
            {
                return mNameId;
            }
            set
            {
                mNameId = value;
            }
        }
        
        private uint mNameId;
        
        public virtual int EffectId
        {
            get
            {
                return mEffectId;
            }
            set
            {
                mEffectId = value;
            }
        }
        
        private int mEffectId;
        
        public virtual uint GfxId
        {
            get
            {
                return mGfxId;
            }
            set
            {
                mGfxId = value;
            }
        }
        
        private uint mGfxId;
        
        public AlignmentGift()
        {
        }
    }
}