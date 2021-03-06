// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Protocols.TestTools.StackSdk;

namespace Microsoft.Protocols.TestTools.StackSdk.FileAccessService.Cifs
{
    /// <summary>
    /// Packets for SmbFind Response
    /// </summary>
    public class SmbFindResponsePacket : SmbSingleResponsePacket
    {
        #region Fields

        private SMB_COM_FIND_Response_SMB_Parameters smbParameters;
        private SMB_COM_FIND_Response_SMB_Data smbData;

        #endregion


        #region Properties

        /// <summary>
        /// get or set the Smb_Parameters:SMB_COM_FIND_Response_SMB_Parameters
        /// </summary>
        public SMB_COM_FIND_Response_SMB_Parameters SmbParameters
        {
            get
            {
                return this.smbParameters;
            }
            set
            {
                this.smbParameters = value;
            }
        }


        /// <summary>
        /// get or set the Smb_Data:SMB_COM_FIND_Response_SMB_Data
        /// </summary>
        public SMB_COM_FIND_Response_SMB_Data SmbData
        {
            get
            {
                return this.smbData;
            }
            set
            {
                this.smbData = value;
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SmbFindResponsePacket()
            : base()
        {
            this.InitDefaultValue();
        }


        /// <summary>
        /// Constructor: Create a request directly from a buffer.
        /// </summary>
        public SmbFindResponsePacket(byte[] data)
            : base(data)
        {
        }


        /// <summary>
        /// Deep copy constructor.
        /// </summary>
        public SmbFindResponsePacket(SmbFindResponsePacket packet)
            : base(packet)
        {
            this.InitDefaultValue();

            this.smbParameters.WordCount = packet.SmbParameters.WordCount;
            this.smbParameters.Count = packet.SmbParameters.Count;
            this.smbData.ByteCount = packet.SmbData.ByteCount;
            this.smbData.BufferFormat = packet.SmbData.BufferFormat;
            this.smbData.DataLength = packet.SmbData.DataLength;

            if (packet.smbData.DirectoryInformationData != null)
            {
                ushort dirInformationLength = 43;
                this.smbData.DirectoryInformationData = new SMB_Directory_Information[packet.smbData.DataLength /
                    dirInformationLength];
                for (int i = 0; i < packet.smbData.DirectoryInformationData.Length; i ++)
                {
                    this.smbData.DirectoryInformationData[i] = packet.smbData.DirectoryInformationData[i];
                    this.smbData.DirectoryInformationData[i].FileName =
                        new byte[packet.smbData.DirectoryInformationData[i].FileName.Length];
                    Array.Copy(
                        packet.smbData.DirectoryInformationData[i].FileName,
                        this.smbData.DirectoryInformationData[i].FileName,
                        packet.smbData.DirectoryInformationData[i].FileName.Length);
                }
            }
            else
            {
                this.smbData.DirectoryInformationData = new SMB_Directory_Information[0];
            }
        }

        #endregion


        #region override methods

        /// <summary>
        /// to create an instance of the StackPacket class that is identical to the current StackPacket. 
        /// </summary>
        /// <returns>a new Packet cloned from this.</returns>
        public override StackPacket Clone()
        {
            return new SmbFindResponsePacket(this);
        }


        /// <summary>
        /// Encode the struct of SMB_COM_FIND_Response_SMB_Parameters into the struct of SmbParameters
        /// </summary>
        protected override void EncodeParameters()
        {
            this.smbParametersBlock = TypeMarshal.ToStruct<SmbParameters>(
                CifsMessageUtils.ToBytes<SMB_COM_FIND_Response_SMB_Parameters>(this.smbParameters));
        }


        /// <summary>
        /// Encode the struct of SMB_COM_FIND_Response_SMB_Data into the struct of SmbData
        /// </summary>
        protected override void EncodeData()
        {
            this.smbDataBlock = TypeMarshal.ToStruct<SmbData>(
                CifsMessageUtils.ToBytes<SMB_COM_FIND_Response_SMB_Data>(this.SmbData));
        }


        /// <summary>
        /// to decode the smb parameters: from the general SmbParameters to the concrete Smb Parameters.
        /// </summary>
        protected override void DecodeParameters()
        {
            if (this.smbParametersBlock.WordCount > 0)
            {
                this.smbParameters = TypeMarshal.ToStruct<SMB_COM_FIND_Response_SMB_Parameters>(
                     TypeMarshal.ToBytes(this.smbParametersBlock));
            }
        }


        /// <summary>
        /// to decode the smb data: from the general SmbDada to the concrete Smb Data.
        /// </summary>
        protected override void DecodeData()
        {
            if (this.smbDataBlock.ByteCount > 0)
            {
                this.smbData = TypeMarshal.ToStruct<SMB_COM_FIND_Response_SMB_Data>(
                     TypeMarshal.ToBytes(this.smbDataBlock));
            }
        }

        #endregion


        #region initialize fields with default value

        /// <summary>
        /// init packet, set default field data
        /// </summary>
        private void InitDefaultValue()
        {
        }

        #endregion
    }
}
