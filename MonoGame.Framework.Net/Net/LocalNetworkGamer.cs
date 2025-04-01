// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

#region Using clause
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.GamerServices;

#endregion Using clause

namespace Microsoft.Xna.Framework.Net
{
    public sealed class LocalNetworkGamer : NetworkGamer
    {

        private SignedInGamer sig;
        internal Queue<CommandReceiveData> receivedData;

        public LocalNetworkGamer() : base(null, 0, 0)
        {
            sig = new SignedInGamer();
            receivedData = new Queue<CommandReceiveData>();
        }

        public LocalNetworkGamer(NetworkSession session, byte id, GamerStates state)
            : base(session, id, state | GamerStates.Local)
        {
            sig = new SignedInGamer();
            receivedData = new Queue<CommandReceiveData>();
        }

        public void EnableSendVoice(
            NetworkGamer remoteGamer,
            bool enable)
        {
            throw new NotImplementedException();
        }

        public int ReceiveData(
            byte[] data,
            int offset,
            out NetworkGamer sender)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (receivedData.Count <= 0)
            {
                sender = null;
                return 0;
            }

            lock (receivedData)
            {

                CommandReceiveData crd;

                // we will peek at the value first to see if we can process it
                crd = (CommandReceiveData)receivedData.Peek();

                if (offset + crd.data.Length > data.Length)
                    throw new ArgumentOutOfRangeException("data", "The length + offset is greater than parameter can hold.");

                // no exception thrown yet so let's process it
                // take it off the queue
                receivedData.Dequeue();

                Array.Copy(crd.data, offset, data, 0, data.Length);
                sender = crd.gamer;
                return data.Length;
            }

        }

        public int ReceiveData(
            byte[] data,
            out NetworkGamer sender)
        {
            return ReceiveData(data, 0, out sender);
        }

        public int ReceiveData(
            PacketReader data,
            out NetworkGamer sender)
        {
            lock (receivedData)
            {
                if (receivedData.Count >= 0)
                {
                    data.Reset(0);

                    // take it off the queue
                    CommandReceiveData crd = (CommandReceiveData)receivedData.Dequeue();

                    // lets make sure that we can handle the data
                    if (data.Length < crd.data.Length)
                    {
                        data.Reset(crd.data.Length);
                    }

                    Array.Copy(crd.data, data.Data, data.Length);
                    sender = crd.gamer;
                    return data.Length;

                }
                else
                {
                    sender = null;
                    return 0;
                }

            }
        }

        public void SendData(
            byte[] data,
            int offset,
            int count,
            SendDataOptions options)
        {
            CommandEvent cme = new CommandEvent(new CommandSendData(data, offset, count, options, null, this));
            Session.commandQueue.Enqueue(cme);
        }

        public void SendData(
            byte[] data,
            int offset,
            int count,
            SendDataOptions options,
            NetworkGamer recipient)
        {
            CommandEvent cme = new CommandEvent(new CommandSendData(data, offset, count, options, recipient, this));
            Session.commandQueue.Enqueue(cme);
        }

        public void SendData(
            byte[] data,
            SendDataOptions options)
        {
            CommandEvent cme = new CommandEvent(new CommandSendData(data, 0, data.Length, options, null, this));
            Session.commandQueue.Enqueue(cme);
        }

        public void SendData(
            byte[] data,
            SendDataOptions options,
            NetworkGamer recipient)
        {
            CommandEvent cme = new CommandEvent(new CommandSendData(data, 0, data.Length, options, recipient, this));
            Session.commandQueue.Enqueue(cme);
        }

        public void SendData(
            PacketWriter data,
            SendDataOptions options)
        {
            SendData(data.Data, 0, data.Length, options, null);
            data.Reset();
        }

        public void SendData(
            PacketWriter data,
            SendDataOptions options,
            NetworkGamer recipient)
        {
            SendData(data.Data, 0, data.Length, options, recipient);
            data.Reset();
        }

        public bool IsDataAvailable
        {
            get
            {
                lock (receivedData)
                {
                    return receivedData.Count > 0;
                }
            }
        }

        public SignedInGamer SignedInGamer
        {
            get
            {
                return sig;
            }

            internal set
            {
                sig = value;
                DisplayName = sig.DisplayName;
                Gamertag = sig.Gamertag;
            }
        }
    }
}
