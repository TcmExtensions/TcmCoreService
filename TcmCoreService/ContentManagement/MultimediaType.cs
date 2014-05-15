#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Multimedia Type
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
	/// <see cref="MultimediaType" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.MultimediaTypeData" />.
	/// </summary>
	public class MultimediaType : SystemWideObject
	{
		private MultimediaTypeData mMultimediaTypeData;

		/// <summary>
		/// Initializes a new instance of the <see cref="MultimediaType"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="multimediaTypeData"><see cref="T:Tridion.ContentManager.CoreService.Client.MultimediaTypeData" /></param>
		protected MultimediaType(Session session, MultimediaTypeData multimediaTypeData): base(session, multimediaTypeData)
		{
			if (multimediaTypeData == null)
				throw new ArgumentNullException("multimediaTypeData");

			mMultimediaTypeData = multimediaTypeData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MultimediaType"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal MultimediaType(Session session, TcmUri uri): this(session, session.Read<MultimediaTypeData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="MultimediaType" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.MultimediaTypeData" />
		/// </summary>
		/// <param name="multimediaTypeData"><see cref="T:Tridion.ContentManager.CoreService.Client.MultimediaTypeData" /></param>
		protected void Reload(MultimediaTypeData multimediaTypeData)
		{
			if (multimediaTypeData == null)
				throw new ArgumentNullException("multimediaTypeData");

			mMultimediaTypeData = multimediaTypeData;
			base.Reload(multimediaTypeData);
		}

		/// <summary>
		/// Reload the <see cref="MultimediaType" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<MultimediaTypeData>(this.Id));			
		}

		/// <summary>
		/// Gets or sets <see cref="T:Tridion.ContentManager.CoreService.Client.FileExtensonsList" /> for this <see cref="MultimediaType" />
		/// </summary>
		/// <value>
		/// <see cref="T:Tridion.ContentManager.CoreService.Client.FileExtensonsList" /> for this <see cref="MultimediaType" />
		/// </value>
		public FileExtensionsList FileExtensions
		{
			get
			{
				return mMultimediaTypeData.FileExtensions;
			}
			set
			{
				mMultimediaTypeData.FileExtensions = value;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="MultimediaType" /> mimetype
		/// </summary>
		/// <value>
		/// The <see cref="MultimediaType" /> mimetype
		/// </value>
		public String MimeType
		{
			get
			{
				return mMultimediaTypeData.MimeType;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mMultimediaTypeData.MimeType = value;
			}
		}
	}
}
