﻿#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Binary Content
// ---------------------------------------------------------------------------------
//	Date Created	: March 19, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
// 	Change History
//	Date Modified       : 
//	Changed By          : 
//	Change Description  : 
//
////////////////////////////////////////////////////////////////////////////////////
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TcmCoreService.ContentManagement;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="Binary" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.BinaryContentData" />
	/// </summary>
	public class BinaryContent : InfoBase
	{
		private BinaryContentData mBinaryContentData;

		private MultimediaType mMultimediaType = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="BinaryContent"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="binaryContentData"><see cref="T:Tridion.ContentManager.CoreService.Client.BinaryContentData" /></param>
		public BinaryContent(Client client, BinaryContentData binaryContentData): base(client)
		{
			mBinaryContentData = binaryContentData;
		}

		/// <summary>
		/// Gets the <see cref="T:Tridion.ContentManager.CoreService.Client.BinaryContentData" /> of this <see cref="BinaryContent" /> instance.
		/// </summary>
		/// <value>
		/// <see cref="T:Tridion.ContentManager.CoreService.Client.BinaryContentData" /> of this <see cref="BinaryContent" /> instance.
		/// </value>
		internal BinaryContentData BinaryContentData
		{
			get
			{
				return mBinaryContentData;
			}
		}

		/// <summary>
		/// Gets the <see cref="BinaryContent" /> binary identifier
		/// </summary>
		/// <value>
		/// <see cref="BinaryContent" /> binary identifier
		/// </value>
		public int BinaryId
		{
			get
			{
				return mBinaryContentData.BinaryId.GetValueOrDefault(0);				
			}
            set
            {
                mBinaryContentData.BinaryId = value;
            }
		}

        /// <summary>
        /// Gets or sets the <see cref="BinaryContent" /> external binary uri
        /// </summary>
        /// <value>
        /// <see cref="BinaryContent" /> external binary uri
        /// </value>
        public String ExternalBinaryUri
        {
            get
            {
                return mBinaryContentData.ExternalBinaryUri;
            }
            set
            {
                mBinaryContentData.ExternalBinaryUri = value;
            }
        }

		/// <summary>
		/// Gets the <see cref="BinaryContent" /> binary filename
		/// </summary>
		/// <value>
		/// <see cref="BinaryContent" /> binary filename
		/// </value>
		public String FileName
		{
			get
			{
				return mBinaryContentData.Filename;
			}
		}

		/// <summary>
		/// Gets the <see cref="BinaryContent" /> binary filesize
		/// </summary>
		/// <value>
		/// <see cref="BinaryContent" /> binary filesize
		/// </value>
		public int FileSize
		{
			get
			{
				return mBinaryContentData.FileSize.GetValueOrDefault(0);
			}
		}

		/// <summary>
		/// Gets the wether the <see cref="BinaryContent" /> binary is external
		/// </summary>
		/// <value>
		/// <c>true</c> if <see cref="BinaryContent" /> binary is external, otherwise <c>false</c>
		/// </value>
		public Boolean IsExternal
		{
			get
			{
				return mBinaryContentData.IsExternal.GetValueOrDefault(false);
			}
		}

		/// <summary>
		/// Gets the <see cref="BinaryContent" /> binary mimetype
		/// </summary>
		/// <value>
		/// <see cref="BinaryContent" /> binary mimetype
		/// </value>
		public String MimeType
		{
			get
			{
				return mBinaryContentData.MimeType;
			}
		}

		/// <summary>
		/// Gets the <see cref="BinaryContent" /> <see cref="T:TcmCoreService.ContentManagement.MultimediaType" />
		/// </summary>
		/// <value>
		/// <see cref="BinaryContent" /> <see cref="T:TcmCoreService.ContentManagement.MultimediaType" />
		/// </value>
		public MultimediaType MultimediaType
		{
			get
			{
				if (mMultimediaType == null)
					mMultimediaType = new MultimediaType(Client, mBinaryContentData.MultimediaType.IdRef);

				return mMultimediaType;
            }
		}

        /// <summary>
        /// Gets the <see cref="BinaryContent" /> <see cref="T:TcmCoreService.ContentManagement.MultimediaType" /> <see cref="T:TcmCoreService.Misc.TcmUri" />
        /// </summary>
        /// <value>
        /// <see cref="BinaryContent" /> <see cref="T:TcmCoreService.ContentManagement.MultimediaType" /> <see cref="T:TcmCoreService.Misc.TcmUri" />
        /// </value>
        public TcmUri MultimediaTypeUri
        {
            get
            {
                return mBinaryContentData.MultimediaType.IdRef;
            }
            set
            {
                mMultimediaType = null;

                if (value == null)
                    mBinaryContentData.MultimediaType.IdRef = TcmUri.NullUri;
                else
                    mBinaryContentData.MultimediaType.IdRef = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="BinaryContent" /> size
        /// </summary>
        /// <value>
        /// <see cref="BinaryContent" /> size
        /// </value>
        public long Size
        {
            get
            {   
                return mBinaryContentData.Size.GetValueOrDefault(0);
            }
        }

        /// <summary>
        /// Sets the <see cref="BinaryContent" /> upload from file reference
        /// </summary>
        /// <value>
        /// <see cref="BinaryContent" /> upload from file reference
        /// </value>
        public String UploadFromFile
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                    mBinaryContentData.UploadFromFile = value;
            }
        }
	}
}
