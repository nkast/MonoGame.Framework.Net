// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.IO;

using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Net
{


    public class PacketReader : BinaryReader
    {

        // Read comments within the PacketWriter
        #region Constructors
        public PacketReader() : this(0)
        {
        }


        public PacketReader(int capacity) : base(new MemoryStream(0))
        {

        }
        #endregion

        #region Methods
        internal byte[] Data
        {
            get
            {
                MemoryStream stream = (MemoryStream)this.BaseStream;
                return stream.GetBuffer();
            }
            set
            {
                MemoryStream ms = (MemoryStream)this.BaseStream;
                ms.Write(value, 0, value.Length);
            }
        }

        public Color ReadColor()
        {
            Color newColor = Color.Transparent;
            newColor.PackedValue = this.ReadUInt32();
            return newColor;
        }

        public override double ReadDouble()
        {
            return this.ReadDouble();
        }

        public Matrix ReadMatrix()
        {
            Matrix matrix = new Matrix();

            matrix.M11 = this.ReadSingle();
            matrix.M12 = this.ReadSingle();
            matrix.M13 = this.ReadSingle();
            matrix.M14 = this.ReadSingle();

            matrix.M21 = this.ReadSingle();
            matrix.M22 = this.ReadSingle();
            matrix.M23 = this.ReadSingle();
            matrix.M24 = this.ReadSingle();

            matrix.M31 = this.ReadSingle();
            matrix.M32 = this.ReadSingle();
            matrix.M33 = this.ReadSingle();
            matrix.M34 = this.ReadSingle();

            matrix.M41 = this.ReadSingle();
            matrix.M42 = this.ReadSingle();
            matrix.M43 = this.ReadSingle();
            matrix.M44 = this.ReadSingle();

            return matrix;
        }

        public Quaternion ReadQuaternion()
        {
            Quaternion quat = new Quaternion();
            quat.X = this.ReadSingle();
            quat.Y = this.ReadSingle();
            quat.Z = this.ReadSingle();
            quat.W = this.ReadSingle();

            return quat;

        }

        //		public override float ReadSingle()
        //		{
        //			return this.ReadSingle();
        //		}

        public Vector2 ReadVector2()
        {
            Vector2 vect = new Vector2();
            vect.X = this.ReadSingle();
            vect.Y = this.ReadSingle();

            return vect;
        }

        public Vector3 ReadVector3()
        {
            Vector3 vect = new Vector3();
            vect.X = this.ReadSingle();
            vect.Y = this.ReadSingle();
            vect.Z = this.ReadSingle();

            return vect;
        }

        public Vector4 ReadVector4()
        {
            Vector4 vect = new Vector4();
            vect.X = this.ReadSingle();
            vect.Y = this.ReadSingle();
            vect.Z = this.ReadSingle();
            vect.W = this.ReadSingle();

            return vect;
        }

        internal void Reset(int size)
        {
            MemoryStream ms = (MemoryStream)BaseStream;
            ms.SetLength(size);
            ms.Position = 0;
        }
        #endregion

        #region Properties
        public int Length
        {
            get
            {
                return (int)BaseStream.Length;
            }
        }

        public int Position
        {
            get
            {
                return (int)BaseStream.Position;
            }
            set
            {
                if (BaseStream.Position != value)
                    BaseStream.Position = value;
            }
        }
        #endregion
    }
}
