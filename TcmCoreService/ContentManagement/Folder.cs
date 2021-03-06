﻿#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Folder
// ---------------------------------------------------------------------------------
//	Date Created	: March 21, 2014
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
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.ContentManagement
{
	/// <summary>
	/// <see cref="Folder" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.FolderData" />.
	/// </summary>
	public class Folder : OrganizationalItem
	{
		private FolderData mFolderData;
		private Schema mLinkedSchema = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Folder"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="folderData"><see cref="T:Tridion.ContentManager.CoreService.Client.FolderData" /></param>
		protected Folder(Client client, FolderData folderData): base(client, folderData)
		{
			if (folderData == null)
				throw new ArgumentNullException("folderData");

			mFolderData = folderData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Folder"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Folder(Client client, TcmUri uri): this(client, client.Read<FolderData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Folder" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.FolderData" />
		/// </summary>
		/// <param name="folderData"><see cref="T:Tridion.ContentManager.CoreService.Client.FolderData" /></param>
		protected void Reload(FolderData folderData)
		{
			if (folderData == null)
				throw new ArgumentNullException("folderData");

			mFolderData = folderData;
			base.Reload(folderData);

			mLinkedSchema = null;
		}

		/// <summary>
		/// Reload the <see cref="Folder" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<FolderData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="Folder" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<FolderData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="Folder" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<FolderData>(this.Id));
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="Folder" /> linked <see cref="T:TcmCoreService.ContentManager.Schema" /> is mandatory.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="Folder" /> linked <see cref="T:TcmCoreService.ContentManager.Schema" /> is mandatory; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsLinkedSchemaMandatory
		{
			get
			{	
				return mFolderData.IsLinkedSchemaMandatory.GetValueOrDefault(false);
			}
			set
			{
				mFolderData.IsLinkedSchemaMandatory = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="Folder" /> linked <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </summary>
		/// <value>
		/// <see cref="Folder" /> linked <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </value>
		public Schema LinkedSchema
		{
			get
			{
				if (mLinkedSchema == null && mFolderData.LinkedSchema.IdRef != TcmUri.NullUri)
					mLinkedSchema = new Schema(Client, mFolderData.LinkedSchema.IdRef);

				return mLinkedSchema;
			}
			set
			{
				mLinkedSchema = value;

				if (value != null)
					mFolderData.LinkedSchema.IdRef = value.Id;
				else
					mFolderData.LinkedSchema.IdRef = TcmUri.NullUri;
			}
		}

        /// <summary>
        /// Gets or sets the <see cref="Folder" /> linked <see cref="T:TcmCoreService.ContentManagement.Schema" /> <see cref="T:TcmCoreService.Misc.TcmUri" />
        /// </summary>
        /// <value>
        /// <see cref="Folder" /> linked <see cref="T:TcmCoreService.ContentManagement.Schema" /> <see cref="T:TcmCoreService.Misc.TcmUri" />
        /// </value>
        public TcmUri LinkedSchemaUri
        {
            get
            {
                return mFolderData.LinkedSchema.IdRef;
            }
            set
            {
                mLinkedSchema = null;

                if (value == null)
                    mFolderData.LinkedSchema.IdRef = TcmUri.NullUri;
                else
                    mFolderData.LinkedSchema.IdRef = value;
            }
        }
	}
}
