#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Virtual Folder
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
	/// <see cref="VirtualFolder" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.VirtualFolderData" />.
	/// </summary>
	public class VirtualFolder : OrganizationalItem
	{
		private VirtualFolderData mVirtualFolderData;

		private Schema mTypeSchema = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="VirtualFolder"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="folderData"><see cref="T:Tridion.ContentManager.CoreService.Client.VirtualFolderData" /></param>
		protected VirtualFolder(Client client, VirtualFolderData virtualFolderData): base(client, virtualFolderData)
		{
			if (virtualFolderData == null)
				throw new ArgumentNullException("virtualFolderData");

			mVirtualFolderData = virtualFolderData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="VirtualFolder"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal VirtualFolder(Client client, TcmUri uri): this(client, client.Read<VirtualFolderData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="VirtualFolder" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.VirtualFolderData" />
		/// </summary>
		/// <param name="folderData"><see cref="T:Tridion.ContentManager.CoreService.Client.VirtualFolderData" /></param>
		protected void Reload(VirtualFolderData virtualFolderData)
		{
			if (virtualFolderData == null)
				throw new ArgumentNullException("virtualFolderData");

			mVirtualFolderData = virtualFolderData;
			base.Reload(virtualFolderData);

			mTypeSchema = null;
		}

		/// <summary>
		/// Reload the <see cref="VirtualFolder" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<VirtualFolderData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="VirtualFolder" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<VirtualFolderData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="VirtualFolder" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<VirtualFolderData>(this.Id));			
		}

		/// <summary>
		/// Gets or sets the <see cref="VirtualFolder" /> configuration.
		/// </summary>
		/// <value>
		/// The <see cref="VirtualFolder" /> configuration.
		/// </value>
		public String Configuration
		{
			get
			{
				return mVirtualFolderData.Configuration;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mVirtualFolderData.Configuration = value;

			}
		}

		/// <summary>
		/// Gets or sets the <see cref="VirtualFolder" /> type <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </summary>
		/// <value>
		/// <see cref="VirtualFolder" /> type <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </value>
		public Schema TypeSchema
		{
			get
			{
				if (mTypeSchema == null && mVirtualFolderData.TypeSchema.IdRef != TcmUri.NullUri)
					mTypeSchema = new Schema(Client, mVirtualFolderData.TypeSchema.IdRef);

				return mTypeSchema;
			}
			set
			{
				mTypeSchema = value;

				if (value != null)
					mVirtualFolderData.TypeSchema.IdRef = value.Id;
				else
					mVirtualFolderData.TypeSchema.IdRef = TcmUri.NullUri;
			}
		}
	}
}
