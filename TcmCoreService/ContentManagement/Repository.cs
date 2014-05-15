#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Repository
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
	/// <see cref="Repository" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryData" />.
	/// </summary>
	public class Repository : SystemWideObject
	{
		private RepositoryData mRepositoryData;

		/// <summary>
		/// Initializes a new instance of the <see cref="Repository"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="schemaData"><see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryData" /></param>
		protected Repository(Session session, RepositoryData repositoryData): base(session, repositoryData)
		{
			if (repositoryData == null)
				throw new ArgumentNullException("repositoryData");

			mRepositoryData = repositoryData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Repository"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal Repository(Session session, TcmUri uri): this(session, session.Read<RepositoryData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="Repository" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryData" />
		/// </summary>
		/// <param name="repositoryData"><see cref="T:Tridion.ContentManager.CoreService.Client.RepositoryData" /></param>
		protected void Reload(RepositoryData repositoryData)
		{
			if (repositoryData == null)
				throw new ArgumentNullException("repositoryData");

			mRepositoryData = repositoryData;
			base.Reload(repositoryData);
		}

		/// <summary>
		/// Reload the <see cref="Repository" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<RepositoryData>(this.Id));
		}

		/// <summary>
		/// Gets or sets the <see cref="Repository" /> metadata Xml
		/// </summary>
		/// <value>
		/// <see cref="Repository" /> metadata Xml
		/// </value>
		public String Metadata
		{
			get
			{
				return mRepositoryData.Metadata;
			}
			set
			{
				mRepositoryData.Metadata = value;
			}
		}

		/// <summary>
		/// Gets or sets the metadata <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </summary>
		/// <value>
		/// Metadata <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </value>
		public Schema MetadataSchema
		{
			get
			{
				if (mRepositoryData.MetadataSchema.IdRef != TcmUri.NullUri)
					return new Schema(Session, mRepositoryData.MetadataSchema.IdRef);
				else
					return null;
			}
			set
			{
				if (value != null)
					mRepositoryData.MetadataSchema.IdRef = value.Id;
				else
					mRepositoryData.MetadataSchema.IdRef = TcmUri.NullUri;
			}
		}
	}
}
