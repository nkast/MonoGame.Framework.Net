// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.IO;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;


namespace Microsoft.Xna.Framework.Net
{
    public class PacketWriter : BinaryWriter
    {
    
        // I thought about using an array but that means more code in my opinion and it does not make sense
        //  since the memory stream is perfect for this.
        // Using a memory stream also fits nicely with the constructors see:
        // http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.net.packetwriter.packetwriter.aspx
        // We will see when testing begins.
        #region Constructors
        public PacketWriter () : this (0)
        {
        }

        public PacketWriter (int capacity) : base ( new MemoryStream(capacity))
        {
        }

        #endregion

        #region Methods
        public void Write (Color Value)
        {
            base.Write (Value.PackedValue);
        }

        public override void Write (double Value)
        {
            base.Write (Value);
        }
        
        public void Write (Matrix Value)
        {
            // After looking at a captured packet it looks like all the values of 
            //  the matrix are written.  This is different than the Lidgren XNAExtensions
            base.Write (Value.M11);
            base.Write (Value.M12);
            base.Write (Value.M13);
            base.Write (Value.M14);
            base.Write (Value.M21);
            base.Write (Value.M22);
            base.Write (Value.M23);
            base.Write (Value.M24);
            base.Write (Value.M31);
            base.Write (Value.M32);
            base.Write (Value.M33);
            base.Write (Value.M34);
            base.Write (Value.M41);
            base.Write (Value.M42);
            base.Write (Value.M43);
            base.Write (Value.M44);
        }

        public void Write (Quaternion Value)
        {
            // This may need to be corrected as have no test for it
            base.Write(Value.X);
            base.Write(Value.Y);
            base.Write(Value.Z);
            base.Write(Value.W);			
            
        }

        public override void Write (float Value)
        {
            base.Write(Value);
        }

        public void Write (Vector2 Value)
        {
            base.Write(Value.X);
            base.Write(Value.Y);
        }

        public void Write (Vector3 Value)
        {
            base.Write(Value.X);
            base.Write(Value.Y);
            base.Write(Value.Z);
        }

        public void Write (Vector4 Value)
        {
            base.Write(Value.X);
            base.Write(Value.Y);
            base.Write(Value.Z);
            base.Write(Value.W);
        }

        #endregion
        
        internal byte[] Data
        {
            get {
                MemoryStream stream = (MemoryStream)this.BaseStream;
                return stream.GetBuffer();
            }
        }
        
        #region Properties
        public int Length { 
            get {
                return (int)BaseStream.Length;
            }
        }

        public int Position { 
            get {
                return (int)BaseStream.Position;
            }
            set {
                if (BaseStream.Position != value)
                    BaseStream.Position = value;
            } 
        }
        
        internal void Reset() 
        {
            MemoryStream stream = (MemoryStream)this.BaseStream;
            stream.SetLength(0);
            stream.Position = 0;
            
        }
        #endregion
    }
}
