#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: IdentifiableObject
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
	/// <see cref="IdentifiableObject" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.IdentifiableObjectData" />.
	/// </summary>
	public abstract class IdentifiableObject
	{
		private Client mClient;
		private IdentifiableObjectData mIdentifiableObjectData;

		/// <summary>
		/// Retrieves the associated <see cref="T:TcmCoreService.Client" /> of this <see cref="IdentifiableObject" />
		/// </summary>
		/// <value>
		/// Associated <see cref="T:TcmCoreService.Client" /> of this <see cref="IdentifiableObject" />
		/// </value>
		protected Client Client
		{
			get
			{
				return mClient;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentifiableObject" /> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="identifiableObjectData"><see cref="T:Tridion.ContentManager.CoreService.Client.IdentifiableObjectData" /></param>	
		protected IdentifiableObject(Client client, IdentifiableObjectData identifiableObjectData)
		{
			if (client == null)
				throw new ArgumentNullException("client");
			
			if (identifiableObjectData == null)
				throw new ArgumentNullException("identifiableObjectData");

			mClient = client;
			mIdentifiableObjectData = identifiableObjectData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentifiableObject"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal IdentifiableObject(Client client, TcmUri uri): this(client, client.Read<IdentifiableObjectData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="IdentifiableObject" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.IdentifiableObjectData" />
		/// </summary>
		/// <param name="identifiableObjectData"><see cref="T:Tridion.ContentManager.CoreService.Client.IdentifiableObjectData" /></param>
		protected void Reload(IdentifiableObjectData identifiableObjectData)
		{
			if (identifiableObjectData == null)
				throw new ArgumentNullException("identifiableObjectData");

			mIdentifiableObjectData = identifiableObjectData;
		}

		/// <summary>
		/// Reload the <see cref="IdentifiableObject" /> data from the Content Manager
		/// </summary>
		public virtual void Reload()
		{
			Reload(Client.Read<RepositoryLocalObjectData>(this.Id));
		}

		/// <summary>
		/// Retrieve the <see cref="T:TcmCoreService.Misc.TcmUri" /> for the <see cref="IdentifiableObject" />.
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Misc.TcmUri" /> for the <see cref="IdentifiableObject" />
		/// </value>
		public TcmUri Id
		{
			get
			{
				return mIdentifiableObjectData.Id;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="IdentifiableObject" /> title.
		/// </summary>
		/// <value>
		/// <see cref="IdentifiableObject" /> title.
		/// </value>
		public String Title
		{
			get
			{
				return mIdentifiableObjectData.Title;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mIdentifiableObjectData.Title = value;
			}
		}

		/// <summary>
		/// Returns the <see cref="T:Tridion.ContentManager.CoreService.Client.AllowedActions" /> for this <see cref="IdentifiableObject" />
		/// </summary>
		/// <value>
		/// <see cref="T:Tridion.ContentManager.CoreService.Client.AllowedActions" />
		/// </value>
		public AllowedActions AllowedActions
		{
			get
			{
				return mIdentifiableObjectData.AllowedActions;
			}
			set
			{
				mIdentifiableObjectData.AllowedActions = value;
			}
		}

		/// <summary>
		/// Gets the <see cref="T:System.DateTime" /> this <see cref="IdentifiableObject" /> was created
		/// </summary>
		/// <value>
		/// <see cref="T:System.DateTime" /> this <see cref="IdentifiableObject" /> was created or <see cref="P:System.DateTime.MinValue" />
		/// </value>
		public DateTime CreationDate
		{
			get
			{
				return mIdentifiableObjectData.VersionInfo.CreationDate.GetValueOrDefault(DateTime.MinValue);
			}
		}

		/// <summary>
		/// Gets the <see cref="T:System.DateTime" /> this <see cref="IdentifiableObject" /> was revised
		/// </summary>
		/// <value>
		/// <see cref="T:System.DateTime" /> this <see cref="IdentifiableObject" /> was revised or <see cref="P:System.DateTime.MinValue" />
		/// </value>
		public DateTime RevisionDate
		{
			get
			{
				return mIdentifiableObjectData.VersionInfo.RevisionDate.GetValueOrDefault(DateTime.MinValue);
			}
		}

		/// <summary>
		/// Indicates wether this <see cref="IdentifiableObject" /> is editable.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="IdentifiableObject" /> is editable; otherwise, <c>false</c>.
		/// </value>
		public bool IsEditable
		{
			get
			{
				return mIdentifiableObjectData.IsEditable.GetValueOrDefault(false);
			}
		}

		/// <summary>
		/// Deletes this instaqnce of <see cref="IdentifiableObject" /> from Tridion
		/// </summary>
		public void Delete()
		{
			mClient.Delete(this.Id);
		}
		
		/// <summary>
		/// Saves this instance and CheckIn the <see cref="IdentifiableObject" />.
		/// </summary>
		public virtual void Save()
		{
			// Newly created item
			//if (Id.IsNull)
				//mIdentifiableObjectData = mSession.Create(mIdentifiableObjectData);
			//else
				// Update existing item
				mClient.Save(mIdentifiableObjectData);
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override String ToString()
		{
			return String.Format("{0}: \"{1}\" ({2})", this.GetType().Name, this.Title, this.Id);
		}
	}
}
